using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiftahFix.Forms;
using MiftahFix.Models;
using MiftahFix.Services;

namespace MiftahFix
{
    public partial class Form1 : Form
    {
        private const int MainHotkeyId = 1001;

        private readonly KeyboardLayoutConverter
            _converter;

        private readonly SelectedTextReplacementService
            _selectedTextReplacementService;

        private readonly SettingsService
            _settingsService;

        private readonly StartupManagerService
            _startupManagerService;

        private readonly HistoryService
            _historyService;

        private readonly ApplicationSettings
            _settings;

        private GlobalHotkeyManager?
            _globalHotkeyManager;

        private TrayIconService?
            _trayIconService;

        private HistoryForm?
            _historyForm;

        private readonly System.Windows.Forms.Timer
            _instantHistoryTimer;

        private string _pendingHistoryOriginalText =
            string.Empty;

        private string _pendingHistoryConvertedText =
            string.Empty;

        private string _pendingHistoryMode =
            "Automatic";

        private string _lastHistoryOriginalText =
            string.Empty;

        private string _lastHistoryConvertedText =
            string.Empty;

        private string _lastHistoryMode =
            string.Empty;

        private string _lastHistorySource =
            string.Empty;

        private bool _isInternalUpdate;
        private bool _isLoadingSettings;
        private bool _isHotkeyRegistered;
        private bool _isApplicationExiting;
        private bool _hasShownTrayNotification;

        public Form1()
            : this(
                new SettingsService(),
                null)
        {
        }

        public Form1(
            SettingsService settingsService,
            ApplicationSettings? settings)
        {
            InitializeComponent();
            InitializeModernInterface();
            InitializeModernButtonStyles();
            InitializeAdvancedExperience();

            _settingsService =
                settingsService ??
                throw new ArgumentNullException(
                    nameof(settingsService));

            _settings =
                settings ??
                _settingsService.Load();

            _settings.Normalize();

            _startupManagerService =
                new StartupManagerService();

            _historyService =
                new HistoryService();

            _instantHistoryTimer =
                new System.Windows.Forms.Timer
                {
                    Interval = 900
                };

            _instantHistoryTimer.Tick +=
                InstantHistoryTimer_Tick;

            _converter =
                new KeyboardLayoutConverter();

            _selectedTextReplacementService =
                new SelectedTextReplacementService(
                    _converter);

            InitializeApplication();
            InitializeSmartTyping();
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

            btnSettings.Click +=
                BtnSettings_Click;

            btnHistory.Click +=
                BtnHistory_Click;

            Shown +=
                Form1_Shown;

            FormClosing +=
                Form1_FormClosing;

            FormClosed +=
                Form1_FormClosed;

            Resize +=
                Form1_Resize;

            ApplySavedSettings();

            UpdateInputStatistics();
            UpdateOutputStatistics();
            UpdateTextDirections();
            UpdateShortcutInformation();

            ShowStatus(
                "جاري تجهيز البرنامج...",
                StatusType.Information);

            txtInput.Focus();
        }

        private void ApplySavedSettings()
        {
            _isLoadingSettings = true;
            _isInternalUpdate = true;

            try
            {
                cmbConversionMode.SelectedIndex =
                    GetConversionModeIndex(
                        _settings.Conversion.Mode);

                chkInstantConversion.Checked =
                    _settings.Conversion
                        .InstantConversion;
            }
            finally
            {
                _isInternalUpdate = false;
                _isLoadingSettings = false;
            }
        }

