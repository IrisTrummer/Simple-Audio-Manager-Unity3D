using TMPro;

namespace Core
{
    public static class TMPTextExtensions
    {
        public static void SetTextBetweenTags(this TMP_Text t, string text)
        {
            t.text = t.text.SetTextBetweenTag(text);
        }

        public static void EncapsulateInStyleTag(this TMP_Text t, string textStyle, string text)
        {
            t.text = t.text.EncapsulateInStyleTag(textStyle, text);
        }
    }
}
