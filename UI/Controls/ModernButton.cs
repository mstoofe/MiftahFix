using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MiftahFix.UI.Themes;

namespace MiftahFix.UI.Controls
{
    public enum ModernButtonStyle
    {
        Primary,
        Success,
        Danger,
        Warning,
        Info,
        Neutral,
        Outline,
        Ghost
    }

    public enum ModernButtonSize
    {
        Compact,
        Normal,
        Large
    }

    [DefaultEvent(nameof(Click))]
    public class ModernButton : Button
    {
        private ModernButtonStyle _buttonStyle = ModernButtonStyle.Primary;
        private ModernButtonSize _buttonSize = ModernButtonSize.Normal;
        private int _cornerRadius = AppTheme.Radius.Medium;
        private bool _isHovered;
        private bool _isPressed;
        private Color _normalBackColor;
        private Color _hoverBackColor;
        private Color _pressedBackColor;
        private Color _normalForeColor;
        private Color _borderColor;

        public ModernButton()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                true);

            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Cursor = Cursors.Hand;
            Font = AppTheme.Fonts.Button;
            TextAlign = ContentAlignment.MiddleCenter;
            UseVisualStyleBackColor = false;
            AutoSize = false;
            Height = AppTheme.ControlSizes.ButtonHeight;
            Padding = new Padding(
                AppTheme.Spacing.Medium,
                0,
                AppTheme.Spacing.Medium,
                0);

