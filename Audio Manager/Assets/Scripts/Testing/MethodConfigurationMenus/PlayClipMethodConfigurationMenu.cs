using System.Collections.Generic;
using SimpleAudioManager;
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

        public override void InitialiseSelf()
        {
            // TODO configure from outside
            SetMethodName(nameof(AudioManager.PlayOnce));
            
            volumeField.Initialise(FieldNames.Volume, 1);
            pitchField.Initialise(FieldNames.Pitch, 1);
        }

        public void Initialise(List<string> clipNames, List<string> groupNames)
        {
            clipDropdownField.Initialise(FieldNames.Clip, clipNames);
            groupDropdownField.Initialise(FieldNames.Group, groupNames);
        }

        protected override MethodParameters.MethodParameters GetMethodParameters()
        {
            return new PlayClipMethodParameters(clipDropdownField.GetCurrentlySelectedOption(), groupDropdownField.GetCurrentlySelectedOption(), volumeField.GetCurrentValue(), pitchField.GetCurrentValue());
        }
    }
}
