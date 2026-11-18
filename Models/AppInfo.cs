using MiftahFix.Services;

namespace MiftahFix.Models
{
    /// <summary>
    /// معلومات المطوّر والحقوق في مكان واحد، تُستخدم في نافذة "حول" وشاشة الترحيب.
    /// </summary>
    public static class AppInfo
    {
        public const string ApplicationName = "MiftahFix";

        public const string DeveloperNameArabic = "مصطفى كامل";
        public const string DeveloperNameEnglish = "Mustafa Kamil";

        public const string DeveloperDisplayName =
            DeveloperNameArabic + " — " + DeveloperNameEnglish;

        public const string CopyrightYear = "2026";

        public const string CopyrightArabic =
            "© " + CopyrightYear + " " + DeveloperNameArabic + ". مفتوح المصدر بترخيص MIT.";

        public const string CopyrightEnglish =
            "Copyright © " + CopyrightYear + " " + DeveloperNameEnglish + ". Open source under the MIT License.";

        public const string LicenseName = "مفتوح المصدر (MIT)";

        public const string MadeIn = "صُنع بحب في العراق ❤";

        public const string FacebookUrl =
            "https://www.facebook.com/AynRutba/";

        public static string SourceCodeUrl =>
            $"https://github.com/{UpdateService.RepositoryOwner}/{UpdateService.RepositoryName}";

        /// <summary>
        /// دعم المطوّر حالياً عبر متابعة الصفحة ومشاركتها.
        /// غيّره لرابط تبرع إذا أصبح لديك واحد.
        /// </summary>
        public const string SupportUrl = FacebookUrl;
    }
}
