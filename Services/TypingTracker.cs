using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MiftahFix.Services
{
    /// <summary>
    /// حرف واحد كتبه المستخدم: المفتاح الفعلي (حسب لوحة الإنجليزية US)
    /// ولغة لوحة المفاتيح التي كانت مفعّلة وقت الكتابة.
    /// </summary>
    public readonly struct TypedKey
    {
        public TypedKey(char physicalCharacter, char latinDisplayCharacter, bool typedInArabic)
        {
            PhysicalCharacter = physicalCharacter;
            LatinDisplayCharacter = latinDisplayCharacter;
            TypedInArabic = typedInArabic;
        }

        /// <summary>الحرف الإنجليزي للمفتاح مع Shift (بدون Caps Lock).</summary>
        public char PhysicalCharacter { get; }

        /// <summary>الحرف كما يظهر على لوحة الإنجليزية (مع Caps Lock).</summary>
        public char LatinDisplayCharacter { get; }

        public bool TypedInArabic { get; }

        public TypedKey WithLayout(bool typedInArabic)
        {
            return new TypedKey(
                PhysicalCharacter,
                PhysicalCharacter,
                typedInArabic);
        }
    }

    /// <summary>
    /// مقطع من آخر ما كُتب بنفس اللغة، جاهز للتحويل.
    /// </summary>
    public sealed class TypedSegment
    {
        public TypedSegment(
            int keyCount,
            string displayedText,
            string convertedText,
            bool typedInArabic,
            IntPtr window)
        {
            KeyCount = keyCount;
            DisplayedText = displayedText;
            ConvertedText = convertedText;
            TypedInArabic = typedInArabic;
            Window = window;
        }

        public int KeyCount { get; }

        /// <summary>النص كما ظهر على الشاشة.</summary>
        public string DisplayedText { get; }

        /// <summary>النص الذي كان المستخدم يقصده.</summary>
        public string ConvertedText { get; }

        public bool TypedInArabic { get; }

        public IntPtr Window { get; }
    }

    public sealed class WordCompletedEventArgs : EventArgs
    {
        public WordCompletedEventArgs(
            string displayedWord,
            string physicalWord,
            bool typedInArabic)
        {
            DisplayedWord = displayedWord;
            PhysicalWord = physicalWord;
            TypedInArabic = typedInArabic;
        }

        public string DisplayedWord { get; }

        public string PhysicalWord { get; }

        public bool TypedInArabic { get; }
    }

    /// <summary>
    /// يراقب ما يكتبه المستخدم في البرامج الأخرى (في الذاكرة فقط، بدون حفظ أو إرسال)
    /// حتى يمكن تصحيح آخر ما كُتب بدون تحديده، واكتشاف الكتابة بلغة خاطئة.
    /// يُمسح المخزن عند تغيير النافذة أو النقر بالماوس أو الضغط على Enter والأسهم.
    /// </summary>
    public sealed class TypingTracker : IDisposable
    {
        private const int MaximumKeys = 400;

        private const int HookKeyboardLowLevel = 13;
        private const int HookMouseLowLevel = 14;

        private const int WmKeyDown = 0x0100;
        private const int WmSystemKeyDown = 0x0104;
        private const int WmLeftButtonDown = 0x0201;
        private const int WmRightButtonDown = 0x0204;
        private const int WmMiddleButtonDown = 0x0207;

        private const uint LowLevelInjectedFlag = 0x10;

        private readonly KeyboardLayoutConverter _converter;
        private readonly List<TypedKey> _keys = new();
        private readonly HookProcedure _keyboardProcedure;
        private readonly HookProcedure _mouseProcedure;
        private readonly uint _currentProcessId;

        private IntPtr _keyboardHook;
        private IntPtr _mouseHook;
        private IntPtr _window;
        private bool _isDisposed;
        private SynchronizationContext? _synchronizationContext;

        private Keys _ignoredHotkey = Keys.None;
        private bool _ignoredControl;
        private bool _ignoredShift;
        private bool _ignoredAlt;
        private bool _ignoredWindows;

        public TypingTracker(KeyboardLayoutConverter converter)
        {
            _converter =
                converter ??
                throw new ArgumentNullException(nameof(converter));

            _keyboardProcedure = KeyboardHookCallback;
            _mouseProcedure = MouseHookCallback;

            using Process process = Process.GetCurrentProcess();
            _currentProcessId = (uint)process.Id;
        }

        /// <summary>يُطلق بعد اكتمال كلمة (عند الضغط على المسافة).</summary>
        public event EventHandler<WordCompletedEventArgs>? WordCompleted;

        public bool IsRunning =>
            _keyboardHook != IntPtr.Zero;

        public IntPtr Window =>
            _window;

        public bool Start()
        {
            ThrowIfDisposed();

            if (IsRunning)
            {
                return true;
            }

            _synchronizationContext = SynchronizationContext.Current;

            IntPtr module = GetModuleHandle(null);

            _keyboardHook =
                SetWindowsHookEx(
                    HookKeyboardLowLevel,
                    _keyboardProcedure,
                    module,
                    0);

            _mouseHook =
                SetWindowsHookEx(
                    HookMouseLowLevel,
                    _mouseProcedure,
                    module,
                    0);

            return _keyboardHook != IntPtr.Zero;
        }

        public void Stop()
        {
            if (_keyboardHook != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_keyboardHook);
                _keyboardHook = IntPtr.Zero;
            }

            if (_mouseHook != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_mouseHook);
                _mouseHook = IntPtr.Zero;
            }

            Reset();
        }

        /// <summary>
        /// اختصار البرنامج نفسه لا يجب أن يمسح المخزن عند ضغطه.
        /// </summary>
        public void SetIgnoredHotkey(
            Keys key,
            bool control,
            bool shift,
            bool alt,
            bool windows)
        {
            _ignoredHotkey = key & Keys.KeyCode;
            _ignoredControl = control;
            _ignoredShift = shift;
            _ignoredAlt = alt;
            _ignoredWindows = windows;
        }

        public void Reset()
        {
            _keys.Clear();
        }

        /// <summary>
        /// آخر مقطع مكتوب بنفس اللغة (مثلاً كل الكلمات التي كُتبت بالإنجليزية بالخطأ).
        /// يعيد null إذا لم يوجد نص يمكن تحويله.
        /// </summary>
        public TypedSegment? GetTrailingSegment()
        {
            if (_keys.Count == 0)
            {
                return null;
            }

            bool typedInArabic = _keys[^1].TypedInArabic;
            int start = _keys.Count;

            while (start > 0 &&
                   _keys[start - 1].TypedInArabic == typedInArabic)
            {
                start--;
            }

            StringBuilder displayed = new();
            StringBuilder converted = new();
            bool hasLetters = false;

            for (int index = start; index < _keys.Count; index++)
            {
                TypedKey key = _keys[index];

                string arabicText =
                    _converter.MapPhysicalKeyToArabic(key.PhysicalCharacter);

                if (typedInArabic)
                {
                    displayed.Append(arabicText);
                    converted.Append(key.PhysicalCharacter);
                }
                else
                {
                    displayed.Append(key.LatinDisplayCharacter);
                    converted.Append(arabicText);
                }

                if (!char.IsWhiteSpace(key.PhysicalCharacter))
                {
                    hasLetters = true;
                }
            }

            if (!hasLetters)
            {
                return null;
            }

            string displayedText = displayed.ToString();
            string convertedText = converted.ToString();

            if (string.Equals(displayedText, convertedText, StringComparison.Ordinal))
            {
                return null;
            }

            return new TypedSegment(
                _keys.Count - start,
                displayedText,
                convertedText,
                typedInArabic,
                _window);
        }

        /// <summary>
        /// بعد التحويل نغيّر لغة المقطع في المخزن، فإذا ضغط المستخدم الاختصار مرة أخرى يعود النص كما كان.
        /// </summary>
        public void FlipTrailingSegment(TypedSegment segment)
        {
            int start = _keys.Count - segment.KeyCount;

            if (start < 0)
            {
                Reset();
                return;
            }

            for (int index = start; index < _keys.Count; index++)
            {
                _keys[index] = _keys[index].WithLayout(!segment.TypedInArabic);
            }
        }

        private IntPtr KeyboardHookCallback(
            int code,
            IntPtr wParam,
            IntPtr lParam)
        {
            if (code >= 0)
            {
                try
                {
                    int message = wParam.ToInt32();

                    if (message == WmKeyDown ||
                        message == WmSystemKeyDown)
                    {
                        KBDLLHOOKSTRUCT data =
                            Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam);

                        if ((data.Flags & LowLevelInjectedFlag) == 0)
                        {
                            ProcessKeyDown((int)data.VirtualKey);
                        }
                    }
                }
                catch
                {
                    Reset();
                }
            }

            return CallNextHookEx(_keyboardHook, code, wParam, lParam);
        }

        private IntPtr MouseHookCallback(
            int code,
            IntPtr wParam,
            IntPtr lParam)
        {
            if (code >= 0)
            {
                int message = wParam.ToInt32();

                if (message == WmLeftButtonDown ||
                    message == WmRightButtonDown ||
                    message == WmMiddleButtonDown)
                {
                    // النقر قد يغيّر مكان المؤشر أو يحدد نصاً، لذلك نبدأ من جديد.
                    Reset();
                }
            }

            return CallNextHookEx(_mouseHook, code, wParam, lParam);
        }

        private void ProcessKeyDown(int virtualKey)
        {
            if (IsModifierKey(virtualKey))
            {
                return;
            }

            IntPtr foreground = NativeInput.GetForegroundWindowHandle();

            if (foreground != _window)
            {
                Reset();
                _window = foreground;
            }

            if (foreground == IntPtr.Zero ||
                NativeInput.GetWindowProcessId(foreground) == _currentProcessId)
            {
                // لا نتابع الكتابة داخل MiftahFix نفسه.
                Reset();
                return;
            }

            bool control = NativeInput.IsKeyPressed(0x11);
            bool shift = NativeInput.IsKeyPressed(0x10);
            bool alt = NativeInput.IsKeyPressed(0x12);
            bool windows = NativeInput.IsKeyPressed(0x5B) || NativeInput.IsKeyPressed(0x5C);

            if (IsIgnoredHotkey(virtualKey, control, shift, alt, windows))
            {
                return;
            }

            if (control || alt || windows)
            {
                // اختصارات مثل Ctrl+A أو Ctrl+Z تغيّر النص بطريقة لا نعرفها.
                Reset();
                return;
            }

            if (virtualKey == 0x08)
            {
                if (_keys.Count > 0)
                {
                    _keys.RemoveAt(_keys.Count - 1);
                }

                return;
            }

            if (!TryGetPhysicalCharacter(virtualKey, shift, out char physical))
            {
                // Enter وTab والأسهم وHome وغيرها: المؤشر تحرك أو انتهى السطر.
                Reset();
                return;
            }

            char latinDisplay = physical;

            if (char.IsLetter(physical) && NativeInput.IsCapsLockOn())
            {
                latinDisplay = char.IsUpper(physical)
                    ? char.ToLowerInvariant(physical)
                    : char.ToUpperInvariant(physical);
            }

            bool typedInArabic =
                KeyboardLayoutSwitcher.IsArabicLayoutActive(foreground);

            _keys.Add(new TypedKey(physical, latinDisplay, typedInArabic));

            if (_keys.Count > MaximumKeys)
            {
                _keys.RemoveRange(0, _keys.Count - MaximumKeys);
            }

            if (physical == ' ')
            {
                RaiseWordCompleted();
            }
        }

        private void RaiseWordCompleted()
        {
            EventHandler<WordCompletedEventArgs>? handler = WordCompleted;

            if (handler == null)
            {
                return;
            }

            int end = _keys.Count - 1; // موقع المسافة

            if (end <= 0 ||
                _keys[end - 1].PhysicalCharacter == ' ')
            {
                return;
            }

            bool typedInArabic = _keys[end - 1].TypedInArabic;
            int start = end;

            while (start > 0 &&
                   _keys[start - 1].PhysicalCharacter != ' ' &&
                   _keys[start - 1].TypedInArabic == typedInArabic)
            {
                start--;
            }

            StringBuilder displayed = new();
            StringBuilder physical = new();

            for (int index = start; index < end; index++)
            {
                TypedKey key = _keys[index];

                physical.Append(key.PhysicalCharacter);

                displayed.Append(
                    typedInArabic
                        ? _converter.MapPhysicalKeyToArabic(key.PhysicalCharacter)
                        : key.LatinDisplayCharacter.ToString());
            }

            WordCompletedEventArgs args =
                new(displayed.ToString(), physical.ToString(), typedInArabic);

            // نخرج من دالة الـ Hook بسرعة ثم نعالج الكلمة لاحقاً على واجهة البرنامج.
            if (_synchronizationContext != null)
            {
                _synchronizationContext.Post(_ => handler(this, args), null);
            }
        }

        private bool IsIgnoredHotkey(
            int virtualKey,
            bool control,
            bool shift,
            bool alt,
            bool windows)
        {
            return _ignoredHotkey != Keys.None &&
                   virtualKey == (int)_ignoredHotkey &&
                   control == _ignoredControl &&
                   shift == _ignoredShift &&
                   alt == _ignoredAlt &&
                   windows == _ignoredWindows;
        }

        private static bool IsModifierKey(int virtualKey)
        {
            return virtualKey is
                0x10 or 0x11 or 0x12 or // Shift Ctrl Alt
                0xA0 or 0xA1 or 0xA2 or 0xA3 or 0xA4 or 0xA5 or
                0x5B or 0x5C or // Win
                0x14 or 0x90 or 0x91; // Caps Num Scroll
        }

        /// <summary>
        /// يحوّل رمز المفتاح إلى الحرف المطبوع عليه في لوحة الإنجليزية (US).
        /// </summary>
        internal static bool TryGetPhysicalCharacter(
            int virtualKey,
            bool shift,
            out char character)
        {
            if (virtualKey is >= 0x41 and <= 0x5A)
            {
                char letter = (char)('a' + (virtualKey - 0x41));
                character = shift ? char.ToUpperInvariant(letter) : letter;
                return true;
            }

            if (virtualKey is >= 0x30 and <= 0x39)
            {
                const string shifted = ")!@#$%^&*(";
                int digit = virtualKey - 0x30;
                character = shift ? shifted[digit] : (char)('0' + digit);
                return true;
            }

            if (virtualKey is >= 0x60 and <= 0x69)
            {
                character = (char)('0' + (virtualKey - 0x60));
                return true;
            }

            (char normal, char shifted)? pair = virtualKey switch
            {
                0x20 => (' ', ' '),
                0xBA => (';', ':'),
                0xBB => ('=', '+'),
                0xBC => (',', '<'),
                0xBD => ('-', '_'),
                0xBE => ('.', '>'),
                0xBF => ('/', '?'),
                0xC0 => ('`', '~'),
                0xDB => ('[', '{'),
                0xDC => ('\\', '|'),
                0xDD => (']', '}'),
                0xDE => ('\'', '"'),
                0x6A => ('*', '*'),
                0x6B => ('+', '+'),
                0x6D => ('-', '-'),
                0x6E => ('.', '.'),
                0x6F => ('/', '/'),
                _ => null
            };

            if (pair.HasValue)
            {
                character = shift ? pair.Value.shifted : pair.Value.normal;
                return true;
            }

            character = '\0';
            return false;
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            Stop();
            _isDisposed = true;
        }

        private void ThrowIfDisposed()
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);
        }

        private delegate IntPtr HookProcedure(
            int code,
            IntPtr wParam,
            IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public uint VirtualKey;
            public uint ScanCode;
            public uint Flags;
            public uint Time;
            public IntPtr ExtraInfo;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(
            int hookId,
            HookProcedure procedure,
            IntPtr module,
            uint threadId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hook);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(
            IntPtr hook,
            int code,
            IntPtr wParam,
            IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr GetModuleHandle(string? moduleName);
    }
}
