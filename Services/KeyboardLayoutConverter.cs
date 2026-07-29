using System;
using System.Collections.Generic;
using System.Text;

namespace MiftahFix.Services
{
    public enum ConversionDirection
    {
        Automatic,
        EnglishToArabic,
        ArabicToEnglish
    }

    public sealed class KeyboardLayoutConverter
    {
        private readonly Dictionary<char, string> _englishToArabic;
        private readonly Dictionary<char, string> _arabicToEnglish;

        public KeyboardLayoutConverter()
        {
            _englishToArabic = CreateEnglishToArabicMap();
            _arabicToEnglish = CreateArabicToEnglishMap();
        }

        public string Convert(string text, ConversionDirection direction)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            return direction switch
            {
                ConversionDirection.EnglishToArabic =>
                    ConvertEnglishToArabic(text),

                ConversionDirection.ArabicToEnglish =>
                    ConvertArabicToEnglish(text),

                _ =>
                    ConvertAutomatically(text)
            };
        }

        public ConversionDirection DetectDirection(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return ConversionDirection.EnglishToArabic;
            }

            int arabicCharacters = 0;
            int englishCharacters = 0;

            foreach (char character in text)
            {
                if (IsArabicCharacter(character))
                {
                    arabicCharacters++;
                }
                else if (IsEnglishCharacter(character))
                {
                    englishCharacters++;
                }
            }

            return arabicCharacters > englishCharacters
                ? ConversionDirection.ArabicToEnglish
                : ConversionDirection.EnglishToArabic;
        }

        private string ConvertAutomatically(string text)
        {
            ConversionDirection detectedDirection = DetectDirection(text);

            return detectedDirection == ConversionDirection.ArabicToEnglish
                ? ConvertArabicToEnglish(text)
                : ConvertEnglishToArabic(text);
        }

        private string ConvertEnglishToArabic(string text)
        {
            StringBuilder result = new();

            foreach (char character in text)
            {
                if (_englishToArabic.TryGetValue(
                        character,
                        out string? convertedText))
                {
                    result.Append(convertedText);
                }
                else
                {
                    result.Append(character);
                }
            }

            return result.ToString();
        }

        private string ConvertArabicToEnglish(string text)
        {
            StringBuilder result = new();

            for (int index = 0; index < text.Length; index++)
            {
                if (index + 1 < text.Length)
                {
                    string twoCharacters = text.Substring(index, 2);

                    string? combinedResult = twoCharacters switch
                    {
                        "لا" => "b",
                        "لأ" => "G",
                        "لإ" => "T",
                        "لآ" => "B",
                        _ => null
                    };

                    if (combinedResult != null)
                    {
                        result.Append(combinedResult);
                        index++;
                        continue;
                    }
                }

                char character = text[index];

                if (_arabicToEnglish.TryGetValue(
                        character,
                        out string? convertedText))
                {
                    result.Append(convertedText);
                }
                else
                {
                    result.Append(character);
                }
            }

            return result.ToString();
        }

        private static bool IsArabicCharacter(char character)
        {
            return character is >= '\u0600' and <= '\u06FF'
                || character is >= '\u0750' and <= '\u077F'
                || character is >= '\u08A0' and <= '\u08FF';
        }

        private static bool IsEnglishCharacter(char character)
        {
            return character is >= 'a' and <= 'z'
                || character is >= 'A' and <= 'Z';
        }

        private static Dictionary<char, string> CreateEnglishToArabicMap()
        {
            return new Dictionary<char, string>
            {
                ['`'] = "ذ",
                ['q'] = "ض",
                ['w'] = "ص",
                ['e'] = "ث",
                ['r'] = "ق",
                ['t'] = "ف",
                ['y'] = "غ",
                ['u'] = "ع",
                ['i'] = "ه",
                ['o'] = "خ",
                ['p'] = "ح",
                ['['] = "ج",
                [']'] = "د",

                ['a'] = "ش",
                ['s'] = "س",
                ['d'] = "ي",
                ['f'] = "ب",
                ['g'] = "ل",
                ['h'] = "ا",
                ['j'] = "ت",
                ['k'] = "ن",
                ['l'] = "م",
                [';'] = "ك",
                ['\''] = "ط",

                ['z'] = "ئ",
                ['x'] = "ء",
                ['c'] = "ؤ",
                ['v'] = "ر",
                ['b'] = "لا",
                ['n'] = "ى",
                ['m'] = "ة",
                [','] = "و",
                ['.'] = "ز",
                ['/'] = "ظ",

                ['Q'] = "َ",
                ['W'] = "ً",
                ['E'] = "ُ",
                ['R'] = "ٌ",
                ['T'] = "لإ",
                ['Y'] = "إ",
                ['U'] = "‘",
                ['I'] = "÷",
                ['O'] = "×",
                ['P'] = "؛",
                ['{'] = "<",
                ['}'] = ">",

                ['A'] = "ِ",
                ['S'] = "ٍ",
                ['D'] = "]",
                ['F'] = "[",
                ['G'] = "لأ",
                ['H'] = "أ",
                ['J'] = "ـ",
                ['K'] = "،",
                ['L'] = "/",
                [':'] = ":",
                ['"'] = "\"",

                ['Z'] = "~",
                ['X'] = "ْ",
                ['C'] = "}",
                ['V'] = "{",
                ['B'] = "لآ",
                ['N'] = "آ",
                ['M'] = "’",
                ['<'] = ",",
                ['>'] = ".",
                ['?'] = "؟"
            };
        }

        private static Dictionary<char, string> CreateArabicToEnglishMap()
        {
            return new Dictionary<char, string>
            {
                ['ذ'] = "`",
                ['ض'] = "q",
                ['ص'] = "w",
                ['ث'] = "e",
                ['ق'] = "r",
                ['ف'] = "t",
                ['غ'] = "y",
                ['ع'] = "u",
                ['ه'] = "i",
                ['خ'] = "o",
                ['ح'] = "p",
                ['ج'] = "[",
                ['د'] = "]",

                ['ش'] = "a",
                ['س'] = "s",
                ['ي'] = "d",
                ['ب'] = "f",
                ['ل'] = "g",
                ['ا'] = "h",
                ['ت'] = "j",
                ['ن'] = "k",
                ['م'] = "l",
                ['ك'] = ";",
                ['ط'] = "'",

                ['ئ'] = "z",
                ['ء'] = "x",
                ['ؤ'] = "c",
                ['ر'] = "v",
                ['ى'] = "n",
                ['ة'] = "m",
                ['و'] = ",",
                ['ز'] = ".",
                ['ظ'] = "/",

                ['َ'] = "Q",
                ['ً'] = "W",
                ['ُ'] = "E",
                ['ٌ'] = "R",
                ['إ'] = "Y",
                ['ِ'] = "A",
                ['ٍ'] = "S",
                ['أ'] = "H",
                ['ـ'] = "J",
                ['،'] = "K",
                ['ْ'] = "X",
                ['آ'] = "N",
                ['؟'] = "?"
            };
        }
    }
}