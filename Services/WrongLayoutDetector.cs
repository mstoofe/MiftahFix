using System;
using System.Collections.Generic;
using System.Linq;

namespace MiftahFix.Services
{
    /// <summary>
    /// يقدّر هل الكلمة المكتوبة للتو كُتبت بلغة لوحة مفاتيح خاطئة.
    /// يعتمد على قواعد بسيطة وقوائم كلمات شائعة، ويجمع النقاط عبر الكلمات المتتالية
    /// حتى لا يتدخل من كلمة واحدة مشكوك فيها.
    /// </summary>
    public sealed class WrongLayoutDetector
    {
        private static readonly HashSet<string> CommonArabicWords = new(StringComparer.Ordinal)
        {
            "في", "من", "على", "الى", "إلى", "عن", "مع", "هذا", "هذه", "ذلك", "تلك",
            "كان", "كانت", "ما", "لا", "لم", "لن", "هل", "انا", "أنا", "انت", "أنت",
            "هو", "هي", "نحن", "هم", "نعم", "شكرا", "شكراً", "مرحبا", "السلام", "عليكم",
            "كيف", "حالك", "الحمد", "لله", "الله", "ان", "أن", "إن", "او", "أو", "ثم",
            "قد", "كل", "بعد", "قبل", "عند", "حتى", "اذا", "إذا", "لكن", "التي", "الذي",
            "يا", "اخي", "أخي", "صباح", "مساء", "الخير", "اليوم", "غدا", "وين", "شلون",
            "زين", "هسه", "اكو", "ماكو", "شنو", "ليش", "منو", "اريد", "أريد", "ممكن",
            "طيب", "تمام", "والله", "ان شاء", "جدا", "جداً", "اهلا", "أهلا", "وعليكم"
        };

        private static readonly HashSet<string> CommonEnglishWords = new(StringComparer.OrdinalIgnoreCase)
        {
            "the", "and", "you", "that", "for", "with", "this", "have", "are", "was",
            "not", "but", "what", "all", "can", "will", "your", "from", "they", "there",
            "hello", "thanks", "thank", "please", "yes", "how", "why", "when", "where",
            "who", "good", "morning", "night", "love", "like", "just", "know", "time",
            "file", "open", "save", "windows", "google", "facebook", "youtube", "email",
            "password", "name", "new", "help", "test", "about", "more", "some", "one",
            "two", "she", "his", "her", "our", "out", "get", "make", "see", "now",
            "want", "need", "work", "home", "game", "school", "student", "teacher"
        };

        private const string EnglishVowels = "aeiouy";

        private int _score;
        private bool _scoreTypedInArabic;

        /// <summary>
        /// النقاط المتراكمة للكلمات المتتالية المشكوك فيها.
        /// </summary>
        public int Score => _score;

        public void Reset()
        {
            _score = 0;
        }

        /// <summary>
        /// يضيف كلمة جديدة ويعيد مجموع النقاط الحالي.
        /// 0 يعني أن الكلمة تبدو صحيحة.
        /// </summary>
        public int AddWord(
            string displayedWord,
            string physicalWord,
            bool typedInArabic)
        {
            int wordScore = typedInArabic
                ? ScoreArabicLayoutWord(displayedWord, physicalWord)
                : ScoreLatinLayoutWord(displayedWord, physicalWord);

            if (wordScore <= 0)
            {
                _score = 0;
                return 0;
            }

            if (_score > 0 && _scoreTypedInArabic != typedInArabic)
            {
                _score = 0;
            }

            _scoreTypedInArabic = typedInArabic;
            _score += wordScore;
            return _score;
        }

        /// <summary>
        /// كلمة كُتبت بالإنجليزية: هل كان المستخدم يقصد العربية؟
        /// </summary>
        internal static int ScoreLatinLayoutWord(string displayedWord, string physicalWord)
        {
            string word = TrimPunctuation(physicalWord);

            if (word.Length == 0 || word.Any(char.IsDigit))
            {
                return 0;
            }

            string lower = word.ToLowerInvariant();

            if (CommonEnglishWords.Contains(lower))
            {
                return 0;
            }

            // الاختصارات مثل HTML وPDF غالباً مقصودة.
            if (displayedWord.Length > 1 &&
                displayedWord.Where(char.IsLetter).All(char.IsUpper))
            {
                return 0;
            }

            string arabic = ConvertPhysicalToArabic(word);

            if (CommonArabicWords.Contains(arabic))
            {
                return 2;
            }

            int score = 0;

            // "hg" = "ال" في بداية الكلمة.
            if (lower.StartsWith("hg", StringComparison.Ordinal) && lower.Length >= 4)
            {
                score += 2;
            }

            // رموز مثل ; , [ ] ' داخل الكلمة = حروف عربية (ك و ج د ط).
            for (int index = 1; index < word.Length - 1; index++)
            {
                if (";,[]'`./".IndexOf(word[index]) >= 0 &&
                    char.IsLetter(word[index - 1]) &&
                    char.IsLetter(word[index + 1]))
                {
                    score += 2;
                    break;
                }
            }

            int letters = lower.Count(char.IsLetter);
            int vowels = lower.Count(character => EnglishVowels.IndexOf(character) >= 0);

            if (letters >= 4 && vowels == 0)
            {
                score += 1;
            }
            else if (letters >= 6 && vowels * 6 < letters)
            {
                score += 1;
            }

            return score;
        }

        /// <summary>
        /// كلمة كُتبت بالعربية: هل كان المستخدم يقصد الإنجليزية؟
        /// </summary>
        internal static int ScoreArabicLayoutWord(string displayedWord, string physicalWord)
        {
            string word = TrimPunctuation(physicalWord);

            if (word.Length == 0 || word.Any(char.IsDigit))
            {
                return 0;
            }

            string displayed = displayedWord.Trim();

            if (CommonArabicWords.Contains(displayed))
            {
                return 0;
            }

            if (word.Length >= 3 && CommonEnglishWords.Contains(word))
            {
                return 2;
            }

            int score = 0;

            // ى وة لا تأتيان إلا في آخر الكلمة العربية.
            for (int index = 0; index < displayed.Length - 1; index++)
            {
                if (displayed[index] is 'ى' or 'ة')
                {
                    score += 2;
                    break;
                }
            }

            // ؤ أو ء في بداية الكلمة نادر جداً بالعربية.
            if (displayed.Length >= 3 && displayed[0] is 'ؤ' or 'ء')
            {
                score += 1;
            }

            // حركات (فتحة، ضمة...) وسط كلمة كُتبت بسرعة تعني غالباً حروفاً إنجليزية كبيرة.
            if (displayed.Count(character => character is >= 'ً' and <= 'ْ') >= 2)
            {
                score += 1;
            }

            return score;
        }

        private static string ConvertPhysicalToArabic(string physicalWord)
        {
            KeyboardLayoutConverter converter = SharedConverter.Value;

            return string.Concat(
                physicalWord.Select(converter.MapPhysicalKeyToArabic));
        }

        private static string TrimPunctuation(string word)
        {
            return word.Trim().TrimEnd('?', '!', ':', '"', '>', '<', ')', '(');
        }

        private static readonly Lazy<KeyboardLayoutConverter> SharedConverter =
            new(() => new KeyboardLayoutConverter());
    }
}