        private void Form1_Shown(
            object? sender,
            EventArgs e)
        {
            ApplyWindowsStartupSetting(
                showError: true);

            RegisterMainGlobalHotkey();
            InitializeTrayIcon();
            CheckForUpdatesOnStartup();
            ShowWelcomeIfFirstRun();

            if (_settings.General.StartMinimized)
            {
                BeginInvoke(
                    new Action(
                        HideWindowToTray));
            }
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

                ConnectTrayExtras();

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
                _settings.General
                    .ShowStartupNotification &&
                _trayIconService != null)
            {
                _trayIconService.ShowBalloon(
                    "MiftahFix",
                    $"يستمر البرنامج بالعمل في الخلفية. استخدم {GetHotkeyDisplayText()} لتصحيح ما كتبته أو النص المحدد.",
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
                if (!TryGetConfiguredHotkey(
                        out Keys key))
                {
                    _settings.Hotkey
                        .ResetToDefault();

                    SaveSettings(
                        showError: false);

                    key = Keys.Space;
                }

                _globalHotkeyManager?.Dispose();

                _globalHotkeyManager =
                    new GlobalHotkeyManager(
                        MainHotkeyId);

                _globalHotkeyManager.HotkeyPressed +=
                    GlobalHotkeyManager_HotkeyPressed;

                _isHotkeyRegistered =
                    _globalHotkeyManager.Register(
                        key,
                        control:
                            _settings.Hotkey.Control,
                        shift:
                            _settings.Hotkey.Shift,
                        alt:
                            _settings.Hotkey.Alt,
                        windows:
                            _settings.Hotkey.Windows);

                UpdateShortcutInformation();
                UpdateTrackerHotkey();

                if (_isHotkeyRegistered)
                {
                    ShowStatus(
                        $"الاختصار العام {GetHotkeyDisplayText()} جاهز.",
                        StatusType.Success);
                }
                else
                {
                    ShowStatus(
                        $"تعذر تسجيل الاختصار {GetHotkeyDisplayText()}. قد يكون مستخدمًا في برنامج آخر.",
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

        private bool TryGetConfiguredHotkey(
            out Keys key)
        {
            string keyName =
                _settings.Hotkey.Key;

            if (Enum.TryParse(
                    keyName,
                    ignoreCase: true,
                    out key))
            {
                Keys keyCode =
                    key & Keys.KeyCode;

                if (keyCode != Keys.None &&
                    keyCode != Keys.ControlKey &&
                    keyCode != Keys.ShiftKey &&
                    keyCode != Keys.Menu &&
                    keyCode != Keys.LWin &&
                    keyCode != Keys.RWin)
                {
                    key = keyCode;
                    return true;
                }
            }

            key = Keys.Space;
            return false;
        }

        private string GetHotkeyDisplayText()
        {
            string displayText =
                string.Empty;

            if (_settings.Hotkey.Control)
            {
                displayText += "Ctrl + ";
            }

            if (_settings.Hotkey.Shift)
            {
                displayText += "Shift + ";
            }

            if (_settings.Hotkey.Alt)
            {
                displayText += "Alt + ";
            }

            if (_settings.Hotkey.Windows)
            {
                displayText += "Win + ";
            }

            string keyText =
                GetConfiguredKeyDisplayText();

            return displayText + keyText;
        }

        private string GetConfiguredKeyDisplayText()
        {
            if (!TryGetConfiguredHotkey(
                    out Keys key))
            {
                return "Space";
            }

            return key switch
            {
                Keys.Space => "Space",
                Keys.Return => "Enter",
                Keys.Escape => "Esc",
                Keys.Back => "Backspace",
                Keys.Delete => "Delete",
                Keys.Insert => "Insert",
                Keys.Prior => "Page Up",
                Keys.Next => "Page Down",
                _ => key.ToString()
            };
        }

        private void UpdateShortcutInformation()
        {
            lblShortcut.Text =
                $"داخل الأداة: Ctrl + Enter    |    التصحيح العام: {GetHotkeyDisplayText()}";
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

            if (_isPaused)
            {
                ShowTrayNotification(
                    "التصحيح متوقف مؤقتاً. استأنفه من أيقونة البرنامج بجانب الساعة.");

                return;
            }

            _ = HandleMainGlobalHotkeyAsync();
        }

        private async Task
            HandleMainGlobalHotkeyAsync()
        {
            if (_selectedTextReplacementService
                    .IsProcessing ||
                _isTypedConversionRunning)
            {
                return;
            }

            // إذا كان المستخدم قد كتب للتو (بدون تحديد نص) نصحح ما كتبه مباشرة.
            if (HasTypedTextToConvert())
            {
                bool converted =
                    await ConvertTypedTextAsync(
                        "TypedText",
                        triggeredByHotkey: true);

                if (converted)
                {
                    return;
                }
            }

            ShowStatus(
                "جاري تصحيح النص المحدد...",
                StatusType.Information);

            TryGetConfiguredHotkey(
                out Keys hotkeyKey);

            ConversionDirection direction =
                GetSelectedConversionDirection();

            IntPtr targetWindow =
                NativeInput.GetForegroundWindowHandle();

            if (IsBlockedByElevation(targetWindow))
            {
                ShowStatus(
                    ElevationMessage,
                    StatusType.Error);

                ShowTrayNotification(
                    ElevationMessage,
                    icon: ToolTipIcon.Warning,
                    timeoutMilliseconds: 5000);

                return;
            }

            SelectedTextReplacementResult result =
                await _selectedTextReplacementService
                    .ReplaceSelectedTextAsync(
                        direction,
                        hotkeyKey);

            if (result.IsSuccess)
            {
                SwitchLayoutAfterSelectionConversion(
                    result.ConvertedText,
                    targetWindow);

                RecordHistory(
                    result.OriginalText,
                    result.ConvertedText,
                    GetConversionModeName(),
                    "GlobalHotkey");

                ShowStatus(
                    result.Message,
                    StatusType.Success);

                return;
            }

            ShowStatus(
                result.Message,
                StatusType.Error);

            // البرنامج يعمل في الخلفية غالباً، فنُظهر سبب الفشل كإشعار أيضاً.
            if (!Visible)
            {
                ShowTrayNotification(
                    result.Message,
                    icon: ToolTipIcon.Warning);
            }
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

            _instantHistoryTimer.Stop();
            _instantHistoryTimer.Tick -=
                InstantHistoryTimer_Tick;

            btnSettings.Click -=
                BtnSettings_Click;

            btnHistory.Click -=
                BtnHistory_Click;

            if (_historyForm != null)
            {
                _historyForm.FormClosed -=
                    HistoryForm_FormClosed;

                if (!_historyForm.IsDisposed)
                {
                    _historyForm.Close();
                }

                _historyForm = null;
            }

            if (_trayIconService != null)
            {
                _trayIconService.ShowRequested -=
                    TrayIconService_ShowRequested;

                _trayIconService.ExitRequested -=
                    TrayIconService_ExitRequested;

                DisconnectTrayExtras();

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
                CancelPendingInstantHistory();

                RecordHistory(
                    inputText,
                    convertedText,
                    GetConversionModeName(),
                    "Manual");

                ShowStatus(
                    "تم تحويل النص بنجاح.",
                    StatusType.Success);
            }
            else
            {
                ScheduleInstantHistory(
                    inputText,
                    convertedText,
                    GetConversionModeName());
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

        private static int GetConversionModeIndex(
            string mode)
        {
            if (string.Equals(
                    mode,
                    "EnglishToArabic",
                    StringComparison.OrdinalIgnoreCase))
            {
                return 1;
            }

            if (string.Equals(
                    mode,
                    "ArabicToEnglish",
                    StringComparison.OrdinalIgnoreCase))
            {
                return 2;
            }

            return 0;
        }

        private string GetConversionModeName()
        {
            return cmbConversionMode
                .SelectedIndex switch
            {
                1 => "EnglishToArabic",
                2 => "ArabicToEnglish",
                _ => "Automatic"
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
                    CancelPendingInstantHistory();

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

            if (!_isLoadingSettings)
            {
                _settings.Conversion.Mode =
                    GetConversionModeName();

                SaveSettings(
                    showError: true);
            }

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
            if (!_isLoadingSettings)
            {
                _settings.Conversion
                    .InstantConversion =
                    chkInstantConversion.Checked;

                SaveSettings(
                    showError: true);
            }

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
                CancelPendingInstantHistory();

                ShowStatus(
                    "تم إيقاف التحويل الفوري.",
                    StatusType.Information);
            }
        }


        private void BtnHistory_Click(
            object? sender,
            EventArgs e)
        {
            OpenHistoryWindow();
        }

        private void OpenHistoryWindow()
        {
            if (_historyForm != null &&
                !_historyForm.IsDisposed)
            {
                if (!_historyForm.Visible)
                {
                    _historyForm.Show(
                        this);
                }

                if (_historyForm.WindowState ==
                    FormWindowState.Minimized)
                {
                    _historyForm.WindowState =
                        FormWindowState.Normal;
                }

                _historyForm.Activate();
                _historyForm.BringToFront();

                return;
            }

            _historyForm =
                new HistoryForm(
                    _historyService);

            _historyForm.FormClosed +=
                HistoryForm_FormClosed;

            _historyForm.Show(
                this);
        }

        private void HistoryForm_FormClosed(
            object? sender,
            FormClosedEventArgs e)
        {
            if (_historyForm != null)
            {
                _historyForm.FormClosed -=
                    HistoryForm_FormClosed;

                _historyForm.Dispose();
                _historyForm = null;
            }

            if (!IsDisposed &&
                !Disposing &&
                Visible)
            {
                Activate();
                BringToFront();
            }
        }

        private void BtnSettings_Click(
            object? sender,
            EventArgs e)
        {
            using SettingsForm settingsForm =
                new SettingsForm(
                    _settings);

            DialogResult result =
                settingsForm.ShowDialog(
                    this);

            if (result !=
                DialogResult.OK)
            {
                return;
            }

            ApplicationSettings updatedSettings =
                settingsForm.UpdatedSettings;

            updatedSettings.Normalize();

            if (!_settingsService.TrySave(
                    updatedSettings,
                    out string errorMessage))
            {
                ShowStatus(
                    $"تعذر حفظ الإعدادات: {errorMessage}",
                    StatusType.Error);

                MessageBox.Show(
                    this,
                    $"تعذر حفظ الإعدادات.{Environment.NewLine}{Environment.NewLine}{errorMessage}",
                    "MiftahFix",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            CopySettings(
                updatedSettings,
                _settings);

            ApplySavedSettings();

            ApplyWindowsStartupSetting(
                showError: true);

            if (_settings.History.EnableHistory)
            {
                _historyService.ApplyMaximumItems(
                    _settings.History.MaxItems);
            }
            else
            {
                CancelPendingInstantHistory();
            }

            ReRegisterMainGlobalHotkey();
            UpdateShortcutInformation();
            UpdateTrackerHotkey();
            ApplySmartTypingSettings();

            ShowStatus(
                "تم حفظ الإعدادات وتطبيقها مباشرة.",
                StatusType.Success);
        }

        private void ApplyWindowsStartupSetting(
            bool showError)
        {
            if (_startupManagerService.TrySetEnabled(
                    _settings.General.RunWithWindows,
                    out string errorMessage))
            {
                return;
            }

            if (!showError)
            {
                return;
            }

            string actionText =
                _settings.General.RunWithWindows
                    ? "تفعيل التشغيل مع Windows"
                    : "إلغاء التشغيل مع Windows";

            ShowStatus(
                $"تعذر {actionText}: {errorMessage}",
                StatusType.Error);

            MessageBox.Show(
                this,
                $"تعذر {actionText}.{Environment.NewLine}{Environment.NewLine}{errorMessage}",
                "MiftahFix",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        private void ReRegisterMainGlobalHotkey()
        {
            if (_globalHotkeyManager != null)
            {
                _globalHotkeyManager.HotkeyPressed -=
                    GlobalHotkeyManager_HotkeyPressed;

                _globalHotkeyManager.Dispose();
                _globalHotkeyManager = null;
            }

            _isHotkeyRegistered = false;

            RegisterMainGlobalHotkey();
        }

        private static void CopySettings(
            ApplicationSettings source,
            ApplicationSettings destination)
        {
            destination.General.RunWithWindows =
                source.General.RunWithWindows;

            destination.General.StartMinimized =
                source.General.StartMinimized;

            destination.General.ShowStartupNotification =
                source.General.ShowStartupNotification;

            destination.Hotkey.Control =
                source.Hotkey.Control;

            destination.Hotkey.Shift =
                source.Hotkey.Shift;

            destination.Hotkey.Alt =
                source.Hotkey.Alt;

            destination.Hotkey.Windows =
                source.Hotkey.Windows;

            destination.Hotkey.Key =
                source.Hotkey.Key;

            destination.Conversion.InstantConversion =
                source.Conversion.InstantConversion;

            destination.Conversion.Mode =
                source.Conversion.Mode;

            destination.Conversion.ConvertTypedText =
                source.Conversion.ConvertTypedText;

            destination.Conversion.SwitchKeyboardLayout =
                source.Conversion.SwitchKeyboardLayout;

            destination.Conversion.TypingDetection =
                source.Conversion.TypingDetection;

            destination.History.EnableHistory =
                source.History.EnableHistory;

            destination.History.MaxItems =
                source.History.MaxItems;

            destination.Advanced.CheckForUpdates =
                source.Advanced.CheckForUpdates;

            destination.Normalize();
        }


        private void ScheduleInstantHistory(
            string originalText,
            string convertedText,
            string conversionMode)
        {
            if (!_settings.History.EnableHistory)
            {
                CancelPendingInstantHistory();
                return;
            }

            if (string.IsNullOrWhiteSpace(
                    originalText) ||
                string.IsNullOrWhiteSpace(
                    convertedText) ||
                string.Equals(
                    originalText,
                    convertedText,
                    StringComparison.Ordinal))
            {
                CancelPendingInstantHistory();
                return;
            }

            _pendingHistoryOriginalText =
                originalText;

            _pendingHistoryConvertedText =
                convertedText;

            _pendingHistoryMode =
                conversionMode;

            _instantHistoryTimer.Stop();
            _instantHistoryTimer.Start();
        }

        private void InstantHistoryTimer_Tick(
            object? sender,
            EventArgs e)
        {
            _instantHistoryTimer.Stop();

            string originalText =
                _pendingHistoryOriginalText;

            string convertedText =
                _pendingHistoryConvertedText;

            string conversionMode =
                _pendingHistoryMode;

            ClearPendingHistoryValues();

            RecordHistory(
                originalText,
                convertedText,
                conversionMode,
                "Instant");
        }

        private void CancelPendingInstantHistory()
        {
            _instantHistoryTimer.Stop();

            ClearPendingHistoryValues();
        }

        private void ClearPendingHistoryValues()
        {
            _pendingHistoryOriginalText =
                string.Empty;

            _pendingHistoryConvertedText =
                string.Empty;

            _pendingHistoryMode =
                "Automatic";
        }

        private void RecordHistory(
            string originalText,
            string convertedText,
            string conversionMode,
            string source)
        {
            if (!_settings.History.EnableHistory)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(
                    originalText) ||
                string.IsNullOrWhiteSpace(
                    convertedText))
            {
                return;
            }

            if (string.Equals(
                    originalText,
                    convertedText,
                    StringComparison.Ordinal))
            {
                return;
            }

            if (IsDuplicateHistoryEntry(
                    originalText,
                    convertedText,
                    conversionMode,
                    source))
            {
                return;
            }

            HistoryEntry entry =
                HistoryEntry.Create(
                    originalText,
                    convertedText,
                    conversionMode,
                    source);

            if (!_historyService.TryAdd(
                    entry,
                    _settings.History.MaxItems,
                    out HistoryEntry? savedEntry,
                    out string errorMessage))
            {
                ShowStatus(
                    $"تم التحويل، لكن تعذر حفظ السجل: {errorMessage}",
                    StatusType.Error);

                return;
            }

            if (savedEntry == null)
            {
                return;
            }

            _lastHistoryOriginalText =
                savedEntry.OriginalText;

            _lastHistoryConvertedText =
                savedEntry.ConvertedText;

            _lastHistoryMode =
                savedEntry.ConversionMode;

            _lastHistorySource =
                savedEntry.Source;
        }

        private bool IsDuplicateHistoryEntry(
            string originalText,
            string convertedText,
            string conversionMode,
            string source)
        {
            return string.Equals(
                       _lastHistoryOriginalText,
                       originalText,
                       StringComparison.Ordinal) &&
                   string.Equals(
                       _lastHistoryConvertedText,
                       convertedText,
                       StringComparison.Ordinal) &&
                   string.Equals(
                       _lastHistoryMode,
                       conversionMode,
                       StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(
                       _lastHistorySource,
                       source,
                       StringComparison.OrdinalIgnoreCase);
        }

        private void SaveSettings(
            bool showError)
        {
            if (_settingsService.TrySave(
                    _settings,
                    out string errorMessage))
            {
                return;
            }

            if (showError)
            {
                ShowStatus(
                    $"تعذر حفظ الإعدادات: {errorMessage}",
                    StatusType.Error);
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
            CancelPendingInstantHistory();

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

            _settings.Conversion.Mode =
                GetConversionModeName();

            SaveSettings(
                showError: true);

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

            pnlStatusIndicator.BackColor =
                statusType switch
                {
                    StatusType.Success =>
                        Color.FromArgb(
                            34,
                            197,
                            94),

                    StatusType.Error =>
                        Color.FromArgb(
                            239,
                            68,
                            68),

                    StatusType.Information =>
                        Color.FromArgb(
                            59,
                            130,
                            246),

                    _ =>
                        Color.FromArgb(
                            148,
                            163,
                            184)
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


        private ToolTip? _interfaceToolTip;

        private void InitializeModernInterface()
        {
            DoubleBuffered = true;
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);

            Icon = Icon.ExtractAssociatedIcon(
                Application.ExecutablePath);

            if (Icon != null)
            {
                picApplicationIcon.Image =
                    Icon.ToBitmap();
            }

            _interfaceToolTip =
                new ToolTip
                {
                    AutoPopDelay = 6500,
                    InitialDelay = 450,
                    ReshowDelay = 150,
                    ShowAlways = true
                };

            _interfaceToolTip.SetToolTip(
                btnConvert,
                "تحويل النص الحالي. الاختصار داخل الأداة: Ctrl + Enter");

            _interfaceToolTip.SetToolTip(
                btnSwap,
                "تبديل النص الأصلي مع النص المصحح");

            _interfaceToolTip.SetToolTip(
                btnClear,
                "مسح النصين والبدء من جديد");

            _interfaceToolTip.SetToolTip(
                btnPaste,
                "لصق النص الموجود في الحافظة");

            _interfaceToolTip.SetToolTip(
                btnCopy,
                "نسخ النص المصحح إلى الحافظة");

            _interfaceToolTip.SetToolTip(
                btnHistory,
                "فتح سجل عمليات التحويل السابقة");

            _interfaceToolTip.SetToolTip(
                btnSettings,
                "فتح إعدادات البرنامج والاختصار العام");

            ConfigureInteractiveButton(
                btnConvert,
                Color.FromArgb(37, 99, 235),
                Color.FromArgb(29, 78, 216),
                Color.FromArgb(30, 64, 175));

            ConfigureInteractiveButton(
                btnSwap,
                Color.FromArgb(241, 245, 249),
                Color.FromArgb(226, 232, 240),
                Color.FromArgb(203, 213, 225));

            ConfigureInteractiveButton(
                btnClear,
                Color.FromArgb(254, 242, 242),
                Color.FromArgb(254, 226, 226),
                Color.FromArgb(254, 202, 202));

            ConfigureInteractiveButton(
                btnPaste,
                Color.FromArgb(239, 246, 255),
                Color.FromArgb(219, 234, 254),
                Color.FromArgb(191, 219, 254));

            ConfigureInteractiveButton(
                btnCopy,
                Color.FromArgb(236, 253, 245),
                Color.FromArgb(209, 250, 229),
                Color.FromArgb(167, 243, 208));

            ConfigureInteractiveButton(
                btnHistory,
                Color.FromArgb(236, 253, 245),
                Color.FromArgb(209, 250, 229),
                Color.FromArgb(167, 243, 208));

            ConfigureInteractiveButton(
                btnSettings,
                Color.FromArgb(238, 242, 255),
                Color.FromArgb(224, 231, 255),
                Color.FromArgb(199, 210, 254));

            ApplyRoundedRegion(
                pnlHeaderContent,
                22);

            ApplyRoundedRegion(
                pnlControlBar,
                18);

            ApplyRoundedRegion(
                pnlInputCard,
                18);

            ApplyRoundedRegion(
                pnlOutputCard,
                18);

            ApplyRoundedRegion(
                picApplicationIcon,
                18);

            pnlHeaderContent.Resize +=
                (_, _) => ApplyRoundedRegion(
                    pnlHeaderContent,
                    22);

            pnlControlBar.Resize +=
                (_, _) => ApplyRoundedRegion(
                    pnlControlBar,
                    18);

            pnlInputCard.Resize +=
                (_, _) => ApplyRoundedRegion(
                    pnlInputCard,
                    18);

            pnlOutputCard.Resize +=
                (_, _) => ApplyRoundedRegion(
                    pnlOutputCard,
                    18);

            picApplicationIcon.Resize +=
                (_, _) => ApplyRoundedRegion(
                    picApplicationIcon,
                    18);
        }

        private static void ConfigureInteractiveButton(
            Button button,
            Color normalColor,
            Color hoverColor,
            Color pressedColor)
        {
            button.FlatStyle =
                FlatStyle.Flat;

            button.FlatAppearance.BorderSize =
                0;

            button.BackColor =
                normalColor;

            button.Cursor =
                Cursors.Hand;

            button.MouseEnter +=
                (_, _) =>
                    button.BackColor =
                        hoverColor;

            button.MouseLeave +=
                (_, _) =>
                    button.BackColor =
                        normalColor;

            button.MouseDown +=
                (_, _) =>
                    button.BackColor =
                        pressedColor;

            button.MouseUp +=
                (_, _) =>
                    button.BackColor =
                        button.ClientRectangle.Contains(
                            button.PointToClient(
                                Cursor.Position))
                            ? hoverColor
                            : normalColor;

            button.Resize +=
                (_, _) => ApplyRoundedRegion(
                    button,
                    12);

            ApplyRoundedRegion(
                button,
                12);
        }

        private static void ApplyRoundedRegion(
            Control control,
            int radius)
        {
            if (control.Width <= 0 ||
                control.Height <= 0)
            {
                return;
            }

            int diameter =
                radius * 2;

            Rectangle bounds =
                new Rectangle(
                    0,
                    0,
                    control.Width,
                    control.Height);

            using GraphicsPath path =
                new GraphicsPath();

            path.AddArc(
                bounds.Left,
                bounds.Top,
                diameter,
                diameter,
                180,
                90);

            path.AddArc(
                bounds.Right - diameter,
                bounds.Top,
                diameter,
                diameter,
                270,
                90);

            path.AddArc(
                bounds.Right - diameter,
                bounds.Bottom - diameter,
                diameter,
                diameter,
                0,
                90);

            path.AddArc(
                bounds.Left,
                bounds.Bottom - diameter,
                diameter,
                diameter,
                90,
                90);

            path.CloseFigure();

            Region? oldRegion =
                control.Region;

            control.Region =
                new Region(
                    path);

            oldRegion?.Dispose();
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