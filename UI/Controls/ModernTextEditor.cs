using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MiftahFix.UI.Themes;

namespace MiftahFix.UI.Controls
{
    [DefaultEvent(nameof(TextChanged))]
    public sealed class ModernTextEditor : UserControl
    {
        private readonly Panel _toolbar;
        private readonly FlowLayoutPanel _toolbarActions;
        private readonly Label _titleLabel;
        private readonly Label _statisticsLabel;
        private readonly RichTextBox _editor;
        private readonly Label _placeholderLabel;
        private readonly ToolTip _toolTip;

        private readonly ModernButton _openButton;
        private readonly ModernButton _saveButton;
        private readonly ModernButton _pasteButton;
        private readonly ModernButton _copyButton;
        private readonly ModernButton _clearButton;

        private string _placeholderText =
            "اكتب النص هنا أو اسحب ملف TXT إلى هذه المساحة...";

        private bool _showOpenButton = true;
        private bool _showSaveButton = true;
        private bool _showPasteButton = true;
        private bool _showCopyButton = true;
        private bool _showClearButton = true;
        private bool _isReadOnly;
        private float _editorFontSize = 14F;

        public ModernTextEditor()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);

            BackColor = AppTheme.Colors.Surface;
            Padding = new Padding(1);
            MinimumSize = new Size(260, 220);
            AllowDrop = true;

            _toolTip = new ToolTip
            {
                AutoPopDelay = 5000,
                InitialDelay = 350,
                ReshowDelay = 100,
                ShowAlways = true
            };

