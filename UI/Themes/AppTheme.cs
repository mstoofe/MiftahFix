using System.Drawing;

namespace MiftahFix.UI.Themes
{
    public static class AppTheme
    {
        public static class Colors
        {
            public static readonly Color Primary = Color.FromArgb(37, 99, 235);
            public static readonly Color PrimaryHover = Color.FromArgb(29, 78, 216);
            public static readonly Color PrimaryPressed = Color.FromArgb(30, 64, 175);
            public static readonly Color Success = Color.FromArgb(22, 163, 74);
            public static readonly Color SuccessHover = Color.FromArgb(21, 128, 61);
            public static readonly Color SuccessPressed = Color.FromArgb(20, 83, 45);
            public static readonly Color Danger = Color.FromArgb(220, 38, 38);
            public static readonly Color DangerHover = Color.FromArgb(185, 28, 28);
            public static readonly Color DangerPressed = Color.FromArgb(153, 27, 27);
            public static readonly Color Warning = Color.FromArgb(217, 119, 6);
            public static readonly Color WarningHover = Color.FromArgb(180, 83, 9);
            public static readonly Color WarningPressed = Color.FromArgb(146, 64, 14);
            public static readonly Color Info = Color.FromArgb(8, 145, 178);
            public static readonly Color InfoHover = Color.FromArgb(14, 116, 144);
            public static readonly Color InfoPressed = Color.FromArgb(21, 94, 117);
            public static readonly Color Neutral = Color.FromArgb(241, 245, 249);
            public static readonly Color NeutralHover = Color.FromArgb(226, 232, 240);
            public static readonly Color NeutralPressed = Color.FromArgb(203, 213, 225);
            public static readonly Color Surface = Color.White;
            public static readonly Color SurfaceAlternative = Color.FromArgb(248, 250, 252);
            public static readonly Color Background = Color.FromArgb(241, 245, 249);
            public static readonly Color TextPrimary = Color.FromArgb(15, 23, 42);
            public static readonly Color TextSecondary = Color.FromArgb(71, 85, 105);
            public static readonly Color TextMuted = Color.FromArgb(148, 163, 184);
            public static readonly Color Border = Color.FromArgb(226, 232, 240);
            public static readonly Color DisabledBackground = Color.FromArgb(241, 245, 249);
            public static readonly Color DisabledForeground = Color.FromArgb(148, 163, 184);
        }

        public static class Fonts
        {
            public static Font Button => new Font("Segoe UI", 10F, FontStyle.Bold);
            public static Font Body => new Font("Segoe UI", 10F, FontStyle.Regular);
            public static Font Small => new Font("Segoe UI", 9F, FontStyle.Regular);
            public static Font Title => new Font("Segoe UI", 16F, FontStyle.Bold);
        }

        public static class Spacing
        {
            public const int ExtraSmall = 4;
            public const int Small = 8;
            public const int Medium = 12;
            public const int Large = 16;
            public const int ExtraLarge = 24;
        }

        public static class Radius
        {
            public const int Small = 8;
            public const int Medium = 12;
            public const int Large = 18;
        }

        public static class ControlSizes
        {
            public const int ButtonHeight = 48;
            public const int CompactButtonHeight = 40;
            public const int LargeButtonHeight = 56;
        }
    }
}
