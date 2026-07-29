using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiftahFix.Services;

namespace MiftahFix
{
    public partial class Form1 : Form
    {
        private const int MainHotkeyId = 1001;

        private readonly KeyboardLayoutConverter _converter;

        private readonly SelectedTextReplacementService
            _selectedTextReplacementService;

        private GlobalHotkeyManager?
            _globalHotkeyManager;

        private TrayIconService?
            _trayIconService;

        private bool _isInternalUpdate;
        private bool _isHotkeyRegistered;
        private bool _isApplicationExiting;
        private bool _hasShownTrayNotification;

        public Form1()
        {
            InitializeComponent();

            _converter =
                new KeyboardLayoutConverter();

            _selectedTextReplacementService =
                new SelectedTextReplacementService(
                    _converter);

            InitializeApplication();
        }

        private void InitializeApplication()
        {
            txtInput.TextChanged +=
                TxtInput_TextChanged;

            txtOutput.TextChanged +=
                TxtOutput_TextChanged;

            cmbConversionMode.SelectedIndexChanged +=
                CmbConversionMode_SelectedIndexChanged;

            chkInstantConversion.CheckedChanged +=
                ChkInstantConversion_CheckedChanged;

            Shown +=
                Form1_Shown;

            FormClosing +=
                Form1_FormClosing;

            FormClosed +=
                Form1_FormClosed;

            Resize +=
                Form1_Resize;

            cmbConversionMode.SelectedIndex = 0;
            chkInstantConversion.Checked = true;

            UpdateInputStatistics();
            UpdateOutputStatistics();
            UpdateTextDirections();

            ShowStatus(
                "جاري تجهيز البرنامج...",
                StatusType.Information);

            txtInput.Focus();
        }

        private void Form1_Shown(
            object? sender,
            EventArgs e)
        {
            RegisterMainGlobalHotkey();
            InitializeTrayIcon();
        }

        private void InitializeTrayIcon()
        {
            if (_trayIconService != null)
            {
                return;
            }

            try
            {
                _trayIconService =
                    new TrayIconService();

                _trayIconService.ShowRequested +=
                    TrayIconService_ShowRequested;

                _trayIconService.ExitRequested +=
                    TrayIconService_ExitRequested;

                _trayIconService.Show();

                ShowStatus(
                    "البرنامج جاهز وأيقونة شريط النظام مفعّلة.",
                    StatusType.Success);
            }
            catch (Exception exception)
            {
                ShowStatus(
                    $"تعذر إنشاء أيقونة شريط النظام: {exception.Message}",
                    StatusType.Error);
            }
        }

        private void TrayIconService_ShowRequested(
            object? sender,
            EventArgs e)
        {
            RestoreAndActivateWindow();
        }

        private void TrayIconService_ExitRequested(
            object? sender,
            EventArgs e)
        {
            ExitApplication();
        }

        private void Form1_FormClosing(
            object? sender,
            FormClosingEventArgs e)
        {
            if (_isApplicationExiting)
            {
                return;
            }

            if (e.CloseReason ==
                CloseReason.WindowsShutDown)
            {
                _isApplicationExiting = true;
                return;
            }

            if (e.CloseReason ==
                CloseReason.TaskManagerClosing)
            {
                _isApplicationExiting = true;
                return;
            }

            e.Cancel = true;

            HideWindowToTray();
        }

        private void Form1_Resize(
            object? sender,
            EventArgs e)
        {
            if (WindowState ==
                FormWindowState.Minimized)
            {
                HideWindowToTray();
            }
        }

        private void HideWindowToTray()
        {
            Hide();

            if (!_hasShownTrayNotification &&
                _trayIconService != null)
            {
                _trayIconService.ShowBalloon(
                    "MiftahFix",
                    "يستمر البرنامج بالعمل في الخلفية. استخدم Ctrl + Shift + Space لتصحيح النص المحدد.",
                    ToolTipIcon.Info,
                    2500);

                _hasShownTrayNotification = true;
            }
        }