            ApplySizePreset();
            ApplyStylePreset();
        }

        [Category("MiftahFix")]
        [DefaultValue(ModernButtonStyle.Primary)]
        public ModernButtonStyle ButtonStyle
        {
            get => _buttonStyle;
            set
            {
                if (_buttonStyle == value)
                {
                    return;
                }

                _buttonStyle = value;
                ApplyStylePreset();
                Invalidate();
            }
        }

        [Category("MiftahFix")]
        [DefaultValue(ModernButtonSize.Normal)]
        public ModernButtonSize ButtonSize
        {
            get => _buttonSize;
            set
            {
                if (_buttonSize == value)
                {
                    return;
                }

                _buttonSize = value;
                ApplySizePreset();
                Invalidate();
            }
        }

        [Category("MiftahFix")]
        [DefaultValue(AppTheme.Radius.Medium)]
        public int CornerRadius
        {
            get => _cornerRadius;
            set
            {
                int normalizedValue = Math.Max(0, value);

                if (_cornerRadius == normalizedValue)
                {
                    return;
                }

                _cornerRadius = normalizedValue;
                UpdateControlRegion();
                Invalidate();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsHovered => _isHovered;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsPressed => _isPressed;

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Cursor = Enabled ? Cursors.Hand : Cursors.Default;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _isHovered = true;
            base.OnMouseEnter(e);
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _isHovered = false;
            _isPressed = false;
            base.OnMouseLeave(e);
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                _isPressed = true;
            }

            base.OnMouseDown(mevent);
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            _isPressed = false;
            base.OnMouseUp(mevent);
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateControlRegion();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle bounds = new Rectangle(0, 0, Width - 1, Height - 1);

            using GraphicsPath path = CreateRoundedPath(bounds, _cornerRadius);

            Color backColor = GetCurrentBackColor();
            Color foreColor = Enabled
                ? _normalForeColor
                : AppTheme.Colors.DisabledForeground;

            using SolidBrush backgroundBrush = new SolidBrush(backColor);
            pevent.Graphics.FillPath(backgroundBrush, path);

            if (ShouldDrawBorder())
            {
                using Pen borderPen = new Pen(
                    Enabled ? _borderColor : AppTheme.Colors.Border,
                    1F);

                pevent.Graphics.DrawPath(borderPen, path);
            }

            TextRenderer.DrawText(
                pevent.Graphics,
                Text,
                Font,
                bounds,
                foreColor,
                TextFormatFlags.HorizontalCenter |
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.EndEllipsis |
                TextFormatFlags.NoPadding);
        }

        private void ApplySizePreset()
        {
            Height = _buttonSize switch
            {
                ModernButtonSize.Compact => AppTheme.ControlSizes.CompactButtonHeight,
                ModernButtonSize.Large => AppTheme.ControlSizes.LargeButtonHeight,
                _ => AppTheme.ControlSizes.ButtonHeight
            };

            Font = _buttonSize switch
            {
                ModernButtonSize.Compact => new Font("Segoe UI", 9F, FontStyle.Bold),
                ModernButtonSize.Large => new Font("Segoe UI", 11F, FontStyle.Bold),
                _ => AppTheme.Fonts.Button
            };
        }

        private void ApplyStylePreset()
        {
            switch (_buttonStyle)
            {
                case ModernButtonStyle.Success:
                    SetPalette(
                        AppTheme.Colors.Success,
                        AppTheme.Colors.SuccessHover,
                        AppTheme.Colors.SuccessPressed,
                        Color.White,
                        AppTheme.Colors.Success);
                    break;

                case ModernButtonStyle.Danger:
                    SetPalette(
                        AppTheme.Colors.Danger,
                        AppTheme.Colors.DangerHover,
                        AppTheme.Colors.DangerPressed,
                        Color.White,
                        AppTheme.Colors.Danger);
                    break;

                case ModernButtonStyle.Warning:
                    SetPalette(
                        AppTheme.Colors.Warning,
                        AppTheme.Colors.WarningHover,
                        AppTheme.Colors.WarningPressed,
                        Color.White,
                        AppTheme.Colors.Warning);
                    break;

                case ModernButtonStyle.Info:
                    SetPalette(
                        AppTheme.Colors.Info,
                        AppTheme.Colors.InfoHover,
                        AppTheme.Colors.InfoPressed,
                        Color.White,
                        AppTheme.Colors.Info);
                    break;

                case ModernButtonStyle.Neutral:
                    SetPalette(
                        AppTheme.Colors.Neutral,
                        AppTheme.Colors.NeutralHover,
                        AppTheme.Colors.NeutralPressed,
                        AppTheme.Colors.TextSecondary,
                        AppTheme.Colors.Border);
                    break;

                case ModernButtonStyle.Outline:
                    SetPalette(
                        AppTheme.Colors.Surface,
                        Color.FromArgb(239, 246, 255),
                        Color.FromArgb(219, 234, 254),
                        AppTheme.Colors.Primary,
                        AppTheme.Colors.Primary);
                    break;

                case ModernButtonStyle.Ghost:
                    SetPalette(
                        Color.Transparent,
                        AppTheme.Colors.Neutral,
                        AppTheme.Colors.NeutralHover,
                        AppTheme.Colors.TextSecondary,
                        Color.Transparent);
                    break;

                default:
                    SetPalette(
                        AppTheme.Colors.Primary,
                        AppTheme.Colors.PrimaryHover,
                        AppTheme.Colors.PrimaryPressed,
                        Color.White,
                        AppTheme.Colors.Primary);
                    break;
            }

            Invalidate();
        }

        private void SetPalette(
            Color normalBackColor,
            Color hoverBackColor,
            Color pressedBackColor,
            Color normalForeColor,
            Color borderColor)
        {
            _normalBackColor = normalBackColor;
            _hoverBackColor = hoverBackColor;
            _pressedBackColor = pressedBackColor;
            _normalForeColor = normalForeColor;
            _borderColor = borderColor;
        }

        private Color GetCurrentBackColor()
        {
            if (!Enabled)
            {
                return AppTheme.Colors.DisabledBackground;
            }

            if (_isPressed)
            {
                return _pressedBackColor;
            }

            if (_isHovered)
            {
                return _hoverBackColor;
            }

            return _normalBackColor;
        }

        private bool ShouldDrawBorder()
        {
            return _buttonStyle == ModernButtonStyle.Outline ||
                   _buttonStyle == ModernButtonStyle.Neutral;
        }

        private void UpdateControlRegion()
        {
            if (Width <= 0 || Height <= 0)
            {
                return;
            }

            Rectangle bounds = new Rectangle(0, 0, Width, Height);

            using GraphicsPath path = CreateRoundedPath(bounds, _cornerRadius);

            Region? oldRegion = Region;
            Region = new Region(path);
            oldRegion?.Dispose();
        }

        private static GraphicsPath CreateRoundedPath(
            Rectangle rectangle,
            int radius)
        {
            GraphicsPath path = new GraphicsPath();

            if (radius <= 0)
            {
                path.AddRectangle(rectangle);
                path.CloseFigure();
                return path;
            }

            int diameter = Math.Min(
                radius * 2,
                Math.Min(rectangle.Width, rectangle.Height));

            Rectangle arc = new Rectangle(
                rectangle.Location,
                new Size(diameter, diameter));

            path.AddArc(arc, 180, 90);
            arc.X = rectangle.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = rectangle.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = rectangle.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}
