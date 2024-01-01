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
        private FloatField volumeField;

        [SerializeField]
        private FloatField durationField;

        public void Initialise(string methodName, List<string> groupNames, float volumeDefaultValue, Vector2 fromToDefaultValue, float durationDefaultValue)
        {
            SetMethodName(methodName);

            groupDropdownField.Initialise(FieldNames.Group, groupNames);
            volumeField.Initialise(FieldNames.Volume, volumeDefaultValue);
            fromToField.Initialise(FieldNames.Volume, fromToDefaultValue);
            durationField.Initialise(FieldNames.Duration, durationDefaultValue);

            volumeField.SetActive(true);
            fromToField.SetActive(false);

            volumeField.Interact += OnInteractWithVolumeField;
            fromToField.Interact += OnInteractWithFromToField;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            volumeField.Interact -= OnInteractWithVolumeField;
            fromToField.Interact -= OnInteractWithFromToField;
        }

        protected override MethodParameters.MethodParameters GetMethodParameters()
        {
            return new FadeGroupVolumeMethodParameters(groupDropdownField.GetCurrentValue(), volumeField.GetCurrentValue(), fromToField.GetCurrentValue(), durationField.GetCurrentValue(),
                fromToField.IsActive);
        }

        private void OnInteractWithVolumeField()
        {
            volumeField.SetActive(true);
            fromToField.SetActive(false);
        }

        private void OnInteractWithFromToField()
        {
            fromToField.SetActive(true);
            volumeField.SetActive(false);
        }
    }
}