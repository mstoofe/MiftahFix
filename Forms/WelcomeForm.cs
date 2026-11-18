using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MiftahFix.Models;

namespace MiftahFix.Forms
{
    /// <summary>
    /// شاشة ترحيب تظهر مرة واحدة عند أول تشغيل لكل إصدار رئيسي،
    /// تشرح طريقة الاستخدام باختصار.
    /// </summary>
    public sealed class WelcomeForm : Form
    {
        /// <summary>
        /// غيّر هذا الرقم عندما تريد أن تظهر الشاشة من جديد بعد تحديث فيه ميزات مهمة.
        /// </summary>
        private const string WelcomeVersion = "1.1";

        private static readonly Color DarkBlue = Color.FromArgb(15, 23, 42);
        private static readonly Color AccentBlue = Color.FromArgb(37, 99, 235);

        public WelcomeForm(string hotkeyText)
        {
            Text = "مرحباً بك في MiftahFix";
            ClientSize = new Size(620, 600);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = true;
            TopMost = true;
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.White;

            try
            {
                Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            }
            catch
            {
            }

            Panel header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = DarkBlue
            };

            header.Controls.Add(new Label
            {
                AutoSize = true,
                Text = "مرحباً بك في MiftahFix",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(28, 24)
            });

            header.Controls.Add(new Label
            {
                AutoSize = true,
                Text = "لا داعي لإعادة الكتابة عندما تنسى تغيير لغة لوحة المفاتيح",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(203, 213, 225),
                Location = new Point(30, 74)
            });

            TableLayoutPanel body = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(28, 16, 28, 16),
                ColumnCount = 1,
                RowCount = 6
            };

            body.RowStyles.Add(new RowStyle(SizeType.Absolute, 88F));
            body.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            body.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            body.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            body.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            body.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            body.Controls.Add(
                CreateStep(
                    "⌨",
                    $"اكتب ثم اضغط  {hotkeyText}",
                    "إذا كتبت «hgsghl» بدل «السلام» اضغط الاختصار فيُصحَّح ما كتبته وتتبدل اللغة تلقائياً. اضغطه مرة أخرى للتراجع."),
                0,
                0);

            body.Controls.Add(
                CreateStep(
                    "🖱",
                    "أو حدد النص ثم اضغط الاختصار",
                    "يعمل في أي برنامج: المتصفح، Word، واتساب، فيسبوك..."),
                0,
                1);

            body.Controls.Add(
                CreateStep(
                    "🔔",
                    "تنبيه عند الكتابة بلغة خاطئة",
                    "يمكنك جعله يصحح تلقائياً من الإعدادات ← التحويل."),
                0,
                2);

            body.Controls.Add(
                CreateStep(
                    "🔒",
                    "خصوصيتك محفوظة",
                    "ما تكتبه يبقى في الذاكرة فقط لتصحيحه، ولا يُحفظ أو يُرسل إلى أي مكان. والبرنامج مفتوح المصدر."),
                0,
                3);

            body.Controls.Add(
                new Label
                {
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleRight,
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    Text = $"البرنامج يعمل في الخلفية من أيقونته بجانب الساعة.   •   تطوير: {AppInfo.DeveloperDisplayName}"
                },
                0,
                4);

            FlowLayoutPanel buttons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(0, 8, 0, 0)
            };

            Button startButton = new Button
            {
                BackColor = AccentBlue,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(170, 46),
                Text = "ابدأ الاستخدام",
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 12, 0)
            };

            startButton.FlatAppearance.BorderSize = 0;
            startButton.Click += (_, _) => Close();

            LinkLabel facebookLink = new LinkLabel
            {
                AutoSize = true,
                Text = "تابع المطوّر على فيسبوك",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                LinkColor = AccentBlue,
                LinkBehavior = LinkBehavior.HoverUnderline,
                Margin = new Padding(8, 14, 0, 0)
            };

            facebookLink.LinkClicked += (_, _) => AboutForm.OpenUrl(AppInfo.FacebookUrl);

            buttons.Controls.Add(startButton);
            buttons.Controls.Add(facebookLink);

            body.Controls.Add(buttons, 0, 5);

            Controls.Add(body);
            Controls.Add(header);

            AcceptButton = startButton;
            CancelButton = startButton;
        }

        private static Control CreateStep(string icon, string title, string text)
        {
            TableLayoutPanel step = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                Margin = new Padding(0, 4, 0, 4)
            };

            step.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 52F));
            step.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            step.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            step.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            Label iconLabel = new Label
            {
                Dock = DockStyle.Fill,
                Text = icon,
                Font = new Font("Segoe UI Emoji", 18F),
                TextAlign = ContentAlignment.MiddleCenter
            };

            step.Controls.Add(iconLabel, 0, 0);
            step.SetRowSpan(iconLabel, 2);

            step.Controls.Add(new Label
            {
                Dock = DockStyle.Fill,
                Text = title,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = DarkBlue,
                TextAlign = ContentAlignment.MiddleRight
            }, 1, 0);

            step.Controls.Add(new Label
            {
                Dock = DockStyle.Fill,
                Text = text,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(71, 85, 105),
                TextAlign = ContentAlignment.TopRight
            }, 1, 1);

            return step;
        }

        private static string MarkerFilePath =>
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "MiftahFix",
                $"welcome-{WelcomeVersion}.seen");

        public static bool ShouldShow()
        {
            try
            {
                return !File.Exists(MarkerFilePath);
            }
            catch
            {
                return false;
            }
        }

        public static void MarkAsShown()
        {
            try
            {
                string path = MarkerFilePath;
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                File.WriteAllText(path, DateTime.Now.ToString("O"));
            }
            catch
            {
            }
        }
    }
}
