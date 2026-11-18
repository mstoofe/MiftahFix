using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MiftahFix.Services
{
    public sealed class StartupManagerService
    {
        private const string RunRegistryPath =
            @"Software\Microsoft\Windows\CurrentVersion\Run";

        private const string ApplicationValueName =
            "MiftahFix";

        public bool TrySetEnabled(
            bool enabled,
            out string errorMessage)
        {
            try
            {
                if (enabled)
                {
                    Enable();
                }
                else
                {
                    Disable();
                }

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

        public bool IsEnabled()
        {
            try
            {
                using RegistryKey? runKey =
                    Registry.CurrentUser.OpenSubKey(
                        RunRegistryPath,
                        writable: false);

                string? registeredCommand =
                    runKey?.GetValue(
                        ApplicationValueName)
                        as string;

                if (string.IsNullOrWhiteSpace(
                        registeredCommand))
                {
                    return false;
                }

                string expectedCommand =
                    BuildStartupCommand();

                return string.Equals(
                    NormalizeCommand(
                        registeredCommand),
                    NormalizeCommand(
                        expectedCommand),
                    StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        private static void Enable()
        {
            string executablePath =
                Application.ExecutablePath;

            if (string.IsNullOrWhiteSpace(
                    executablePath))
            {
                throw new InvalidOperationException(
                    "تعذر تحديد مسار ملف تشغيل البرنامج.");
            }

            if (!File.Exists(
                    executablePath))
            {
                throw new FileNotFoundException(
                    "ملف تشغيل البرنامج غير موجود.",
                    executablePath);
            }

            using RegistryKey runKey =
                Registry.CurrentUser.CreateSubKey(
                    RunRegistryPath,
                    writable: true)
                ?? throw new InvalidOperationException(
                    "تعذر فتح مفتاح بدء التشغيل في سجل Windows.");

            runKey.SetValue(
                ApplicationValueName,
                BuildStartupCommand(),
                RegistryValueKind.String);
        }

        private static void Disable()
        {
            using RegistryKey? runKey =
                Registry.CurrentUser.OpenSubKey(
                    RunRegistryPath,
                    writable: true);

            if (runKey == null)
            {
                return;
            }

            if (runKey.GetValue(
                    ApplicationValueName) != null)
            {
                runKey.DeleteValue(
                    ApplicationValueName,
                    throwOnMissingValue: false);
            }
        }

        private static string BuildStartupCommand()
        {
            string executablePath =
                Application.ExecutablePath;

            return $"\"{executablePath}\"";
        }

        private static string NormalizeCommand(
            string command)
        {
            return command
                .Trim()
                .Trim('"')
                .Replace(
                    '/',
                    '\\');
        }
    }
}