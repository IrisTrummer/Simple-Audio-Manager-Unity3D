using UnityEngine;
using UnityEngine.UI;

namespace Testing.Fields
{
    public class ToggleField : Field
    {
        [SerializeField]
        private Toggle toggle;

        public void Initialise(string name, bool defaultValue)
        {
            SetFieldName(name);
            SetValue(defaultValue);
        }

        public void SetValue(bool value)
        {
            toggle.isOn = value;
        }

        public bool GetCurrentValue()
        {
            return toggle.isOn;
        }
    }
}
