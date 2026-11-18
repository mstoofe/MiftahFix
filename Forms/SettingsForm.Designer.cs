namespace MiftahFix.Forms
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TabControl tabSettings;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabHotkey;
        private System.Windows.Forms.TabPage tabConversion;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.TabPage tabAdvanced;
        private System.Windows.Forms.FlowLayoutPanel bottomButtonsPanel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRestoreDefaults;

        private System.Windows.Forms.TableLayoutPanel generalLayout;
        private System.Windows.Forms.CheckBox chkRunWithWindows;
        private System.Windows.Forms.CheckBox chkStartMinimized;
        private System.Windows.Forms.CheckBox chkShowStartupNotification;

        private System.Windows.Forms.TableLayoutPanel hotkeyLayout;
        private System.Windows.Forms.Label lblHotkeyExplanation;
        private System.Windows.Forms.FlowLayoutPanel hotkeyModifiersPanel;
        private System.Windows.Forms.CheckBox chkHotkeyControl;
        private System.Windows.Forms.CheckBox chkHotkeyShift;
        private System.Windows.Forms.CheckBox chkHotkeyAlt;
        private System.Windows.Forms.CheckBox chkHotkeyWindows;
        private System.Windows.Forms.Label lblHotkeyKey;
        private System.Windows.Forms.ComboBox cmbHotkeyKey;

        private System.Windows.Forms.TableLayoutPanel conversionLayout;
        private System.Windows.Forms.CheckBox chkInstantConversion;
        private System.Windows.Forms.Label lblConversionMode;
        private System.Windows.Forms.ComboBox cmbConversionMode;

        private System.Windows.Forms.TableLayoutPanel historyLayout;
        private System.Windows.Forms.CheckBox chkEnableHistory;
        private System.Windows.Forms.Label lblHistoryMaxItems;
        private System.Windows.Forms.NumericUpDown nudHistoryMaxItems;
        private System.Windows.Forms.Label lblHistoryNote;

        private System.Windows.Forms.TableLayoutPanel advancedLayout;
        private System.Windows.Forms.CheckBox chkCheckForUpdates;
        private System.Windows.Forms.Label lblAdvancedNote;

        protected override void Dispose(
            bool disposing)
        {
            if (disposing &&
                components != null)
            {
                components.Dispose();
            }

            base.Dispose(
                disposing);
        }

        private void InitializeComponent()
        {
            components =
                new System.ComponentModel.Container();

            rootLayout =
                new System.Windows.Forms.TableLayoutPanel();

            lblTitle =
                new System.Windows.Forms.Label();

            lblDescription =
                new System.Windows.Forms.Label();

            tabSettings =
                new System.Windows.Forms.TabControl();

            tabGeneral =
                new System.Windows.Forms.TabPage();

            tabHotkey =
                new System.Windows.Forms.TabPage();

            tabConversion =
                new System.Windows.Forms.TabPage();

            tabHistory =
                new System.Windows.Forms.TabPage();

            tabAdvanced =
                new System.Windows.Forms.TabPage();

            bottomButtonsPanel =
                new System.Windows.Forms.FlowLayoutPanel();

            btnSave =
                new System.Windows.Forms.Button();

            btnCancel =
                new System.Windows.Forms.Button();

            btnRestoreDefaults =
                new System.Windows.Forms.Button();

            generalLayout =
                new System.Windows.Forms.TableLayoutPanel();

            chkRunWithWindows =
                new System.Windows.Forms.CheckBox();

            chkStartMinimized =
                new System.Windows.Forms.CheckBox();

            chkShowStartupNotification =
                new System.Windows.Forms.CheckBox();

            hotkeyLayout =
                new System.Windows.Forms.TableLayoutPanel();

            lblHotkeyExplanation =
                new System.Windows.Forms.Label();

            hotkeyModifiersPanel =
                new System.Windows.Forms.FlowLayoutPanel();

            chkHotkeyControl =
                new System.Windows.Forms.CheckBox();

            chkHotkeyShift =
                new System.Windows.Forms.CheckBox();

            chkHotkeyAlt =
                new System.Windows.Forms.CheckBox();

            chkHotkeyWindows =
                new System.Windows.Forms.CheckBox();

            lblHotkeyKey =
                new System.Windows.Forms.Label();

            cmbHotkeyKey =
                new System.Windows.Forms.ComboBox();

            conversionLayout =
                new System.Windows.Forms.TableLayoutPanel();

            chkInstantConversion =
                new System.Windows.Forms.CheckBox();

            lblConversionMode =
                new System.Windows.Forms.Label();

            cmbConversionMode =
                new System.Windows.Forms.ComboBox();

            historyLayout =
                new System.Windows.Forms.TableLayoutPanel();

            chkEnableHistory =
                new System.Windows.Forms.CheckBox();

            lblHistoryMaxItems =
                new System.Windows.Forms.Label();

            nudHistoryMaxItems =
                new System.Windows.Forms.NumericUpDown();

            lblHistoryNote =
                new System.Windows.Forms.Label();

            advancedLayout =
                new System.Windows.Forms.TableLayoutPanel();

            chkCheckForUpdates =
                new System.Windows.Forms.CheckBox();

            lblAdvancedNote =
                new System.Windows.Forms.Label();

            rootLayout.SuspendLayout();
            tabSettings.SuspendLayout();
            tabGeneral.SuspendLayout();
            tabHotkey.SuspendLayout();
            tabConversion.SuspendLayout();
            tabHistory.SuspendLayout();
            tabAdvanced.SuspendLayout();
            bottomButtonsPanel.SuspendLayout();
            generalLayout.SuspendLayout();
            hotkeyLayout.SuspendLayout();
            hotkeyModifiersPanel.SuspendLayout();
            conversionLayout.SuspendLayout();
            historyLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)
                nudHistoryMaxItems).BeginInit();
            advancedLayout.SuspendLayout();
            SuspendLayout();

            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));
            rootLayout.Controls.Add(
                lblTitle,
                0,
                0);
            rootLayout.Controls.Add(
                lblDescription,
                0,
                1);
            rootLayout.Controls.Add(
                tabSettings,
                0,
                2);
            rootLayout.Controls.Add(
                bottomButtonsPanel,
                0,
                3);
            rootLayout.Dock =
                System.Windows.Forms.DockStyle.Fill;
            rootLayout.Location =
                new System.Drawing.Point(
                    18,
                    18);
            rootLayout.Name =
                "rootLayout";
            rootLayout.RowCount = 4;
            rootLayout.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    44F));
            rootLayout.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    42F));
            rootLayout.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));
            rootLayout.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    62F));
            rootLayout.Size =
                new System.Drawing.Size(
                    714,
                    524);
            rootLayout.TabIndex = 0;

            lblTitle.AutoSize = true;
            lblTitle.Dock =
                System.Windows.Forms.DockStyle.Fill;
            lblTitle.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    17F,
                    System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor =
                System.Drawing.Color.FromArgb(
                    31,
                    41,
                    55);
            lblTitle.Location =
                new System.Drawing.Point(
                    3,
                    0);
            lblTitle.Name =
                "lblTitle";
            lblTitle.Size =
                new System.Drawing.Size(
                    708,
                    44);
            lblTitle.TabIndex = 0;
            lblTitle.Text =
                "إعدادات MiftahFix";
            lblTitle.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            lblDescription.AutoSize = true;
            lblDescription.Dock =
                System.Windows.Forms.DockStyle.Fill;
            lblDescription.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);
            lblDescription.ForeColor =
                System.Drawing.Color.FromArgb(
                    107,
                    114,
                    128);
            lblDescription.Location =
                new System.Drawing.Point(
                    3,
                    44);
            lblDescription.Name =
                "lblDescription";
            lblDescription.Size =
                new System.Drawing.Size(
                    708,
                    42);
            lblDescription.TabIndex = 1;
            lblDescription.Text =
                "اضبط طريقة تشغيل البرنامج والاختصار العام وخيارات التحويل.";
            lblDescription.TextAlign =
                System.Drawing.ContentAlignment.TopRight;

            tabSettings.Controls.Add(
                tabGeneral);
            tabSettings.Controls.Add(
                tabHotkey);
            tabSettings.Controls.Add(
                tabConversion);
            tabSettings.Controls.Add(
                tabHistory);
            tabSettings.Controls.Add(
                tabAdvanced);
            tabSettings.Dock =
                System.Windows.Forms.DockStyle.Fill;
            tabSettings.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F);
            tabSettings.Location =
                new System.Drawing.Point(
                    3,
                    89);
            tabSettings.Name =
                "tabSettings";
            tabSettings.RightToLeft =
                System.Windows.Forms.RightToLeft.Yes;
            tabSettings.RightToLeftLayout =
                true;
            tabSettings.SelectedIndex = 0;
            tabSettings.Size =
                new System.Drawing.Size(
                    708,
                    370);
            tabSettings.TabIndex = 2;

            tabGeneral.BackColor =
                System.Drawing.Color.White;
            tabGeneral.Controls.Add(
                generalLayout);
            tabGeneral.Location =
                new System.Drawing.Point(
                    4,
                    32);
            tabGeneral.Name =
                "tabGeneral";
            tabGeneral.Padding =
                new System.Windows.Forms.Padding(
                    18);
            tabGeneral.Size =
                new System.Drawing.Size(
                    700,
                    334);
            tabGeneral.TabIndex = 0;
            tabGeneral.Text =
                "عام";

            generalLayout.ColumnCount = 1;
            generalLayout.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));
            generalLayout.Controls.Add(
                chkRunWithWindows,
                0,
                0);
            generalLayout.Controls.Add(
                chkStartMinimized,
                0,
                1);
            generalLayout.Controls.Add(
                chkShowStartupNotification,
                0,
                2);
            generalLayout.Dock =
                System.Windows.Forms.DockStyle.Top;
            generalLayout.Location =
                new System.Drawing.Point(
                    18,
                    18);
            generalLayout.Name =
                "generalLayout";
            generalLayout.RowCount = 3;
            generalLayout.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    54F));
            generalLayout.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    54F));
            generalLayout.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    54F));
            generalLayout.Size =
                new System.Drawing.Size(
                    664,
                    162);
            generalLayout.TabIndex = 0;

            chkRunWithWindows.AutoSize = true;
            chkRunWithWindows.Dock =
                System.Windows.Forms.DockStyle.Fill;
            chkRunWithWindows.Location =
                new System.Drawing.Point(
                    3,
                    3);
            chkRunWithWindows.Name =
                "chkRunWithWindows";
            chkRunWithWindows.Size =
                new System.Drawing.Size(
                    658,
                    48);
            chkRunWithWindows.TabIndex = 0;
            chkRunWithWindows.Text =
                "تشغيل البرنامج تلقائيًا مع Windows";
            chkRunWithWindows.UseVisualStyleBackColor =
                true;

            chkStartMinimized.AutoSize = true;
            chkStartMinimized.Dock =
                System.Windows.Forms.DockStyle.Fill;
            chkStartMinimized.Location =
                new System.Drawing.Point(
                    3,
                    57);
            chkStartMinimized.Name =
                "chkStartMinimized";
            chkStartMinimized.Size =
                new System.Drawing.Size(
                    658,
                    48);
            chkStartMinimized.TabIndex = 1;
            chkStartMinimized.Text =
                "بدء البرنامج مصغرًا في شريط النظام";
            chkStartMinimized.UseVisualStyleBackColor =
                true;

            chkShowStartupNotification.AutoSize = true;
            chkShowStartupNotification.Dock =
                System.Windows.Forms.DockStyle.Fill;
            chkShowStartupNotification.Location =
                new System.Drawing.Point(
                    3,
                    111);
            chkShowStartupNotification.Name =
                "chkShowStartupNotification";
            chkShowStartupNotification.Size =
                new System.Drawing.Size(
                    658,
                    48);
            chkShowStartupNotification.TabIndex = 2;
            chkShowStartupNotification.Text =
                "إظهار إشعار عند انتقال البرنامج إلى شريط النظام";
            chkShowStartupNotification.UseVisualStyleBackColor =
                true;

            tabHotkey.BackColor =
                System.Drawing.Color.White;
            tabHotkey.Controls.Add(
                hotkeyLayout);
            tabHotkey.Location =
                new System.Drawing.Point(
                    4,
                    32);
            tabHotkey.Name =
                "tabHotkey";
            tabHotkey.Padding =
                new System.Windows.Forms.Padding(
                    18);
            tabHotkey.Size =
                new System.Drawing.Size(
                    700,
                    334);
            tabHotkey.TabIndex = 1;
            tabHotkey.Text =
                "الاختصار";

            hotkeyLayout.ColumnCount = 2;
            hotkeyLayout.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    72F));
            hotkeyLayout.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    28F));
            hotkeyLayout.Controls.Add(
                lblHotkeyExplanation,
                0,
                0);
            hotkeyLayout.SetColumnSpan(
                lblHotkeyExplanation,
                2);
            hotkeyLayout.Controls.Add(
                hotkeyModifiersPanel,
                0,
                1);
            hotkeyLayout.SetColumnSpan(
                hotkeyModifiersPanel,
                2);
            hotkeyLayout.Controls.Add(
                cmbHotkeyKey,
                0,
                2);
            hotkeyLayout.Controls.Add(
                lblHotkeyKey,
                1,
                2);
            hotkeyLayout.Dock =
                System.Windows.Forms.DockStyle.Top;
            hotkeyLayout.Location =
                new System.Drawing.Point(
                    18,
                    18);
            hotkeyLayout.Name =
                "hotkeyLayout";
            hotkeyLayout.RowCount = 3;
            hotkeyLayout.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    60F));
            hotkeyLayout.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    76F));
            hotkeyLayout.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    54F));
            hotkeyLayout.Size =
                new System.Drawing.Size(
                    664,
                    190);
            hotkeyLayout.TabIndex = 0;

            lblHotkeyExplanation.Dock =
                System.Windows.Forms.DockStyle.Fill;
            lblHotkeyExplanation.ForeColor =
                System.Drawing.Color.FromArgb(
                    75,
                    85,
                    99);
            lblHotkeyExplanation.Location =
                new System.Drawing.Point(
                    3,
                    0);
            lblHotkeyExplanation.Name =
                "lblHotkeyExplanation";
            lblHotkeyExplanation.Size =
                new System.Drawing.Size(
                    658,
                    60);
            lblHotkeyExplanation.TabIndex = 0;
            lblHotkeyExplanation.Text =
                "اختر مفاتيح الاختصار المستخدم لتصحيح النص المحدد في أي برنامج.";
            lblHotkeyExplanation.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            hotkeyModifiersPanel.Controls.Add(
                chkHotkeyControl);
            hotkeyModifiersPanel.Controls.Add(
                chkHotkeyShift);
            hotkeyModifiersPanel.Controls.Add(
                chkHotkeyAlt);
            hotkeyModifiersPanel.Controls.Add(
                chkHotkeyWindows);
            hotkeyModifiersPanel.Dock =
                System.Windows.Forms.DockStyle.Fill;
            hotkeyModifiersPanel.FlowDirection =
                System.Windows.Forms.FlowDirection.RightToLeft;
            hotkeyModifiersPanel.Location =
                new System.Drawing.Point(
                    3,
                    63);
            hotkeyModifiersPanel.Name =
                "hotkeyModifiersPanel";
            hotkeyModifiersPanel.Padding =
                new System.Windows.Forms.Padding(
                    0,
                    14,
                    0,
                    0);
            hotkeyModifiersPanel.Size =
                new System.Drawing.Size(
                    658,
                    70);
            hotkeyModifiersPanel.TabIndex = 1;
            hotkeyModifiersPanel.WrapContents =
                false;

            chkHotkeyControl.AutoSize = true;
            chkHotkeyControl.Margin =
                new System.Windows.Forms.Padding(
                    10,
                    3,
                    10,
                    3);
            chkHotkeyControl.Name =
                "chkHotkeyControl";
            chkHotkeyControl.Size =
                new System.Drawing.Size(
                    60,
                    27);
            chkHotkeyControl.TabIndex = 0;
            chkHotkeyControl.Text =
                "Ctrl";
            chkHotkeyControl.UseVisualStyleBackColor =
                true;

            chkHotkeyShift.AutoSize = true;
            chkHotkeyShift.Margin =
                new System.Windows.Forms.Padding(
                    10,
                    3,
                    10,
                    3);
            chkHotkeyShift.Name =
                "chkHotkeyShift";
            chkHotkeyShift.Size =
                new System.Drawing.Size(
                    65,
                    27);
            chkHotkeyShift.TabIndex = 1;
            chkHotkeyShift.Text =
                "Shift";
            chkHotkeyShift.UseVisualStyleBackColor =
                true;

            chkHotkeyAlt.AutoSize = true;
            chkHotkeyAlt.Margin =
                new System.Windows.Forms.Padding(
                    10,
                    3,
                    10,
                    3);
            chkHotkeyAlt.Name =
                "chkHotkeyAlt";
            chkHotkeyAlt.Size =
                new System.Drawing.Size(
                    53,
                    27);
            chkHotkeyAlt.TabIndex = 2;
            chkHotkeyAlt.Text =
                "Alt";
            chkHotkeyAlt.UseVisualStyleBackColor =
                true;

            chkHotkeyWindows.AutoSize = true;
            chkHotkeyWindows.Margin =
                new System.Windows.Forms.Padding(
                    10,
                    3,
                    10,
                    3);
            chkHotkeyWindows.Name =
                "chkHotkeyWindows";
            chkHotkeyWindows.Size =
                new System.Drawing.Size(
                    60,
                    27);
            chkHotkeyWindows.TabIndex = 3;
            chkHotkeyWindows.Text =
                "Win";
            chkHotkeyWindows.UseVisualStyleBackColor =
                true;

            cmbHotkeyKey.Dock =
                System.Windows.Forms.DockStyle.Fill;
            cmbHotkeyKey.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbHotkeyKey.FormattingEnabled =
                true;
            cmbHotkeyKey.Location =
                new System.Drawing.Point(
                    3,
                    145);
            cmbHotkeyKey.Margin =
                new System.Windows.Forms.Padding(
                    3,
                    9,
                    12,
                    3);
            cmbHotkeyKey.Name =
                "cmbHotkeyKey";
            cmbHotkeyKey.Size =
                new System.Drawing.Size(
                    463,
                    31);
            cmbHotkeyKey.TabIndex = 2;

            lblHotkeyKey.Dock =
                System.Windows.Forms.DockStyle.Fill;
            lblHotkeyKey.Location =
                new System.Drawing.Point(
                    484,
                    136);
            lblHotkeyKey.Name =
                "lblHotkeyKey";
            lblHotkeyKey.Size =
                new System.Drawing.Size(
                    177,
                    54);
            lblHotkeyKey.TabIndex = 3;
            lblHotkeyKey.Text =
                "المفتاح الرئيسي:";
            lblHotkeyKey.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            tabConversion.BackColor =
                System.Drawing.Color.White;
            tabConversion.Controls.Add(
                conversionLayout);
            tabConversion.Location =
                new System.Drawing.Point(
                    4,
                    32);
            tabConversion.Name =
                "tabConversion";
            tabConversion.Padding =
                new System.Windows.Forms.Padding(
                    18);
            tabConversion.Size =
                new System.Drawing.Size(
                    700,
                    334);
            tabConversion.TabIndex = 2;
            tabConversion.Text =
                "التحويل";

            conversionLayout.ColumnCount = 2;
            conversionLayout.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    70F));
            conversionLayout.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    30F));
            conversionLayout.Controls.Add(
                chkInstantConversion,
                0,
                0);
            conversionLayout.SetColumnSpan(
                chkInstantConversion,
                2);
            conversionLayout.Controls.Add(
                cmbConversionMode,
                0,
                1);
            conversionLayout.Controls.Add(
                lblConversionMode,
                1,
                1);
            conversionLayout.Dock =
                System.Windows.Forms.DockStyle.Top;
            conversionLayout.Location =
                new System.Drawing.Point(
                    18,
                    18);
            conversionLayout.Name =
                "conversionLayout";
            conversionLayout.RowCount = 2;
            conversionLayout.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    64F));
            conversionLayout.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    60F));
            conversionLayout.Size =
                new System.Drawing.Size(
                    664,
                    124);
            conversionLayout.TabIndex = 0;

            chkInstantConversion.AutoSize = true;
            chkInstantConversion.Dock =
                System.Windows.Forms.DockStyle.Fill;
            chkInstantConversion.Location =
                new System.Drawing.Point(
                    3,
                    3);
            chkInstantConversion.Name =
                "chkInstantConversion";
            chkInstantConversion.Size =
                new System.Drawing.Size(
                    658,
                    58);
            chkInstantConversion.TabIndex = 0;
            chkInstantConversion.Text =
                "تشغيل التحويل الفوري أثناء الكتابة";
            chkInstantConversion.UseVisualStyleBackColor =
                true;

            cmbConversionMode.Dock =
                System.Windows.Forms.DockStyle.Fill;
            cmbConversionMode.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbConversionMode.FormattingEnabled =
                true;
            cmbConversionMode.Location =
                new System.Drawing.Point(
                    3,
                    76);
            cmbConversionMode.Margin =
                new System.Windows.Forms.Padding(
                    3,
                    12,
                    12,
                    3);
            cmbConversionMode.Name =
                "cmbConversionMode";
            cmbConversionMode.Size =
                new System.Drawing.Size(
                    449,
                    31);
            cmbConversionMode.TabIndex = 1;

            lblConversionMode.Dock =
                System.Windows.Forms.DockStyle.Fill;
            lblConversionMode.Location =
                new System.Drawing.Point(
                    470,
                    64);
            lblConversionMode.Name =
                "lblConversionMode";
            lblConversionMode.Size =
                new System.Drawing.Size(
                    191,
                    60);
            lblConversionMode.TabIndex = 2;
            lblConversionMode.Text =
                "طريقة التحويل:";
            lblConversionMode.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            tabHistory.BackColor =
                System.Drawing.Color.White;
            tabHistory.Controls.Add(
                historyLayout);
            tabHistory.Location =
                new System.Drawing.Point(
                    4,
                    32);
            tabHistory.Name =
                "tabHistory";
            tabHistory.Padding =
                new System.Windows.Forms.Padding(
                    18);
            tabHistory.Size =
                new System.Drawing.Size(
                    700,
                    334);
            tabHistory.TabIndex = 3;
            tabHistory.Text =
                "السجل";

            historyLayout.ColumnCount = 2;
            historyLayout.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    70F));
            historyLayout.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    30F));
            historyLayout.Controls.Add(
                chkEnableHistory,
                0,
                0);
            historyLayout.SetColumnSpan(
                chkEnableHistory,
                2);
            historyLayout.Controls.Add(
                nudHistoryMaxItems,
                0,
                1);
            historyLayout.Controls.Add(
                lblHistoryMaxItems,
                1,
                1);
            historyLayout.Controls.Add(
                lblHistoryNote,
                0,
                2);
            historyLayout.SetColumnSpan(
                lblHistoryNote,
                2);
            historyLayout.Dock =
                System.Windows.Forms.DockStyle.Top;
            historyLayout.Location =
                new System.Drawing.Point(
                    18,
                    18);
            historyLayout.Name =
                "historyLayout";
            historyLayout.RowCount = 3;
            historyLayout.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    60F));
            historyLayout.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    60F));
            historyLayout.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    72F));
            historyLayout.Size =
                new System.Drawing.Size(
                    664,
                    192);
            historyLayout.TabIndex = 0;

            chkEnableHistory.AutoSize = true;
            chkEnableHistory.Dock =
                System.Windows.Forms.DockStyle.Fill;
            chkEnableHistory.Location =
                new System.Drawing.Point(
                    3,
                    3);
            chkEnableHistory.Name =
                "chkEnableHistory";
            chkEnableHistory.Size =
                new System.Drawing.Size(
                    658,
                    54);
            chkEnableHistory.TabIndex = 0;
            chkEnableHistory.Text =
                "تفعيل حفظ سجل عمليات التحويل";
            chkEnableHistory.UseVisualStyleBackColor =
                true;

            nudHistoryMaxItems.Dock =
                System.Windows.Forms.DockStyle.Fill;
            nudHistoryMaxItems.Location =
                new System.Drawing.Point(
                    3,
                    73);
            nudHistoryMaxItems.Margin =
                new System.Windows.Forms.Padding(
                    3,
                    13,
                    12,
                    3);
            nudHistoryMaxItems.Maximum =
                new decimal(
                    new int[]
                    {
                        1000,
                        0,
                        0,
                        0
                    });
            nudHistoryMaxItems.Minimum =
                new decimal(
                    new int[]
                    {
                        10,
                        0,
                        0,
                        0
                    });
            nudHistoryMaxItems.Name =
                "nudHistoryMaxItems";
            nudHistoryMaxItems.Size =
                new System.Drawing.Size(
                    449,
                    30);
            nudHistoryMaxItems.TabIndex = 1;
            nudHistoryMaxItems.TextAlign =
                System.Windows.Forms.HorizontalAlignment.Center;
            nudHistoryMaxItems.Value =
                new decimal(
                    new int[]
                    {
                        100,
                        0,
                        0,
                        0
                    });

            lblHistoryMaxItems.Dock =
                System.Windows.Forms.DockStyle.Fill;
            lblHistoryMaxItems.Location =
                new System.Drawing.Point(
                    470,
                    60);
            lblHistoryMaxItems.Name =
                "lblHistoryMaxItems";
            lblHistoryMaxItems.Size =
                new System.Drawing.Size(
                    191,
                    60);
            lblHistoryMaxItems.TabIndex = 2;
            lblHistoryMaxItems.Text =
                "أقصى عدد للعناصر:";
            lblHistoryMaxItems.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            lblHistoryNote.Dock =
                System.Windows.Forms.DockStyle.Fill;
            lblHistoryNote.ForeColor =
                System.Drawing.Color.FromArgb(
                    107,
                    114,
                    128);
            lblHistoryNote.Location =
                new System.Drawing.Point(
                    3,
                    120);
            lblHistoryNote.Name =
                "lblHistoryNote";
            lblHistoryNote.Size =
                new System.Drawing.Size(
                    658,
                    72);
            lblHistoryNote.TabIndex = 3;
            lblHistoryNote.Text =
                "سيتم استخدام هذه الخيارات عند إضافة شاشة سجل التحويلات في مرحلة لاحقة.";
            lblHistoryNote.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            tabAdvanced.BackColor =
                System.Drawing.Color.White;
            tabAdvanced.Controls.Add(
                advancedLayout);
            tabAdvanced.Location =
                new System.Drawing.Point(
                    4,
                    32);
            tabAdvanced.Name =
                "tabAdvanced";
            tabAdvanced.Padding =
                new System.Windows.Forms.Padding(
                    18);
            tabAdvanced.Size =
                new System.Drawing.Size(
                    700,
                    334);
            tabAdvanced.TabIndex = 4;
            tabAdvanced.Text =
                "متقدم";

            advancedLayout.ColumnCount = 1;
            advancedLayout.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(
                    System.Windows.Forms.SizeType.Percent,
                    100F));
            advancedLayout.Controls.Add(
                chkCheckForUpdates,
                0,
                0);
            advancedLayout.Controls.Add(
                lblAdvancedNote,
                0,
                1);
            advancedLayout.Dock =
                System.Windows.Forms.DockStyle.Top;
            advancedLayout.Location =
                new System.Drawing.Point(
                    18,
                    18);
            advancedLayout.Name =
                "advancedLayout";
            advancedLayout.RowCount = 2;
            advancedLayout.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    60F));
            advancedLayout.RowStyles.Add(
                new System.Windows.Forms.RowStyle(
                    System.Windows.Forms.SizeType.Absolute,
                    80F));
            advancedLayout.Size =
                new System.Drawing.Size(
                    664,
                    140);
            advancedLayout.TabIndex = 0;

            chkCheckForUpdates.AutoSize = true;
            chkCheckForUpdates.Dock =
                System.Windows.Forms.DockStyle.Fill;
            chkCheckForUpdates.Location =
                new System.Drawing.Point(
                    3,
                    3);
            chkCheckForUpdates.Name =
                "chkCheckForUpdates";
            chkCheckForUpdates.Size =
                new System.Drawing.Size(
                    658,
                    54);
            chkCheckForUpdates.TabIndex = 0;
            chkCheckForUpdates.Text =
                "التحقق من وجود تحديثات عند بدء التشغيل";
            chkCheckForUpdates.UseVisualStyleBackColor =
                true;

            lblAdvancedNote.Dock =
                System.Windows.Forms.DockStyle.Fill;
            lblAdvancedNote.ForeColor =
                System.Drawing.Color.FromArgb(
                    107,
                    114,
                    128);
            lblAdvancedNote.Location =
                new System.Drawing.Point(
                    3,
                    60);
            lblAdvancedNote.Name =
                "lblAdvancedNote";
            lblAdvancedNote.Size =
                new System.Drawing.Size(
                    658,
                    80);
            lblAdvancedNote.TabIndex = 1;
            lblAdvancedNote.Text =
                "لن يتصل البرنامج بالإنترنت في النسخة الحالية. هذا الخيار محفوظ للتحديثات المستقبلية.";
            lblAdvancedNote.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            bottomButtonsPanel.Controls.Add(
                btnSave);
            bottomButtonsPanel.Controls.Add(
                btnCancel);
            bottomButtonsPanel.Controls.Add(
                btnRestoreDefaults);
            bottomButtonsPanel.Dock =
                System.Windows.Forms.DockStyle.Fill;
            bottomButtonsPanel.FlowDirection =
                System.Windows.Forms.FlowDirection.RightToLeft;
            bottomButtonsPanel.Location =
                new System.Drawing.Point(
                    3,
                    465);
            bottomButtonsPanel.Name =
                "bottomButtonsPanel";
            bottomButtonsPanel.Padding =
                new System.Windows.Forms.Padding(
                    0,
                    10,
                    0,
                    0);
            bottomButtonsPanel.Size =
                new System.Drawing.Size(
                    708,
                    56);
            bottomButtonsPanel.TabIndex = 3;
            bottomButtonsPanel.WrapContents =
                false;

            btnSave.BackColor =
                System.Drawing.Color.FromArgb(
                    37,
                    99,
                    235);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;
            btnSave.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F,
                    System.Drawing.FontStyle.Bold);
            btnSave.ForeColor =
                System.Drawing.Color.White;
            btnSave.Location =
                new System.Drawing.Point(
                    568,
                    13);
            btnSave.Margin =
                new System.Windows.Forms.Padding(
                    8,
                    3,
                    0,
                    3);
            btnSave.Name =
                "btnSave";
            btnSave.Size =
                new System.Drawing.Size(
                    140,
                    40);
            btnSave.TabIndex = 0;
            btnSave.Text =
                "حفظ";
            btnSave.UseVisualStyleBackColor =
                false;

            btnCancel.BackColor =
                System.Drawing.Color.White;
            btnCancel.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;
            btnCancel.ForeColor =
                System.Drawing.Color.FromArgb(
                    55,
                    65,
                    81);
            btnCancel.Location =
                new System.Drawing.Point(
                    420,
                    13);
            btnCancel.Margin =
                new System.Windows.Forms.Padding(
                    8,
                    3,
                    0,
                    3);
            btnCancel.Name =
                "btnCancel";
            btnCancel.Size =
                new System.Drawing.Size(
                    140,
                    40);
            btnCancel.TabIndex = 1;
            btnCancel.Text =
                "إلغاء";
            btnCancel.UseVisualStyleBackColor =
                false;

            btnRestoreDefaults.BackColor =
                System.Drawing.Color.White;
            btnRestoreDefaults.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;
            btnRestoreDefaults.ForeColor =
                System.Drawing.Color.FromArgb(
                    55,
                    65,
                    81);
            btnRestoreDefaults.Location =
                new System.Drawing.Point(
                    222,
                    13);
            btnRestoreDefaults.Margin =
                new System.Windows.Forms.Padding(
                    8,
                    3,
                    0,
                    3);
            btnRestoreDefaults.Name =
                "btnRestoreDefaults";
            btnRestoreDefaults.Size =
                new System.Drawing.Size(
                    190,
                    40);
            btnRestoreDefaults.TabIndex = 2;
            btnRestoreDefaults.Text =
                "استعادة الافتراضي";
            btnRestoreDefaults.UseVisualStyleBackColor =
                false;

            AcceptButton =
                btnSave;
            AutoScaleDimensions =
                new System.Drawing.SizeF(
                    8F,
                    20F);
            AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;
            BackColor =
                System.Drawing.Color.FromArgb(
                    248,
                    250,
                    252);
            CancelButton =
                btnCancel;
            ClientSize =
                new System.Drawing.Size(
                    750,
                    560);
            Controls.Add(
                rootLayout);
            Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    9F);
            FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name =
                "SettingsForm";
            Padding =
                new System.Windows.Forms.Padding(
                    18);
            RightToLeft =
                System.Windows.Forms.RightToLeft.Yes;
            RightToLeftLayout = true;
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;
            Text =
                "الإعدادات - MiftahFix";

            rootLayout.ResumeLayout(
                false);
            rootLayout.PerformLayout();
            tabSettings.ResumeLayout(
                false);
            tabGeneral.ResumeLayout(
                false);
            tabHotkey.ResumeLayout(
                false);
            tabConversion.ResumeLayout(
                false);
            tabHistory.ResumeLayout(
                false);
            tabAdvanced.ResumeLayout(
                false);
            bottomButtonsPanel.ResumeLayout(
                false);
            generalLayout.ResumeLayout(
                false);
            generalLayout.PerformLayout();
            hotkeyLayout.ResumeLayout(
                false);
            hotkeyModifiersPanel.ResumeLayout(
                false);
            hotkeyModifiersPanel.PerformLayout();
            conversionLayout.ResumeLayout(
                false);
            conversionLayout.PerformLayout();
            historyLayout.ResumeLayout(
                false);
            historyLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)
                nudHistoryMaxItems).EndInit();
            advancedLayout.ResumeLayout(
                false);
            advancedLayout.PerformLayout();
            ResumeLayout(
                false);
        }
    }
}