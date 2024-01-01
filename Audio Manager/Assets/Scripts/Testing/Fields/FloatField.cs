using Testing.Fields.FieldPrimitives;
using UnityEngine;

namespace Testing.Fields
{
    public class FloatField : Field
    {
        [SerializeField]
        private FloatInputFieldPrimitive floatInputField;

        private float defaultValue;

        private const string InactiveText = "\u2500"; // horizontal line

        public void Initialise(string name, float defaultValue)
        {
            SetFieldName(name);

            this.defaultValue = defaultValue;
            floatInputField.SetInputFieldPlaceholder(defaultValue);
        }

        public float GetCurrentValue()
        {
            return floatInputField.GetCurrentValue();
        }

        public override void SetActive(bool active)
        {
            base.SetActive(active);
            
            floatInputField.ClearText();
            floatInputField.SetInputFieldPlaceholder(active ? defaultValue.ToString("#0.##") : InactiveText);
        }
    }
}
