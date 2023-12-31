using System;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Testing
{
    public class DebugInformationElement : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text indexText;

        [SerializeField]
        private TMP_Text elementNameText;

        [SerializeField]
        private TMP_Text groupNameText;
        
        [SerializeField]
        private Button button;

        [SerializeField]
        private float minDisplayTime = 1f;
        
        public int Index { get; private set; }
        
        public Action<DebugInformationElement> ButtonPress;
        public Action<DebugInformationElement> ReadyForDestruction;

        protected bool ShouldBeKilled;
        
        private float displayTime;

        protected virtual void Awake()
        {
            button.onClick.AddListener(RaiseButtonPressedEvent);
        }

        protected virtual void OnDestroy()
        {
            button.onClick.RemoveListener(RaiseButtonPressedEvent);
        }
        
        protected virtual void Update()
        {
            displayTime += Time.deltaTime;
            
            if (displayTime >= minDisplayTime && ShouldBeKilled)
                ReadyForDestruction?.Invoke(this);
        }

        public void Kill()
        {
            ShouldBeKilled = true;
            button.enabled = false;
            
            if (displayTime >= minDisplayTime)
                ReadyForDestruction?.Invoke(this);
        }

        public void Configure(int index, string name, string groupName)
        {
            SetIndex(index);
            SetName(name);
            SetGroupName(groupName);
        }

        public void SetIndex(int index)
        {
            Index = index;
            indexText.SetTextBetweenTags(index >= 0 ? index.ToString() : "-");
        }

        public void SetName(string name)
        {
            elementNameText.SetTextBetweenTags(name);
        }

        public void SetGroupName(string groupName)
        {
            groupNameText.SetTextBetweenTags(groupName);
        }

        private void RaiseButtonPressedEvent()
        {
            ButtonPress.Invoke(this);
        }
    }
}