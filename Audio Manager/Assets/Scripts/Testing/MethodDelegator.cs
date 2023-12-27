using System;
using SimpleAudioManager;
using Testing.MethodConfigurationMenus;
using Testing.MethodConfigurationMenus.MethodParameters;
using UnityEngine;

namespace Testing
{
    public class MethodDelegator : MonoBehaviour
    {
        // TODO get fields from ui initialisor
        [SerializeField]
        private StopClipMethodConfigurationMenu stopClip;

        [SerializeField]
        private PlayClipMethodConfigurationMenu playOnce;

        [SerializeField]
        private UIInitialisor uiInitialisor;

        private void Awake()
        {
            stopClip.StartButtonClicked += StopClipButtonClicked;
            playOnce.StartButtonClicked += PlayOnceButtonClicked;
        }

        private void OnDestroy()
        {
            stopClip.StartButtonClicked -= StopClipButtonClicked;
            playOnce.StartButtonClicked -= PlayOnceButtonClicked;
        }

        private void StopClipButtonClicked(MethodParameters methodParameters)
        {
            StopClipMethodParameters parameters = (StopClipMethodParameters)methodParameters;
            AudioClip audioClip = uiInitialisor.GetAudioClipByName(parameters.AudioClipName);

            AudioManager.Instance.StopAudioClip(audioClip);
        }

        private void PlayOnceButtonClicked(MethodParameters methodParameters)
        {
            PlayClipMethodParameters parameters = (PlayClipMethodParameters)methodParameters;
            
            AudioClip audioClip = uiInitialisor.GetAudioClipByName(parameters.AudioClipName);
            SoundType group = GetGroupFromName(parameters.GroupName);

            AudioManager.Instance.PlayOnce(audioClip, group, parameters.Pitch, parameters.Volume);
        }

        private SoundType GetGroupFromName(string groupName)
        {
            return Enum.Parse<SoundType>(groupName);
        }
    }
}