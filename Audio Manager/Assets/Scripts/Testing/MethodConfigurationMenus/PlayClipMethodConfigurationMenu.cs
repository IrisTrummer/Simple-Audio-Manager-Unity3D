using System.Collections.Generic;
using Testing.Fields;
using Testing.MethodConfigurationMenus.MethodParameters;
using UnityEngine;

namespace Testing.MethodConfigurationMenus
{
    public class PlayClipMethodConfigurationMenu : MethodConfigurationMenu
    {
        [SerializeField]
        private DropdownField clipDropdownField;
        
        [SerializeField]
        private DropdownField groupDropdownField;

        [SerializeField]
        private FloatField volumeField;

        [SerializeField]
        private FloatField pitchField;
        
        // TODO random pitch

        public void Initialise(string methodName, List<string> clipNames, List<string> groupNames, float volumeDefaultValue, float pitchDefaultValue)
        {
            SetMethodName(methodName);
            
            clipDropdownField.Initialise(FieldNames.Clip, clipNames);
            groupDropdownField.Initialise(FieldNames.Group, groupNames);
            volumeField.Initialise(FieldNames.Volume, volumeDefaultValue);
            pitchField.Initialise(FieldNames.Pitch, pitchDefaultValue);
        }

        protected override MethodParameters.MethodParameters GetMethodParameters()
        {
            return new PlayClipMethodParameters(clipDropdownField.GetCurrentlySelectedOption(), groupDropdownField.GetCurrentlySelectedOption(), volumeField.GetCurrentValue(), pitchField.GetCurrentValue());
        }
    }
}
