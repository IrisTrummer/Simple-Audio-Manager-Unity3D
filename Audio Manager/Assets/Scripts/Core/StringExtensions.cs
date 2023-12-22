using System.Text.RegularExpressions;

namespace Core
{
    public static class StringExtensions
    {
        public static string SetTextBetweenTag(this string s, string desiredText)
        {
            return Regex.Replace(s, @"(?<=<style=.+?>).*(?=<\/style>)", desiredText);
        }
    }
}
