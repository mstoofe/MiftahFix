using System;
using System.Threading;
using System.Windows.Forms;
using MiftahFix.Models;
using MiftahFix.Services;

namespace MiftahFix
{
    internal static class Program
    {
        /// <summary>
        /// نقطة الدخول الرئيسية للبرنامج.
        /// </summary>
        private const string SingleInstanceMutexName =
            @"Local\MiftahFix_SingleInstance";

        private const string ShowWindowEventName =
            @"Local\MiftahFix_ShowWindow";

        [STAThread]
        private static void Main()
        {
            // نسخة واحدة فقط: إذا كان البرنامج يعمل نُظهر نافذته بدل فتح نسخة ثانية.
            using Mutex singleInstanceMutex =
                new Mutex(
                    initiallyOwned: true,
                    SingleInstanceMutexName,
                    out bool isFirstInstance);

            if (!isFirstInstance)
            {
                try
                {
                    using EventWaitHandle showEvent =
                        EventWaitHandle.OpenExisting(
                            ShowWindowEventName);

                    showEvent.Set();
                }
                catch
                {
                }

                return;
            }

            using EventWaitHandle showWindowEvent =
                new EventWaitHandle(
                    false,
                    EventResetMode.AutoReset,
                    ShowWindowEventName);

            ApplicationConfiguration.Initialize();

            SettingsService settingsService =
                new SettingsService();

            ApplicationSettings settings;

            if (!settingsService.TryLoad(
                    out settings,
                    out string errorMessage))
            {
                MessageBox.Show(
                    "تعذر تحميل إعدادات البرنامج، لذلك سيتم استخدام الإعدادات الافتراضية.\n\n" +
                    errorMessage,
                    "MiftahFix",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            Form1 mainForm =
                new Form1(
                    settingsService,
                    settings);

            RegisteredWaitHandle showWindowWait =
                ThreadPool.RegisterWaitForSingleObject(
                    showWindowEvent,
                    (_, _) =>
                    {
                        if (!mainForm.IsDisposed &&
                            mainForm.IsHandleCreated)
                        {
                            mainForm.BeginInvoke(
                                new Action(
                                    mainForm.ShowFromAnotherInstance));
                        }
                    },
                    null,
                    Timeout.Infinite,
                    executeOnlyOnce: false);

            try
            {
                Application.Run(
                    mainForm);
            }
            finally
            {
                showWindowWait.Unregister(null);
            }
        }
    }
}
