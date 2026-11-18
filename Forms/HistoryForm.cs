using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MiftahFix.Models;
using MiftahFix.Services;

namespace MiftahFix.Forms
{
    public partial class HistoryForm : Form
    {
        private readonly HistoryService
            _historyService;

        private readonly List<HistoryEntry>
            _allEntries;

        private bool _isLoading;

        public HistoryForm(
            HistoryService historyService)
        {
            InitializeComponent();

            _historyService =
                historyService ??
                throw new ArgumentNullException(
                    nameof(historyService));

            _allEntries =
                new List<HistoryEntry>();

            InitializeHistoryWindow();
        }

        private void InitializeHistoryWindow()
        {
            txtSearch.TextChanged +=
                TxtSearch_TextChanged;

            dgvHistory.SelectionChanged +=
                DgvHistory_SelectionChanged;

            btnRefresh.Click +=
                BtnRefresh_Click;

            btnCopyOriginal.Click +=
                BtnCopyOriginal_Click;

            btnCopyConverted.Click +=
                BtnCopyConverted_Click;

            btnDeleteSelected.Click +=
                BtnDeleteSelected_Click;

            btnClearHistory.Click +=
                BtnClearHistory_Click;

            btnClose.Click +=
                BtnClose_Click;

            Shown +=
                HistoryForm_Shown;

            FormClosed +=
                HistoryForm_FormClosed;

            ConfigureHistoryGrid();
            ClearDetails();
        }

        private void ConfigureHistoryGrid()
        {
            dgvHistory.AutoGenerateColumns =
                false;

            dgvHistory.Columns.Clear();

            dgvHistory.Columns.Add(
                CreateTextColumn(
                    name: "colDate",
                    headerText: "التاريخ والوقت",
                    width: 175));

            dgvHistory.Columns.Add(
                CreateTextColumn(
                    name: "colSource",
                    headerText: "المصدر",
                    width: 115));

            dgvHistory.Columns.Add(
                CreateTextColumn(
                    name: "colMode",
                    headerText: "طريقة التحويل",
                    width: 145));

            DataGridViewTextBoxColumn originalColumn =
                CreateTextColumn(
                    name: "colOriginal",
                    headerText: "النص الأصلي",
                    width: 280);

            originalColumn.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;

            originalColumn.FillWeight =
                50;

            dgvHistory.Columns.Add(
                originalColumn);

            DataGridViewTextBoxColumn convertedColumn =
                CreateTextColumn(
                    name: "colConverted",
                    headerText: "النص المصحح",
                    width: 280);

            convertedColumn.AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill;

            convertedColumn.FillWeight =
                50;

            dgvHistory.Columns.Add(
                convertedColumn);
        }

        private static DataGridViewTextBoxColumn
            CreateTextColumn(
                string name,
                string headerText,
                int width)
        {
            return new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = headerText,
                Width = width,
                ReadOnly = true,
                SortMode =
                    DataGridViewColumnSortMode
                        .Automatic
            };
        }

        private void HistoryForm_Shown(
            object? sender,
            EventArgs e)
        {
            LoadHistory();
            txtSearch.Focus();
        }

