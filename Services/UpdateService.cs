using System;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MiftahFix.Services
{
    public sealed class UpdateCheckResult
    {
        public UpdateCheckResult(
            bool isUpdateAvailable,
            Version currentVersion,
            Version? latestVersion,
            string downloadPage,
            string errorMessage)
        {
            IsUpdateAvailable = isUpdateAvailable;
            CurrentVersion = currentVersion;
            LatestVersion = latestVersion;
            DownloadPage = downloadPage;
            ErrorMessage = errorMessage;
        }

        public bool IsUpdateAvailable { get; }

        public Version CurrentVersion { get; }

        public Version? LatestVersion { get; }

        public string DownloadPage { get; }

        public string ErrorMessage { get; }

        public bool HasError =>
            !string.IsNullOrEmpty(ErrorMessage);
    }

    /// <summary>
    /// يتحقق من آخر إصدار منشور في صفحة Releases على GitHub.
    /// لنشر تحديث: أنشئ Release جديداً باسم مثل v1.2.0 وارفع ملف المثبّت عليه.
    /// </summary>
    public sealed class UpdateService
    {
        /// <summary>غيّر هذين السطرين إذا نشرت البرنامج في مستودع آخر.</summary>
        public const string RepositoryOwner = "mstoofe";
        public const string RepositoryName = "MiftahFix";

        public static string ReleasesPage =>
            $"https://github.com/{RepositoryOwner}/{RepositoryName}/releases/latest";

        private static string LatestReleaseApi =>
            $"https://api.github.com/repos/{RepositoryOwner}/{RepositoryName}/releases/latest";

        public static Version CurrentVersion
        {
            get
            {
                Version? version =
                    Assembly.GetExecutingAssembly()
                        .GetName()
                        .Version;

                return version == null
                    ? new Version(1, 0, 0)
                    : new Version(
                        version.Major,
                        version.Minor,
                        Math.Max(version.Build, 0));
            }
        }

        public async Task<UpdateCheckResult> CheckAsync(
            CancellationToken cancellationToken = default)
        {
            Version current = CurrentVersion;

            try
            {
                using HttpClient client = new()
                {
                    Timeout = TimeSpan.FromSeconds(10)
                };

                client.DefaultRequestHeaders.UserAgent.ParseAdd(
                    $"MiftahFix/{current}");

                client.DefaultRequestHeaders.Accept.ParseAdd(
                    "application/vnd.github+json");

                using HttpResponseMessage response =
                    await client.GetAsync(
                        LatestReleaseApi,
                        cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    return new UpdateCheckResult(
                        false,
                        current,
                        null,
                        ReleasesPage,
                        response.StatusCode == System.Net.HttpStatusCode.NotFound
                            ? "لا يوجد إصدار منشور بعد."
                            : $"رد الخادم: {(int)response.StatusCode}");
                }

                string json =
                    await response.Content.ReadAsStringAsync(
                        cancellationToken);

                using JsonDocument document =
                    JsonDocument.Parse(json);

                JsonElement root = document.RootElement;

                string tag =
                    root.TryGetProperty("tag_name", out JsonElement tagElement)
                        ? tagElement.GetString() ?? string.Empty
                        : string.Empty;

                string page =
                    root.TryGetProperty("html_url", out JsonElement urlElement)
                        ? urlElement.GetString() ?? ReleasesPage
                        : ReleasesPage;

                if (!TryParseVersion(tag, out Version latest))
                {
                    return new UpdateCheckResult(
                        false,
                        current,
                        null,
                        page,
                        $"تعذر قراءة رقم الإصدار: {tag}");
                }

                return new UpdateCheckResult(
                    latest > current,
                    current,
                    latest,
                    page,
                    string.Empty);
            }
            catch (Exception exception) when (
                exception is HttpRequestException or
                TaskCanceledException or
                JsonException)
            {
                return new UpdateCheckResult(
                    false,
                    current,
                    null,
                    ReleasesPage,
                    "تعذر الاتصال بالإنترنت.");
            }
        }

        /// <summary>يقبل صيغاً مثل v1.2.0 أو 1.2 أو MiftahFix-1.2.0.</summary>
        internal static bool TryParseVersion(string tag, out Version version)
        {
            string text = tag.Trim();

            int start = 0;

            while (start < text.Length && !char.IsDigit(text[start]))
            {
                start++;
            }

            int end = start;

            while (end < text.Length &&
                   (char.IsDigit(text[end]) || text[end] == '.'))
            {
                end++;
            }

            string number = text.Substring(start, end - start).Trim('.');

            if (Version.TryParse(number, out Version? parsed) ||
                (int.TryParse(number, out int major) &&
                 (parsed = new Version(major, 0)) != null))
            {
                version = new Version(
                    parsed.Major,
                    parsed.Minor,
                    Math.Max(parsed.Build, 0));

                return true;
            }

            version = new Version(0, 0, 0);
            return false;
        }
    }
}
