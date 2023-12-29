using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;

namespace Testing.Fields
{
    public class DropdownField : Field
    {
        [SerializeField]
        private TMP_Dropdown dropdown;

        [SerializeField]
        private TextStyles dropDownTextStyle = TextStyles.Body;

        public void Initialise(string name, List<string> options)
        {
            SetFieldName(name);
            SetDropdownOptions(options);
        }

        public void SetDropdownOptions(List<string> options)
        {
            dropdown.ClearOptions();

            for (int i = 0; i < options.Count; i++)
            {
                options[i] = options[i].EncapsulateInStyleTag(dropDownTextStyle.ToString(), options[i]);
            }
            
            dropdown.AddOptions(options);
        }

        public string GetCurrentValue()
        {
            return dropdown.options[dropdown.value].text.GetTextBetweenTag();
        }
    }
}
