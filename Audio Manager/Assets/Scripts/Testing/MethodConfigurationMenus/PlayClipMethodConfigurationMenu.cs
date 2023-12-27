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
        
        // TODO missing volume, pitch, random pitch

        public override void InitialiseSelf()
        {
            // TODO configure from outside
            SetMethodName(nameof(AudioManager.PlayOnce));
        }

        public void InitialiseClips(string fieldName, List<string> clipNames)
        {
            clipDropdownField.Initialise(fieldName, clipNames);
        }
        
        public void InitialiseGroups(string fieldName, List<string> clipNames)
        {
            groupDropdownField.Initialise(fieldName, clipNames);
        }

        protected override MethodParameters.MethodParameters GetMethodParameters()
        {
            // TODO replace magic values with configured parameters
            return new PlayClipMethodParameters(clipDropdownField.GetCurrentlySelectedOption(), groupDropdownField.GetCurrentlySelectedOption(), 1, 0);
        }
    }
}