        private void RestoreAndActivateWindow()
        {
            if (!Visible)
            {
                Show();
            }

            WindowState =
                FormWindowState.Normal;

            Activate();
            BringToFront();

            bool originalTopMost =
                TopMost;

            TopMost = true;
            TopMost = originalTopMost;

            txtInput.Focus();
        }

        private void ExitApplication()
        {
            if (_isApplicationExiting)
            {
                return;
            }

            _isApplicationExiting = true;

            _trayIconService?.Hide();

            Close();
        }

        private void RegisterMainGlobalHotkey()
        {
            if (_isHotkeyRegistered)
            {
                return;
            }

            try
            {
                _globalHotkeyManager?.Dispose();

                _globalHotkeyManager =
                    new GlobalHotkeyManager(
                        MainHotkeyId);

                _globalHotkeyManager.HotkeyPressed +=
                    GlobalHotkeyManager_HotkeyPressed;

                _isHotkeyRegistered =
                    _globalHotkeyManager.Register(
                        Keys.Space,
                        control: true,
                        shift: true,
                        alt: false,
                        windows: false);

                if (_isHotkeyRegistered)
                {
                    ShowStatus(
                        "الاختصار العام Ctrl + Shift + Space جاهز.",
                        StatusType.Success);
                }
                else
                {
                    ShowStatus(
                        "تعذر تسجيل الاختصار. قد يكون مستخدمًا في برنامج آخر.",
                        StatusType.Error);
                }
            }
            catch (Exception exception)
            {
                _isHotkeyRegistered = false;

                ShowStatus(
                    $"خطأ في تسجيل الاختصار: {exception.Message}",
                    StatusType.Error);
            }
        }

        private void GlobalHotkeyManager_HotkeyPressed(
            object? sender,
            EventArgs e)
        {

            if (IsDisposed ||
                Disposing ||
                _isApplicationExiting)
            {
                return;
            }

            _ = HandleMainGlobalHotkeyAsync();
        }

        private async Task
            HandleMainGlobalHotkeyAsync()
        {
            
            if (_selectedTextReplacementService
                .IsProcessing)
            {
                return;
            }

            ShowStatus(
                "جاري تصحيح النص المحدد...",
                StatusType.Information);

            SelectedTextReplacementResult result =
                await _selectedTextReplacementService
                    .ReplaceSelectedTextAsync();

            if (result.IsSuccess)
            {
                ShowStatus(
                    result.Message,
                    StatusType.Success);

                return;
            }

            ShowStatus(
                result.Message,
                StatusType.Error);
        }

        private void Form1_FormClosed(
            object? sender,
            FormClosedEventArgs e)
        {
            Shown -=
                Form1_Shown;

            FormClosing -=
                Form1_FormClosing;

            FormClosed -=
                Form1_FormClosed;

            Resize -=
                Form1_Resize;

            txtInput.TextChanged -=
                TxtInput_TextChanged;

            txtOutput.TextChanged -=
                TxtOutput_TextChanged;

            cmbConversionMode.SelectedIndexChanged -=
                CmbConversionMode_SelectedIndexChanged;

            chkInstantConversion.CheckedChanged -=
                ChkInstantConversion_CheckedChanged;

            if (_trayIconService != null)
            {
                _trayIconService.ShowRequested -=
                    TrayIconService_ShowRequested;

                _trayIconService.ExitRequested -=
                    TrayIconService_ExitRequested;

                _trayIconService.Dispose();
                _trayIconService = null;
            }

            if (_globalHotkeyManager != null)
            {
                _globalHotkeyManager.HotkeyPressed -=
                    GlobalHotkeyManager_HotkeyPressed;

                _globalHotkeyManager.Dispose();
                _globalHotkeyManager = null;
            }

            _isHotkeyRegistered = false;
        }

        private void BtnConvert_Click(
            object sender,
            EventArgs e)
        {
            ConvertCurrentText(
                showMessage: true);
        }

