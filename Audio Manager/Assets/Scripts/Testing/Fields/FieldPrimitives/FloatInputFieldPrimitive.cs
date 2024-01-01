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

        public void SetInputFieldPlaceholder(float value)
        {
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
    }
}
