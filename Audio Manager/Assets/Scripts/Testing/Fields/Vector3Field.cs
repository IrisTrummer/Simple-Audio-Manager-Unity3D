using Testing.Fields.FieldPrimitives;
using UnityEngine;

namespace Testing.Fields
{
    public class Vector3Field : Field
    {
        [SerializeField]
        private FloatInputFieldPrimitive xInputField;
        
        [SerializeField]
        private FloatInputFieldPrimitive yInputField;
        
        [SerializeField]
        private FloatInputFieldPrimitive zInputField;
        
        public void Initialise(string name, Vector3 defaultValue)
        {
            SetFieldName(name);
            xInputField.SetInputFieldPlaceholder(defaultValue.x);
            yInputField.SetInputFieldPlaceholder(defaultValue.y);
            zInputField.SetInputFieldPlaceholder(defaultValue.z);
        }

        public Vector3 GetCurrentValue()
        {
            return new Vector3(xInputField.GetCurrentValue(), yInputField.GetCurrentValue(), zInputField.GetCurrentValue());
        }
    }
}
