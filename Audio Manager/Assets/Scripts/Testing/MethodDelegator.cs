using SimpleAudioManager;
using Testing.MethodConfigurationMenus;
using Testing.MethodConfigurationMenus.MethodParameters;
using UnityEngine;

namespace Testing
{
    public class MethodDelegator : MonoBehaviour
    {
        [SerializeField]
        private StopClipMethodConfigurationMenu stopClip;

        [SerializeField]
        private UIInitialisor uiInitialisor;

        private void Awake()
        {
            stopClip.StartButtonClicked += StopClipButtonClicked;
        }

        private void StopClipButtonClicked(MethodParameters methodParameters)
        {
            StopClipMethodParameters stopClipMethodParameters = (StopClipMethodParameters)methodParameters;
            AudioClip audioClip = uiInitialisor.GetAudioClipByName(stopClipMethodParameters.AudioClipName);

            AudioManager.Instance.StopAudioClip(audioClip);
        }
    }
}