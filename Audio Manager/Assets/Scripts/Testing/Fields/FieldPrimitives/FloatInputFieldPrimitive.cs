using TMPro;
using UnityEngine;

namespace Testing.Fields.FieldPrimitives
{
    public class FloatInputFieldPrimitive : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField inputField;
        
        [SerializeField]
        private TMP_Text placeholder;
        
        private float defaultValue;
        
        private const string InactiveText = "\u2500"; // horizontal line

        public void SetInputFieldPlaceholder(float value)
        {
            defaultValue = value;
            SetInputFieldPlaceholder(value.ToString("#0.##"));
        }
        
        public void SetInputFieldPlaceholder(string value)
        {
            placeholder.text = value;
        }

        public void ClearText()
        {
            inputField.text = string.Empty;
        }

        public float GetCurrentValue()
        {
            string value = string.IsNullOrWhiteSpace(inputField.text) ? placeholder.text : inputField.text;
            return float.TryParse(value, out float v) ? v : -1;
        }

        public void SetActive(bool active)
        {
            ClearText();
            SetInputFieldPlaceholder(active ? defaultValue.ToString("#0.##") : InactiveText);
        }
    }
}
