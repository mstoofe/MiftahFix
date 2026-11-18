using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiftahFix.Services
{
    /// <summary>
    /// أدوات مشتركة للتعامل مع لوحة المفاتيح على مستوى Windows:
    /// إرسال نص، حذف أحرف، انتظار تحرير المفاتيح، ومعرفة النافذة المركّز عليها.
    /// </summary>
    internal static class NativeInput
    {
        private const uint InputKeyboard = 1;
        private const uint KeyEventKeyUp = 0x0002;
        private const uint KeyEventUnicode = 0x0004;
        private const ushort VirtualKeyBack = 0x08;

        /// <summary>
        /// يحذف عدداً من الأحرف ثم يكتب النص الجديد في عملية واحدة متصلة،
        /// فلا تتداخل معها ضغطات المستخدم.
        /// </summary>
        public static void ReplaceTypedText(
            int backspaceCount,
            string newText)
        {
            List<INPUT> inputs = new();

            for (int index = 0; index < backspaceCount; index++)
            {
                inputs.Add(CreateVirtualKey(VirtualKeyBack, keyUp: false));
                inputs.Add(CreateVirtualKey(VirtualKeyBack, keyUp: true));
            }

            foreach (char character in newText)
            {
                inputs.Add(CreateUnicode(character, keyUp: false));
                inputs.Add(CreateUnicode(character, keyUp: true));
            }

            if (inputs.Count == 0)
            {
                return;
            }

            INPUT[] array = inputs.ToArray();

            uint sent =
                SendInput(
                    (uint)array.Length,
                    array,
                    Marshal.SizeOf<INPUT>());

            if (sent != array.Length)
            {
                int errorCode = Marshal.GetLastWin32Error();

                throw new Win32Exception(
                    errorCode,
                    $"تعذر إرسال النص المصحح. تم إرسال {sent} من {array.Length}.");
            }
        }

        /// <summary>
        /// ينتظر حتى يرفع المستخدم يده عن مفاتيح الاختصار (Ctrl وShift وAlt وWin والمفتاح نفسه).
        /// </summary>
        public static async Task<bool> WaitForKeysReleasedAsync(
            Keys hotkeyKey,
            int maximumAttempts = 75,
            int delayMilliseconds = 20)
        {
            int[] keys =
            {
                0x10, // Shift
                0x11, // Ctrl
                0x12, // Alt
                0x5B, // Left Win
                0x5C, // Right Win
                (int)(hotkeyKey & Keys.KeyCode)
            };

            for (int attempt = 0; attempt < maximumAttempts; attempt++)
            {
                bool anyPressed = false;

                foreach (int key in keys)
                {
                    if (key != 0 && IsKeyPressed(key))
                    {
                        anyPressed = true;
                        break;
                    }
                }

                if (!anyPressed)
                {
                    return true;
                }

                await Task.Delay(delayMilliseconds);
            }

            return false;
        }

        public static bool IsKeyPressed(int virtualKey)
        {
            return (GetAsyncKeyState(virtualKey) & 0x8000) != 0;
        }

        public static bool IsCapsLockOn()
        {
            return (GetKeyState(0x14) & 0x0001) != 0;
        }

        public static IntPtr GetForegroundWindowHandle()
        {
            return GetForegroundWindow();
        }

        /// <summary>
        /// يعيد النافذة التي يكتب فيها المستخدم فعلياً (عنصر التركيز)، أو النافذة الأمامية.
        /// </summary>
        public static IntPtr GetFocusedWindow(IntPtr foregroundWindow)
        {
            if (foregroundWindow == IntPtr.Zero)
            {
                return IntPtr.Zero;
            }

            uint threadId =
                GetWindowThreadProcessId(foregroundWindow, out _);

            GUITHREADINFO info = new()
            {
                Size = Marshal.SizeOf<GUITHREADINFO>()
            };

            if (threadId != 0 &&
                GetGUIThreadInfo(threadId, ref info) &&
                info.FocusWindow != IntPtr.Zero)
            {
                return info.FocusWindow;
            }

            return foregroundWindow;
        }

        public static uint GetWindowThreadId(IntPtr window)
        {
            return window == IntPtr.Zero
                ? 0
                : GetWindowThreadProcessId(window, out _);
        }

        public static uint GetWindowProcessId(IntPtr window)
        {
            if (window == IntPtr.Zero)
            {
                return 0;
            }

            GetWindowThreadProcessId(window, out uint processId);
            return processId;
        }

        public static bool IsCurrentProcessElevated()
        {
            using System.Diagnostics.Process process =
                System.Diagnostics.Process.GetCurrentProcess();

            return IsProcessElevated((uint)process.Id);
        }

        /// <summary>
        /// هل البرنامج يعمل بصلاحيات المسؤول؟ إذا رفض Windows السؤال نعتبره مرفوع الصلاحيات.
        /// </summary>
        public static bool IsProcessElevated(uint processId)
        {
            if (processId == 0)
            {
                return false;
            }

            const uint ProcessQueryLimitedInformation = 0x1000;
            const uint TokenQuery = 0x0008;
            const int TokenElevationClass = 20;

            IntPtr processHandle =
                OpenProcess(ProcessQueryLimitedInformation, false, processId);

            if (processHandle == IntPtr.Zero)
            {
                return false;
            }

            try
            {
                if (!OpenProcessToken(processHandle, TokenQuery, out IntPtr tokenHandle))
                {
                    return Marshal.GetLastWin32Error() == 5; // Access denied
                }

                try
                {
                    int elevation = 0;

                    return GetTokenInformation(
                               tokenHandle,
                               TokenElevationClass,
                               ref elevation,
                               sizeof(int),
                               out _) &&
                           elevation != 0;
                }
                finally
                {
                    CloseHandle(tokenHandle);
                }
            }
            finally
            {
                CloseHandle(processHandle);
            }
        }

        private static INPUT CreateVirtualKey(ushort virtualKey, bool keyUp)
        {
            return new INPUT
            {
                Type = InputKeyboard,
                Union = new INPUTUNION
                {
                    Keyboard = new KEYBDINPUT
                    {
                        VirtualKey = virtualKey,
                        ScanCode = 0,
                        Flags = keyUp ? KeyEventKeyUp : 0,
                        Time = 0,
                        ExtraInfo = IntPtr.Zero
                    }
                }
            };
        }

        private static INPUT CreateUnicode(char character, bool keyUp)
        {
            return new INPUT
            {
                Type = InputKeyboard,
                Union = new INPUTUNION
                {
                    Keyboard = new KEYBDINPUT
                    {
                        VirtualKey = 0,
                        ScanCode = character,
                        Flags = KeyEventUnicode | (keyUp ? KeyEventKeyUp : 0),
                        Time = 0,
                        ExtraInfo = IntPtr.Zero
                    }
                }
            };
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public uint Type;
            public INPUTUNION Union;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct INPUTUNION
        {
            [FieldOffset(0)]
            public MOUSEINPUT Mouse;

            [FieldOffset(0)]
            public KEYBDINPUT Keyboard;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int DeltaX;
            public int DeltaY;
            public uint MouseData;
            public uint Flags;
            public uint Time;
            public IntPtr ExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public ushort VirtualKey;
            public ushort ScanCode;
            public uint Flags;
            public uint Time;
            public IntPtr ExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct GUITHREADINFO
        {
            public int Size;
            public uint Flags;
            public IntPtr ActiveWindow;
            public IntPtr FocusWindow;
            public IntPtr CaptureWindow;
            public IntPtr MenuOwnerWindow;
            public IntPtr MoveSizeWindow;
            public IntPtr CaretWindow;
            public RECT CaretRectangle;
        }

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int virtualKey);

        [DllImport("user32.dll")]
        private static extern short GetKeyState(int virtualKey);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(
            IntPtr windowHandle,
            out uint processId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetGUIThreadInfo(
            uint threadId,
            ref GUITHREADINFO info);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(
            uint access,
            bool inheritHandle,
            uint processId);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool OpenProcessToken(
            IntPtr processHandle,
            uint desiredAccess,
            out IntPtr tokenHandle);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetTokenInformation(
            IntPtr tokenHandle,
            int informationClass,
            ref int information,
            int informationLength,
            out int returnLength);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr handle);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(
            uint inputCount,
            INPUT[] inputs,
            int inputSize);
    }
}
