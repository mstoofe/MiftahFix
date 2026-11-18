using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MiftahFix.Models;

namespace MiftahFix.Forms
{
    public partial class SettingsForm : Form
    {
        private readonly ApplicationSettings
            _originalSettings;

        public ApplicationSettings UpdatedSettings
        {
            get;
            private set;
        }

        public SettingsForm(
            ApplicationSettings settings)
        {
            InitializeComponent();

            _originalSettings =
                settings ??
                throw new ArgumentNullException(
                    nameof(settings));

            _originalSettings.Normalize();

            UpdatedSettings =
                CloneSettings(
                    _originalSettings);

            InitializeSettingsForm();
        }

        private void InitializeSettingsForm()
        {
            PopulateHotkeyKeys();
            PopulateConversionModes();
            CreateSmartTypingControls();
            LoadSettingsIntoControls();

            chkEnableHistory.CheckedChanged +=
                ChkEnableHistory_CheckedChanged;

            btnSave.Click +=
                BtnSave_Click;

            btnCancel.Click +=
                BtnCancel_Click;

            btnRestoreDefaults.Click +=
                BtnRestoreDefaults_Click;

            FormClosing +=
                SettingsForm_FormClosing;
        }

        private void PopulateHotkeyKeys()
        {
            cmbHotkeyKey.BeginUpdate();

            try
            {
                cmbHotkeyKey.Items.Clear();

                foreach (HotkeyKeyItem item in
                    CreateSupportedHotkeyKeys())
                {
                    cmbHotkeyKey.Items.Add(
                        item);
                }
            }
            finally
            {
                cmbHotkeyKey.EndUpdate();
            }
        }

        private static IReadOnlyList<HotkeyKeyItem>
            CreateSupportedHotkeyKeys()
        {
            List<HotkeyKeyItem> items =
                new List<HotkeyKeyItem>
                {
                    new HotkeyKeyItem(
                        Keys.Space,
                        "Space"),

                    new HotkeyKeyItem(
                        Keys.Enter,
                        "Enter"),

                    new HotkeyKeyItem(
                        Keys.Tab,
                        "Tab"),

                    new HotkeyKeyItem(
                        Keys.Escape,
                        "Esc"),

                    new HotkeyKeyItem(
                        Keys.Insert,
                        "Insert"),

                    new HotkeyKeyItem(
                        Keys.Delete,
                        "Delete"),

                    new HotkeyKeyItem(
                        Keys.Home,
                        "Home"),

                    new HotkeyKeyItem(
                        Keys.End,
                        "End"),

                    new HotkeyKeyItem(
                        Keys.PageUp,
                        "Page Up"),

                    new HotkeyKeyItem(
                        Keys.PageDown,
                        "Page Down")
                };

            for (Keys key = Keys.A;
                key <= Keys.Z;
                key++)
            {
                items.Add(
                    new HotkeyKeyItem(
                        key,
                        key.ToString()));
            }

            for (Keys key = Keys.D0;
                key <= Keys.D9;
                key++)
            {
                string number =
                    ((int)key -
                     (int)Keys.D0)
                    .ToString();

                items.Add(
                    new HotkeyKeyItem(
                        key,
                        number));
            }

            for (Keys key = Keys.F1;
                key <= Keys.F12;
                key++)
            {
                items.Add(
                    new HotkeyKeyItem(
                        key,
                        key.ToString()));
            }

            return items;
        }

        private void PopulateConversionModes()
        {
            cmbConversionMode.Items.Clear();

            cmbConversionMode.Items.Add(
                new ConversionModeItem(
                    "Automatic",
                    "تلقائي"));

            cmbConversionMode.Items.Add(
                new ConversionModeItem(
                    "EnglishToArabic",
                    "إنجليزي إلى عربي"));

            cmbConversionMode.Items.Add(
                new ConversionModeItem(
                    "ArabicToEnglish",
                    "عربي إلى إنجليزي"));
        }

