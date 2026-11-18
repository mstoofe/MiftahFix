using System;
using System.Drawing;
using System.Windows.Forms;
using MiftahFix.Forms;
using MiftahFix.UI.Controls;

namespace MiftahFix
{
    public partial class Form1
    {
        private ModernButton? _btnUndo;
        private ModernButton? _btnAbout;
        private Label? _lblSessionHint;
        private System.Windows.Forms.Timer? _fadeTimer;

        private string _undoInput = string.Empty;
        private string _undoOutput = string.Empty;
        private int _undoModeIndex;
        private bool _hasUndoSnapshot;
        private bool _isRestoringUndo;

        private void InitializeAdvancedExperience()
        {
            CreateAdvancedControls();
            HookAdvancedEvents();
            PrepareFadeAnimation();
            UpdateUndoButtonState();
        }

        private void CreateAdvancedControls()
        {
            _btnUndo = new ModernButton
            {
                Name = "btnUndo",
                ButtonStyle = ModernButtonStyle.Neutral,
                ButtonSize = ModernButtonSize.Normal,
                CornerRadius = 12,
                Text = "↶  تراجع",
                Size = new Size(112, 52),
                Margin = new Padding(3),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                BackColor = Color.FromArgb(248, 250, 252),
                Cursor = Cursors.Hand,
                Enabled = false,
                TabStop = false
            };
            _btnUndo.FlatAppearance.BorderSize = 0;
            flowMainActions.Controls.Add(_btnUndo);
            flowMainActions.Controls.SetChildIndex(_btnUndo, 0);

            ConfigureInteractiveButton(
                _btnUndo,
                Color.FromArgb(248, 250, 252),
                Color.FromArgb(241, 245, 249),
                Color.FromArgb(226, 232, 240));

            _btnAbout = new ModernButton
            {
                Name = "btnAbout",
                ButtonStyle = ModernButtonStyle.Outline,
                ButtonSize = ModernButtonSize.Normal,
                CornerRadius = 12,
                Text = "ⓘ  حول",
                Size = new Size(105, 52),
                Margin = new Padding(3),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 64, 175),
                BackColor = Color.FromArgb(239, 246, 255),
                Cursor = Cursors.Hand,
                TabStop = false
            };
            _btnAbout.FlatAppearance.BorderSize = 0;
            flowUtilityActions.Controls.Add(_btnAbout);

            ConfigureInteractiveButton(
                _btnAbout,
                Color.FromArgb(239, 246, 255),
                Color.FromArgb(219, 234, 254),
                Color.FromArgb(191, 219, 254));

            _lblSessionHint = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(148, 163, 184),
                Text = "Ctrl + Z للتراجع   •   Ctrl + L للمسح   •   Esc للإخفاء",
                Location = new Point(26, 77),
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom
            };
            pnlHeaderContent.Controls.Add(_lblSessionHint);

            _interfaceToolTip?.SetToolTip(
                _btnUndo,
                "التراجع عن آخر عملية تحويل أو تبديل أو مسح");

