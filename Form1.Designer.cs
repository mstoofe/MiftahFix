namespace MiftahFix
{
    partial class Form1
    {
        /// <summary>
        /// متغير المصمم المطلوب.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblApplicationName;
        private System.Windows.Forms.Label lblApplicationDescription;

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlControlBar;
        private System.Windows.Forms.Label lblConversionMode;
        private System.Windows.Forms.ComboBox cmbConversionMode;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnSwap;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox chkInstantConversion;

        private System.Windows.Forms.TableLayoutPanel tableEditors;

        private System.Windows.Forms.Panel pnlInputCard;
        private System.Windows.Forms.Panel pnlInputHeader;
        private System.Windows.Forms.Label lblInputTitle;
        private System.Windows.Forms.Label lblInputDescription;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Panel pnlInputFooter;
        private System.Windows.Forms.Label lblInputStatistics;

        private System.Windows.Forms.Panel pnlOutputCard;
        private System.Windows.Forms.Panel pnlOutputHeader;
        private System.Windows.Forms.Label lblOutputTitle;
        private System.Windows.Forms.Label lblOutputDescription;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Panel pnlOutputFooter;
        private System.Windows.Forms.Label lblOutputStatistics;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblShortcut;

        /// <summary>
        /// تنظيف الموارد المستخدمة.
        /// </summary>
        /// <param name="disposing">
        /// true للتخلص من الموارد المُدارة، وإلا false.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// الطريقة المطلوبة لدعم المصمم.
        /// </summary>
        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblApplicationDescription = new Label();
            lblApplicationName = new Label();
            lblLogo = new Label();
            pnlMain = new Panel();
            tableEditors = new TableLayoutPanel();
            pnlOutputCard = new Panel();
            txtOutput = new TextBox();
            pnlOutputFooter = new Panel();
            lblOutputStatistics = new Label();
            pnlOutputHeader = new Panel();
            btnCopy = new Button();
            lblOutputDescription = new Label();
            lblOutputTitle = new Label();
            pnlInputCard = new Panel();
            txtInput = new TextBox();
            pnlInputFooter = new Panel();
            lblInputStatistics = new Label();
            pnlInputHeader = new Panel();
            btnPaste = new Button();
            lblInputDescription = new Label();
            lblInputTitle = new Label();
            pnlControlBar = new Panel();
            chkInstantConversion = new CheckBox();
            btnClear = new Button();
            btnSwap = new Button();
            btnConvert = new Button();
            cmbConversionMode = new ComboBox();
            lblConversionMode = new Label();
            pnlFooter = new Panel();
            lblShortcut = new Label();
            lblStatus = new Label();
            pnlHeader.SuspendLayout();
            pnlMain.SuspendLayout();
            tableEditors.SuspendLayout();
            pnlOutputCard.SuspendLayout();
            pnlOutputFooter.SuspendLayout();
            pnlOutputHeader.SuspendLayout();
            pnlInputCard.SuspendLayout();
            pnlInputFooter.SuspendLayout();
            pnlInputHeader.SuspendLayout();
            pnlControlBar.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(30, 41, 59);
            pnlHeader.Controls.Add(lblApplicationDescription);
            pnlHeader.Controls.Add(lblApplicationName);
            pnlHeader.Controls.Add(lblLogo);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(4);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(35, 25, 35, 22);
            pnlHeader.Size = new Size(1480, 152);
            pnlHeader.TabIndex = 0;
            // 
            // lblApplicationDescription
            // 
            lblApplicationDescription.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblApplicationDescription.AutoSize = true;
            lblApplicationDescription.Font = new Font("Segoe UI", 10.5F);
            lblApplicationDescription.ForeColor = Color.FromArgb(203, 213, 225);
            lblApplicationDescription.Location = new Point(705, 91);
            lblApplicationDescription.Margin = new Padding(4, 0, 4, 0);
            lblApplicationDescription.Name = "lblApplicationDescription";
            lblApplicationDescription.Size = new Size(515, 30);
            lblApplicationDescription.TabIndex = 2;
            lblApplicationDescription.Text = "تحويل النص المكتوب بلغة لوحة مفاتيح خاطئة بسرعة ودقة";
            // 
            // lblApplicationName
            // 
            lblApplicationName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblApplicationName.AutoSize = true;
            lblApplicationName.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblApplicationName.ForeColor = Color.White;
            lblApplicationName.Location = new Point(1028, 28);
            lblApplicationName.Margin = new Padding(4, 0, 4, 0);
            lblApplicationName.Name = "lblApplicationName";
            lblApplicationName.Size = new Size(293, 60);
            lblApplicationName.TabIndex = 1;
            lblApplicationName.Text = "مفتاح للتصحيح";
            // 
            // lblLogo
            // 
            lblLogo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblLogo.BackColor = Color.FromArgb(59, 130, 246);
            lblLogo.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(1350, 30);
            lblLogo.Margin = new Padding(4, 0, 4, 0);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(90, 90);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "م";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(241, 245, 249);
            pnlMain.Controls.Add(tableEditors);
            pnlMain.Controls.Add(pnlControlBar);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 152);
            pnlMain.Margin = new Padding(4);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(35, 28, 35, 28);
            pnlMain.Size = new Size(1480, 774);
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
            tableEditors.Location = new Point(35, 130);
            tableEditors.Margin = new Padding(4);
            tableEditors.Name = "tableEditors";
            tableEditors.Padding = new Padding(0, 22, 0, 0);
            tableEditors.RowCount = 1;
            tableEditors.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableEditors.Size = new Size(1410, 616);
            tableEditors.TabIndex = 1;
            // 
            // pnlOutputCard
            // 
            pnlOutputCard.BackColor = Color.White;
            pnlOutputCard.Controls.Add(txtOutput);
            pnlOutputCard.Controls.Add(pnlOutputFooter);
            pnlOutputCard.Controls.Add(pnlOutputHeader);
            pnlOutputCard.Dock = DockStyle.Fill;
            pnlOutputCard.Location = new Point(724, 26);
            pnlOutputCard.Margin = new Padding(0, 4, 19, 4);
            pnlOutputCard.Name = "pnlOutputCard";
            pnlOutputCard.Padding = new Padding(1);
            pnlOutputCard.Size = new Size(686, 586);
            pnlOutputCard.TabIndex = 1;
            // 
            // txtOutput
            // 
            txtOutput.AcceptsReturn = true;
            txtOutput.BackColor = Color.White;
            txtOutput.BorderStyle = BorderStyle.None;
            txtOutput.Dock = DockStyle.Fill;
            txtOutput.Font = new Font("Segoe UI", 14F);
            txtOutput.ForeColor = Color.FromArgb(15, 23, 42);
            txtOutput.Location = new Point(1, 106);
            txtOutput.Margin = new Padding(4);
            txtOutput.Multiline = true;
            txtOutput.Name = "txtOutput";
            txtOutput.PlaceholderText = "ستظهر النتيجة هنا...";
            txtOutput.ReadOnly = true;
            txtOutput.RightToLeft = RightToLeft.Yes;
            txtOutput.ScrollBars = ScrollBars.Vertical;
            txtOutput.Size = new Size(684, 419);
            txtOutput.TabIndex = 1;
            txtOutput.KeyDown += TxtOutput_KeyDown;
            // 
            // pnlOutputFooter
            // 
            pnlOutputFooter.BackColor = Color.FromArgb(248, 250, 252);
            pnlOutputFooter.Controls.Add(lblOutputStatistics);
            pnlOutputFooter.Dock = DockStyle.Bottom;
            pnlOutputFooter.Location = new Point(1, 525);
            pnlOutputFooter.Margin = new Padding(4);
            pnlOutputFooter.Name = "pnlOutputFooter";
            pnlOutputFooter.Padding = new Padding(20, 12, 20, 12);
            pnlOutputFooter.Size = new Size(684, 60);
            pnlOutputFooter.TabIndex = 2;
            // 
            // lblOutputStatistics
            // 
            lblOutputStatistics.AutoSize = true;
            lblOutputStatistics.Dock = DockStyle.Right;
            lblOutputStatistics.Font = new Font("Segoe UI", 9F);
            lblOutputStatistics.ForeColor = Color.FromArgb(100, 116, 139);
            lblOutputStatistics.Location = new Point(397, 12);
            lblOutputStatistics.Margin = new Padding(4, 0, 4, 0);
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
            pnlOutputHeader.Location = new Point(1, 1);
            pnlOutputHeader.Margin = new Padding(4);
            pnlOutputHeader.Name = "pnlOutputHeader";
            pnlOutputHeader.Padding = new Padding(22);
            pnlOutputHeader.Size = new Size(684, 105);
            pnlOutputHeader.TabIndex = 0;
            // 
            // btnCopy
            // 
            btnCopy.BackColor = Color.FromArgb(236, 253, 245);
            btnCopy.Cursor = Cursors.Hand;
            btnCopy.FlatAppearance.BorderSize = 0;
            btnCopy.FlatStyle = FlatStyle.Flat;
            btnCopy.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnCopy.ForeColor = Color.FromArgb(4, 120, 87);
            btnCopy.Location = new Point(22, 28);
            btnCopy.Margin = new Padding(4);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(130, 50);
            btnCopy.TabIndex = 2;
            btnCopy.Text = "نسخ";
            btnCopy.UseVisualStyleBackColor = false;
            btnCopy.Click += BtnCopy_Click;
            // 
            // lblOutputDescription
            // 
            lblOutputDescription.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblOutputDescription.AutoSize = true;
            lblOutputDescription.Font = new Font("Segoe UI", 9F);
            lblOutputDescription.ForeColor = Color.FromArgb(100, 116, 139);
            lblOutputDescription.Location = new Point(346, 56);
            lblOutputDescription.Margin = new Padding(4, 0, 4, 0);
            lblOutputDescription.Name = "lblOutputDescription";
            lblOutputDescription.Size = new Size(278, 25);
            lblOutputDescription.TabIndex = 1;
            lblOutputDescription.Text = "ستظهر نتيجة التحويل في هذا الحقل";
            // 
            // lblOutputTitle
            // 
            lblOutputTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblOutputTitle.AutoSize = true;
            lblOutputTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblOutputTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblOutputTitle.Location = new Point(485, 16);
            lblOutputTitle.Margin = new Padding(4, 0, 4, 0);
            lblOutputTitle.Name = "lblOutputTitle";
            lblOutputTitle.Size = new Size(155, 32);
            lblOutputTitle.TabIndex = 0;
            lblOutputTitle.Text = "النص المصحح";
            // 
            // pnlInputCard
            // 
            pnlInputCard.BackColor = Color.White;
            pnlInputCard.Controls.Add(txtInput);
            pnlInputCard.Controls.Add(pnlInputFooter);
            pnlInputCard.Controls.Add(pnlInputHeader);
            pnlInputCard.Dock = DockStyle.Fill;
            pnlInputCard.Location = new Point(0, 26);
            pnlInputCard.Margin = new Padding(19, 4, 0, 4);
            pnlInputCard.Name = "pnlInputCard";
            pnlInputCard.Padding = new Padding(1);
            pnlInputCard.Size = new Size(686, 586);
            pnlInputCard.TabIndex = 0;
            // 
            // txtInput
            // 
            txtInput.AcceptsReturn = true;
            txtInput.AcceptsTab = true;
            txtInput.BackColor = Color.White;
            txtInput.BorderStyle = BorderStyle.None;
            txtInput.Dock = DockStyle.Fill;
            txtInput.Font = new Font("Segoe UI", 14F);
            txtInput.ForeColor = Color.FromArgb(15, 23, 42);
            txtInput.Location = new Point(1, 106);
            txtInput.Margin = new Padding(4);
            txtInput.Multiline = true;
            txtInput.Name = "txtInput";
            txtInput.PlaceholderText = "مثال: hgsghl ugd;l";
            txtInput.RightToLeft = RightToLeft.No;
            txtInput.ScrollBars = ScrollBars.Vertical;
            txtInput.Size = new Size(684, 419);
            txtInput.TabIndex = 1;
            txtInput.KeyDown += TxtInput_KeyDown;
            // 
            // pnlInputFooter
            // 
            pnlInputFooter.BackColor = Color.FromArgb(248, 250, 252);
            pnlInputFooter.Controls.Add(lblInputStatistics);
            pnlInputFooter.Dock = DockStyle.Bottom;
            pnlInputFooter.Location = new Point(1, 525);
            pnlInputFooter.Margin = new Padding(4);
            pnlInputFooter.Name = "pnlInputFooter";
            pnlInputFooter.Padding = new Padding(20, 12, 20, 12);
            pnlInputFooter.Size = new Size(684, 60);
            pnlInputFooter.TabIndex = 2;
            // 
            // lblInputStatistics
            // 
            lblInputStatistics.AutoSize = true;
            lblInputStatistics.Dock = DockStyle.Right;
            lblInputStatistics.Font = new Font("Segoe UI", 9F);
            lblInputStatistics.ForeColor = Color.FromArgb(100, 116, 139);
            lblInputStatistics.Location = new Point(397, 12);
            lblInputStatistics.Margin = new Padding(4, 0, 4, 0);
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
            pnlInputHeader.Location = new Point(1, 1);
            pnlInputHeader.Margin = new Padding(4);
            pnlInputHeader.Name = "pnlInputHeader";
            pnlInputHeader.Padding = new Padding(22);
            pnlInputHeader.Size = new Size(684, 105);
            pnlInputHeader.TabIndex = 0;
            // 
            // btnPaste
            // 
            btnPaste.BackColor = Color.FromArgb(239, 246, 255);
            btnPaste.Cursor = Cursors.Hand;
            btnPaste.FlatAppearance.BorderSize = 0;
            btnPaste.FlatStyle = FlatStyle.Flat;
            btnPaste.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnPaste.ForeColor = Color.FromArgb(29, 78, 216);
            btnPaste.Location = new Point(22, 28);
            btnPaste.Margin = new Padding(4);
            btnPaste.Name = "btnPaste";
            btnPaste.Size = new Size(130, 50);
            btnPaste.TabIndex = 2;
            btnPaste.Text = "لصق";
            btnPaste.UseVisualStyleBackColor = false;
            btnPaste.Click += BtnPaste_Click;
            // 
            // lblInputDescription
            // 
            lblInputDescription.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblInputDescription.AutoSize = true;
            lblInputDescription.Font = new Font("Segoe UI", 9F);
            lblInputDescription.ForeColor = Color.FromArgb(100, 116, 139);
            lblInputDescription.Location = new Point(315, 56);
            lblInputDescription.Margin = new Padding(4, 0, 4, 0);
            lblInputDescription.Name = "lblInputDescription";
            lblInputDescription.Size = new Size(303, 25);
            lblInputDescription.TabIndex = 1;
            lblInputDescription.Text = "الصق أو اكتب النص الذي تريد تصحيحه";
            // 
            // lblInputTitle
            // 
            lblInputTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblInputTitle.AutoSize = true;
            lblInputTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblInputTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblInputTitle.Location = new Point(470, 16);
            lblInputTitle.Margin = new Padding(4, 0, 4, 0);
            lblInputTitle.Name = "lblInputTitle";
            lblInputTitle.Size = new Size(147, 32);
            lblInputTitle.TabIndex = 0;
            lblInputTitle.Text = "النص الأصلي";
            // 
            // pnlControlBar
            // 
            pnlControlBar.BackColor = Color.White;
            pnlControlBar.Controls.Add(chkInstantConversion);
            pnlControlBar.Controls.Add(btnClear);
            pnlControlBar.Controls.Add(btnSwap);
            pnlControlBar.Controls.Add(btnConvert);
            pnlControlBar.Controls.Add(cmbConversionMode);
            pnlControlBar.Controls.Add(lblConversionMode);
            pnlControlBar.Dock = DockStyle.Top;
            pnlControlBar.Location = new Point(35, 28);
            pnlControlBar.Margin = new Padding(4);
            pnlControlBar.Name = "pnlControlBar";
            pnlControlBar.Padding = new Padding(22);
            pnlControlBar.Size = new Size(1410, 102);
            pnlControlBar.TabIndex = 0;
            // 
            // chkInstantConversion
            // 
            chkInstantConversion.AutoSize = true;
            chkInstantConversion.Checked = true;
            chkInstantConversion.CheckState = CheckState.Checked;
            chkInstantConversion.Cursor = Cursors.Hand;
            chkInstantConversion.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            chkInstantConversion.ForeColor = Color.FromArgb(51, 65, 85);
            chkInstantConversion.Location = new Point(22, 35);
            chkInstantConversion.Margin = new Padding(4);
            chkInstantConversion.Name = "chkInstantConversion";
            chkInstantConversion.RightToLeft = RightToLeft.Yes;
            chkInstantConversion.Size = new Size(219, 29);
            chkInstantConversion.TabIndex = 5;
            chkInstantConversion.Text = "تحويل فوري أثناء الكتابة";
            chkInstantConversion.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClear.BackColor = Color.FromArgb(254, 226, 226);
            btnClear.Cursor = Cursors.Hand;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClear.ForeColor = Color.FromArgb(185, 28, 28);
            btnClear.Location = new Point(265, 24);
            btnClear.Margin = new Padding(4);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(181, 55);
            btnClear.TabIndex = 4;
            btnClear.Text = "مسح الكل";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += BtnClear_Click;
            // 
            // btnSwap
            // 
            btnSwap.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSwap.BackColor = Color.FromArgb(226, 232, 240);
            btnSwap.Cursor = Cursors.Hand;
            btnSwap.FlatAppearance.BorderSize = 0;
            btnSwap.FlatStyle = FlatStyle.Flat;
            btnSwap.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSwap.ForeColor = Color.FromArgb(51, 65, 85);
            btnSwap.Location = new Point(468, 24);
            btnSwap.Margin = new Padding(4);
            btnSwap.Name = "btnSwap";
            btnSwap.Size = new Size(181, 55);
            btnSwap.TabIndex = 3;
            btnSwap.Text = "تبديل النصين";
            btnSwap.UseVisualStyleBackColor = false;
            btnSwap.Click += BtnSwap_Click;
            // 
            // btnConvert
            // 
            btnConvert.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnConvert.BackColor = Color.FromArgb(37, 99, 235);
            btnConvert.Cursor = Cursors.Hand;
            btnConvert.FlatAppearance.BorderSize = 0;
            btnConvert.FlatStyle = FlatStyle.Flat;
            btnConvert.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnConvert.ForeColor = Color.White;
            btnConvert.Location = new Point(670, 24);
            btnConvert.Margin = new Padding(4);
            btnConvert.Name = "btnConvert";
            btnConvert.Size = new Size(238, 55);
            btnConvert.TabIndex = 2;
            btnConvert.Text = "تحويل النص";
            btnConvert.UseVisualStyleBackColor = false;
            btnConvert.Click += BtnConvert_Click;
            // 
            // cmbConversionMode
            // 
            cmbConversionMode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbConversionMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbConversionMode.Font = new Font("Segoe UI", 10.5F);
            cmbConversionMode.FormattingEnabled = true;
            cmbConversionMode.Items.AddRange(new object[] { "اكتشاف تلقائي", "إنجليزي إلى عربي", "عربي إلى إنجليزي" });
            cmbConversionMode.Location = new Point(935, 30);
            cmbConversionMode.Margin = new Padding(4);
            cmbConversionMode.Name = "cmbConversionMode";
            cmbConversionMode.RightToLeft = RightToLeft.Yes;
            cmbConversionMode.Size = new Size(274, 38);
            cmbConversionMode.TabIndex = 1;
            // 
            // lblConversionMode
            // 
            lblConversionMode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblConversionMode.AutoSize = true;
            lblConversionMode.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblConversionMode.ForeColor = Color.FromArgb(51, 65, 85);
            lblConversionMode.Location = new Point(1230, 35);
            lblConversionMode.Margin = new Padding(4, 0, 4, 0);
            lblConversionMode.Name = "lblConversionMode";
            lblConversionMode.Size = new Size(148, 30);
            lblConversionMode.TabIndex = 0;
            lblConversionMode.Text = "طريقة التحويل:";
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.White;
            pnlFooter.Controls.Add(lblShortcut);
            pnlFooter.Controls.Add(lblStatus);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 926);
            pnlFooter.Margin = new Padding(4);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(35, 15, 35, 15);
            pnlFooter.Size = new Size(1480, 60);
            pnlFooter.TabIndex = 2;
            // 
            // lblShortcut
            // 
            lblShortcut.AutoSize = true;
            lblShortcut.Dock = DockStyle.Left;
            lblShortcut.Font = new Font("Segoe UI", 9F);
            lblShortcut.ForeColor = Color.FromArgb(100, 116, 139);
            lblShortcut.Location = new Point(35, 15);
            lblShortcut.Margin = new Padding(4, 0, 4, 0);
            lblShortcut.Name = "lblShortcut";
            lblShortcut.Size = new Size(311, 25);
            lblShortcut.TabIndex = 1;
            lblShortcut.Text = "اختصار التحويل داخل الأداة: Ctrl + Enter";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Dock = DockStyle.Right;
            lblStatus.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(95, 99, 104);
            lblStatus.Location = new Point(1331, 15);
            lblStatus.Margin = new Padding(4, 0, 4, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(114, 25);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "جاهز للتحويل";
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
            Margin = new Padding(4);
            MinimumSize = new Size(1244, 861);
            Name = "Form1";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "مفتاح للتصحيح - MiftahFix";
            WindowState = FormWindowState.Maximized;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
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
            pnlControlBar.PerformLayout();
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}