        private void ConvertCurrentText(
            bool showMessage)
        {
            if (_isInternalUpdate)
            {
                return;
            }

            string inputText =
                txtInput.Text;

            if (string.IsNullOrEmpty(
                inputText))
            {
                _isInternalUpdate = true;

                try
                {
                    txtOutput.Clear();
                }
                finally
                {
                    _isInternalUpdate = false;
                }

                if (showMessage)
                {
                    ShowStatus(
                        "أدخل النص المراد تحويله أولاً.",
                        StatusType.Error);

                    txtInput.Focus();
                }

                return;
            }

            ConversionDirection direction =
                GetSelectedConversionDirection();

            string convertedText =
                _converter.Convert(
                    inputText,
                    direction);

            _isInternalUpdate = true;

            try
            {
                txtOutput.Text =
                    convertedText;
            }
            finally
            {
                _isInternalUpdate = false;
            }

            UpdateOutputStatistics();
            UpdateTextDirections();

            if (showMessage)
            {
                ShowStatus(
                    "تم تحويل النص بنجاح.",
                    StatusType.Success);
            }
        }

        private ConversionDirection
            GetSelectedConversionDirection()
        {
            return cmbConversionMode
                .SelectedIndex switch
            {
                1 =>
                    ConversionDirection
                        .EnglishToArabic,

                2 =>
                    ConversionDirection
                        .ArabicToEnglish,

                _ =>
                    ConversionDirection
                        .Automatic
            };
        }

        private void TxtInput_TextChanged(
            object? sender,
            EventArgs e)
        {
            if (_isInternalUpdate)
            {
                return;
            }

            UpdateInputStatistics();
            UpdateTextDirections();

            if (chkInstantConversion.Checked)
            {
                ConvertCurrentText(
                    showMessage: false);

                if (string.IsNullOrEmpty(
                    txtInput.Text))
                {
                    ShowStatus(
                        "التحويل الفوري مفعّل.",
                        StatusType.Information);
                }
                else
                {
                    ShowStatus(
                        "تم تحديث النتيجة تلقائيًا.",
                        StatusType.Success);
                }
            }
            else
            {
                ShowStatus(
                    "جاهز للتحويل.",
                    StatusType.Normal);
            }
        }

        private void TxtOutput_TextChanged(
            object? sender,
            EventArgs e)
        {
            UpdateOutputStatistics();
        }

        private void
            CmbConversionMode_SelectedIndexChanged(
                object? sender,
                EventArgs e)
        {
            UpdateTextDirections();

            if (chkInstantConversion.Checked &&
                !string.IsNullOrEmpty(
                    txtInput.Text))
            {
                ConvertCurrentText(
                    showMessage: false);

                ShowStatus(
                    "تم تحديث طريقة التحويل.",
                    StatusType.Information);
            }
        }

        private void
            ChkInstantConversion_CheckedChanged(
                object? sender,
                EventArgs e)
        {
            if (chkInstantConversion.Checked)
            {
                ConvertCurrentText(
                    showMessage: false);

                ShowStatus(
                    "تم تشغيل التحويل الفوري.",
                    StatusType.Success);
            }
            else
            {
                ShowStatus(
                    "تم إيقاف التحويل الفوري.",
                    StatusType.Information);
            }
        }

        private void BtnPaste_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                if (!Clipboard.ContainsText())
                {
                    ShowStatus(
                        "الحافظة لا تحتوي على نص.",
                        StatusType.Error);

                    return;
                }

                txtInput.Text =
                    Clipboard.GetText();

                txtInput.Focus();

                txtInput.SelectionStart =
                    txtInput.TextLength;

