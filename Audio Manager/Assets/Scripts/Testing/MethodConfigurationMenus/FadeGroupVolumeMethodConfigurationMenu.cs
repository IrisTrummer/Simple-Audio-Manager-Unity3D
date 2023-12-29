using System.Collections.Generic;
using Testing.Fields;
using Testing.MethodConfigurationMenus.MethodParameters;
using UnityEngine;

namespace Testing.MethodConfigurationMenus
{
    public class FadeGroupVolumeMethodConfigurationMenu : MethodConfigurationMenu
    {
        [SerializeField]
        private DropdownField groupDropdownField;

        [SerializeField]
        private Vector2Field fromToField;

        [SerializeField]
        private FloatField durationField;
        
        // TODO only target volume

        public void Initialise(string methodName, List<string> groupNames, Vector2 fromToDefaultValue, float durationDefaultValue)
        {
            SetMethodName(methodName);
            
            groupDropdownField.Initialise(FieldNames.Group, groupNames);
            fromToField.Initialise(FieldNames.Volume, fromToDefaultValue);
            durationField.Initialise(FieldNames.Duration, durationDefaultValue);
        }

        protected override MethodParameters.MethodParameters GetMethodParameters()
        {
            return new FadeGroupVolumeMethodParameters(groupDropdownField.GetCurrentValue(), fromToField.GetCurrentValue(), durationField.GetCurrentValue());
        }
    }
}
