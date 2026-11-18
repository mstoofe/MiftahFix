using System;

namespace MiftahFix.Models
{
    public sealed class HistoryEntry
    {
        public Guid Id { get; set; } =
            Guid.NewGuid();

        public string OriginalText { get; set; } =
            string.Empty;

        public string ConvertedText { get; set; } =
            string.Empty;

        public string ConversionMode { get; set; } =
            "Automatic";

        public string Source { get; set; } =
            "MainWindow";

        public DateTimeOffset CreatedAt { get; set; } =
            DateTimeOffset.Now;

        public static HistoryEntry Create(
            string originalText,
            string convertedText,
            string conversionMode,
            string source = "MainWindow")
        {
            HistoryEntry entry =
                new HistoryEntry
                {
                    OriginalText =
                        originalText ?? string.Empty,

                    ConvertedText =
                        convertedText ?? string.Empty,

                    ConversionMode =
                        conversionMode ?? "Automatic",

                    Source =
                        source ?? "MainWindow",

                    CreatedAt =
                        DateTimeOffset.Now
                };

            entry.Normalize();

            return entry;
        }

        public HistoryEntry Clone()
        {
            return new HistoryEntry
            {
                Id = Id,
                OriginalText = OriginalText,
                ConvertedText = ConvertedText,
                ConversionMode = ConversionMode,
                Source = Source,
                CreatedAt = CreatedAt
            };
        }

        public void Normalize()
        {
            if (Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }

            OriginalText ??=
                string.Empty;

            ConvertedText ??=
                string.Empty;

            ConversionMode =
                NormalizeConversionMode(
                    ConversionMode);

            Source =
                string.IsNullOrWhiteSpace(
                    Source)
                    ? "MainWindow"
                    : Source.Trim();

            if (CreatedAt ==
                default)
            {
                CreatedAt =
                    DateTimeOffset.Now;
            }
        }

        private static string NormalizeConversionMode(
            string? conversionMode)
        {
            if (string.Equals(
                    conversionMode,
                    "EnglishToArabic",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "EnglishToArabic";
            }

            if (string.Equals(
                    conversionMode,
                    "ArabicToEnglish",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "ArabicToEnglish";
            }

            return "Automatic";
        }
    }
}