        private void LoadHistory()
        {
            _isLoading = true;

            try
            {
                _allEntries.Clear();

                if (!_historyService.TryLoad(
                        out IReadOnlyList<HistoryEntry> entries,
                        out string errorMessage))
                {
                    ShowStatus(
                        $"تعذر تحميل السجل: {errorMessage}",
                        StatusType.Error);

                    RefreshGrid(
                        Array.Empty<HistoryEntry>());

                    return;
                }

                _allEntries.AddRange(
                    entries.Select(
                        entry =>
                            entry.Clone()));

                ApplySearchFilter();

                ShowStatus(
                    $"تم تحميل {_allEntries.Count:N0} عملية.",
                    StatusType.Success);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void ApplySearchFilter()
        {
            string searchText =
                txtSearch.Text.Trim();

            IEnumerable<HistoryEntry> filteredEntries =
                _allEntries;

            if (!string.IsNullOrWhiteSpace(
                    searchText))
            {
                filteredEntries =
                    filteredEntries.Where(
                        entry =>
                            ContainsText(
                                entry.OriginalText,
                                searchText) ||
                            ContainsText(
                                entry.ConvertedText,
                                searchText) ||
                            ContainsText(
                                GetModeDisplayText(
                                    entry.ConversionMode),
                                searchText) ||
                            ContainsText(
                                GetSourceDisplayText(
                                    entry.Source),
                                searchText));
            }

            List<HistoryEntry> visibleEntries =
                filteredEntries
                    .OrderByDescending(
                        entry =>
                            entry.CreatedAt)
                    .ToList();

            RefreshGrid(
                visibleEntries);

            lblResultsCount.Text =
                $"النتائج: {visibleEntries.Count:N0} من {_allEntries.Count:N0}";
        }

        private static bool ContainsText(
            string? value,
            string searchText)
        {
            return !string.IsNullOrEmpty(
                       value) &&
                   value.Contains(
                       searchText,
                       StringComparison
                           .CurrentCultureIgnoreCase);
        }

        private void RefreshGrid(
            IReadOnlyList<HistoryEntry> entries)
        {
            dgvHistory.Rows.Clear();

            foreach (HistoryEntry entry
                in entries)
            {
                int rowIndex =
                    dgvHistory.Rows.Add(
                        entry.CreatedAt
                            .LocalDateTime
                            .ToString(
                                "yyyy/MM/dd  hh:mm tt"),
                        GetSourceDisplayText(
                            entry.Source),
                        GetModeDisplayText(
                            entry.ConversionMode),
                        CreatePreviewText(
                            entry.OriginalText),
                        CreatePreviewText(
                            entry.ConvertedText));

                dgvHistory.Rows[rowIndex].Tag =
                    entry;
            }

            if (dgvHistory.Rows.Count > 0)
            {
                dgvHistory.ClearSelection();

                dgvHistory.Rows[0].Selected =
                    true;

                dgvHistory.CurrentCell =
                    dgvHistory.Rows[0].Cells[0];
            }
            else
            {
                ClearDetails();
            }
        }

        private static string CreatePreviewText(
            string text)
        {
            string singleLineText =
                (text ?? string.Empty)
                    .Replace(
                        "\r",
                        " ")
                    .Replace(
                        "\n",
                        " ")
                    .Replace(
                        "\t",
                        " ")
                    .Trim();

            while (singleLineText.Contains(
                "  ",
                StringComparison.Ordinal))
            {
                singleLineText =
                    singleLineText.Replace(
                        "  ",
                        " ",
                        StringComparison.Ordinal);
            }

            const int maximumLength =
                100;

            if (singleLineText.Length <=
                maximumLength)
            {
                return singleLineText;
            }

            return singleLineText[..maximumLength] +
                   "...";
        }

        private void TxtSearch_TextChanged(
            object? sender,
            EventArgs e)
        {
            if (_isLoading)
            {
                return;
            }

            ApplySearchFilter();
        }

        private void DgvHistory_SelectionChanged(
            object? sender,
            EventArgs e)
        {
            HistoryEntry? selectedEntry =
                GetSelectedEntry();

            if (selectedEntry == null)
            {
                ClearDetails();
                return;
            }

            txtOriginalDetails.Text =
                selectedEntry.OriginalText;

            txtConvertedDetails.Text =
                selectedEntry.ConvertedText;

            lblSelectedMetadata.Text =
                $"التاريخ: {selectedEntry.CreatedAt.LocalDateTime:yyyy/MM/dd hh:mm:ss tt}" +
                $"    |    المصدر: {GetSourceDisplayText(selectedEntry.Source)}" +
                $"    |    التحويل: {GetModeDisplayText(selectedEntry.ConversionMode)}";

            btnCopyOriginal.Enabled =
                !string.IsNullOrEmpty(
                    selectedEntry.OriginalText);

            btnCopyConverted.Enabled =
                !string.IsNullOrEmpty(
                    selectedEntry.ConvertedText);

            btnDeleteSelected.Enabled =
                true;
        }

        private HistoryEntry? GetSelectedEntry()
        {
            if (dgvHistory.SelectedRows.Count == 0)
            {
                return null;
            }

            return dgvHistory
                .SelectedRows[0]
                .Tag as HistoryEntry;
        }

        private void ClearDetails()
        {
            txtOriginalDetails.Clear();
            txtConvertedDetails.Clear();

            lblSelectedMetadata.Text =
                "حدد عملية من الجدول لعرض التفاصيل.";

            btnCopyOriginal.Enabled =
                false;

            btnCopyConverted.Enabled =
                false;

            btnDeleteSelected.Enabled =
                false;
        }

        private void BtnRefresh_Click(
            object? sender,
            EventArgs e)
        {
            LoadHistory();
        }

        private void BtnCopyOriginal_Click(
            object? sender,
            EventArgs e)
        {
            HistoryEntry? selectedEntry =
                GetSelectedEntry();

            if (selectedEntry == null)
            {
                return;
            }

            CopyTextToClipboard(
                selectedEntry.OriginalText,
                "تم نسخ النص الأصلي.");
        }

        private void BtnCopyConverted_Click(
            object? sender,
            EventArgs e)
        {
            HistoryEntry? selectedEntry =
                GetSelectedEntry();

            if (selectedEntry == null)
            {
                return;
            }

            CopyTextToClipboard(
                selectedEntry.ConvertedText,
                "تم نسخ النص المصحح.");
        }

        private void CopyTextToClipboard(
            string text,
            string successMessage)
        {
            if (string.IsNullOrEmpty(
                    text))
            {
                ShowStatus(
                    "لا يوجد نص لنسخه.",
                    StatusType.Error);

                return;
            }

            try
            {
                Clipboard.SetText(
                    text);

                ShowStatus(
                    successMessage,
                    StatusType.Success);
            }
            catch (Exception exception)
            {
                ShowStatus(
                    $"تعذر الوصول إلى الحافظة: {exception.Message}",
                    StatusType.Error);
            }
        }

        private void BtnDeleteSelected_Click(
            object? sender,
            EventArgs e)
        {
            HistoryEntry? selectedEntry =
                GetSelectedEntry();

            if (selectedEntry == null)
            {
                return;
            }

            DialogResult confirmation =
                MessageBox.Show(
                    this,
                    "هل تريد حذف العملية المحددة من السجل؟",
                    "حذف عملية",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

            if (confirmation !=
                DialogResult.Yes)
            {
                return;
            }

            if (!_historyService.TryDelete(
                    selectedEntry.Id,
                    out bool wasDeleted,
                    out string errorMessage))
            {
                ShowStatus(
                    $"تعذر حذف العملية: {errorMessage}",
                    StatusType.Error);

                return;
            }

            if (!wasDeleted)
            {
                ShowStatus(
                    "لم يتم العثور على العملية المحددة.",
                    StatusType.Error);

                LoadHistory();
                return;
            }

            _allEntries.RemoveAll(
                entry =>
                    entry.Id ==
                    selectedEntry.Id);

            ApplySearchFilter();

            ShowStatus(
                "تم حذف العملية المحددة.",
                StatusType.Success);
        }

        private void BtnClearHistory_Click(
            object? sender,
            EventArgs e)
        {
            if (_allEntries.Count == 0)
            {
                ShowStatus(
                    "السجل فارغ بالفعل.",
                    StatusType.Information);

                return;
            }

            DialogResult confirmation =
                MessageBox.Show(
                    this,
                    "سيتم حذف جميع عمليات التحويل نهائيًا.\r\nهل تريد المتابعة؟",
                    "مسح السجل بالكامل",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

            if (confirmation !=
                DialogResult.Yes)
            {
                return;
            }

            if (!_historyService.TryClear(
                    out string errorMessage))
            {
                ShowStatus(
                    $"تعذر مسح السجل: {errorMessage}",
                    StatusType.Error);

                return;
            }

            _allEntries.Clear();
            ApplySearchFilter();

            ShowStatus(
                "تم مسح سجل التحويلات بالكامل.",
                StatusType.Success);
        }

        private void BtnClose_Click(
            object? sender,
            EventArgs e)
        {
            Close();
        }

        private static string GetModeDisplayText(
            string conversionMode)
        {
            if (string.Equals(
                    conversionMode,
                    "EnglishToArabic",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "إنجليزي إلى عربي";
            }

            if (string.Equals(
                    conversionMode,
                    "ArabicToEnglish",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "عربي إلى إنجليزي";
            }

            return "اكتشاف تلقائي";
        }

        private static string GetSourceDisplayText(
            string source)
        {
            if (string.Equals(
                    source,
                    "Manual",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "يدوي";
            }

            if (string.Equals(
                    source,
                    "Instant",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "فوري";
            }

            if (string.Equals(
                    source,
                    "GlobalHotkey",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "اختصار عام";
            }

            if (string.Equals(
                    source,
                    "TypedText",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "تصحيح ما كُتب";
            }

            if (string.Equals(
                    source,
                    "AutoCorrect",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "تصحيح تلقائي";
            }

            return "الواجهة الرئيسية";
        }

        private void ShowStatus(
            string message,
            StatusType statusType)
        {
            lblStatus.Text =
                message;

            lblStatus.ForeColor =
                statusType switch
                {
                    StatusType.Success =>
                        Color.FromArgb(
                            22,
                            163,
                            74),

                    StatusType.Error =>
                        Color.FromArgb(
                            220,
                            38,
                            38),

                    StatusType.Information =>
                        Color.FromArgb(
                            37,
                            99,
                            235),

                    _ =>
                        Color.FromArgb(
                            100,
                            116,
                            139)
                };
        }

        private void HistoryForm_FormClosed(
            object? sender,
            FormClosedEventArgs e)
        {
            txtSearch.TextChanged -=
                TxtSearch_TextChanged;

            dgvHistory.SelectionChanged -=
                DgvHistory_SelectionChanged;

            btnRefresh.Click -=
                BtnRefresh_Click;

            btnCopyOriginal.Click -=
                BtnCopyOriginal_Click;

            btnCopyConverted.Click -=
                BtnCopyConverted_Click;

            btnDeleteSelected.Click -=
                BtnDeleteSelected_Click;

            btnClearHistory.Click -=
                BtnClearHistory_Click;

            btnClose.Click -=
                BtnClose_Click;

            Shown -=
                HistoryForm_Shown;

            FormClosed -=
                HistoryForm_FormClosed;
        }

        private enum StatusType
        {
            Normal,
            Success,
            Error,
            Information
        }
    }
}