            _toolbar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 58,
                BackColor = AppTheme.Colors.SurfaceAlternative,
                Padding = new Padding(12, 8, 12, 8)
            };

            _titleLabel = new Label
            {
                Dock = DockStyle.Right,
                Width = 165,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = AppTheme.Colors.TextSecondary,
                Text = "محرر النص",
                TextAlign = ContentAlignment.MiddleRight
            };

            _statisticsLabel = new Label
            {
                Dock = DockStyle.Left,
                Width = 205,
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = AppTheme.Colors.TextMuted,
                Text = "0 حرف  •  0 كلمة  •  0 سطر",
                TextAlign = ContentAlignment.MiddleLeft
            };

            _toolbarActions = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                AutoScroll = false,
                BackColor = Color.Transparent,
                Padding = new Padding(4, 1, 4, 1)
            };

            _openButton = CreateToolbarButton("فتح", ModernButtonStyle.Outline);
            _saveButton = CreateToolbarButton("حفظ", ModernButtonStyle.Outline);
            _pasteButton = CreateToolbarButton("لصق", ModernButtonStyle.Neutral);
            _copyButton = CreateToolbarButton("نسخ", ModernButtonStyle.Success);
            _clearButton = CreateToolbarButton("مسح", ModernButtonStyle.Danger);
            _toolbarActions.Controls.AddRange(
                new Control[]
                {
                    _openButton,
                    _saveButton,
                    _pasteButton,
                    _copyButton,
                    _clearButton
                });

            _toolbar.Controls.Add(_toolbarActions);
            _toolbar.Controls.Add(_statisticsLabel);
            _toolbar.Controls.Add(_titleLabel);

            Panel editorHost = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = AppTheme.Colors.Surface,
                Padding = new Padding(14, 12, 14, 12)
            };

            _editor = new RichTextBox
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                BackColor = AppTheme.Colors.Surface,
                ForeColor = AppTheme.Colors.TextPrimary,
                Font = new Font("Segoe UI", _editorFontSize),
                DetectUrls = true,
                HideSelection = false,
                AcceptsTab = true,
                EnableAutoDragDrop = false,
                ScrollBars = RichTextBoxScrollBars.Vertical,
                RightToLeft = RightToLeft.No,
                WordWrap = true
            };

            _placeholderLabel = new Label
            {
                AutoSize = false,
                BackColor = Color.Transparent,
                ForeColor = AppTheme.Colors.TextMuted,
                Font = new Font("Segoe UI", 11F),
                Text = _placeholderText,
                TextAlign = ContentAlignment.TopRight,
                Padding = new Padding(8),
                Cursor = Cursors.IBeam
            };

            editorHost.Controls.Add(_editor);
            editorHost.Controls.Add(_placeholderLabel);

            Controls.Add(editorHost);
            Controls.Add(_toolbar);

            _editor.TextChanged += Editor_TextChanged;
            _editor.KeyDown += Editor_KeyDown;
            _editor.MouseWheel += Editor_MouseWheel;
            _editor.Enter += Editor_Enter;
            _editor.Leave += Editor_Leave;

            _placeholderLabel.Click += PlaceholderLabel_Click;

            _openButton.Click += OpenButton_Click;
            _saveButton.Click += SaveButton_Click;
            _pasteButton.Click += PasteButton_Click;
            _copyButton.Click += CopyButton_Click;
            _clearButton.Click += ClearButton_Click;
            DragEnter += ModernTextEditor_DragEnter;
            DragDrop += ModernTextEditor_DragDrop;
            editorHost.DragEnter += ModernTextEditor_DragEnter;
            editorHost.DragDrop += ModernTextEditor_DragDrop;
            _editor.AllowDrop = true;
            _editor.DragEnter += ModernTextEditor_DragEnter;
            _editor.DragDrop += ModernTextEditor_DragDrop;

            Resize += ModernTextEditor_Resize;

            ConfigureToolTips();
            UpdateToolbarVisibility();
            UpdatePlaceholder();
            UpdateStatistics();
        }

        [Category("MiftahFix")]
        [DefaultValue("محرر النص")]
        public string EditorTitle
        {
            get => _titleLabel.Text;
            set => _titleLabel.Text = value ?? string.Empty;
        }

        [Category("MiftahFix")]
        [DefaultValue("اكتب النص هنا أو اسحب ملف TXT إلى هذه المساحة...")]
        public string PlaceholderText
        {
            get => _placeholderText;
            set
            {
                _placeholderText = value ?? string.Empty;
                _placeholderLabel.Text = _placeholderText;
                UpdatePlaceholder();
            }
        }

        [Category("MiftahFix")]
        [DefaultValue(false)]
        public bool ReadOnly
        {
            get => _isReadOnly;
            set
            {
                _isReadOnly = value;
                _editor.ReadOnly = value;
                _pasteButton.Visible = !value && _showPasteButton;
                _clearButton.Visible = !value && _showClearButton;
            }
        }

        [Category("MiftahFix")]
        [DefaultValue(true)]
        public bool ShowOpenButton
        {
            get => _showOpenButton;
            set
            {
                _showOpenButton = value;
                UpdateToolbarVisibility();
            }
        }

        [Category("MiftahFix")]
        [DefaultValue(true)]
        public bool ShowSaveButton
        {
            get => _showSaveButton;
            set
            {
                _showSaveButton = value;
                UpdateToolbarVisibility();
            }
        }

        [Category("MiftahFix")]
        [DefaultValue(true)]
        public bool ShowPasteButton
        {
            get => _showPasteButton;
            set
            {
                _showPasteButton = value;
                UpdateToolbarVisibility();
            }
        }

        [Category("MiftahFix")]
        [DefaultValue(true)]
        public bool ShowCopyButton
        {
            get => _showCopyButton;
            set
            {
                _showCopyButton = value;
                UpdateToolbarVisibility();
            }
        }

        [Category("MiftahFix")]
        [DefaultValue(true)]
        public bool ShowClearButton
        {
            get => _showClearButton;
            set
            {
                _showClearButton = value;
                UpdateToolbarVisibility();
            }
        }

        [Category("MiftahFix")]
        [DefaultValue(14F)]
        public float EditorFontSize
        {
            get => _editorFontSize;
            set => SetEditorFontSize(value);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get => _editor == null
                ? base.Text
                : _editor.Text;
            set
            {
                string normalized = value ?? string.Empty;

                if (_editor == null)
                {
                    base.Text = normalized;
                    return;
                }

                if (_editor.Text == normalized)
                {
                    return;
                }

                _editor.Text = normalized;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TextLength => _editor.TextLength;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get => _editor.SelectionStart;
            set => _editor.SelectionStart =
                Math.Max(0, Math.Min(value, _editor.TextLength));
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionLength
        {
            get => _editor.SelectionLength;
            set => _editor.SelectionLength =
                Math.Max(0, Math.Min(value, _editor.TextLength));
        }

        public new void Focus()
        {
            _editor.Focus();
        }

        public void Clear()
        {
            _editor.Clear();
        }

        public void SelectAll()
        {
            _editor.SelectAll();
        }

        public void Copy()
        {
            _editor.Copy();
        }

        public void Paste()
        {
            if (!_isReadOnly && Clipboard.ContainsText())
            {
                _editor.Paste();
            }
        }

        public void Undo()
        {
            if (!_isReadOnly && _editor.CanUndo)
            {
                _editor.Undo();
            }
        }

        public void Redo()
        {
            if (!_isReadOnly && _editor.CanRedo)
            {
                _editor.Redo();
            }
        }

        private ModernButton CreateToolbarButton(
            string text,
            ModernButtonStyle style,
            int width = 68)
        {
            return new ModernButton
            {
                Text = text,
                ButtonStyle = style,
                ButtonSize = ModernButtonSize.Compact,
                CornerRadius = AppTheme.Radius.Small,
                Width = width,
                Height = 38,
                Margin = new Padding(3, 0, 3, 0),
                TabStop = false
            };
        }

        private void ConfigureToolTips()
        {
            _toolTip.SetToolTip(_openButton, "فتح ملف نصي UTF-8");
            _toolTip.SetToolTip(_saveButton, "حفظ النص في ملف TXT");
            _toolTip.SetToolTip(_pasteButton, "لصق النص من الحافظة");
            _toolTip.SetToolTip(_copyButton, "نسخ النص إلى الحافظة");
            _toolTip.SetToolTip(_clearButton, "مسح محتوى المحرر");
        }

        private void UpdateToolbarVisibility()
        {
            _openButton.Visible = _showOpenButton && !_isReadOnly;
            _saveButton.Visible = _showSaveButton;
            _pasteButton.Visible = _showPasteButton && !_isReadOnly;
            _copyButton.Visible = _showCopyButton;
            _clearButton.Visible = _showClearButton && !_isReadOnly;
        }

        private void Editor_TextChanged(object? sender, EventArgs e)
        {
            UpdatePlaceholder();
            UpdateStatistics();
            base.OnTextChanged(e);
        }

        private void Editor_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Add)
            {
                ChangeZoom(1F);
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == Keys.Subtract)
            {
                ChangeZoom(-1F);
                e.SuppressKeyPress = true;
            }

            base.OnKeyDown(e);
        }

        private void Editor_MouseWheel(object? sender, MouseEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) != Keys.Control)
            {
                return;
            }

            ChangeZoom(e.Delta > 0 ? 1F : -1F);
        }

        private void Editor_Enter(object? sender, EventArgs e)
        {
            BackColor = AppTheme.Colors.Primary;
            Invalidate();
        }

        private void Editor_Leave(object? sender, EventArgs e)
        {
            BackColor = AppTheme.Colors.Border;
            Invalidate();
        }

        private void PlaceholderLabel_Click(object? sender, EventArgs e)
        {
            _editor.Focus();
        }

        private void OpenButton_Click(object? sender, EventArgs e)
        {
            if (_isReadOnly)
            {
                return;
            }

            using OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "فتح ملف نصي",
                Filter = "الملفات النصية (*.txt)|*.txt|جميع الملفات (*.*)|*.*",
                CheckFileExists = true,
                Multiselect = false
            };

            if (dialog.ShowDialog(FindForm()) != DialogResult.OK)
            {
                return;
            }

            LoadTextFile(dialog.FileName);
        }

        private void SaveButton_Click(object? sender, EventArgs e)
        {
            using SaveFileDialog dialog = new SaveFileDialog
            {
                Title = "حفظ النص",
                Filter = "ملف نصي UTF-8 (*.txt)|*.txt",
                DefaultExt = "txt",
                AddExtension = true,
                FileName = "MiftahFix.txt"
            };

            if (dialog.ShowDialog(FindForm()) != DialogResult.OK)
            {
                return;
            }

            try
            {
                File.WriteAllText(
                    dialog.FileName,
                    _editor.Text,
                    new UTF8Encoding(true));
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    FindForm(),
                    $"تعذر حفظ الملف.{Environment.NewLine}{exception.Message}",
                    "MiftahFix",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void PasteButton_Click(object? sender, EventArgs e)
        {
            Paste();
        }

        private void CopyButton_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_editor.Text))
            {
                return;
            }

            Clipboard.SetText(_editor.Text);
        }

        private void ClearButton_Click(object? sender, EventArgs e)
        {
            if (!_isReadOnly)
            {
                _editor.Clear();
                _editor.Focus();
            }
        }

        private void ChangeZoom(float amount)
        {
            SetEditorFontSize(_editorFontSize + amount);
        }

        private void SetEditorFontSize(float value)
        {
            float normalized = Math.Max(9F, Math.Min(28F, value));

            if (Math.Abs(_editorFontSize - normalized) < 0.01F)
            {
                return;
            }

            _editorFontSize = normalized;
            _editor.Font = new Font("Segoe UI", _editorFontSize);
        }

        private void ModernTextEditor_DragEnter(
            object? sender,
            DragEventArgs e)
        {
            if (_isReadOnly ||
                !e.Data!.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            string[]? files =
                e.Data.GetData(DataFormats.FileDrop) as string[];

            e.Effect =
                files != null &&
                files.Length == 1 &&
                string.Equals(
                    Path.GetExtension(files[0]),
                    ".txt",
                    StringComparison.OrdinalIgnoreCase)
                    ? DragDropEffects.Copy
                    : DragDropEffects.None;
        }

        private void ModernTextEditor_DragDrop(
            object? sender,
            DragEventArgs e)
        {
            if (_isReadOnly)
            {
                return;
            }

            string[]? files =
                e.Data!.GetData(DataFormats.FileDrop) as string[];

            if (files == null ||
                files.Length != 1)
            {
                return;
            }

            LoadTextFile(files[0]);
        }

        private void LoadTextFile(string filePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);

                if (!fileInfo.Exists)
                {
                    return;
                }

                if (fileInfo.Length > 10 * 1024 * 1024)
                {
                    MessageBox.Show(
                        FindForm(),
                        "حجم الملف أكبر من 10 ميغابايت.",
                        "MiftahFix",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                _editor.Text = File.ReadAllText(
                    filePath,
                    Encoding.UTF8);

                _editor.SelectionStart =
                    _editor.TextLength;

                _editor.Focus();
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    FindForm(),
                    $"تعذر فتح الملف.{Environment.NewLine}{exception.Message}",
                    "MiftahFix",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void UpdatePlaceholder()
        {
            _placeholderLabel.Visible =
                string.IsNullOrEmpty(_editor.Text);

            _placeholderLabel.BringToFront();
        }

        private void UpdateStatistics()
        {
            string text = _editor.Text ?? string.Empty;
            int characters = text.Length;
            int words = 0;
            int lines = characters == 0 ? 0 : 1;
            bool insideWord = false;

            for (int index = 0; index < characters; index++)
            {
                char character = text[index];

                if (character == '\n')
                {
                    lines++;
                }

                if (char.IsWhiteSpace(character))
                {
                    insideWord = false;
                    continue;
                }

                if (!insideWord)
                {
                    words++;
                    insideWord = true;
                }
            }

            _statisticsLabel.Text =
                $"{characters:N0} حرف  •  {words:N0} كلمة  •  {lines:N0} سطر";
        }

        private void ModernTextEditor_Resize(object? sender, EventArgs e)
        {
            PositionPlaceholder();
        }

        private void PositionPlaceholder()
        {
            if (_editor.Parent == null)
            {
                return;
            }

            _placeholderLabel.Location =
                new Point(
                    _editor.Left + 4,
                    _editor.Top + 6);

            _placeholderLabel.Size =
                new Size(
                    Math.Max(0, _editor.Width - 8),
                    70);
        }

        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);

            _editor.RightToLeft = RightToLeft;
            _placeholderLabel.RightToLeft = RightToLeft;
            _titleLabel.RightToLeft = RightToLeft;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _toolTip.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
