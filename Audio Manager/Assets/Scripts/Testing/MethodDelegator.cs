using System;
using SimpleAudioManager;
using Testing.MethodConfigurationMenus;
using Testing.MethodConfigurationMenus.MethodParameters;
using UnityEngine;

namespace Testing
{
    public class MethodDelegator : MonoBehaviour
    {
        [SerializeField]
        private UIInitialisor uiInitialisor;

        private void Awake()
        {
            uiInitialisor.StopClip.StartButtonClicked += StopClipButtonClicked;
            uiInitialisor.PlayOnce.StartButtonClicked += PlayOnceButtonClicked;
            uiInitialisor.PlayOnLoop.StartButtonClicked += PlayOnLoopButtonClicked;
        }

        private void OnDestroy()
        {
            uiInitialisor.StopClip.StartButtonClicked -= StopClipButtonClicked;
            uiInitialisor.PlayOnce.StartButtonClicked -= PlayOnceButtonClicked;
            uiInitialisor.PlayOnLoop.StartButtonClicked -= PlayOnLoopButtonClicked;
        }

        private void StopClipButtonClicked(MethodParameters methodParameters)
        {
            StopClipMethodParameters parameters = (StopClipMethodParameters)methodParameters;
            AudioClip audioClip = uiInitialisor.GetAudioClipByName(parameters.AudioClipName);

            AudioManager.Instance.StopAudioClip(audioClip);
        }

        private void PlayOnceButtonClicked(MethodParameters methodParameters)
        {
            PlayClipMethodParameters parameters = GetPlayClipMethodParameters(methodParameters, out AudioClip audioClip, out SoundType group);
            AudioManager.Instance.PlayOnce(audioClip, group, parameters.Pitch, parameters.Volume);
        }
        
        private void PlayOnLoopButtonClicked(MethodParameters methodParameters)
        {
            PlayClipMethodParameters parameters = GetPlayClipMethodParameters(methodParameters, out AudioClip audioClip, out SoundType group);
            AudioManager.Instance.PlayOnLoop(audioClip, group, parameters.Pitch, parameters.Volume);
        }

        private PlayClipMethodParameters GetPlayClipMethodParameters(MethodParameters methodParameters, out AudioClip audioClip, out SoundType group)
        {
            PlayClipMethodParameters parameters = (PlayClipMethodParameters)methodParameters;
            
            audioClip = uiInitialisor.GetAudioClipByName(parameters.AudioClipName);
            group = GetGroupFromName(parameters.GroupName);

            return parameters;
        }

        private SoundType GetGroupFromName(string groupName)
        {
            return Enum.Parse<SoundType>(groupName);
        }
    }
}