            _interfaceToolTip?.SetToolTip(
                _btnAbout,
                "عرض معلومات البرنامج والإصدار");
        }

        private void HookAdvancedEvents()
        {
            _btnUndo!.Click += BtnUndo_Click;
            _btnAbout!.Click += BtnAbout_Click;

            btnConvert.MouseDown += CaptureUndoBeforeAction;
            btnSwap.MouseDown += CaptureUndoBeforeAction;
            btnClear.MouseDown += CaptureUndoBeforeAction;
            btnPaste.MouseDown += CaptureUndoBeforeAction;

            KeyPreview = true;
            KeyDown += Form1_AdvancedKeyDown;
            FormClosed += Form1_AdvancedFormClosed;
        }

        private void PrepareFadeAnimation()
        {
            Opacity = 0D;

            _fadeTimer = new System.Windows.Forms.Timer
            {
                Interval = 18
            };

            _fadeTimer.Tick += FadeTimer_Tick;
            Shown += Form1_AdvancedShown;
        }

        private void Form1_AdvancedShown(object? sender, EventArgs e)
        {
            _fadeTimer?.Start();
        }

        private void FadeTimer_Tick(object? sender, EventArgs e)
        {
            Opacity = Math.Min(1D, Opacity + 0.09D);

            if (Opacity >= 1D)
            {
                _fadeTimer?.Stop();
            }
        }

        private void CaptureUndoBeforeAction(
            object? sender,
            MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CaptureUndoSnapshot();
            }
        }

        private void CaptureUndoSnapshot()
        {
            if (_isRestoringUndo)
            {
                return;
            }

            _undoInput = txtInput.Text;
            _undoOutput = txtOutput.Text;
            _undoModeIndex = cmbConversionMode.SelectedIndex;
            _hasUndoSnapshot = true;

            UpdateUndoButtonState();
        }

        private void BtnUndo_Click(object? sender, EventArgs e)
        {
            RestoreUndoSnapshot();
        }

        private void RestoreUndoSnapshot()
        {
            if (!_hasUndoSnapshot)
            {
                ShowStatus(
                    "لا توجد عملية سابقة للتراجع عنها.",
                    StatusType.Information);
                return;
            }

            _isRestoringUndo = true;
            _isInternalUpdate = true;

            try
            {
                txtInput.Text = _undoInput;
                txtOutput.Text = _undoOutput;

                if (_undoModeIndex >= 0 &&
                    _undoModeIndex < cmbConversionMode.Items.Count)
                {
                    cmbConversionMode.SelectedIndex = _undoModeIndex;
                }
            }
            finally
            {
                _isInternalUpdate = false;
                _isRestoringUndo = false;
            }

            _hasUndoSnapshot = false;

            UpdateInputStatistics();
            UpdateOutputStatistics();
            UpdateTextDirections();
            UpdateUndoButtonState();

            ShowStatus(
                "تم التراجع عن آخر عملية.",
                StatusType.Success);

            txtInput.Focus();
        }

        private void UpdateUndoButtonState()
        {
            if (_btnUndo == null)
            {
                return;
            }

            _btnUndo.Enabled = _hasUndoSnapshot;
            _btnUndo.ForeColor = _hasUndoSnapshot
                ? Color.FromArgb(51, 65, 85)
                : Color.FromArgb(148, 163, 184);
        }

        private void BtnAbout_Click(object? sender, EventArgs e)
        {
            using AboutForm aboutForm =
                new AboutForm(
                    CheckForUpdatesManuallyAsync);
            aboutForm.ShowDialog(this);
        }

        private void Form1_AdvancedKeyDown(
            object? sender,
            KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                RestoreUndoSnapshot();
                e.SuppressKeyPress = true;
                e.Handled = true;
                return;
            }

            if (e.Control && e.KeyCode == Keys.L)
            {
                CaptureUndoSnapshot();
                BtnClear_Click(this, EventArgs.Empty);
                e.SuppressKeyPress = true;
                e.Handled = true;
                return;
            }

            if (e.KeyCode == Keys.Escape)
            {
                HideWindowToTray();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void Form1_AdvancedFormClosed(
            object? sender,
            FormClosedEventArgs e)
        {
            Shown -= Form1_AdvancedShown;
            KeyDown -= Form1_AdvancedKeyDown;
            FormClosed -= Form1_AdvancedFormClosed;

            btnConvert.MouseDown -= CaptureUndoBeforeAction;
            btnSwap.MouseDown -= CaptureUndoBeforeAction;
            btnClear.MouseDown -= CaptureUndoBeforeAction;
            btnPaste.MouseDown -= CaptureUndoBeforeAction;

            if (_btnUndo != null)
            {
                _btnUndo.Click -= BtnUndo_Click;
            }

            if (_btnAbout != null)
            {
                _btnAbout.Click -= BtnAbout_Click;
            }

            if (_fadeTimer != null)
            {
                _fadeTimer.Stop();
                _fadeTimer.Tick -= FadeTimer_Tick;
                _fadeTimer.Dispose();
                _fadeTimer = null;
            }
        }
    }
}
