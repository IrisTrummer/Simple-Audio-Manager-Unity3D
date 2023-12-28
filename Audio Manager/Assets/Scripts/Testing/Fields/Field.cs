using Core;
using TMPro;
using UnityEngine;

namespace Testing.Fields
{
    public class Field : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text fieldNameText;

        public string FieldName { get; private set; }
        
        public void SetFieldName(string fieldName)
        {
            FieldName = fieldName;
            fieldNameText.SetTextBetweenTags(fieldName);
        }
    }
}
