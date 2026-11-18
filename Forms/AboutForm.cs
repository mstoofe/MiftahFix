using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiftahFix.Models;

namespace MiftahFix.Forms
{
    public sealed class AboutForm : Form
    {
        private static readonly Color DarkBlue = Color.FromArgb(15, 23, 42);
        private static readonly Color MutedText = Color.FromArgb(71, 85, 105);
        private static readonly Color AccentBlue = Color.FromArgb(37, 99, 235);

        private readonly PictureBox _applicationIcon;
        private readonly Label _versionValue;

        public AboutForm()
            : this(null)
        {
        }

        public AboutForm(
            Func<IWin32Window, Task>? checkForUpdates)
        {
            Text = "حول MiftahFix";
            ClientSize = new Size(660, 640);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            Font = new Font("Segoe UI", 9F);
            BackColor = Color.White;

            // ===== الرأس =====
            Panel header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 160,
                BackColor = DarkBlue
            };

            Label appName = new Label
            {
                AutoSize = true,
                Text = AppInfo.ApplicationName,
                Font = new Font("Segoe UI", 23F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 30)
            };

            Label tagline = new Label
            {
                AutoSize = true,
                Text = "تصحيح النصوص المكتوبة بلغة لوحة مفاتيح خاطئة",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(203, 213, 225),
                Location = new Point(32, 90)
            };

            Label developerLine = new Label
            {
                AutoSize = true,
                Text = $"تطوير: {AppInfo.DeveloperDisplayName}",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(147, 197, 253),
                Location = new Point(32, 118)
            };

            _applicationIcon = new PictureBox
            {
                BackColor = Color.FromArgb(30, 41, 59),
                Location = new Point(520, 27),
                Size = new Size(106, 106),
                Padding = new Padding(12),
                SizeMode = PictureBoxSizeMode.Zoom
            };

            header.Controls.Add(appName);
            header.Controls.Add(tagline);
            header.Controls.Add(developerLine);
            header.Controls.Add(_applicationIcon);

            // ===== المحتوى =====
            TableLayoutPanel body = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(30, 18, 30, 18),
                ColumnCount = 1,
                RowCount = 6
            };