                ShowStatus(
                    "تم لصق النص وتحويله.",
                    StatusType.Success);
            }
            catch
            {
                ShowStatus(
                    "تعذر الوصول إلى الحافظة.",
                    StatusType.Error);
            }
        }

        private void BtnCopy_Click(
            object sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtOutput.Text))
            {
                ShowStatus(
                    "لا يوجد نص ناتج لنسخه.",
                    StatusType.Error);

                return;
            }

            try
            {
                Clipboard.SetText(
                    txtOutput.Text);

                ShowStatus(
                    "تم نسخ النص الناتج.",
                    StatusType.Success);
            }
            catch
            {
                ShowStatus(
                    "تعذر نسخ النص إلى الحافظة.",
                    StatusType.Error);
            }
        }

        private void BtnClear_Click(
            object sender,
            EventArgs e)
        {
            _isInternalUpdate = true;

            try
            {
                txtInput.Clear();
                txtOutput.Clear();
            }
            finally
            {
                _isInternalUpdate = false;
            }

            UpdateInputStatistics();
            UpdateOutputStatistics();
            UpdateTextDirections();

            ShowStatus(
                "تم مسح النصوص.",
                StatusType.Success);

            txtInput.Focus();
        }

        private void BtnSwap_Click(
            object sender,
            EventArgs e)
        {
            string inputText =
                txtInput.Text;

            string outputText =
                txtOutput.Text;

            _isInternalUpdate = true;

            try
            {
                txtInput.Text =
                    outputText;

                txtOutput.Text =
                    inputText;

                cmbConversionMode
                    .SelectedIndex =
                    cmbConversionMode
                        .SelectedIndex switch
                    {
                        1 => 2,
                        2 => 1,
                        _ => 0
                    };
            }
            finally
            {
                _isInternalUpdate = false;
            }

            UpdateInputStatistics();
            UpdateOutputStatistics();
            UpdateTextDirections();

            if (chkInstantConversion.Checked &&
                !string.IsNullOrEmpty(
                    txtInput.Text))
            {
                ConvertCurrentText(
                    showMessage: false);
            }

            ShowStatus(
                "تم تبديل النصين.",
                StatusType.Success);
        }

        private void UpdateTextDirections()
        {
            ConversionDirection direction =
                GetSelectedConversionDirection();

            if (direction ==
                    ConversionDirection.Automatic &&
                !string.IsNullOrWhiteSpace(
                    txtInput.Text))
            {
                direction =
                    _converter.DetectDirection(
                        txtInput.Text);
            }

            if (direction ==
                ConversionDirection.ArabicToEnglish)
            {
                txtInput.RightToLeft =
                    RightToLeft.Yes;

                txtOutput.RightToLeft =
                    RightToLeft.No;
            }
            else
            {
                txtInput.RightToLeft =
                    RightToLeft.No;

                txtOutput.RightToLeft =
                    RightToLeft.Yes;
            }
        }

        private void UpdateInputStatistics()
        {
            int charactersCount =
                txtInput.Text.Length;

            int wordsCount =
                CountWords(
                    txtInput.Text);

            int linesCount =
                CountLines(
                    txtInput.Text);

            lblInputStatistics.Text =
                $"الأحرف: {charactersCount:N0}    " +
                $"الكلمات: {wordsCount:N0}    " +
                $"الأسطر: {linesCount:N0}";
        }

        private void UpdateOutputStatistics()
        {
            int charactersCount =
                txtOutput.Text.Length;

            int wordsCount =
                CountWords(
                    txtOutput.Text);

            int linesCount =
                CountLines(
                    txtOutput.Text);

            lblOutputStatistics.Text =
                $"الأحرف: {charactersCount:N0}    " +
                $"الكلمات: {wordsCount:N0}    " +
                $"الأسطر: {linesCount:N0}";
        }

        private static int CountWords(
            string text)
        {
            if (string.IsNullOrWhiteSpace(
                text))
            {
                return 0;
            }

            return text.Split(
                new[]
                {
                    ' ',
                    '\r',
                    '\n',
                    '\t'
                },
                StringSplitOptions
                    .RemoveEmptyEntries).Length;
        }

        private static int CountLines(
            string text)
        {
            if (string.IsNullOrEmpty(
                text))
            {
                return 0;
            }

            return text.Split('\n').Length;
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
                            95,
                            99,
                            104)
                };
        }

        private void TxtInput_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.Control &&
                e.KeyCode ==
                    Keys.Enter)
            {
                ConvertCurrentText(
                    showMessage: true);

                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void TxtOutput_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.Control &&
                e.KeyCode ==
                    Keys.C)
            {
                BtnCopy_Click(
                    sender,
                    EventArgs.Empty);

                e.SuppressKeyPress = true;
                e.Handled = true;
            }
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