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
            placeholder.text = value.ToString("#0.##");
        }

        public float GetCurrentValue()
        {
            string value = string.IsNullOrWhiteSpace(inputField.text) ? placeholder.text : inputField.text;
            return float.Parse(value);
        }
    }
}
