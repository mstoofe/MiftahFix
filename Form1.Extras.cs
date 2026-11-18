using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiftahFix.Services;

namespace MiftahFix
{
    /// <summary>
    /// التحقق من التحديثات، الإيقاف المؤقت، والتنبيه عند العمل مع برامج المسؤول.
    /// </summary>
    public partial class Form1
    {
        private readonly UpdateService _updateService =
            new();

        private bool _isPaused;

        /// <summary>رابط يُفتح عند النقر على آخر إشعار (مثلاً صفحة التحديث).</summary>
        private string? _balloonClickUrl;

        private bool _hasCheckedForUpdates;

        private void ConnectTrayExtras()
        {
            if (_trayIconService == null)
            {
                return;
            }

            _trayIconService.PauseToggleRequested +=
                TrayIconService_PauseToggleRequested;

            _trayIconService.BalloonClicked +=
                TrayIconService_BalloonClicked;

            _trayIconService.SetPaused(_isPaused);
        }

        private void TrayIconService_PauseToggleRequested(
            object? sender,
            EventArgs e)
        {
            SetPaused(!_isPaused);
        }

        /// <summary>
        /// الإيقاف المؤقت مفيد أثناء الألعاب أو كتابة كلمات المرور.
        /// </summary>
        private void SetPaused(bool paused)
        {
            _isPaused = paused;

            if (paused)
            {
                _typingTracker?.Stop();
                _wrongLayoutDetector.Reset();
            }
            else
            {
                ApplySmartTypingSettings();
            }

            _trayIconService?.SetPaused(paused);

            ShowStatus(
                paused
                    ? "تم إيقاف التصحيح مؤقتاً. استأنفه من أيقونة البرنامج بجانب الساعة."
                    : "تم استئناف التصحيح.",
                StatusType.Information);
        }

        private void TrayIconService_BalloonClicked(
            object? sender,
            EventArgs e)
        {
            string? url = _balloonClickUrl;
            _balloonClickUrl = null;

            if (!string.IsNullOrEmpty(url))
            {
                OpenUrl(url);
            }
            else
            {
                RestoreAndActivateWindow();
            }
        }

        private void ShowTrayNotification(
            string message,
            string? clickUrl = null,
            ToolTipIcon icon = ToolTipIcon.Info,
            int timeoutMilliseconds = 3000)
        {
            _balloonClickUrl = clickUrl;

            _trayIconService?.ShowBalloon(
                "MiftahFix",
                message,
                icon,
                timeoutMilliseconds);
        }

        /// <summary>
        /// يتحقق من التحديثات بعد بدء التشغيل بقليل، حتى لا يبطئ فتح البرنامج.
        /// </summary>
        private async void CheckForUpdatesOnStartup()
        {
            if (_hasCheckedForUpdates ||
                !_settings.Advanced.CheckForUpdates)
            {
                return;
            }

            _hasCheckedForUpdates = true;

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(8));

                if (IsDisposed || _isApplicationExiting)
                {
                    return;
                }

                UpdateCheckResult result =
                    await _updateService.CheckAsync();

                if (IsDisposed || _isApplicationExiting)
                {
                    return;
                }

                if (result.IsUpdateAvailable &&
                    result.LatestVersion != null)
                {
                    string message =
                        $"يتوفر إصدار جديد {result.LatestVersion}. انقر هنا لتحميله.";

                    ShowStatus(
                        $"يتوفر إصدار جديد {result.LatestVersion} (الحالي {result.CurrentVersion}).",
                        StatusType.Information);

                    ShowTrayNotification(
                        message,
                        result.DownloadPage,
                        ToolTipIcon.Info,
                        6000);
                }
            }
            catch
            {
                // التحقق من التحديثات لا يجب أن يزعج المستخدم إذا فشل.
            }
        }

        /// <summary>
        /// تحقق يدوي (من نافذة حول مثلاً) مع رسالة بالنتيجة.
        /// </summary>
        internal async Task CheckForUpdatesManuallyAsync(IWin32Window owner)
        {
            UpdateCheckResult result =
                await _updateService.CheckAsync();

            if (result.IsUpdateAvailable &&
                result.LatestVersion != null)
            {
                DialogResult answer =
                    MessageBox.Show(
                        owner,
                        $"يتوفر إصدار جديد {result.LatestVersion}.{Environment.NewLine}إصدارك الحالي {result.CurrentVersion}.{Environment.NewLine}{Environment.NewLine}هل تريد فتح صفحة التحميل؟",
                        "تحديث MiftahFix",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                if (answer == DialogResult.Yes)
                {
                    OpenUrl(result.DownloadPage);
                }

                return;
            }

            MessageBox.Show(
                owner,
                result.HasError
                    ? $"تعذر التحقق من التحديثات.{Environment.NewLine}{result.ErrorMessage}"
                    : $"لديك أحدث إصدار ({result.CurrentVersion}).",
                "تحديث MiftahFix",
                MessageBoxButtons.OK,
                result.HasError
                    ? MessageBoxIcon.Warning
                    : MessageBoxIcon.Information);
        }

        private static void OpenUrl(string url)
        {
            try
            {
                Process.Start(
                    new ProcessStartInfo(url)
                    {
                        UseShellExecute = true
                    });
            }
            catch
            {
            }
        }

        /// <summary>
        /// Windows يمنع البرامج العادية من الكتابة داخل البرامج المفتوحة كمسؤول.
        /// </summary>
        private bool IsBlockedByElevation(IntPtr targetWindow)
        {
            if (targetWindow == IntPtr.Zero ||
                NativeInput.IsCurrentProcessElevated())
            {
                return false;
            }

            uint processId =
                NativeInput.GetWindowProcessId(targetWindow);

            return NativeInput.IsProcessElevated(processId);
        }

        private const string ElevationMessage =
            "هذا البرنامج مفتوح بصلاحيات المسؤول، وWindows لا يسمح بتصحيح النص فيه. شغّل MiftahFix كمسؤول أو افتح البرنامج الآخر بشكل عادي.";

        /// <summary>
        /// يُستدعى عندما يحاول المستخدم فتح البرنامج مرة ثانية.
        /// </summary>
        internal void ShowFromAnotherInstance()
        {
            if (IsDisposed || _isApplicationExiting)
            {
                return;
            }

            RestoreAndActivateWindow();
        }

        /// <summary>
        /// شاشة الترحيب تظهر مرة واحدة فقط بعد التثبيت أو التحديث.
        /// </summary>
        private void ShowWelcomeIfFirstRun()
        {
            if (!Forms.WelcomeForm.ShouldShow())
            {
                return;
            }

            Forms.WelcomeForm.MarkAsShown();

            BeginInvoke(
                new Action(() =>
                {
                    if (IsDisposed || _isApplicationExiting)
                    {
                        return;
                    }

                    using Forms.WelcomeForm welcomeForm =
                        new Forms.WelcomeForm(
                            GetHotkeyDisplayText());

                    welcomeForm.ShowDialog(
                        Visible ? this : null);
                }));
        }

        private void DisconnectTrayExtras()
        {
            if (_trayIconService == null)
            {
                return;
            }

            _trayIconService.PauseToggleRequested -=
                TrayIconService_PauseToggleRequested;

            _trayIconService.BalloonClicked -=
                TrayIconService_BalloonClicked;
        }
    }
}
