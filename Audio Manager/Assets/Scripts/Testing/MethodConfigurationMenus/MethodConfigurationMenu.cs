using System;
using System.Collections.Generic;
using Core;
using Testing.Fields;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Testing.MethodConfigurationMenus
{
    public abstract class MethodConfigurationMenu : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text methodNameText;

        [SerializeField]
        private Button startButton;

        public Action<MethodParameters.MethodParameters> StartButtonClicked;

        protected virtual void Awake()
        {
            startButton.onClick.AddListener(RaiseStartButtonClickedEvent);
        }

        private void OnDestroy()
        {
            startButton.onClick.RemoveListener(RaiseStartButtonClickedEvent);
        }
        
        protected void SetMethodName(string methodName)
        {
            methodNameText.SetTextBetweenTags(methodName.WithSpacesBetweenCapitalLetters());
        }

        protected abstract MethodParameters.MethodParameters GetMethodParameters();
        
        private void RaiseStartButtonClickedEvent()
        {
            StartButtonClicked.Invoke(GetMethodParameters());
        }
    }
}