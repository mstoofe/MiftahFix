using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiftahFix.Services;

namespace MiftahFix
{
    /// <summary>
    /// الكتابة الذكية: تصحيح آخر ما كُتب بدون تحديد، تبديل لغة لوحة المفاتيح،
    /// واكتشاف الكتابة بلغة خاطئة أثناء الكتابة.
    /// </summary>
    public partial class Form1
    {
        private const int NotifyScoreThreshold = 2;
        private const int AutoCorrectScoreThreshold = 3;

        private static readonly TimeSpan NotificationInterval =
            TimeSpan.FromSeconds(20);

        private TypingTracker? _typingTracker;

        private readonly WrongLayoutDetector _wrongLayoutDetector =
            new();

        private DateTime _lastWrongLayoutNotification =
            DateTime.MinValue;

        private bool _isTypedConversionRunning;

        private void InitializeSmartTyping()
        {
            _typingTracker =
                new TypingTracker(
                    _converter);

            _typingTracker.WordCompleted +=
                TypingTracker_WordCompleted;

            FormClosed +=
                Form1_SmartTypingFormClosed;

            UpdateTrackerHotkey();
            ApplySmartTypingSettings();
        }

        /// <summary>
        /// تشغيل المراقبة فقط إذا كانت إحدى الميزتين مفعّلة.
        /// </summary>
        private void ApplySmartTypingSettings()
        {
            if (_typingTracker == null)
            {
                return;
            }

            bool needed =
                !_isPaused &&
                (_settings.Conversion.ConvertTypedText ||
                 !IsTypingDetectionMode("Off"));

            if (needed)
            {
                if (!_typingTracker.Start())
                {
                    ShowStatus(
                        "تعذر تشغيل متابعة الكتابة، سيعمل التصحيح على النص المحدد فقط.",
                        StatusType.Error);
                }
            }
            else
            {
                _typingTracker.Stop();
            }

            _wrongLayoutDetector.Reset();
        }

        private void UpdateTrackerHotkey()
        {
            if (_typingTracker == null)
            {
                return;
            }

            TryGetConfiguredHotkey(
                out Keys key);

            _typingTracker.SetIgnoredHotkey(
                key,
                _settings.Hotkey.Control,
                _settings.Hotkey.Shift,
                _settings.Hotkey.Alt,
                _settings.Hotkey.Windows);
        }