        private void LoadSettingsIntoControls()
        {
            chkRunWithWindows.Checked =
                UpdatedSettings.General
                    .RunWithWindows;

            chkStartMinimized.Checked =
                UpdatedSettings.General
                    .StartMinimized;

            chkShowStartupNotification.Checked =
                UpdatedSettings.General
                    .ShowStartupNotification;

            chkHotkeyControl.Checked =
                UpdatedSettings.Hotkey.Control;

            chkHotkeyShift.Checked =
                UpdatedSettings.Hotkey.Shift;

            chkHotkeyAlt.Checked =
                UpdatedSettings.Hotkey.Alt;

            chkHotkeyWindows.Checked =
                UpdatedSettings.Hotkey.Windows;

            SelectHotkeyKey(
                UpdatedSettings.Hotkey.Key);

            chkInstantConversion.Checked =
                UpdatedSettings.Conversion
                    .InstantConversion;

            SelectConversionMode(
                UpdatedSettings.Conversion.Mode);

            LoadSmartTypingSettings();

            chkEnableHistory.Checked =
                UpdatedSettings.History
                    .EnableHistory;

            nudHistoryMaxItems.Value =
                Math.Clamp(
                    UpdatedSettings.History
                        .MaxItems,
                    (int)nudHistoryMaxItems.Minimum,
                    (int)nudHistoryMaxItems.Maximum);

            chkCheckForUpdates.Checked =
                UpdatedSettings.Advanced
                    .CheckForUpdates;

            UpdateHistoryControlsState();
        }

        private void SelectHotkeyKey(
            string keyName)
        {
            HotkeyKeyItem? matchingItem =
                cmbHotkeyKey.Items
                    .OfType<HotkeyKeyItem>()
                    .FirstOrDefault(
                        item =>
                            string.Equals(
                                item.Key.ToString(),
                                keyName,
                                StringComparison
                                    .OrdinalIgnoreCase));

            if (matchingItem != null)
            {
                cmbHotkeyKey.SelectedItem =
                    matchingItem;

                return;
            }

            cmbHotkeyKey.SelectedIndex = 0;
        }

        private void SelectConversionMode(
            string modeName)
        {
            ConversionModeItem? matchingItem =
                cmbConversionMode.Items
                    .OfType<ConversionModeItem>()
                    .FirstOrDefault(
                        item =>
                            string.Equals(
                                item.Value,
                                modeName,
                                StringComparison
                                    .OrdinalIgnoreCase));

            if (matchingItem != null)
            {
                cmbConversionMode.SelectedItem =
                    matchingItem;

                return;
            }

            cmbConversionMode.SelectedIndex = 0;
        }

        private void ChkEnableHistory_CheckedChanged(
            object? sender,
            EventArgs e)
        {
            UpdateHistoryControlsState();
        }

        private void UpdateHistoryControlsState()
        {
            lblHistoryMaxItems.Enabled =
                chkEnableHistory.Checked;

            nudHistoryMaxItems.Enabled =
                chkEnableHistory.Checked;
        }

        private void BtnSave_Click(
            object? sender,
            EventArgs e)
        {
            if (!ValidateSettings())
            {
                return;
            }

            UpdatedSettings =
                BuildSettingsFromControls();

            DialogResult =
                DialogResult.OK;

            Close();
        }

