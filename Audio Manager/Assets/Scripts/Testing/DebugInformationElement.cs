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
        
        // TODO introduce min time so that clips with very short duration can still be read

        public int Index { get; private set; }
        
        public Action<DebugInformationElement> ButtonPress;

        private void Awake()
        {
            button.onClick.AddListener(RaiseButtonPressedEvent);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(RaiseButtonPressedEvent);
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
            indexText.SetTextBetweenTags(index.ToString());
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