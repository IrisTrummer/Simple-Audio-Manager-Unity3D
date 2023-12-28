using System.Collections.Generic;
using Testing.Fields;
using Testing.MethodConfigurationMenus.MethodParameters;
using UnityEngine;

namespace Testing.MethodConfigurationMenus
{
    public class StopClipMethodConfigurationMenu : MethodConfigurationMenu
    {
        [SerializeField]
        private DropdownField dropdownField;

        public void InitialiseClips(string methodName, List<string> clipNames)
        {
            SetMethodName(methodName);
            dropdownField.Initialise(FieldNames.Clip, clipNames);
        }

        protected override MethodParameters.MethodParameters GetMethodParameters()
        {
            return new StopClipMethodParameters(dropdownField.GetCurrentlySelectedOption());
        }
    }
}
