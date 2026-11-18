using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiftahFix.Services
{
    public sealed class SelectedTextReplacementService
    {
        private const int VirtualKeyControl = 0x11;
        private const int VirtualKeyShift = 0x10;
        private const int VirtualKeySpace = 0x20;
        private const ushort VirtualKeyC = 0x43;
        private const ushort VirtualKeyV = 0x56;

        private const uint InputKeyboard = 1;
        private const uint KeyEventKeyUp = 0x0002;

        private readonly KeyboardLayoutConverter _converter;

        private bool _isProcessing;

        public SelectedTextReplacementService(
            KeyboardLayoutConverter converter)
        {
            _converter =
                converter ??
                throw new ArgumentNullException(
                    nameof(converter));
        }

        public bool IsProcessing =>
            _isProcessing;

        public Task<SelectedTextReplacementResult>
            ReplaceSelectedTextAsync()
        {
            return ReplaceSelectedTextAsync(
                ConversionDirection.Automatic,
                Keys.Space);
        }

        public async Task<SelectedTextReplacementResult>
            ReplaceSelectedTextAsync(
                ConversionDirection direction,
                Keys hotkeyKey)
        {
            if (_isProcessing)
            {
                return SelectedTextReplacementResult.Fail(
                    "هناك عملية تحويل قيد التنفيذ.");
            }

            _isProcessing = true;

            string? previousClipboardText = null;
            bool hadPreviousClipboardText = false;
            DataObject? previousClipboardData = null;

            IntPtr targetWindow =
                GetForegroundWindow();

            try
            {
                bool keysReleased =
                    await NativeInput.WaitForKeysReleasedAsync(
                        hotkeyKey);

                if (!keysReleased)
                {
                    return SelectedTextReplacementResult.Fail(
                        "ارفع يدك عن مفاتيح الاختصار ثم حاول مرة أخرى.");
                }

                if (targetWindow == IntPtr.Zero)
                {
                    return SelectedTextReplacementResult.Fail(
                        "تعذر تحديد النافذة التي تحتوي على النص المحدد.");
                }

                previousClipboardData =
                    CaptureClipboardData();

                try
                {
                    if (Clipboard.ContainsText())
                    {
                        previousClipboardText =
                            Clipboard.GetText();

                        hadPreviousClipboardText = true;
                    }
                }
                catch
                {
                    previousClipboardText = null;
                    hadPreviousClipboardText = false;
                }

                bool clipboardCleared =
                    await ClearClipboardWithRetryAsync();

                if (!clipboardCleared)
                {
                    return SelectedTextReplacementResult.Fail(
                        "تعذر تجهيز الحافظة.");
                }

                await Task.Delay(100);

                if (!ActivateTargetWindow(targetWindow))
                {
                    return SelectedTextReplacementResult.Fail(
                        "تعذر إعادة التركيز إلى البرنامج الذي يحتوي على النص.");
                }

                await Task.Delay(100);

                SendControlShortcut(VirtualKeyC);

                string selectedText =
                    await WaitForClipboardTextAsync(
                        maximumAttempts: 50,
                        delayMilliseconds: 50);

                if (string.IsNullOrWhiteSpace(
                    selectedText))
                {
                    return SelectedTextReplacementResult.Fail(
                        "لم يتم نسخ النص المحدد. تأكد من تحديد النص ثم حاول مرة أخرى.");
                }

                string convertedText =
                    _converter.Convert(
                        selectedText,
                        direction);

                if (string.Equals(
                    selectedText,
                    convertedText,
                    StringComparison.Ordinal))
                {
                    return SelectedTextReplacementResult.Fail(
                        "لم يتغير النص بعد التحويل.");
                }

                bool clipboardUpdated =
                    await SetClipboardTextWithRetryAsync(
                        convertedText);

                if (!clipboardUpdated)
                {
                    return SelectedTextReplacementResult.Fail(
                        "تعذر وضع النص المحول في الحافظة.");
                }

                await Task.Delay(150);

                if (!ActivateTargetWindow(targetWindow))
                {
                    return SelectedTextReplacementResult.Fail(
                        "تعذر إعادة التركيز إلى البرنامج قبل لصق النص.");
                }

                await Task.Delay(100);

                SendControlShortcut(VirtualKeyV);

                await Task.Delay(1200);

                return SelectedTextReplacementResult.Success(
                    selectedText,
                    convertedText);
            }
            catch (Exception exception)
            {
                return SelectedTextReplacementResult.Fail(
                    $"تعذر تحويل النص المحدد: {exception.Message}");
            }
            finally
            {
                if (previousClipboardData != null)
                {
                    await RestoreClipboardDataAsync(
                        previousClipboardData,
                        previousClipboardText);
                }
                else if (hadPreviousClipboardText &&
                    previousClipboardText != null)
                {
                    await RestoreClipboardTextAsync(
                        previousClipboardText);
                }

                _isProcessing = false;
            }
        }

        private static bool ActivateTargetWindow(
            IntPtr targetWindow)
        {
            if (targetWindow == IntPtr.Zero)
            {
                return false;
            }

            IntPtr currentForeground =
                GetForegroundWindow();

            if (currentForeground == targetWindow)
            {
                return true;
            }

            uint currentThreadId =
                GetCurrentThreadId();

            uint targetThreadId =
                GetWindowThreadProcessId(
                    targetWindow,
                    out _);

            bool threadsAttached = false;

            try
            {
                if (targetThreadId != 0 &&
                    targetThreadId != currentThreadId)
                {
                    threadsAttached =
                        AttachThreadInput(
                            currentThreadId,
                            targetThreadId,
                            true);
                }

                ShowWindow(
                    targetWindow,
                    ShowWindowRestore);

                BringWindowToTop(
                    targetWindow);

                SetForegroundWindow(
                    targetWindow);

                SetFocus(
                    targetWindow);
            }
            finally
            {
                if (threadsAttached)
                {
                    AttachThreadInput(
                        currentThreadId,
                        targetThreadId,
                        false);
                }
            }

            return GetForegroundWindow() ==
                   targetWindow;
        }

        private static void SendControlShortcut(
            ushort virtualKey)
        {
            INPUT[] inputs =
            {
                CreateKeyboardInput(
                    (ushort)VirtualKeyControl,
                    keyUp: false),

                CreateKeyboardInput(
                    virtualKey,
                    keyUp: false),

                CreateKeyboardInput(
                    virtualKey,
                    keyUp: true),

                CreateKeyboardInput(
                    (ushort)VirtualKeyControl,
                    keyUp: true)
            };

            uint sent =
                SendInput(
                    (uint)inputs.Length,
                    inputs,
                    Marshal.SizeOf<INPUT>());

            if (sent != inputs.Length)
            {
                int errorCode =
                    Marshal.GetLastWin32Error();

                throw new Win32Exception(
                    errorCode,
                    $"تعذر إرسال اختصار لوحة المفاتيح. تم إرسال {sent} من {inputs.Length}. رمز الخطأ: {errorCode}.");
            }
        }

        private static INPUT CreateKeyboardInput(
            ushort virtualKey,
            bool keyUp)
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
                        Flags = keyUp
                            ? KeyEventKeyUp
                            : 0,
                        Time = 0,
                        ExtraInfo = IntPtr.Zero
                    }
                }
            };
        }

        private static async Task<bool>
            ClearClipboardWithRetryAsync()
        {
            for (int attempt = 0;
                 attempt < 20;
                 attempt++)
            {
                try
                {
                    Clipboard.Clear();

                    return true;
                }
                catch
                {
                    await Task.Delay(50);
                }
            }

            return false;
        }

        private static async Task<string>
            WaitForClipboardTextAsync(
                int maximumAttempts,
                int delayMilliseconds)
        {
            for (int attempt = 0;
                 attempt < maximumAttempts;
                 attempt++)
            {
                try
                {
                    if (Clipboard.ContainsText(
                        TextDataFormat.UnicodeText))
                    {
                        string text =
                            Clipboard.GetText(
                                TextDataFormat.UnicodeText);

                        if (!string.IsNullOrEmpty(
                            text))
                        {
                            return text;
                        }
                    }

                    if (Clipboard.ContainsText())
                    {
                        string text =
                            Clipboard.GetText();

                        if (!string.IsNullOrEmpty(
                            text))
                        {
                            return text;
                        }
                    }
                }
                catch
                {
                }

                await Task.Delay(
                    delayMilliseconds);
            }

            return string.Empty;
        }

        private static async Task<bool>
            SetClipboardTextWithRetryAsync(
                string text)
        {
            for (int attempt = 0;
                 attempt < 20;
                 attempt++)
            {
                try
                {
                    Clipboard.SetText(
                        text,
                        TextDataFormat.UnicodeText);

                    return true;
                }
                catch
                {
                    await Task.Delay(50);
                }
            }

            return false;
        }

        private static async Task
            RestoreClipboardTextAsync(
                string previousText)
        {
            await Task.Delay(250);

            for (int attempt = 0;
                 attempt < 10;
                 attempt++)
            {
                try
                {
                    Clipboard.SetText(
                        previousText,
                        TextDataFormat.UnicodeText);

                    return;
                }
                catch
                {
                    await Task.Delay(60);
                }
            }
        }

        /// <summary>
        /// نسخة من كل محتوى الحافظة (صور، ملفات، نص منسق...) لإرجاعها بعد التصحيح.
        /// </summary>
        private static DataObject? CaptureClipboardData()
        {
            try
            {
                IDataObject? current =
                    Clipboard.GetDataObject();

                if (current == null)
                {
                    return null;
                }

                string[] formats =
                    current.GetFormats(
                        autoConvert: false);

                if (formats.Length == 0)
                {
                    return null;
                }

                DataObject copy = new();
                int copiedFormats = 0;

                foreach (string format in formats)
                {
                    try
                    {
                        object? data =
                            current.GetData(
                                format,
                                autoConvert: false);

                        if (data != null)
                        {
                            copy.SetData(
                                format,
                                data);

                            copiedFormats++;
                        }
                    }
                    catch
                    {
                        // بعض الصيغ الخاصة لا يمكن نسخها، نتجاوزها.
                    }
                }

                return copiedFormats > 0
                    ? copy
                    : null;
            }
            catch
            {
                return null;
            }
        }

        private static async Task
            RestoreClipboardDataAsync(
                DataObject previousData,
                string? previousText)
        {
            await Task.Delay(250);

            for (int attempt = 0;
                 attempt < 10;
                 attempt++)
            {
                try
                {
                    Clipboard.SetDataObject(
                        previousData,
                        copy: true);

                    return;
                }
                catch
                {
                    await Task.Delay(60);
                }
            }

            if (previousText != null)
            {
                await RestoreClipboardTextAsync(
                    previousText);
            }
        }

        private const int ShowWindowRestore = 9;

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

            [FieldOffset(0)]
            public HARDWAREINPUT Hardware;
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
        private struct HARDWAREINPUT
        {
            public uint Message;
            public ushort ParameterLow;
            public ushort ParameterHigh;
        }

        [DllImport(
            "user32.dll")]
        private static extern short
            GetAsyncKeyState(
                int virtualKey);

        [DllImport(
            "user32.dll")]
        private static extern IntPtr
            GetForegroundWindow();

        [DllImport(
            "user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool
            SetForegroundWindow(
                IntPtr windowHandle);

        [DllImport(
            "user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool
            BringWindowToTop(
                IntPtr windowHandle);

        [DllImport(
            "user32.dll")]
        private static extern IntPtr
            SetFocus(
                IntPtr windowHandle);

        [DllImport(
            "user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool
            ShowWindow(
                IntPtr windowHandle,
                int commandShow);

        [DllImport(
            "user32.dll")]
        private static extern uint
            GetWindowThreadProcessId(
                IntPtr windowHandle,
                out uint processId);

        [DllImport(
            "kernel32.dll")]
        private static extern uint
            GetCurrentThreadId();

        [DllImport(
            "user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool
            AttachThreadInput(
                uint idAttach,
                uint idAttachTo,
                bool attach);

        [DllImport(
            "user32.dll",
            SetLastError = true)]
        private static extern uint
            SendInput(
                uint inputCount,
                INPUT[] inputs,
                int inputSize);
    }

    public sealed class SelectedTextReplacementResult
    {
        private SelectedTextReplacementResult(
            bool isSuccess,
            string message,
            string originalText,
            string convertedText)
        {
            IsSuccess = isSuccess;
            Message = message;
            OriginalText = originalText;
            ConvertedText = convertedText;
        }

        public bool IsSuccess { get; }

        public string Message { get; }

        public string OriginalText { get; }

        public string ConvertedText { get; }

        public static SelectedTextReplacementResult
            Success(
                string originalText,
                string convertedText)
        {
            return new SelectedTextReplacementResult(
                isSuccess: true,
                message: "تم تصحيح النص المحدد.",
                originalText: originalText,
                convertedText: convertedText);
        }

        public static SelectedTextReplacementResult
            Fail(
                string message)
        {
            return new SelectedTextReplacementResult(
                isSuccess: false,
                message: message,
                originalText: string.Empty,
                convertedText: string.Empty);
        }
    }
}