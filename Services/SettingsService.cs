using System;
using System.IO;
using System.Text;
using System.Text.Json;
using MiftahFix.Models;

namespace MiftahFix.Services
{
    public sealed class SettingsService
    {
        private const string ApplicationFolderName =
            "MiftahFix";

        private const string ConfigFolderName =
            "Config";

        private const string SettingsFileName =
            "settings.json";

        private readonly JsonSerializerOptions
            _jsonOptions;

        public SettingsService()
        {
            _jsonOptions =
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNameCaseInsensitive = true
                };
        }

        public string SettingsDirectoryPath =>
            Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                ApplicationFolderName,
                ConfigFolderName);

        public string SettingsFilePath =>
            Path.Combine(
                SettingsDirectoryPath,
                SettingsFileName);

        public ApplicationSettings Load()
        {
            EnsureSettingsDirectoryExists();

            if (!File.Exists(
                    SettingsFilePath))
            {
                ApplicationSettings defaultSettings =
                    ApplicationSettings.CreateDefault();

                Save(defaultSettings);

                return defaultSettings;
            }

            try
            {
                string json =
                    File.ReadAllText(
                        SettingsFilePath,
                        Encoding.UTF8);

                ApplicationSettings? settings =
                    JsonSerializer.Deserialize<ApplicationSettings>(
                        json,
                        _jsonOptions);

                if (settings == null)
                {
                    throw new InvalidDataException(
                        "ملف الإعدادات فارغ أو غير صالح.");
                }

                settings.Normalize();

                Save(settings);

                return settings;
            }
            catch
            {
                BackupInvalidSettingsFile();

                ApplicationSettings defaultSettings =
                    ApplicationSettings.CreateDefault();

                Save(defaultSettings);

                return defaultSettings;
            }
        }

        public void Save(
            ApplicationSettings settings)
        {
            ArgumentNullException.ThrowIfNull(
                settings);

            EnsureSettingsDirectoryExists();

            settings.Normalize();

            string json =
                JsonSerializer.Serialize(
                    settings,
                    _jsonOptions);

            string temporaryFilePath =
                SettingsFilePath + ".tmp";

            File.WriteAllText(
                temporaryFilePath,
                json,
                new UTF8Encoding(
                    encoderShouldEmitUTF8Identifier: false));

            File.Move(
                temporaryFilePath,
                SettingsFilePath,
                overwrite: true);
        }

        public ApplicationSettings Reset()
        {
            ApplicationSettings defaultSettings =
                ApplicationSettings.CreateDefault();

            Save(defaultSettings);

            return defaultSettings;
        }

        public bool TrySave(
            ApplicationSettings settings,
            out string errorMessage)
        {
            try
            {
                Save(settings);

                errorMessage =
                    string.Empty;

                return true;
            }
            catch (Exception exception)
            {
                errorMessage =
                    exception.Message;

                return false;
            }
        }

        public bool TryLoad(
            out ApplicationSettings settings,
            out string errorMessage)
        {
            try
            {
                settings =
                    Load();

                errorMessage =
                    string.Empty;

                return true;
            }
            catch (Exception exception)
            {
                settings =
                    ApplicationSettings.CreateDefault();

                errorMessage =
                    exception.Message;

                return false;
            }
        }

        private void EnsureSettingsDirectoryExists()
        {
            Directory.CreateDirectory(
                SettingsDirectoryPath);
        }

        private void BackupInvalidSettingsFile()
        {
            if (!File.Exists(
                    SettingsFilePath))
            {
                return;
            }

            try
            {
                string timestamp =
                    DateTime.Now.ToString(
                        "yyyyMMdd-HHmmss");

                string backupFilePath =
                    Path.Combine(
                        SettingsDirectoryPath,
                        $"settings.invalid-{timestamp}.json");

                File.Copy(
                    SettingsFilePath,
                    backupFilePath,
                    overwrite: false);
            }
            catch
            {
                // لا نوقف تشغيل البرنامج إذا تعذر إنشاء النسخة الاحتياطية.
            }
        }
    }
}