            body.RowStyles.Add(new RowStyle(SizeType.Absolute, 66F));  // الوصف
            body.RowStyles.Add(new RowStyle(SizeType.Absolute, 136F)); // التفاصيل
            body.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));  // الروابط
            body.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));  // صنع في العراق
            body.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));  // الحقوق
            body.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));  // الأزرار

            Label description = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 10F),
                ForeColor = MutedText,
                Text =
                    "أداة لتصحيح النصوص العربية والإنجليزية المكتوبة بلغة لوحة مفاتيح خاطئة: " +
                    "تصحيح آخر ما كتبته أو النص المحدد باختصار واحد، تبديل اللغة تلقائياً، وكشف الخطأ أثناء الكتابة."
            };

            TableLayoutPanel details = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 4
            };

            details.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            details.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));

            for (int index = 0; index < 4; index++)
            {
                details.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            }

            _versionValue = CreateValueLabel("1.1.0");

            AddDetail(details, 0, "إصدار البرنامج", _versionValue);
            AddDetail(details, 1, "المطوّر", CreateValueLabel(AppInfo.DeveloperDisplayName));
            AddDetail(details, 2, "الترخيص", CreateValueLabel(AppInfo.LicenseName));
            AddDetail(
                details,
                3,
                "بيئة التشغيل",
                CreateValueLabel(
                    $".NET {Environment.Version.Major} • " +
                    (Environment.Is64BitProcess ? "Windows x64" : "Windows x86")));

            FlowLayoutPanel links = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(0, 10, 0, 0)
            };

            links.Controls.Add(CreateLink("صفحة المطوّر على فيسبوك", () => OpenUrl(AppInfo.FacebookUrl)));
            links.Controls.Add(CreateLink("الكود المصدري (GitHub)", () => OpenUrl(AppInfo.SourceCodeUrl)));
            links.Controls.Add(CreateLink("ادعم المطوّر", () => OpenUrl(AppInfo.SupportUrl)));
            links.Controls.Add(CreateLink("الترخيص", OpenLicense));

            Label madeIn = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(22, 101, 52),
                Text = AppInfo.MadeIn
            };

            Label copyright = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Text = AppInfo.CopyrightArabic + Environment.NewLine + AppInfo.CopyrightEnglish
            };

            FlowLayoutPanel buttons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(0, 12, 0, 0)
            };

            Button closeButton = CreateButton("إغلاق", AccentBlue, Color.White);
            closeButton.Click += (_, _) => Close();
            buttons.Controls.Add(closeButton);

            if (checkForUpdates != null)
            {
                Button updatesButton =
                    CreateButton(
                        "التحقق من التحديثات",
                        Color.FromArgb(241, 245, 249),
                        DarkBlue);

                updatesButton.Width = 190;

                updatesButton.Click += async (_, _) =>
                {
                    updatesButton.Enabled = false;
                    updatesButton.Text = "جاري التحقق...";

                    try
                    {
                        await checkForUpdates(this);
                    }
                    finally
                    {
                        if (!updatesButton.IsDisposed)
                        {
                            updatesButton.Enabled = true;
                            updatesButton.Text = "التحقق من التحديثات";
                        }
                    }
                };

                buttons.Controls.Add(updatesButton);
            }

            body.Controls.Add(description, 0, 0);
            body.Controls.Add(details, 0, 1);
            body.Controls.Add(links, 0, 2);
            body.Controls.Add(madeIn, 0, 3);
            body.Controls.Add(copyright, 0, 4);
            body.Controls.Add(buttons, 0, 5);

            Controls.Add(body);
            Controls.Add(header);

            AcceptButton = closeButton;
            CancelButton = closeButton;

            InitializeApplicationInformation();
        }

        private void InitializeApplicationInformation()
        {
            try
            {
                Icon = Icon.ExtractAssociatedIcon(
                    Application.ExecutablePath);

                if (Icon != null)
                {
                    _applicationIcon.Image = Icon.ToBitmap();
                }
            }
            catch
            {
            }

            Version? version =
                Assembly.GetExecutingAssembly()
                    .GetName()
                    .Version;

            if (version != null)
            {
                _versionValue.Text =
                    $"{version.Major}.{version.Minor}.{Math.Max(version.Build, 0)}";
            }
        }

        /// <summary>
        /// يفتح ملف الترخيص المرفق مع البرنامج، أو صفحته على GitHub إن لم يوجد.
        /// </summary>
        private static void OpenLicense()
        {
            string licensePath =
                Path.Combine(
                    AppContext.BaseDirectory,
                    "LICENSE.txt");

            OpenUrl(
                File.Exists(licensePath)
                    ? licensePath
                    : AppInfo.SourceCodeUrl + "/blob/main/LICENSE.txt");
        }

        private static void AddDetail(
            TableLayoutPanel table,
            int row,
            string title,
            Label value)
        {
            table.Controls.Add(CreateTitleLabel(title), 0, row);
            table.Controls.Add(value, 1, row);
        }

        private static LinkLabel CreateLink(string text, Action onClick)
        {
            LinkLabel link = new LinkLabel
            {
                AutoSize = true,
                Text = text,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                LinkColor = AccentBlue,
                ActiveLinkColor = DarkBlue,
                LinkBehavior = LinkBehavior.HoverUnderline,
                Margin = new Padding(0, 0, 18, 0)
            };

            link.LinkClicked += (_, _) => onClick();
            return link;
        }

        internal static void OpenUrl(string target)
        {
            if (string.IsNullOrWhiteSpace(target))
            {
                return;
            }

            try
            {
                Process.Start(
                    new ProcessStartInfo(target)
                    {
                        UseShellExecute = true
                    });
            }
            catch
            {
            }
        }

        private static Button CreateButton(string text, Color background, Color foreground)
        {
            Button button = new Button
            {
                BackColor = background,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = foreground,
                Size = new Size(150, 46),
                Text = text,
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 12, 0)
            };

            button.FlatAppearance.BorderSize = 0;
            return button;
        }

        private static Label CreateTitleLabel(string text)
        {
            return new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85),
                Text = text,
                TextAlign = ContentAlignment.MiddleRight
            };
        }

        private static Label CreateValueLabel(string text)
        {
            return new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 64, 175),
                Text = text,
                TextAlign = ContentAlignment.MiddleRight
            };
        }
    }
}
