using System;
using ColorBindings;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Testing.Fields
{
    public class Field : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private TMP_Text fieldNameText;
        
        [SerializeField]
        private BindableColor baseColor;

        [SerializeField]
        private BindableColor inactiveColor;

        [SerializeField]
        private Image[] imageToColorInactive;

        public string FieldName { get; private set; }
        public bool IsActive { get; private set; } = true;

        public Action Interact;

        public void SetFieldName(string fieldName)
        {
            FieldName = fieldName;
            fieldNameText.SetTextBetweenTags(fieldName);
        }

        public virtual void SetActive(bool active)
        {
            IsActive = active;

            foreach (Image image in imageToColorInactive)
            {
                image.color = active ? baseColor.Value : inactiveColor.Value;
            }
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
            Interact?.Invoke();
        }
    }
}