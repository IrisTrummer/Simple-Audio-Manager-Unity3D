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

        [SerializeField]
        private DropdownField pitchShiftTypeField;

        public virtual void Initialise(string methodName, List<string> clipNames, List<string> groupNames, float volumeDefaultValue, float pitchDefaultValue, List<string> pitchShiftTypeNames)
        {
            SetMethodName(methodName);

            clipDropdownField.Initialise(FieldNames.Clip, clipNames);
            groupDropdownField.Initialise(FieldNames.Group, groupNames);
            volumeField.Initialise(FieldNames.Volume, volumeDefaultValue);
            pitchField.Initialise(FieldNames.Pitch, pitchDefaultValue);
            pitchShiftTypeField.Initialise("", pitchShiftTypeNames);

            pitchShiftTypeField.SetActive(false);
            
            pitchField.Interact += OnInteractWithPitch;
            pitchShiftTypeField.Interact += OnInteractWithPitchShiftType;
        }

        protected override MethodParameters.MethodParameters GetMethodParameters()
        {
            return new PlayClipMethodParameters(clipDropdownField.GetCurrentValue(), groupDropdownField.GetCurrentValue(), volumeField.GetCurrentValue(), pitchField.GetCurrentValue(),
                pitchShiftTypeField.GetCurrentValue(), pitchShiftTypeField.IsActive);
        }

        private void OnInteractWithPitch()
        {
            pitchField.SetActive(true);
            pitchShiftTypeField.SetActive(false);
        }

        private void OnInteractWithPitchShiftType()
        {
            pitchShiftTypeField.SetActive(true);
            pitchField.SetActive(false);
        }
    }
}