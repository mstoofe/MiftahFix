namespace MiftahFix.Forms
{
    partial class HistoryForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDescription;

        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblResultsCount;

        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.DataGridView dgvHistory;

        private System.Windows.Forms.Panel pnlDetailsHeader;
        private System.Windows.Forms.Label lblDetailsTitle;
        private System.Windows.Forms.Label lblSelectedMetadata;

        private System.Windows.Forms.TableLayoutPanel tableDetails;

        private System.Windows.Forms.Panel pnlOriginalCard;
        private System.Windows.Forms.Label lblOriginalTitle;
        private System.Windows.Forms.TextBox txtOriginalDetails;
        private System.Windows.Forms.Button btnCopyOriginal;

        private System.Windows.Forms.Panel pnlConvertedCard;
        private System.Windows.Forms.Label lblConvertedTitle;
        private System.Windows.Forms.TextBox txtConvertedDetails;
        private System.Windows.Forms.Button btnCopyConverted;

        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnDeleteSelected;
        private System.Windows.Forms.Button btnClearHistory;
        private System.Windows.Forms.Button btnClose;

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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblDescription = new Label();
            lblTitle = new Label();
            pnlToolbar = new Panel();
            lblResultsCount = new Label();
            btnRefresh = new Button();
            txtSearch = new TextBox();
            lblSearch = new Label();
            splitMain = new SplitContainer();
            dgvHistory = new DataGridView();
            tableDetails = new TableLayoutPanel();
            pnlConvertedCard = new Panel();
            txtConvertedDetails = new TextBox();
            btnCopyConverted = new Button();
            lblConvertedTitle = new Label();
            pnlOriginalCard = new Panel();
            txtOriginalDetails = new TextBox();
            btnCopyOriginal = new Button();
            lblOriginalTitle = new Label();
            pnlDetailsHeader = new Panel();
            lblSelectedMetadata = new Label();
            lblDetailsTitle = new Label();
            pnlFooter = new Panel();
            btnClose = new Button();
            btnClearHistory = new Button();
            btnDeleteSelected = new Button();
            lblStatus = new Label();
            pnlHeader.SuspendLayout();
            pnlToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            tableDetails.SuspendLayout();
            pnlConvertedCard.SuspendLayout();
            pnlOriginalCard.SuspendLayout();
            pnlDetailsHeader.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(30, 41, 59);
            pnlHeader.Controls.Add(lblDescription);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(28, 20, 28, 18);
            pnlHeader.Size = new Size(1380, 112);
            pnlHeader.TabIndex = 0;
            // 
            // lblDescription
            // 
            lblDescription.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 9.5F);
            lblDescription.ForeColor = Color.FromArgb(203, 213, 225);
            lblDescription.Location = new Point(805, 67);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(534, 25);
            lblDescription.TabIndex = 1;
            lblDescription.Text = "عرض عمليات التحويل السابقة والبحث والنسخ وإدارة السجل";
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(1071, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(268, 54);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "سجل التحويلات";
            // 
            // pnlToolbar
            // 
            pnlToolbar.BackColor = Color.White;
            pnlToolbar.Controls.Add(lblResultsCount);
            pnlToolbar.Controls.Add(btnRefresh);
            pnlToolbar.Controls.Add(txtSearch);
            pnlToolbar.Controls.Add(lblSearch);
            pnlToolbar.Dock = DockStyle.Top;
            pnlToolbar.Location = new Point(0, 112);
            pnlToolbar.Name = "pnlToolbar";
            pnlToolbar.Padding = new Padding(25, 18, 25, 18);
            pnlToolbar.Size = new Size(1380, 82);
            pnlToolbar.TabIndex = 1;
            // 
            // lblResultsCount
            // 
            lblResultsCount.AutoSize = true;
            lblResultsCount.Dock = DockStyle.Left;
            lblResultsCount.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblResultsCount.ForeColor = Color.FromArgb(71, 85, 105);
            lblResultsCount.Location = new Point(25, 18);
            lblResultsCount.Name = "lblResultsCount";
            lblResultsCount.Padding = new Padding(0, 10, 0, 0);
            lblResultsCount.Size = new Size(169, 35);
            lblResultsCount.TabIndex = 3;
            lblResultsCount.Text = "النتائج: 0 من 0";
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.BackColor = Color.FromArgb(226, 232, 240);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.FromArgb(51, 65, 85);
            btnRefresh.Location = new Point(748, 18);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(130, 46);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "تحديث";
            btnRefresh.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtSearch.Font = new Font("Segoe UI", 10.5F);
            txtSearch.Location = new Point(895, 23);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "ابحث في النص الأصلي أو المصحح...";
            txtSearch.RightToLeft = RightToLeft.Yes;
            txtSearch.Size = new Size(352, 35);
            txtSearch.TabIndex = 1;
            // 
            // lblSearch
            // 
            lblSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSearch.ForeColor = Color.FromArgb(51, 65, 85);
            lblSearch.Location = new Point(1262, 27);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(72, 28);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "البحث:";
            // 
            // splitMain
            // 
            splitMain.BackColor = Color.FromArgb(241, 245, 249);
            splitMain.Dock = DockStyle.Fill;
            splitMain.Location = new Point(0, 194);
            splitMain.Name = "splitMain";
            splitMain.Orientation = Orientation.Horizontal;
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(dgvHistory);
            splitMain.Panel1.Padding = new Padding(25, 20, 25, 10);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(tableDetails);
            splitMain.Panel2.Controls.Add(pnlDetailsHeader);
            splitMain.Panel2.Padding = new Padding(25, 10, 25, 20);
            splitMain.Size = new Size(1380, 676);
            splitMain.SplitterDistance = 334;
            splitMain.SplitterWidth = 6;
            splitMain.TabIndex = 2;
            // 
            // dgvHistory
            // 
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.AllowUserToDeleteRows = false;
            dgvHistory.AllowUserToResizeRows = false;
            dgvHistory.BackgroundColor = Color.White;
            dgvHistory.BorderStyle = BorderStyle.None;
            dgvHistory.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvHistory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvHistory.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(226, 232, 240);
            dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            dgvHistory.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(226, 232, 240);
            dgvHistory.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 41, 59);
            dgvHistory.ColumnHeadersHeight = 46;
            dgvHistory.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvHistory.DefaultCellStyle.BackColor = Color.White;
            dgvHistory.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvHistory.DefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
            dgvHistory.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvHistory.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 64, 175);
            dgvHistory.Dock = DockStyle.Fill;
            dgvHistory.EnableHeadersVisualStyles = false;
            dgvHistory.GridColor = Color.FromArgb(226, 232, 240);
            dgvHistory.Location = new Point(25, 20);
            dgvHistory.MultiSelect = false;
            dgvHistory.Name = "dgvHistory";
            dgvHistory.ReadOnly = true;
            dgvHistory.RightToLeft = RightToLeft.Yes;
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.RowHeadersWidth = 51;
            dgvHistory.RowTemplate.Height = 42;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.Size = new Size(1330, 304);
            dgvHistory.TabIndex = 0;
            // 
            // tableDetails
            // 
            tableDetails.ColumnCount = 2;
            tableDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableDetails.Controls.Add(pnlConvertedCard, 0, 0);
            tableDetails.Controls.Add(pnlOriginalCard, 1, 0);
            tableDetails.Dock = DockStyle.Fill;
            tableDetails.Location = new Point(25, 80);
            tableDetails.Name = "tableDetails";
            tableDetails.RowCount = 1;
            tableDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableDetails.Size = new Size(1330, 236);
            tableDetails.TabIndex = 1;
            // 
            // pnlConvertedCard
            // 
            pnlConvertedCard.BackColor = Color.White;
            pnlConvertedCard.Controls.Add(txtConvertedDetails);
            pnlConvertedCard.Controls.Add(btnCopyConverted);
            pnlConvertedCard.Controls.Add(lblConvertedTitle);
            pnlConvertedCard.Dock = DockStyle.Fill;
            pnlConvertedCard.Location = new Point(0, 0);
            pnlConvertedCard.Margin = new Padding(0, 0, 10, 0);
            pnlConvertedCard.Name = "pnlConvertedCard";
            pnlConvertedCard.Padding = new Padding(18);
            pnlConvertedCard.Size = new Size(655, 236);
            pnlConvertedCard.TabIndex = 1;
            // 
            // txtConvertedDetails
            // 
            txtConvertedDetails.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtConvertedDetails.BackColor = Color.FromArgb(248, 250, 252);
            txtConvertedDetails.BorderStyle = BorderStyle.FixedSingle;
            txtConvertedDetails.Font = new Font("Segoe UI", 11F);
            txtConvertedDetails.Location = new Point(18, 65);
            txtConvertedDetails.Multiline = true;
            txtConvertedDetails.Name = "txtConvertedDetails";
            txtConvertedDetails.ReadOnly = true;
            txtConvertedDetails.RightToLeft = RightToLeft.Yes;
            txtConvertedDetails.ScrollBars = ScrollBars.Vertical;
            txtConvertedDetails.Size = new Size(619, 153);
            txtConvertedDetails.TabIndex = 2;
            // 
            // btnCopyConverted
            // 
            btnCopyConverted.BackColor = Color.FromArgb(236, 253, 245);
            btnCopyConverted.Cursor = Cursors.Hand;
            btnCopyConverted.FlatAppearance.BorderSize = 0;
            btnCopyConverted.FlatStyle = FlatStyle.Flat;
            btnCopyConverted.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCopyConverted.ForeColor = Color.FromArgb(4, 120, 87);
            btnCopyConverted.Location = new Point(18, 16);
            btnCopyConverted.Name = "btnCopyConverted";
            btnCopyConverted.Size = new Size(125, 39);
            btnCopyConverted.TabIndex = 1;
            btnCopyConverted.Text = "نسخ المصحح";
            btnCopyConverted.UseVisualStyleBackColor = false;
            // 
            // lblConvertedTitle
            // 
            lblConvertedTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblConvertedTitle.AutoSize = true;
            lblConvertedTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblConvertedTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblConvertedTitle.Location = new Point(497, 20);
            lblConvertedTitle.Name = "lblConvertedTitle";
            lblConvertedTitle.Size = new Size(139, 30);
            lblConvertedTitle.TabIndex = 0;
            lblConvertedTitle.Text = "النص المصحح";
            // 
            // pnlOriginalCard
            // 
            pnlOriginalCard.BackColor = Color.White;
            pnlOriginalCard.Controls.Add(txtOriginalDetails);
            pnlOriginalCard.Controls.Add(btnCopyOriginal);
            pnlOriginalCard.Controls.Add(lblOriginalTitle);
            pnlOriginalCard.Dock = DockStyle.Fill;
            pnlOriginalCard.Location = new Point(675, 0);
            pnlOriginalCard.Margin = new Padding(10, 0, 0, 0);
            pnlOriginalCard.Name = "pnlOriginalCard";
            pnlOriginalCard.Padding = new Padding(18);
            pnlOriginalCard.Size = new Size(655, 236);
            pnlOriginalCard.TabIndex = 0;
            // 
            // txtOriginalDetails
            // 
            txtOriginalDetails.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtOriginalDetails.BackColor = Color.FromArgb(248, 250, 252);
            txtOriginalDetails.BorderStyle = BorderStyle.FixedSingle;
            txtOriginalDetails.Font = new Font("Segoe UI", 11F);
            txtOriginalDetails.Location = new Point(18, 65);
            txtOriginalDetails.Multiline = true;
            txtOriginalDetails.Name = "txtOriginalDetails";
            txtOriginalDetails.ReadOnly = true;
            txtOriginalDetails.RightToLeft = RightToLeft.Yes;
            txtOriginalDetails.ScrollBars = ScrollBars.Vertical;
            txtOriginalDetails.Size = new Size(619, 153);
            txtOriginalDetails.TabIndex = 2;
            // 
            // btnCopyOriginal
            // 
            btnCopyOriginal.BackColor = Color.FromArgb(239, 246, 255);
            btnCopyOriginal.Cursor = Cursors.Hand;
            btnCopyOriginal.FlatAppearance.BorderSize = 0;
            btnCopyOriginal.FlatStyle = FlatStyle.Flat;
            btnCopyOriginal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCopyOriginal.ForeColor = Color.FromArgb(29, 78, 216);
            btnCopyOriginal.Location = new Point(18, 16);
            btnCopyOriginal.Name = "btnCopyOriginal";
            btnCopyOriginal.Size = new Size(125, 39);
            btnCopyOriginal.TabIndex = 1;
            btnCopyOriginal.Text = "نسخ الأصلي";
            btnCopyOriginal.UseVisualStyleBackColor = false;
            // 
            // lblOriginalTitle
            // 
            lblOriginalTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblOriginalTitle.AutoSize = true;
            lblOriginalTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblOriginalTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblOriginalTitle.Location = new Point(505, 20);
            lblOriginalTitle.Name = "lblOriginalTitle";
            lblOriginalTitle.Size = new Size(131, 30);
            lblOriginalTitle.TabIndex = 0;
            lblOriginalTitle.Text = "النص الأصلي";
            // 
            // pnlDetailsHeader
            // 
            pnlDetailsHeader.BackColor = Color.Transparent;
            pnlDetailsHeader.Controls.Add(lblSelectedMetadata);
            pnlDetailsHeader.Controls.Add(lblDetailsTitle);
            pnlDetailsHeader.Dock = DockStyle.Top;
            pnlDetailsHeader.Location = new Point(25, 10);
            pnlDetailsHeader.Name = "pnlDetailsHeader";
            pnlDetailsHeader.Size = new Size(1330, 70);
            pnlDetailsHeader.TabIndex = 0;
            // 
            // lblSelectedMetadata
            // 
            lblSelectedMetadata.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSelectedMetadata.AutoSize = true;
            lblSelectedMetadata.Font = new Font("Segoe UI", 9F);
            lblSelectedMetadata.ForeColor = Color.FromArgb(100, 116, 139);
            lblSelectedMetadata.Location = new Point(659, 37);
            lblSelectedMetadata.Name = "lblSelectedMetadata";
            lblSelectedMetadata.Size = new Size(344, 25);
            lblSelectedMetadata.TabIndex = 1;
            lblSelectedMetadata.Text = "حدد عملية من الجدول لعرض التفاصيل.";
            // 
            // lblDetailsTitle
            // 
            lblDetailsTitle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDetailsTitle.AutoSize = true;
            lblDetailsTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblDetailsTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblDetailsTitle.Location = new Point(1191, 2);
            lblDetailsTitle.Name = "lblDetailsTitle";
            lblDetailsTitle.Size = new Size(139, 32);
            lblDetailsTitle.TabIndex = 0;
            lblDetailsTitle.Text = "تفاصيل العملية";
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.White;
            pnlFooter.Controls.Add(btnClose);
            pnlFooter.Controls.Add(btnClearHistory);
            pnlFooter.Controls.Add(btnDeleteSelected);
            pnlFooter.Controls.Add(lblStatus);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 870);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(25, 15, 25, 15);
            pnlFooter.Size = new Size(1380, 72);
            pnlFooter.TabIndex = 3;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.FromArgb(51, 65, 85);
            btnClose.Cursor = Cursors.Hand;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(25, 14);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(120, 44);
            btnClose.TabIndex = 3;
            btnClose.Text = "إغلاق";
            btnClose.UseVisualStyleBackColor = false;
            // 
            // btnClearHistory
            // 
            btnClearHistory.BackColor = Color.FromArgb(254, 226, 226);
            btnClearHistory.Cursor = Cursors.Hand;
            btnClearHistory.FlatAppearance.BorderSize = 0;
            btnClearHistory.FlatStyle = FlatStyle.Flat;
            btnClearHistory.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnClearHistory.ForeColor = Color.FromArgb(185, 28, 28);
            btnClearHistory.Location = new Point(158, 14);
            btnClearHistory.Name = "btnClearHistory";
            btnClearHistory.Size = new Size(150, 44);
            btnClearHistory.TabIndex = 2;
            btnClearHistory.Text = "مسح السجل";
            btnClearHistory.UseVisualStyleBackColor = false;
            // 
            // btnDeleteSelected
            // 
            btnDeleteSelected.BackColor = Color.FromArgb(255, 247, 237);
            btnDeleteSelected.Cursor = Cursors.Hand;
            btnDeleteSelected.FlatAppearance.BorderSize = 0;
            btnDeleteSelected.FlatStyle = FlatStyle.Flat;
            btnDeleteSelected.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnDeleteSelected.ForeColor = Color.FromArgb(194, 65, 12);
            btnDeleteSelected.Location = new Point(321, 14);
            btnDeleteSelected.Name = "btnDeleteSelected";
            btnDeleteSelected.Size = new Size(168, 44);
            btnDeleteSelected.TabIndex = 1;
            btnDeleteSelected.Text = "حذف المحدد";
            btnDeleteSelected.UseVisualStyleBackColor = false;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Dock = DockStyle.Right;
            lblStatus.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(100, 116, 139);
            lblStatus.Location = new Point(1217, 15);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(0, 8, 0, 0);
            lblStatus.Size = new Size(138, 33);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "جاري التحميل...";
            // 
            // HistoryForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(1380, 942);
            Controls.Add(splitMain);
            Controls.Add(pnlFooter);
            Controls.Add(pnlToolbar);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.Sizable;
            Margin = new Padding(4);
            MinimumSize = new Size(1120, 780);
            Name = "HistoryForm";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "سجل التحويلات - MiftahFix";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlToolbar.ResumeLayout(false);
            pnlToolbar.PerformLayout();
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            tableDetails.ResumeLayout(false);
            pnlConvertedCard.ResumeLayout(false);
            pnlConvertedCard.PerformLayout();
            pnlOriginalCard.ResumeLayout(false);
            pnlOriginalCard.PerformLayout();
            pnlDetailsHeader.ResumeLayout(false);
            pnlDetailsHeader.PerformLayout();
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}