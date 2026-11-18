using System;

namespace MiftahFix.Models
{
    public sealed class ApplicationSettings
    {
        public GeneralSettings General { get; set; } =
            new GeneralSettings();

        public HotkeySettings Hotkey { get; set; } =
            new HotkeySettings();

        public ConversionSettings Conversion { get; set; } =
            new ConversionSettings();

        public HistorySettings History { get; set; } =
            new HistorySettings();

        public AdvancedSettings Advanced { get; set; } =
            new AdvancedSettings();

        public static ApplicationSettings CreateDefault()
        {
            return new ApplicationSettings();
        }

        public void Normalize()
        {
            General ??= new GeneralSettings();
            Hotkey ??= new HotkeySettings();
            Conversion ??= new ConversionSettings();
            History ??= new HistorySettings();
            Advanced ??= new AdvancedSettings();

            General.Normalize();
            Hotkey.Normalize();
            Conversion.Normalize();
            History.Normalize();
            Advanced.Normalize();
        }
    }

    public sealed class GeneralSettings
    {
        public bool RunWithWindows { get; set; }

        public bool StartMinimized { get; set; }

        public bool ShowStartupNotification { get; set; } =
            true;

        internal void Normalize()
        {
        }
    }

    public sealed class HotkeySettings
    {
        public bool Control { get; set; } =
            true;

        public bool Shift { get; set; } =
            true;

        public bool Alt { get; set; }

        public bool Windows { get; set; }

        public string Key { get; set; } =
            "Space";

        public void ResetToDefault()
        {
            Control = true;
            Shift = true;
            Alt = false;
            Windows = false;
            Key = "Space";
        }

        internal void Normalize()
        {
            Key = string.IsNullOrWhiteSpace(Key)
                ? "Space"
                : Key.Trim();

            bool hasModifier =
                Control ||
                Shift ||
                Alt ||
                Windows;

            if (!hasModifier)
            {
                Control = true;
                Shift = true;
            }
        }
    }

    public sealed class ConversionSettings
    {
        public bool InstantConversion { get; set; } =
            true;

        public string Mode { get; set; } =
            "Automatic";

        /// <summary>
        /// عند ضغط الاختصار بدون تحديد نص: تصحيح آخر ما كُتب تلقائياً.
        /// </summary>
        public bool ConvertTypedText { get; set; } =
            true;

        /// <summary>
        /// تبديل لغة لوحة المفاتيح في Windows بعد التصحيح.
        /// </summary>
        public bool SwitchKeyboardLayout { get; set; } =
            true;

        /// <summary>
        /// Off أو Notify أو AutoCorrect.
        /// </summary>
        public string TypingDetection { get; set; } =
            "Notify";

        internal void Normalize()
        {
            if (!string.Equals(TypingDetection, "Off", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(TypingDetection, "Notify", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(TypingDetection, "AutoCorrect", StringComparison.OrdinalIgnoreCase))
            {
                TypingDetection = "Notify";
            }

            if (string.IsNullOrWhiteSpace(Mode))
            {
                Mode = "Automatic";
                return;
            }

            Mode = Mode.Trim();

            if (!string.Equals(
                    Mode,
                    "Automatic",
                    StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(
                    Mode,
                    "EnglishToArabic",
                    StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(
                    Mode,
                    "ArabicToEnglish",
                    StringComparison.OrdinalIgnoreCase))
            {
                Mode = "Automatic";
            }
        }
    }

    public sealed class HistorySettings
    {
        public bool EnableHistory { get; set; } =
            true;

        public int MaxItems { get; set; } =
            100;

        internal void Normalize()
        {
            MaxItems = Math.Clamp(
                MaxItems,
                10,
                1000);
        }
    }

    public sealed class AdvancedSettings
    {
        public bool CheckForUpdates { get; set; } =
            true;

        internal void Normalize()
        {
        }
    }
}
