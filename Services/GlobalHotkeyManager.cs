using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MiftahFix.Services
{
    [Flags]
    public enum HotkeyModifiers : uint
    {
        None = 0x0000,
        Alt = 0x0001,
        Control = 0x0002,
        Shift = 0x0004,
        Windows = 0x0008,
        NoRepeat = 0x4000
    }

    public sealed class GlobalHotkeyManager : NativeWindow, IDisposable
    {
        private const int WmHotkey = 0x0312;
        private const int MessageOnlyWindow = -3;

        private readonly int _hotkeyId;
        private bool _isRegistered;
        private bool _isDisposed;

        public GlobalHotkeyManager(int hotkeyId)
        {
            if (hotkeyId <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(hotkeyId),
                    "يجب أن يكون رقم الاختصار أكبر من صفر.");
            }

            _hotkeyId = hotkeyId;
            CreateMessageWindow();
        }

        public GlobalHotkeyManager(IntPtr ownerHandle, int hotkeyId)
            : this(hotkeyId)
        {
            _ = ownerHandle;
        }

        public GlobalHotkeyManager(int hotkeyId, IntPtr ownerHandle)
            : this(hotkeyId)
        {
            _ = ownerHandle;
        }

        public event EventHandler? HotkeyPressed;

        public bool IsRegistered => _isRegistered;

        public bool Register(
            Keys key,
            bool control,
            bool shift,
            bool alt,
            bool windows)
        {
            ThrowIfDisposed();

            if (Handle == IntPtr.Zero)
            {
                CreateMessageWindow();
            }

            if (_isRegistered)
            {
                Unregister();
            }

            HotkeyModifiers modifiers = HotkeyModifiers.NoRepeat;

            if (control)
            {
                modifiers |= HotkeyModifiers.Control;
            }

            if (shift)
            {
                modifiers |= HotkeyModifiers.Shift;
            }

            if (alt)
            {
                modifiers |= HotkeyModifiers.Alt;
            }

            if (windows)
            {
                modifiers |= HotkeyModifiers.Windows;
            }

            _isRegistered = RegisterHotKey(
                Handle,
                _hotkeyId,
                (uint)modifiers,
                (uint)key);

            return _isRegistered;
        }

        public void Unregister()
        {
            if (_isDisposed || !_isRegistered)
            {
                return;
            }

            if (Handle != IntPtr.Zero)
            {
                UnregisterHotKey(Handle, _hotkeyId);
            }

            _isRegistered = false;
        }

        public bool IsHotkeyMessage(ref Message message)
        {
            return message.Msg == WmHotkey &&
                   message.WParam.ToInt32() == _hotkeyId;
        }

        public bool IsHotkeyMessage(Message message)
        {
            return message.Msg == WmHotkey &&
                   message.WParam.ToInt32() == _hotkeyId;
        }

        protected override void WndProc(ref Message message)
        {
            if (IsHotkeyMessage(ref message))
            {

                HotkeyPressed?.Invoke(this, EventArgs.Empty);
                return;
            }

            base.WndProc(ref message);
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            Unregister();
            HotkeyPressed = null;

            if (Handle != IntPtr.Zero)
            {
                DestroyHandle();
            }

            _isDisposed = true;
            GC.SuppressFinalize(this);
        }

        private void CreateMessageWindow()
        {
            if (Handle != IntPtr.Zero)
            {
                return;
            }

            CreateParams createParams = new CreateParams
            {
                Caption = "MiftahFix.GlobalHotkeyWindow",
                Parent = new IntPtr(MessageOnlyWindow)
            };

            CreateHandle(createParams);

            if (Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException(
                    "تعذر إنشاء نافذة رسائل الاختصار العام.");
            }
        }

        private void ThrowIfDisposed()
        {
            ObjectDisposedException.ThrowIf(
                _isDisposed,
                nameof(GlobalHotkeyManager));
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool RegisterHotKey(
            IntPtr hWnd,
            int id,
            uint fsModifiers,
            uint virtualKey);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnregisterHotKey(
            IntPtr hWnd,
            int id);
    }
}
