using System;
using System.Linq;
using System.Windows.Forms;

namespace MiftahFix.Forms
{
    /// <summary>
    /// خيارات الكتابة الذكية في تبويب التحويل:
    /// تصحيح آخر ما كُتب، تبديل لغة لوحة المفاتيح، وكشف الكتابة بلغة خاطئة.
    /// </summary>
    public partial class SettingsForm
    {
        private CheckBox chkConvertTypedText = null!;
        private CheckBox chkSwitchKeyboardLayout = null!;
        private ComboBox cmbTypingDetection = null!;
        private Label lblTypingDetection = null!;
        private Label lblSmartTypingHint = null!;

        private void CreateSmartTypingControls()
        {
            conversionLayout.SuspendLayout();

            chkConvertTypedText = new CheckBox
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                Name = "chkConvertTypedText",
                Text = "تصحيح آخر ما كتبته عند ضغط الاختصار بدون تحديد نص",
                UseVisualStyleBackColor = true
            };

            chkSwitchKeyboardLayout = new CheckBox
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                Name = "chkSwitchKeyboardLayout",
                Text = "تبديل لغة لوحة المفاتيح تلقائياً بعد التصحيح",
                UseVisualStyleBackColor = true
            };

            cmbTypingDetection = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FormattingEnabled = true,
                Margin = new Padding(3, 12, 12, 3),
                Name = "cmbTypingDetection"
            };

            cmbTypingDetection.Items.AddRange(new object[]
            {
                new TypingDetectionItem("Off", "متوقف"),
                new TypingDetectionItem("Notify", "تنبيهي فقط"),
                new TypingDetectionItem("AutoCorrect", "تصحيح تلقائي")
            });

            cmbTypingDetection.DisplayMember =
                nameof(TypingDetectionItem.DisplayName);

            lblTypingDetection = new Label
            {
                Dock = DockStyle.Fill,
                Name = "lblTypingDetection",
                Text = "كشف الكتابة بلغة خاطئة:",
                TextAlign = System.Drawing.ContentAlignment.MiddleRight
            };

            lblSmartTypingHint = new Label
            {
                Dock = DockStyle.Fill,
                Name = "lblSmartTypingHint",
                ForeColor = System.Drawing.Color.DimGray,
                Text = "تتم متابعة ما تكتبه في الذاكرة فقط لتصحيحه، ولا يُحفظ أو يُرسل إلى أي مكان.",
                TextAlign = System.Drawing.ContentAlignment.MiddleRight
            };

            int firstNewRow = conversionLayout.RowCount;

            conversionLayout.RowCount = firstNewRow + 4;
            conversionLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            conversionLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            conversionLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            conversionLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));

            conversionLayout.Controls.Add(chkConvertTypedText, 0, firstNewRow);
            conversionLayout.SetColumnSpan(chkConvertTypedText, 2);

            conversionLayout.Controls.Add(chkSwitchKeyboardLayout, 0, firstNewRow + 1);
            conversionLayout.SetColumnSpan(chkSwitchKeyboardLayout, 2);

            conversionLayout.Controls.Add(cmbTypingDetection, 0, firstNewRow + 2);
            conversionLayout.Controls.Add(lblTypingDetection, 1, firstNewRow + 2);

            conversionLayout.Controls.Add(lblSmartTypingHint, 0, firstNewRow + 3);
            conversionLayout.SetColumnSpan(lblSmartTypingHint, 2);

            conversionLayout.Height += 40 + 40 + 52 + 36;

            conversionLayout.ResumeLayout(true);

            // احتياطاً إذا كانت الشاشة صغيرة أو الخط أكبر من المعتاد.
            tabConversion.AutoScroll = true;
        }

        private void LoadSmartTypingSettings()
        {
            chkConvertTypedText.Checked =
                UpdatedSettings.Conversion.ConvertTypedText;

            chkSwitchKeyboardLayout.Checked =
                UpdatedSettings.Conversion.SwitchKeyboardLayout;

            TypingDetectionItem? item =
                cmbTypingDetection.Items
                    .OfType<TypingDetectionItem>()
                    .FirstOrDefault(candidate =>
                        string.Equals(
                            candidate.Value,
                            UpdatedSettings.Conversion.TypingDetection,
                            StringComparison.OrdinalIgnoreCase));

            cmbTypingDetection.SelectedItem =
                item ??
                cmbTypingDetection.Items[1];
        }

        private string GetSelectedTypingDetection()
        {
            return cmbTypingDetection.SelectedItem is TypingDetectionItem item
                ? item.Value
                : "Notify";
        }

        private sealed class TypingDetectionItem
        {
            public TypingDetectionItem(string value, string displayName)
            {
                Value = value;
                DisplayName = displayName;
            }

            public string Value { get; }

            public string DisplayName { get; }

            public override string ToString()
            {
                return DisplayName;
            }
        }
    }
}
