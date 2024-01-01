using System.Collections.Generic;
using Testing.Fields;
using Testing.MethodConfigurationMenus.MethodParameters;
using UnityEngine;

namespace Testing.MethodConfigurationMenus
{
    public class PlayClipAtPositionMethodConfigurationMenu : PlayClipMethodConfigurationMenu
    {
        [SerializeField]
        private Vector3Field positionField;

        [SerializeField]
        private ToggleField loopField;

        public override void Initialise(string methodName, List<string> clipNames, List<string> groupNames, float volumeDefaultValue, float pitchDefaultValue, List<string> pitchShiftTypeNames)
        {
            base.Initialise(methodName, clipNames, groupNames, volumeDefaultValue, pitchDefaultValue, pitchShiftTypeNames);

            positionField.Initialise(FieldNames.Position, Vector3.zero);
            loopField.Initialise(FieldNames.Loop, false);
        }

        protected override MethodParameters.MethodParameters GetMethodParameters()
        {
            PlayClipMethodParameters p = (PlayClipMethodParameters)base.GetMethodParameters();
            return new PlayClipAtPositionMethodParameters(p.AudioClipName, p.GroupName, p.Volume, p.Pitch, positionField.GetCurrentValue(), loopField.GetCurrentValue());
        }
    }
}