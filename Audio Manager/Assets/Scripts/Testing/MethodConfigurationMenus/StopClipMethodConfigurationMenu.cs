using System.Collections.Generic;
using SimpleAudioManager;
using Testing.Fields;
using Testing.MethodConfigurationMenus.MethodParameters;
using UnityEngine;

namespace Testing.MethodConfigurationMenus
{
    public class StopClipMethodConfigurationMenu : MethodConfigurationMenu
    {
        [SerializeField]
        private DropdownField dropdownField;

        public override void InitialiseSelf()
        {
            SetMethodName(nameof(AudioManager.StopAudioClip));
        }

        public void InitialiseClips(List<string> clipNames)
        {
            dropdownField.Initialise(FieldNames.Clip, clipNames);
        }

        protected override MethodParameters.MethodParameters GetMethodParameters()
        {
            return new StopClipMethodParameters(dropdownField.GetCurrentlySelectedOption());
        }
    }
}
