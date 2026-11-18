using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiftahFix.Services
{
    public sealed class TrayIconService : IDisposable
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly ContextMenuStrip _contextMenu;

        private readonly ToolStripMenuItem _showMenuItem;
        private readonly ToolStripMenuItem _pauseMenuItem;
        private readonly ToolStripMenuItem _exitMenuItem;

        private bool _isDisposed;

        public TrayIconService()
        {
            _contextMenu = new ContextMenuStrip
            {
                RightToLeft = RightToLeft.Yes,
                ShowImageMargin = false,
                Font = new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Regular,
                    GraphicsUnit.Point)
            };

            _showMenuItem = new ToolStripMenuItem
            {
                Name = "showMenuItem",
                Text = "إظهار البرنامج",
                Font = new Font(
                    "Segoe UI",
                    10F,
                    FontStyle.Bold,
                    GraphicsUnit.Point)
            };

            _pauseMenuItem = new ToolStripMenuItem
            {
                Name = "pauseMenuItem",
                Text = "إيقاف التصحيح مؤقتاً"
            };

            ToolStripSeparator separator =
                new ToolStripSeparator();

            _exitMenuItem = new ToolStripMenuItem
            {
                Name = "exitMenuItem",
                Text = "خروج"
            };

            _contextMenu.Items.Add(
                _showMenuItem);

            _contextMenu.Items.Add(
                _pauseMenuItem);

            _contextMenu.Items.Add(
                separator);

            _contextMenu.Items.Add(
                _exitMenuItem);

            _notifyIcon = new NotifyIcon
            {
                Text = "MiftahFix - مفتاح للتصحيح",
                Icon = LoadApplicationIcon(),
                ContextMenuStrip = _contextMenu,
                Visible = false
            };

            _showMenuItem.Click +=
                ShowMenuItem_Click;

            _exitMenuItem.Click +=
                ExitMenuItem_Click;

            _notifyIcon.DoubleClick +=
                NotifyIcon_DoubleClick;

            _pauseMenuItem.Click +=
                (_, _) => PauseToggleRequested?.Invoke(this, EventArgs.Empty);

            _notifyIcon.BalloonTipClicked +=
                (_, _) => BalloonClicked?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>يُطلق عند الضغط على "إيقاف مؤقت" أو "استئناف".</summary>
        public event EventHandler?
            PauseToggleRequested;

        /// <summary>يُطلق عند النقر على الإشعار.</summary>
        public event EventHandler?
            BalloonClicked;

        public void SetPaused(bool paused)
        {
            if (_isDisposed)
            {
                return;
            }

            _pauseMenuItem.Text = paused
                ? "استئناف التصحيح"
                : "إيقاف التصحيح مؤقتاً";

            _notifyIcon.Text = paused
                ? "MiftahFix - متوقف مؤقتاً"
                : "MiftahFix - مفتاح للتصحيح";
        }

        /// <summary>
        /// أيقونة البرنامج نفسه بدل أيقونة Windows العامة.
        /// </summary>
        private static Icon LoadApplicationIcon()
        {
            try
            {
                string iconPath =
                    System.IO.Path.Combine(
                        AppContext.BaseDirectory,
                        "Assets",
                        "app.ico");

                if (System.IO.File.Exists(iconPath))
                {
                    return new Icon(iconPath);
                }

                Icon? associated =
                    Icon.ExtractAssociatedIcon(
                        Application.ExecutablePath);

                if (associated != null)
                {
                    return associated;
                }
            }
            catch
            {
            }

            return SystemIcons.Application;
        }

        public event EventHandler?
            ShowRequested;

        public event EventHandler?
            ExitRequested;

        public bool IsVisible =>
            _notifyIcon.Visible;

        public void Show()
        {
            ThrowIfDisposed();

            _notifyIcon.Visible = true;
        }

        public void Hide()
        {
            if (_isDisposed)
            {
                return;
            }

            _notifyIcon.Visible = false;
        }

        public void ShowBalloon(
            string title,
            string message,
            ToolTipIcon icon =
                ToolTipIcon.Info,
            int timeoutMilliseconds = 2000)
        {
            ThrowIfDisposed();

            _notifyIcon.BalloonTipTitle =
                title;

            _notifyIcon.BalloonTipText =
                message;

            _notifyIcon.BalloonTipIcon =
                icon;

            _notifyIcon.ShowBalloonTip(
                timeoutMilliseconds);
        }

        private void ShowMenuItem_Click(
            object? sender,
            EventArgs e)
        {
            ShowRequested?.Invoke(
                this,
                EventArgs.Empty);
        }

        private void ExitMenuItem_Click(
            object? sender,
            EventArgs e)
        {
            ExitRequested?.Invoke(
                this,
                EventArgs.Empty);
        }

        private void NotifyIcon_DoubleClick(
            object? sender,
            EventArgs e)
        {
            ShowRequested?.Invoke(
                this,
                EventArgs.Empty);
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _showMenuItem.Click -=
                ShowMenuItem_Click;

            _exitMenuItem.Click -=
                ExitMenuItem_Click;

            _notifyIcon.DoubleClick -=
                NotifyIcon_DoubleClick;

            _notifyIcon.Visible = false;

            _notifyIcon.Dispose();
            _contextMenu.Dispose();

            _isDisposed = true;

            GC.SuppressFinalize(this);
        }

        private void ThrowIfDisposed()
        {
            ObjectDisposedException.ThrowIf(
                _isDisposed,
                nameof(TrayIconService));
        }
    }
}