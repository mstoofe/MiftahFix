using MiftahFix.UI.Controls;
using MiftahFix.UI.Themes;

namespace MiftahFix
{
    public partial class Form1
    {
        private void InitializeModernButtonStyles()
        {
            ConfigureModernButton(
                btnConvert,
                ModernButtonStyle.Primary,
                ModernButtonSize.Normal,
                138);

            ConfigureModernButton(
                btnSwap,
                ModernButtonStyle.Info,
                ModernButtonSize.Normal,
                112);

            ConfigureModernButton(
                btnClear,
                ModernButtonStyle.Danger,
                ModernButtonSize.Normal,
                105);

            ConfigureModernButton(
                btnHistory,
                ModernButtonStyle.Outline,
                ModernButtonSize.Normal,
                112);

            ConfigureModernButton(
                btnSettings,
                ModernButtonStyle.Neutral,
                ModernButtonSize.Normal,
                120);

            ConfigureModernButton(
                btnPaste,
                ModernButtonStyle.Outline,
                ModernButtonSize.Compact,
                105);

            ConfigureModernButton(
                btnCopy,
                ModernButtonStyle.Success,
                ModernButtonSize.Compact,
                105);
        }

        private static void ConfigureModernButton(
            ModernButton button,
            ModernButtonStyle style,
            ModernButtonSize size,
            int width)
        {
            button.ButtonStyle = style;
            button.ButtonSize = size;
            button.CornerRadius =
                AppTheme.Radius.Medium;

            button.Width = width;
            button.Margin =
                new Padding(
                    AppTheme.Spacing.ExtraSmall);

            button.FlatAppearance.BorderSize = 0;
            button.UseVisualStyleBackColor = false;
        }
    }
}
