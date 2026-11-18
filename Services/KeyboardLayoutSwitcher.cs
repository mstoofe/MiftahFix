using System;
using System.Runtime.InteropServices;

namespace MiftahFix.Services
{
    /// <summary>
    /// يعرف لغة لوحة المفاتيح الحالية في البرنامج الذي يكتب فيه المستخدم،
    /// ويبدّلها إلى العربية أو الإنجليزية.
    /// </summary>
    public static class KeyboardLayoutSwitcher
    {
        private const int LanguageArabic = 0x01;
        private const int LanguageEnglish = 0x09;
        private const int WmInputLanguageChangeRequest = 0x0050;

        /// <summary>
        /// هل لغة الكتابة الحالية في النافذة المعطاة عربية؟
        /// </summary>
        public static bool IsArabicLayoutActive(IntPtr foregroundWindow)
        {
            IntPtr layout = GetLayoutForWindow(foregroundWindow);
            return GetPrimaryLanguage(layout) == LanguageArabic;
        }

        public static bool IsArabicLayoutInstalled()
        {
            return FindLayout(LanguageArabic) != IntPtr.Zero;
        }

        /// <summary>
        /// يطلب من البرنامج الأمامي التبديل إلى العربية أو الإنجليزية.
        /// يعيد false إذا كانت اللغة غير مثبتة في Windows.
        /// </summary>
        public static bool SwitchTo(bool arabic, IntPtr foregroundWindow)
        {
            if (foregroundWindow == IntPtr.Zero)
            {
                return false;
            }

            IntPtr layout = arabic
                ? FindLayout(LanguageArabic)
                : FindLayout(LanguageEnglish);

            if (layout == IntPtr.Zero && !arabic)
            {
                // إذا لم تكن الإنجليزية مثبتة نختار أول لغة غير عربية.
                layout = FindFirstNonArabicLayout();
            }

            if (layout == IntPtr.Zero)
            {
                return false;
            }

            if (GetLayoutForWindow(foregroundWindow) == layout)
            {
                return true;
            }

            IntPtr focusWindow =
                NativeInput.GetFocusedWindow(foregroundWindow);

            bool posted =
                PostMessage(
                    focusWindow,
                    WmInputLanguageChangeRequest,
                    IntPtr.Zero,
                    layout);

            if (focusWindow != foregroundWindow)
            {
                posted |=
                    PostMessage(
                        foregroundWindow,
                        WmInputLanguageChangeRequest,
                        IntPtr.Zero,
                        layout);
            }

            return posted;
        }

        private static IntPtr GetLayoutForWindow(IntPtr foregroundWindow)
        {
            IntPtr focusWindow =
                NativeInput.GetFocusedWindow(foregroundWindow);

            uint threadId =
                NativeInput.GetWindowThreadId(focusWindow);

            return GetKeyboardLayout(threadId);
        }

        private static int GetPrimaryLanguage(IntPtr layout)
        {
            long value = layout.ToInt64();
            return (int)(value & 0x3FF);
        }

        private static IntPtr FindLayout(int primaryLanguage)
        {
            foreach (IntPtr layout in GetInstalledLayouts())
            {
                if (GetPrimaryLanguage(layout) == primaryLanguage)
                {
                    return layout;
                }
            }

            return IntPtr.Zero;
        }

        private static IntPtr FindFirstNonArabicLayout()
        {
            foreach (IntPtr layout in GetInstalledLayouts())
            {
                if (GetPrimaryLanguage(layout) != LanguageArabic)
                {
                    return layout;
                }
            }

            return IntPtr.Zero;
        }

        private static IntPtr[] GetInstalledLayouts()
        {
            int count = GetKeyboardLayoutList(0, null);

            if (count <= 0)
            {
                return Array.Empty<IntPtr>();
            }

            IntPtr[] layouts = new IntPtr[count];
            GetKeyboardLayoutList(count, layouts);
            return layouts;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetKeyboardLayout(uint threadId);

        [DllImport("user32.dll")]
        private static extern int GetKeyboardLayoutList(
            int count,
            IntPtr[]? layouts);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PostMessage(
            IntPtr windowHandle,
            int message,
            IntPtr wParam,
            IntPtr lParam);
    }
}
