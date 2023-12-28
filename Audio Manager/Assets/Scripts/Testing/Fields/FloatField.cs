using Testing.Fields.FieldPrimitives;
using UnityEngine;

namespace Testing.Fields
{
    public class FloatField : Field
    {
        [SerializeField]
        private FloatInputFieldPrimitive floatInputField;
        
        public void Initialise(string name, float defaultValue)
        {
            SetFieldName(name);
            floatInputField.SetInputFieldPlaceholder(defaultValue);
        }

        public float GetCurrentValue()
        {
            return floatInputField.GetCurrentValue();
        }
    }
}
