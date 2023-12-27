using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;

namespace Testing.Fields
{
    public class DropdownField : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text fieldNameText;

        public string FieldName { get; private set; }

        [SerializeField]
        private TMP_Dropdown dropdown;

        [SerializeField]
        private TextStyles dropDownTextStyle = TextStyles.Body;

        public void SetFieldName(string fieldName)
        {
            FieldName = fieldName;
            fieldNameText.SetTextBetweenTags(fieldName);
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

        public string GetCurrentlySelectedOption()
        {
            return dropdown.options[dropdown.value].text.GetTextBetweenTag();
        }
    }
}
