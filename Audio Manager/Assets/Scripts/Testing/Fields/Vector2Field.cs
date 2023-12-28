using Testing.Fields.FieldPrimitives;
using UnityEngine;

namespace Testing.Fields
{
    public class Vector2Field : Field
    {
        [SerializeField]
        private FloatInputFieldPrimitive xInputField;
        
        [SerializeField]
        private FloatInputFieldPrimitive yInputField;
        
        public void Initialise(string name, Vector2 defaultValue)
        {
            SetFieldName(name);
            xInputField.SetInputFieldPlaceholder(defaultValue.x);
            yInputField.SetInputFieldPlaceholder(defaultValue.y);
        }

        public Vector2 GetCurrentValue()
        {
            return new Vector2(xInputField.GetCurrentValue(), yInputField.GetCurrentValue());
        }
    }
}
