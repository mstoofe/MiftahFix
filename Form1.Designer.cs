using MiftahFix.UI.Controls;

namespace MiftahFix
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlHeaderContent;
        private System.Windows.Forms.PictureBox picApplicationIcon;
        private System.Windows.Forms.Label lblApplicationName;
        private System.Windows.Forms.Label lblApplicationEnglishName;
        private System.Windows.Forms.Label lblApplicationDescription;
        private System.Windows.Forms.Label lblHeaderBadge;

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlControlBar;
        private System.Windows.Forms.TableLayoutPanel tableControlBar;
        private System.Windows.Forms.FlowLayoutPanel flowConversionOptions;
        private System.Windows.Forms.Label lblConversionMode;
        private System.Windows.Forms.ComboBox cmbConversionMode;
        private System.Windows.Forms.CheckBox chkInstantConversion;
        private System.Windows.Forms.FlowLayoutPanel flowMainActions;
        private ModernButton btnConvert;
        private ModernButton btnSwap;
        private ModernButton btnClear;
        private System.Windows.Forms.FlowLayoutPanel flowUtilityActions;
        private ModernButton btnHistory;
        private ModernButton btnSettings;

        private System.Windows.Forms.TableLayoutPanel tableEditors;

        private System.Windows.Forms.Panel pnlInputCard;
        private System.Windows.Forms.Panel pnlInputAccent;
        private System.Windows.Forms.Panel pnlInputHeader;
        private System.Windows.Forms.Label lblInputTitle;
        private System.Windows.Forms.Label lblInputDescription;
        private ModernButton btnPaste;
        private ModernTextEditor txtInput;
        private System.Windows.Forms.Panel pnlInputFooter;
        private System.Windows.Forms.Label lblInputStatistics;

        private System.Windows.Forms.Panel pnlOutputCard;
        private System.Windows.Forms.Panel pnlOutputAccent;
        private System.Windows.Forms.Panel pnlOutputHeader;
        private System.Windows.Forms.Label lblOutputTitle;
        private System.Windows.Forms.Label lblOutputDescription;
        private ModernButton btnCopy;
        private ModernTextEditor txtOutput;
        private System.Windows.Forms.Panel pnlOutputFooter;
        private System.Windows.Forms.Label lblOutputStatistics;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlStatusIndicator;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblShortcut;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            pnlHeaderContent = new Panel();
            lblHeaderBadge = new Label();
            lblApplicationDescription = new Label();
            lblApplicationEnglishName = new Label();
            lblApplicationName = new Label();
            picApplicationIcon = new PictureBox();
            pnlMain = new Panel();
            tableEditors = new TableLayoutPanel();
            pnlOutputCard = new Panel();
            txtOutput = new ModernTextEditor();
            pnlOutputFooter = new Panel();
            lblOutputStatistics = new Label();
            pnlOutputHeader = new Panel();
            btnCopy = new ModernButton();
            lblOutputDescription = new Label();
            lblOutputTitle = new Label();
            pnlOutputAccent = new Panel();
            pnlInputCard = new Panel();
            txtInput = new ModernTextEditor();
            pnlInputFooter = new Panel();
            lblInputStatistics = new Label();
            pnlInputHeader = new Panel();
            btnPaste = new ModernButton();
            lblInputDescription = new Label();
            lblInputTitle = new Label();
            pnlInputAccent = new Panel();
            pnlControlBar = new Panel();
            tableControlBar = new TableLayoutPanel();
            flowUtilityActions = new FlowLayoutPanel();
            btnSettings = new ModernButton();
            btnHistory = new ModernButton();
            flowMainActions = new FlowLayoutPanel();
            btnClear = new ModernButton();
            btnSwap = new ModernButton();
            btnConvert = new ModernButton();
            flowConversionOptions = new FlowLayoutPanel();
            chkInstantConversion = new CheckBox();
            cmbConversionMode = new ComboBox();
            lblConversionMode = new Label();
            pnlFooter = new Panel();
            lblShortcut = new Label();
            lblStatus = new Label();
            pnlStatusIndicator = new Panel();
            pnlHeader.SuspendLayout();
            pnlHeaderContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picApplicationIcon).BeginInit();
            pnlMain.SuspendLayout();
            tableEditors.SuspendLayout();
            pnlOutputCard.SuspendLayout();
            pnlOutputFooter.SuspendLayout();
            pnlOutputHeader.SuspendLayout();
            pnlInputCard.SuspendLayout();
            pnlInputFooter.SuspendLayout();
            pnlInputHeader.SuspendLayout();
            pnlControlBar.SuspendLayout();
            tableControlBar.SuspendLayout();
            flowUtilityActions.SuspendLayout();
            flowMainActions.SuspendLayout();
            flowConversionOptions.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(15, 23, 42);
            pnlHeader.Controls.Add(pnlHeaderContent);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(34, 24, 34, 20);
            pnlHeader.Size = new Size(1480, 164);
            pnlHeader.TabIndex = 0;
            // 
            // pnlHeaderContent
            // 
            pnlHeaderContent.BackColor = Color.FromArgb(30, 41, 59);
            pnlHeaderContent.Controls.Add(lblHeaderBadge);
            pnlHeaderContent.Controls.Add(lblApplicationDescription);
            pnlHeaderContent.Controls.Add(lblApplicationEnglishName);
            pnlHeaderContent.Controls.Add(lblApplicationName);
            pnlHeaderContent.Controls.Add(picApplicationIcon);
            pnlHeaderContent.Dock = DockStyle.Fill;
            pnlHeaderContent.Location = new Point(34, 24);
            pnlHeaderContent.Name = "pnlHeaderContent";
            pnlHeaderContent.Padding = new Padding(26, 18, 26, 18);
            pnlHeaderContent.Size = new Size(1412, 120);
            pnlHeaderContent.TabIndex = 0;
            // 
            // lblHeaderBadge
            // 
            lblHeaderBadge.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            lblHeaderBadge.BackColor = Color.FromArgb(30, 64, 175);
            lblHeaderBadge.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHeaderBadge.ForeColor = Color.FromArgb(219, 234, 254);
            lblHeaderBadge.Location = new Point(26, 25);
            lblHeaderBadge.Name = "lblHeaderBadge";
            lblHeaderBadge.Size = new Size(188, 34);
            lblHeaderBadge.TabIndex = 4;
            lblHeaderBadge.Text = "الإصدار 1.0.0  •  جاهز";
            lblHeaderBadge.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblApplicationDescription
            // 
            lblApplicationDescription.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblApplicationDescription.AutoSize = true;
            lblApplicationDescription.Font = new Font("Segoe UI", 10F);
            lblApplicationDescription.ForeColor = Color.FromArgb(203, 213, 225);
            lblApplicationDescription.Location = new Point(724, 79);
            lblApplicationDescription.Name = "lblApplicationDescription";
            lblApplicationDescription.Size = new Size(515, 28);
            lblApplicationDescription.TabIndex = 3;
            lblApplicationDescription.Text = "صحّح النص المكتوب بتخطيط لوحة مفاتيح خاطئ فورًا وبدقة";
            // 
            // lblApplicationEnglishName
            // 
            lblApplicationEnglishName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblApplicationEnglishName.AutoSize = true;
            lblApplicationEnglishName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblApplicationEnglishName.ForeColor = Color.FromArgb(96, 165, 250);
            lblApplicationEnglishName.Location = new Point(1081, 19);
            lblApplicationEnglishName.Name = "lblApplicationEnglishName";
            lblApplicationEnglishName.Size = new Size(158, 28);
            lblApplicationEnglishName.TabIndex = 2;
            lblApplicationEnglishName.Text = "MiftahFix";
            // 
            // lblApplicationName
            // 
            lblApplicationName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblApplicationName.AutoSize = true;
            lblApplicationName.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblApplicationName.ForeColor = Color.White;
            lblApplicationName.Location = new Point(885, 35);
            lblApplicationName.Name = "lblApplicationName";
            lblApplicationName.Size = new Size(354, 54);
            lblApplicationName.TabIndex = 1;
            lblApplicationName.Text = "مفتاح للتصحيح";
            // 
            // picApplicationIcon
            // 
            picApplicationIcon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            picApplicationIcon.BackColor = Color.FromArgb(37, 99, 235);
            picApplicationIcon.Location = new Point(1271, 18);
            picApplicationIcon.Name = "picApplicationIcon";
            picApplicationIcon.Padding = new Padding(10);
            picApplicationIcon.Size = new Size(94, 84);
            picApplicationIcon.SizeMode = PictureBoxSizeMode.Zoom;
            picApplicationIcon.TabIndex = 0;
            picApplicationIcon.TabStop = false;
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(241, 245, 249);
            pnlMain.Controls.Add(tableEditors);
            pnlMain.Controls.Add(pnlControlBar);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 164);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(34, 24, 34, 26);
            pnlMain.Size = new Size(1480, 762);
            pnlMain.TabIndex = 1;
            // 
            // tableEditors
            // 
            tableEditors.ColumnCount = 2;
            tableEditors.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableEditors.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableEditors.Controls.Add(pnlOutputCard, 0, 0);
            tableEditors.Controls.Add(pnlInputCard, 1, 0);
            tableEditors.Dock = DockStyle.Fill;
            tableEditors.Location = new Point(34, 142);
            tableEditors.Name = "tableEditors";
            tableEditors.Padding = new Padding(0, 18, 0, 0);
            tableEditors.RowCount = 1;
            tableEditors.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableEditors.Size = new Size(1412, 594);
            tableEditors.TabIndex = 1;
            // 
            // pnlOutputCard
            // 
            pnlOutputCard.BackColor = Color.White;
            pnlOutputCard.Controls.Add(txtOutput);
            pnlOutputCard.Controls.Add(pnlOutputFooter);
            pnlOutputCard.Controls.Add(pnlOutputHeader);
            pnlOutputCard.Controls.Add(pnlOutputAccent);
            pnlOutputCard.Dock = DockStyle.Fill;
            pnlOutputCard.Location = new Point(0, 21);
            pnlOutputCard.Margin = new Padding(0, 3, 12, 3);
            pnlOutputCard.Name = "pnlOutputCard";
            pnlOutputCard.Padding = new Padding(22, 0, 22, 0);
            pnlOutputCard.Size = new Size(694, 570);
            pnlOutputCard.TabIndex = 1;
            // 
            // txtOutput
            // 
            txtOutput.Dock = DockStyle.Fill;
            txtOutput.EditorFontSize = 14F;
            txtOutput.EditorTitle = "النص بعد التصحيح";
            txtOutput.Location = new Point(22, 112);
            txtOutput.Name = "txtOutput";
            txtOutput.PlaceholderText = "ستظهر النتيجة المصححة هنا...";
            txtOutput.ReadOnly = true;
            txtOutput.RightToLeft = RightToLeft.Yes;
            txtOutput.ShowOpenButton = false;
            txtOutput.ShowPasteButton = false;
            txtOutput.ShowClearButton = false;
            txtOutput.Size = new Size(650, 399);
            txtOutput.TabIndex = 1;
            txtOutput.KeyDown += TxtOutput_KeyDown;
            // 
            // pnlOutputFooter
            // 
            pnlOutputFooter.BackColor = Color.FromArgb(248, 250, 252);
            pnlOutputFooter.Controls.Add(lblOutputStatistics);
            pnlOutputFooter.Dock = DockStyle.Bottom;
            pnlOutputFooter.Location = new Point(22, 511);
            pnlOutputFooter.Name = "pnlOutputFooter";
            pnlOutputFooter.Padding = new Padding(16, 12, 16, 12);
            pnlOutputFooter.Size = new Size(650, 59);
            pnlOutputFooter.TabIndex = 2;
            // 
            // lblOutputStatistics
            // 
            lblOutputStatistics.AutoSize = true;
            lblOutputStatistics.Dock = DockStyle.Right;
            lblOutputStatistics.Font = new Font("Segoe UI", 9F);
            lblOutputStatistics.ForeColor = Color.FromArgb(100, 116, 139);
            lblOutputStatistics.Location = new Point(367, 12);
            lblOutputStatistics.Name = "lblOutputStatistics";
            lblOutputStatistics.Size = new Size(267, 25);
            lblOutputStatistics.TabIndex = 0;
            lblOutputStatistics.Text = "الأحرف: 0    الكلمات: 0    الأسطر: 0";
            // 
            // pnlOutputHeader
            // 
            pnlOutputHeader.BackColor = Color.White;
            pnlOutputHeader.Controls.Add(btnCopy);
            pnlOutputHeader.Controls.Add(lblOutputDescription);
            pnlOutputHeader.Controls.Add(lblOutputTitle);
            pnlOutputHeader.Dock = DockStyle.Top;
            pnlOutputHeader.Location = new Point(22, 5);
            pnlOutputHeader.Name = "pnlOutputHeader";
            pnlOutputHeader.Size = new Size(650, 107);
            pnlOutputHeader.TabIndex = 0;
            // 
            // btnCopy
            // 
            btnCopy.BackColor = Color.FromArgb(236, 253, 245);
            btnCopy.FlatAppearance.BorderSize = 0;
            btnCopy.FlatStyle = FlatStyle.Flat;
            btnCopy.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCopy.ForeColor = Color.FromArgb(4, 120, 87);
            btnCopy.Location = new Point(0, 28);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(124, 48);
            btnCopy.TabIndex = 2;
            btnCopy.Text = "نسخ النتيجة";
            btnCopy.UseVisualStyleBackColor = false;
            btnCopy.Click += BtnCopy_Click;
            // 
            // lblOutputDescription
            // 
            lblOutputDescription.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblOutputDescription.AutoSize = true;
            lblOutputDescription.Font = new Font("Segoe UI", 9F);
            lblOutputDescription.ForeColor = Color.FromArgb(100, 116, 139);
            lblOutputDescription.Location = new Point(340, 59);
            lblOutputDescription.Name = "lblOutputDescription";
            lblOutputDescription.Size = new Size(278, 25);
            lblOutputDescription.TabIndex = 1;
            lblOutputDescription.Text = "نتيجة التحويل تظهر مباشرة في هذا الحقل";
            // 
            // lblOutputTitle
            // 
            lblOutputTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblOutputTitle.AutoSize = true;
            lblOutputTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblOutputTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblOutputTitle.Location = new Point(463, 17);
            lblOutputTitle.Name = "lblOutputTitle";
            lblOutputTitle.Size = new Size(155, 32);
            lblOutputTitle.TabIndex = 0;
            lblOutputTitle.Text = "النص المصحح";
            // 
            // pnlOutputAccent
            // 
            pnlOutputAccent.BackColor = Color.FromArgb(16, 185, 129);
            pnlOutputAccent.Dock = DockStyle.Top;
            pnlOutputAccent.Location = new Point(22, 0);
            pnlOutputAccent.Name = "pnlOutputAccent";
            pnlOutputAccent.Size = new Size(650, 5);
            pnlOutputAccent.TabIndex = 3;
            // 
            // pnlInputCard
            // 
            pnlInputCard.BackColor = Color.White;
            pnlInputCard.Controls.Add(txtInput);
            pnlInputCard.Controls.Add(pnlInputFooter);
            pnlInputCard.Controls.Add(pnlInputHeader);
            pnlInputCard.Controls.Add(pnlInputAccent);
            pnlInputCard.Dock = DockStyle.Fill;
            pnlInputCard.Location = new Point(718, 21);
            pnlInputCard.Margin = new Padding(12, 3, 0, 3);
            pnlInputCard.Name = "pnlInputCard";
            pnlInputCard.Padding = new Padding(22, 0, 22, 0);
            pnlInputCard.Size = new Size(694, 570);
            pnlInputCard.TabIndex = 0;
            // 
            // txtInput
            // 
            txtInput.Dock = DockStyle.Fill;
            txtInput.EditorFontSize = 14F;
            txtInput.EditorTitle = "النص الأصلي";
            txtInput.Location = new Point(22, 112);
            txtInput.Name = "txtInput";
            txtInput.PlaceholderText = "اكتب النص هنا أو اسحب ملف TXT إلى المحرر...";
            txtInput.RightToLeft = RightToLeft.No;
            txtInput.ShowCopyButton = false;
            txtInput.Size = new Size(650, 399);
            txtInput.TabIndex = 1;
            txtInput.KeyDown += TxtInput_KeyDown;
            // 
            // pnlInputFooter
            // 
            pnlInputFooter.BackColor = Color.FromArgb(248, 250, 252);
            pnlInputFooter.Controls.Add(lblInputStatistics);
            pnlInputFooter.Dock = DockStyle.Bottom;
            pnlInputFooter.Location = new Point(22, 511);
            pnlInputFooter.Name = "pnlInputFooter";
            pnlInputFooter.Padding = new Padding(16, 12, 16, 12);
            pnlInputFooter.Size = new Size(650, 59);
            pnlInputFooter.TabIndex = 2;
            // 
            // lblInputStatistics
            // 
            lblInputStatistics.AutoSize = true;
            lblInputStatistics.Dock = DockStyle.Right;
            lblInputStatistics.Font = new Font("Segoe UI", 9F);
            lblInputStatistics.ForeColor = Color.FromArgb(100, 116, 139);
            lblInputStatistics.Location = new Point(367, 12);
            lblInputStatistics.Name = "lblInputStatistics";
            lblInputStatistics.Size = new Size(267, 25);
            lblInputStatistics.TabIndex = 0;
            lblInputStatistics.Text = "الأحرف: 0    الكلمات: 0    الأسطر: 0";
            // 
            // pnlInputHeader
            // 
            pnlInputHeader.BackColor = Color.White;
            pnlInputHeader.Controls.Add(btnPaste);
            pnlInputHeader.Controls.Add(lblInputDescription);
            pnlInputHeader.Controls.Add(lblInputTitle);
            pnlInputHeader.Dock = DockStyle.Top;
            pnlInputHeader.Location = new Point(22, 5);
            pnlInputHeader.Name = "pnlInputHeader";
            pnlInputHeader.Size = new Size(650, 107);
            pnlInputHeader.TabIndex = 0;
            // 
            // btnPaste
            // 
            btnPaste.BackColor = Color.FromArgb(239, 246, 255);
            btnPaste.FlatAppearance.BorderSize = 0;
            btnPaste.FlatStyle = FlatStyle.Flat;
            btnPaste.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnPaste.ForeColor = Color.FromArgb(29, 78, 216);
            btnPaste.Location = new Point(0, 28);
            btnPaste.Name = "btnPaste";
            btnPaste.Size = new Size(124, 48);
            btnPaste.TabIndex = 2;
            btnPaste.Text = "لصق النص";
            btnPaste.UseVisualStyleBackColor = false;
            btnPaste.Click += BtnPaste_Click;
            // 
            // lblInputDescription
            // 
            lblInputDescription.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblInputDescription.AutoSize = true;
            lblInputDescription.Font = new Font("Segoe UI", 9F);
            lblInputDescription.ForeColor = Color.FromArgb(100, 116, 139);
            lblInputDescription.Location = new Point(320, 59);
            lblInputDescription.Name = "lblInputDescription";
            lblInputDescription.Size = new Size(298, 25);
            lblInputDescription.TabIndex = 1;
            lblInputDescription.Text = "اكتب النص أو الصقه ثم اختر طريقة التحويل";
            // 
            // lblInputTitle
            // 
            lblInputTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblInputTitle.AutoSize = true;
            lblInputTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblInputTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblInputTitle.Location = new Point(471, 17);
            lblInputTitle.Name = "lblInputTitle";
            lblInputTitle.Size = new Size(147, 32);
            lblInputTitle.TabIndex = 0;
            lblInputTitle.Text = "النص الأصلي";
            // 
            // pnlInputAccent
            // 
            pnlInputAccent.BackColor = Color.FromArgb(37, 99, 235);
            pnlInputAccent.Dock = DockStyle.Top;
            pnlInputAccent.Location = new Point(22, 0);
            pnlInputAccent.Name = "pnlInputAccent";
            pnlInputAccent.Size = new Size(650, 5);
            pnlInputAccent.TabIndex = 3;
            // 
            // pnlControlBar
            // 
            pnlControlBar.BackColor = Color.White;
            pnlControlBar.Controls.Add(tableControlBar);
            pnlControlBar.Dock = DockStyle.Top;
            pnlControlBar.Location = new Point(34, 24);
            pnlControlBar.Name = "pnlControlBar";
            pnlControlBar.Padding = new Padding(18);
            pnlControlBar.Size = new Size(1412, 118);
            pnlControlBar.TabIndex = 0;
            // 
            // tableControlBar
            // 
            tableControlBar.ColumnCount = 3;
            tableControlBar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableControlBar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36F));
            tableControlBar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 39F));
            tableControlBar.Controls.Add(flowUtilityActions, 0, 0);
            tableControlBar.Controls.Add(flowMainActions, 1, 0);
            tableControlBar.Controls.Add(flowConversionOptions, 2, 0);
            tableControlBar.Dock = DockStyle.Fill;
            tableControlBar.Location = new Point(18, 18);
            tableControlBar.Name = "tableControlBar";
            tableControlBar.RowCount = 1;
            tableControlBar.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableControlBar.Size = new Size(1376, 82);
            tableControlBar.TabIndex = 0;
            // 
            // flowUtilityActions
            // 
            flowUtilityActions.Controls.Add(btnSettings);
            flowUtilityActions.Controls.Add(btnHistory);
            flowUtilityActions.Dock = DockStyle.Fill;
            flowUtilityActions.FlowDirection = FlowDirection.LeftToRight;
            flowUtilityActions.Location = new Point(0, 0);
            flowUtilityActions.Margin = new Padding(0);
            flowUtilityActions.Name = "flowUtilityActions";
            flowUtilityActions.Padding = new Padding(0, 12, 0, 0);
            flowUtilityActions.Size = new Size(344, 82);
            flowUtilityActions.TabIndex = 0;
            flowUtilityActions.WrapContents = false;
            // 
            // btnSettings
            // 
            btnSettings.BackColor = Color.FromArgb(238, 242, 255);
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSettings.ForeColor = Color.FromArgb(67, 56, 202);
            btnSettings.Location = new Point(3, 15);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(136, 52);
            btnSettings.TabIndex = 8;
            btnSettings.Text = "⚙  الإعدادات";
            btnSettings.UseVisualStyleBackColor = false;
            // 
            // btnHistory
            // 
            btnHistory.BackColor = Color.FromArgb(236, 253, 245);
            btnHistory.FlatAppearance.BorderSize = 0;
            btnHistory.FlatStyle = FlatStyle.Flat;
            btnHistory.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnHistory.ForeColor = Color.FromArgb(4, 120, 87);
            btnHistory.Location = new Point(145, 15);
            btnHistory.Name = "btnHistory";
            btnHistory.Size = new Size(126, 52);
            btnHistory.TabIndex = 7;
            btnHistory.Text = "◷  السجل";
            btnHistory.UseVisualStyleBackColor = false;
            // 
            // flowMainActions
            // 
            flowMainActions.Controls.Add(btnClear);
            flowMainActions.Controls.Add(btnSwap);
            flowMainActions.Controls.Add(btnConvert);
            flowMainActions.Dock = DockStyle.Fill;
            flowMainActions.FlowDirection = FlowDirection.LeftToRight;
            flowMainActions.Location = new Point(344, 0);
            flowMainActions.Margin = new Padding(0);
            flowMainActions.Name = "flowMainActions";
            flowMainActions.Padding = new Padding(0, 12, 0, 0);
            flowMainActions.Size = new Size(495, 82);
            flowMainActions.TabIndex = 1;
            flowMainActions.WrapContents = false;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(254, 242, 242);
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnClear.ForeColor = Color.FromArgb(185, 28, 28);
            btnClear.Location = new Point(3, 15);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(118, 52);
            btnClear.TabIndex = 5;
            btnClear.Text = "مسح الكل";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += BtnClear_Click;
            // 
            // btnSwap
            // 
            btnSwap.BackColor = Color.FromArgb(241, 245, 249);
            btnSwap.FlatAppearance.BorderSize = 0;
            btnSwap.FlatStyle = FlatStyle.Flat;
            btnSwap.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSwap.ForeColor = Color.FromArgb(51, 65, 85);
            btnSwap.Location = new Point(127, 15);
            btnSwap.Name = "btnSwap";
            btnSwap.Size = new Size(137, 52);
            btnSwap.TabIndex = 4;
            btnSwap.Text = "⇄  تبديل";
            btnSwap.UseVisualStyleBackColor = false;
            btnSwap.Click += BtnSwap_Click;
            // 
            // btnConvert
            // 
            btnConvert.BackColor = Color.FromArgb(37, 99, 235);
            btnConvert.FlatAppearance.BorderSize = 0;
            btnConvert.FlatStyle = FlatStyle.Flat;
            btnConvert.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnConvert.ForeColor = Color.White;
            btnConvert.Location = new Point(270, 15);
            btnConvert.Name = "btnConvert";
            btnConvert.Size = new Size(164, 52);
            btnConvert.TabIndex = 3;
            btnConvert.Text = "تحويل النص  ←";
            btnConvert.UseVisualStyleBackColor = false;
            btnConvert.Click += BtnConvert_Click;
            // 
            // flowConversionOptions
            // 
            flowConversionOptions.Controls.Add(chkInstantConversion);
            flowConversionOptions.Controls.Add(cmbConversionMode);
            flowConversionOptions.Controls.Add(lblConversionMode);
            flowConversionOptions.Dock = DockStyle.Fill;
            flowConversionOptions.FlowDirection = FlowDirection.LeftToRight;
            flowConversionOptions.Location = new Point(839, 0);
            flowConversionOptions.Margin = new Padding(0);
            flowConversionOptions.Name = "flowConversionOptions";
            flowConversionOptions.Padding = new Padding(6, 13, 0, 0);
            flowConversionOptions.RightToLeft = RightToLeft.No;
            flowConversionOptions.Size = new Size(537, 82);
            flowConversionOptions.TabIndex = 2;
            flowConversionOptions.WrapContents = false;
            // 
            // chkInstantConversion
            // 
            chkInstantConversion.AutoSize = true;
            chkInstantConversion.Checked = true;
            chkInstantConversion.CheckState = CheckState.Checked;
            chkInstantConversion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            chkInstantConversion.ForeColor = Color.FromArgb(51, 65, 85);
            chkInstantConversion.Location = new Point(9, 26);
            chkInstantConversion.Margin = new Padding(3, 13, 12, 3);
            chkInstantConversion.Name = "chkInstantConversion";
            chkInstantConversion.RightToLeft = RightToLeft.Yes;
            chkInstantConversion.Size = new Size(188, 29);
            chkInstantConversion.TabIndex = 6;
            chkInstantConversion.Text = "تحويل فوري";
            chkInstantConversion.UseVisualStyleBackColor = true;
            // 
            // cmbConversionMode
            // 
            cmbConversionMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbConversionMode.Font = new Font("Segoe UI", 10F);
            cmbConversionMode.FormattingEnabled = true;
            cmbConversionMode.Items.AddRange(new object[] { "اكتشاف تلقائي", "إنجليزي إلى عربي", "عربي إلى إنجليزي" });
            cmbConversionMode.Location = new Point(212, 20);
            cmbConversionMode.Margin = new Padding(3, 7, 10, 3);
            cmbConversionMode.Name = "cmbConversionMode";
            cmbConversionMode.RightToLeft = RightToLeft.Yes;
            cmbConversionMode.Size = new Size(190, 36);
            cmbConversionMode.TabIndex = 2;
            // 
            // lblConversionMode
            // 
            lblConversionMode.AutoSize = true;
            lblConversionMode.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblConversionMode.ForeColor = Color.FromArgb(51, 65, 85);
            lblConversionMode.Location = new Point(415, 24);
            lblConversionMode.Margin = new Padding(3, 11, 3, 0);
            lblConversionMode.Name = "lblConversionMode";
            lblConversionMode.Size = new Size(116, 28);
            lblConversionMode.TabIndex = 0;
            lblConversionMode.Text = "طريقة التحويل";
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.White;
            pnlFooter.Controls.Add(lblShortcut);
            pnlFooter.Controls.Add(lblStatus);
            pnlFooter.Controls.Add(pnlStatusIndicator);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 926);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(34, 16, 34, 14);
            pnlFooter.Size = new Size(1480, 60);
            pnlFooter.TabIndex = 2;
            // 
            // lblShortcut
            // 
            lblShortcut.AutoSize = true;
            lblShortcut.Dock = DockStyle.Left;
            lblShortcut.Font = new Font("Segoe UI", 9F);
            lblShortcut.ForeColor = Color.FromArgb(100, 116, 139);
            lblShortcut.Location = new Point(34, 16);
            lblShortcut.Name = "lblShortcut";
            lblShortcut.Size = new Size(311, 25);
            lblShortcut.TabIndex = 2;
            lblShortcut.Text = "اختصار التحويل داخل الأداة: Ctrl + Enter";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Dock = DockStyle.Right;
            lblStatus.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(71, 85, 105);
            lblStatus.Location = new Point(1318, 16);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(104, 28);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "جاهز للتحويل";
            // 
            // pnlStatusIndicator
            // 
            pnlStatusIndicator.BackColor = Color.FromArgb(34, 197, 94);
            pnlStatusIndicator.Dock = DockStyle.Right;
            pnlStatusIndicator.Location = new Point(1422, 16);
            pnlStatusIndicator.Margin = new Padding(10, 0, 0, 0);
            pnlStatusIndicator.Name = "pnlStatusIndicator";
            pnlStatusIndicator.Size = new Size(24, 30);
            pnlStatusIndicator.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(1480, 986);
            Controls.Add(pnlMain);
            Controls.Add(pnlFooter);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(1240, 820);
            Name = "Form1";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "مفتاح للتصحيح - MiftahFix";
            WindowState = FormWindowState.Maximized;
            pnlHeader.ResumeLayout(false);
            pnlHeaderContent.ResumeLayout(false);
            pnlHeaderContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picApplicationIcon).EndInit();
            pnlMain.ResumeLayout(false);
            tableEditors.ResumeLayout(false);
            pnlOutputCard.ResumeLayout(false);
            pnlOutputCard.PerformLayout();
            pnlOutputFooter.ResumeLayout(false);
            pnlOutputFooter.PerformLayout();
            pnlOutputHeader.ResumeLayout(false);
            pnlOutputHeader.PerformLayout();
            pnlInputCard.ResumeLayout(false);
            pnlInputCard.PerformLayout();
            pnlInputFooter.ResumeLayout(false);
            pnlInputFooter.PerformLayout();
            pnlInputHeader.ResumeLayout(false);
            pnlInputHeader.PerformLayout();
            pnlControlBar.ResumeLayout(false);
            tableControlBar.ResumeLayout(false);
            flowUtilityActions.ResumeLayout(false);
            flowMainActions.ResumeLayout(false);
            flowConversionOptions.ResumeLayout(false);
            flowConversionOptions.PerformLayout();
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}