        private bool ValidateSettings()
        {
            bool hasModifier =
                chkHotkeyControl.Checked ||
                chkHotkeyShift.Checked ||
                chkHotkeyAlt.Checked ||
                chkHotkeyWindows.Checked;

            if (!hasModifier)
            {
                tabSettings.SelectedTab =
                    tabHotkey;

                MessageBox.Show(
                    this,
                    "يجب اختيار مفتاح مساعد واحد على الأقل للاختصار العام، مثل Ctrl أو Shift.",
                    "MiftahFix",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                chkHotkeyControl.Focus();

                return false;
            }

            if (cmbHotkeyKey.SelectedItem
                is not HotkeyKeyItem)
            {
                tabSettings.SelectedTab =
                    tabHotkey;

                MessageBox.Show(
                    this,
                    "اختر المفتاح الرئيسي للاختصار العام.",
                    "MiftahFix",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbHotkeyKey.Focus();

                return false;
            }

            if (cmbConversionMode.SelectedItem
                is not ConversionModeItem)
            {
                tabSettings.SelectedTab =
                    tabConversion;

                MessageBox.Show(
                    this,
                    "اختر طريقة التحويل.",
                    "MiftahFix",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbConversionMode.Focus();

                return false;
            }

            return true;
        }

        private ApplicationSettings
            BuildSettingsFromControls()
        {
            HotkeyKeyItem hotkeyKey =
                (HotkeyKeyItem)
                cmbHotkeyKey.SelectedItem!;

            ConversionModeItem conversionMode =
                (ConversionModeItem)
                cmbConversionMode.SelectedItem!;

            ApplicationSettings settings =
                new ApplicationSettings
                {
                    General =
                        new GeneralSettings
                        {
                            RunWithWindows =
                                chkRunWithWindows.Checked,

                            StartMinimized =
                                chkStartMinimized.Checked,

                            ShowStartupNotification =
                                chkShowStartupNotification.Checked
                        },

                    Hotkey =
                        new HotkeySettings
                        {
                            Control =
                                chkHotkeyControl.Checked,

                            Shift =
                                chkHotkeyShift.Checked,

                            Alt =
                                chkHotkeyAlt.Checked,

                            Windows =
                                chkHotkeyWindows.Checked,

                            Key =
                                hotkeyKey.Key.ToString()
                        },

                    Conversion =
                        new ConversionSettings
                        {
                            InstantConversion =
                                chkInstantConversion.Checked,

                            Mode =
                                conversionMode.Value,

                            ConvertTypedText =
                                chkConvertTypedText.Checked,

                            SwitchKeyboardLayout =
                                chkSwitchKeyboardLayout.Checked,

                            TypingDetection =
                                GetSelectedTypingDetection()
                        },

                    History =
                        new HistorySettings
                        {
                            EnableHistory =
                                chkEnableHistory.Checked,

                            MaxItems =
                                (int)nudHistoryMaxItems.Value
                        },

                    Advanced =
                        new AdvancedSettings
                        {
                            CheckForUpdates =
                                chkCheckForUpdates.Checked
                        }
                };

            settings.Normalize();

            return settings;
        }

        private void BtnCancel_Click(
            object? sender,
            EventArgs e)
        {
            DialogResult =
                DialogResult.Cancel;

            Close();
        }

        private void BtnRestoreDefaults_Click(
            object? sender,
            EventArgs e)
        {
            DialogResult result =
                MessageBox.Show(
                    this,
                    "هل تريد استعادة جميع الإعدادات الافتراضية داخل هذه النافذة؟ لن يتم اعتمادها إلا بعد الضغط على حفظ.",
                    "استعادة الإعدادات الافتراضية",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

            if (result !=
                DialogResult.Yes)
            {
                return;
            }

            UpdatedSettings =
                ApplicationSettings
                    .CreateDefault();

            UpdatedSettings.Normalize();

            LoadSettingsIntoControls();

            tabSettings.SelectedIndex = 0;
        }

        private void SettingsForm_FormClosing(
            object? sender,
            FormClosingEventArgs e)
        {
            if (DialogResult ==
                DialogResult.None)
            {
                DialogResult =
                    DialogResult.Cancel;
            }
        }

        private static ApplicationSettings
            CloneSettings(
                ApplicationSettings source)
        {
            return new ApplicationSettings
            {
                General =
                    new GeneralSettings
                    {
                        RunWithWindows =
                            source.General
                                .RunWithWindows,

                        StartMinimized =
                            source.General
                                .StartMinimized,

                        ShowStartupNotification =
                            source.General
                                .ShowStartupNotification
                    },

                Hotkey =
                    new HotkeySettings
                    {
                        Control =
                            source.Hotkey.Control,

                        Shift =
                            source.Hotkey.Shift,

                        Alt =
                            source.Hotkey.Alt,

                        Windows =
                            source.Hotkey.Windows,

                        Key =
                            source.Hotkey.Key
                    },

                Conversion =
                    new ConversionSettings
                    {
                        InstantConversion =
                            source.Conversion
                                .InstantConversion,

                        Mode =
                            source.Conversion.Mode,

                        ConvertTypedText =
                            source.Conversion.ConvertTypedText,

                        SwitchKeyboardLayout =
                            source.Conversion.SwitchKeyboardLayout,

                        TypingDetection =
                            source.Conversion.TypingDetection
                    },

                History =
                    new HistorySettings
                    {
                        EnableHistory =
                            source.History
                                .EnableHistory,

                        MaxItems =
                            source.History.MaxItems
                    },

                Advanced =
                    new AdvancedSettings
                    {
                        CheckForUpdates =
                            source.Advanced
                                .CheckForUpdates
                    }
            };
        }

        private sealed class HotkeyKeyItem
        {
            public Keys Key
            {
                get;
            }

            public string DisplayName
            {
                get;
            }

            public HotkeyKeyItem(
                Keys key,
                string displayName)
            {
                Key = key;
                DisplayName = displayName;
            }

            public override string ToString()
            {
                return DisplayName;
            }
        }

        private sealed class ConversionModeItem
        {
            public string Value
            {
                get;
            }

            public string DisplayName
            {
                get;
            }

            public ConversionModeItem(
                string value,
                string displayName)
            {
                Value = value;
                DisplayName = displayName;
            }

            public override string ToString()
            {
                return DisplayName;
            }
        }
    }
}