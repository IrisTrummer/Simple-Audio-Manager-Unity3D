using System;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Testing.Fields
{
    public class Field : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private TMP_Text fieldNameText;

        public string FieldName { get; private set; }

        public Action Interact;

        public void SetFieldName(string fieldName)
        {
            FieldName = fieldName;
            fieldNameText.SetTextBetweenTags(fieldName);
        }

        public void SetActive(bool active)
        {
        }

        // interface callbacks, does not work for some child components like text fields
        public void OnPointerClick(PointerEventData _)
        {
            OnPointerClick();
        }

        // invokable from EventTrigger component through the inspector
        public void OnPointerClick()
        {
            RaiseInteractEvent();
        }

        private void RaiseInteractEvent()
        {
            Interact.Invoke();
        }
    }
}