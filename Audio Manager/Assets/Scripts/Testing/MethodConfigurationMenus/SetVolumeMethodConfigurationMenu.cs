using System.Collections.Generic;
using Testing.Fields;
using Testing.MethodConfigurationMenus.MethodParameters;
using UnityEngine;

namespace Testing.MethodConfigurationMenus
{
    public class SetVolumeMethodConfigurationMenu : MethodConfigurationMenu
    {
        [SerializeField]
        private DropdownField groupDropdownField;

        [SerializeField]
        private FloatField volumeField;

        public void Initialise(string methodName, List<string> groupNames, float volumeDefaultValue)
        {
            SetMethodName(methodName);

            groupDropdownField.Initialise(FieldNames.Group, groupNames);
            volumeField.Initialise(FieldNames.Volume, volumeDefaultValue);
        }

        protected override MethodParameters.MethodParameters GetMethodParameters()
        {
            return new SetVolumeMethodParameters(groupDropdownField.GetCurrentValue(), volumeField.GetCurrentValue());
        }
    }
}