        private bool IsTypingDetectionMode(string mode)
        {
            return string.Equals(
                _settings.Conversion.TypingDetection,
                mode,
                StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// هل يوجد نص مكتوب للتو يمكن تصحيحه بدل النص المحدد؟
        /// </summary>
        private bool HasTypedTextToConvert()
        {
            return _settings.Conversion.ConvertTypedText &&
                   _typingTracker != null &&
                   _typingTracker.IsRunning &&
                   _typingTracker.GetTrailingSegment() != null;
        }

        /// <summary>
        /// يحذف آخر ما كُتب بلغة خاطئة ويكتبه باللغة الصحيحة.
        /// </summary>
        private async Task<bool> ConvertTypedTextAsync(
            string source,
            bool triggeredByHotkey)
        {
            if (_typingTracker == null ||
                _isTypedConversionRunning)
            {
                return false;
            }

            _isTypedConversionRunning = true;

            try
            {
                if (triggeredByHotkey)
                {
                    TryGetConfiguredHotkey(
                        out Keys key);

                    if (!await NativeInput.WaitForKeysReleasedAsync(key))
                    {
                        ShowStatus(
                            "ارفع يدك عن مفاتيح الاختصار ثم حاول مرة أخرى.",
                            StatusType.Error);

                        return false;
                    }
                }

                TypedSegment? segment =
                    _typingTracker.GetTrailingSegment();

                if (segment == null)
                {
                    return false;
                }

                IntPtr foregroundWindow =
                    NativeInput.GetForegroundWindowHandle();

                if (foregroundWindow != segment.Window)
                {
                    _typingTracker.Reset();
                    return false;
                }

                NativeInput.ReplaceTypedText(
                    segment.DisplayedText.Length,
                    segment.ConvertedText);

                _typingTracker.FlipTrailingSegment(
                    segment);

                _wrongLayoutDetector.Reset();

                bool switchToArabic =
                    !segment.TypedInArabic;

                if (_settings.Conversion.SwitchKeyboardLayout)
                {
                    KeyboardLayoutSwitcher.SwitchTo(
                        switchToArabic,
                        foregroundWindow);
                }

                RecordHistory(
                    segment.DisplayedText.Trim(),
                    segment.ConvertedText.Trim(),
                    segment.TypedInArabic
                        ? "ArabicToEnglish"
                        : "EnglishToArabic",
                    source);

                ShowStatus(
                    $"تم تصحيح ما كتبته إلى: {Shorten(segment.ConvertedText.Trim())}",
                    StatusType.Success);

                return true;
            }
            catch (Exception exception)
            {
                _typingTracker.Reset();

                ShowStatus(
                    $"تعذر تصحيح ما كتبته: {exception.Message}",
                    StatusType.Error);

                return false;
            }
            finally
            {
                _isTypedConversionRunning = false;
            }
        }

        /// <summary>
        /// بعد تصحيح نص محدد: نبدّل لغة لوحة المفاتيح إلى لغة النص الناتج.
        /// </summary>
        private void SwitchLayoutAfterSelectionConversion(
            string convertedText,
            IntPtr targetWindow)
        {
            if (!_settings.Conversion.SwitchKeyboardLayout ||
                targetWindow == IntPtr.Zero)
            {
                return;
            }

            bool resultIsArabic =
                _converter.DetectDirection(convertedText) ==
                ConversionDirection.ArabicToEnglish;

            KeyboardLayoutSwitcher.SwitchTo(
                resultIsArabic,
                targetWindow);

            _typingTracker?.Reset();
        }

        private void TypingTracker_WordCompleted(
            object? sender,
            WordCompletedEventArgs e)
        {
            if (IsDisposed ||
                _isApplicationExiting ||
                _isTypedConversionRunning ||
                IsTypingDetectionMode("Off"))
            {
                return;
            }

            // إذا كانت لغة واحدة فقط مثبتة فلا معنى للتنبيه.
            if (!KeyboardLayoutSwitcher.IsArabicLayoutInstalled())
            {
                return;
            }

            int score =
                _wrongLayoutDetector.AddWord(
                    e.DisplayedWord,
                    e.PhysicalWord,
                    e.TypedInArabic);

            if (IsTypingDetectionMode("AutoCorrect"))
            {
                if (score >= AutoCorrectScoreThreshold)
                {
                    _ = ConvertTypedTextAsync(
                        "AutoCorrect",
                        triggeredByHotkey: false);
                }

                return;
            }

            if (score < NotifyScoreThreshold)
            {
                return;
            }

            _wrongLayoutDetector.Reset();

            if (DateTime.Now - _lastWrongLayoutNotification <
                NotificationInterval)
            {
                return;
            }

            _lastWrongLayoutNotification =
                DateTime.Now;

            string languageText =
                e.TypedInArabic
                    ? "بالعربية بدل الإنجليزية"
                    : "بالإنجليزية بدل العربية";

            string message =
                $"يبدو أنك تكتب {languageText}. اضغط {GetHotkeyDisplayText()} لتصحيح ما كتبته.";

            ShowTrayNotification(
                message);

            ShowStatus(
                message,
                StatusType.Information);
        }

        private void Form1_SmartTypingFormClosed(
            object? sender,
            FormClosedEventArgs e)
        {
            FormClosed -=
                Form1_SmartTypingFormClosed;

            if (_typingTracker != null)
            {
                _typingTracker.WordCompleted -=
                    TypingTracker_WordCompleted;

                _typingTracker.Dispose();
                _typingTracker = null;
            }
        }

        private static string Shorten(string text)
        {
            const int maximumLength = 40;

            return text.Length <= maximumLength
                ? text
                : text.Substring(0, maximumLength) + "…";
        }
    }
}
