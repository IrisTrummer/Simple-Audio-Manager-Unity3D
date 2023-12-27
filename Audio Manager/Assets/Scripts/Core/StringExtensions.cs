using System.Text.RegularExpressions;

namespace Core
{
    public static class StringExtensions
    {
        private const string TagPattern = @"(?<=<style=.+?>).*(?=<\/style>)";
        
        public static string SetTextBetweenTag(this string s, string desiredText)
        {
            return Regex.Replace(s, TagPattern, desiredText);
        }
        
        public static string GetTextBetweenTag(this string s)
        {
            return Regex.Match(s, TagPattern).Value;
        }
        
        public static string EncapsulateInStyleTag(this string s, string textStyle, string text)
        {
            return $"<style=\"{textStyle}\">{text}</style>";
        }
    }
}
