using System;
using SimpleAudioManager;
using Testing.MethodConfigurationMenus.MethodParameters;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            uiInitialisor.PlayAtPosition.StartButtonClicked += PlayAtPositionButtonClicked;
            uiInitialisor.SetVolume.StartButtonClicked += SetVolumeButtonClicked;
            uiInitialisor.FadeGroupVolume.StartButtonClicked += FadeGroupVolumeButtonClicked;
            uiInitialisor.ReloadScene.ButtonClicked += ReloadSceneButtonClicked;
        }

        private void OnDestroy()
        {
            uiInitialisor.StopClip.StartButtonClicked -= StopClipButtonClicked;
            uiInitialisor.PlayOnce.StartButtonClicked -= PlayOnceButtonClicked;
            uiInitialisor.PlayOnLoop.StartButtonClicked -= PlayOnLoopButtonClicked;
            uiInitialisor.PlayAtPosition.StartButtonClicked -= PlayAtPositionButtonClicked;
            uiInitialisor.SetVolume.StartButtonClicked -= SetVolumeButtonClicked;
            uiInitialisor.FadeGroupVolume.StartButtonClicked -= FadeGroupVolumeButtonClicked;
            uiInitialisor.ReloadScene.ButtonClicked -= ReloadSceneButtonClicked;
        }

        private void StopClipButtonClicked(MethodParameters methodParameters)
        {
            StopClipMethodParameters parameters = (StopClipMethodParameters)methodParameters;
            AudioClip audioClip = uiInitialisor.GetAudioClipByName(parameters.AudioClipName);

            AudioManager.Instance.StopAudioClip(audioClip);
        }

        private void PlayOnceButtonClicked(MethodParameters methodParameters)
        {
            PlayClipMethodParameters parameters = GetPlayClipMethodParameters(methodParameters, out AudioClip audioClip, out SoundType group, out PitchShiftType pitchShiftType);

            if (parameters.IsUsingPitchShiftType)
                AudioManager.Instance.PlayOnce(audioClip, group, parameters.Volume, pitchShiftType);
            else
                AudioManager.Instance.PlayOnce(audioClip, group, parameters.Pitch, parameters.Volume);
        }

        private void PlayOnLoopButtonClicked(MethodParameters methodParameters)
        {
            PlayClipMethodParameters parameters = GetPlayClipMethodParameters(methodParameters, out AudioClip audioClip, out SoundType group, out PitchShiftType pitchShiftType);

            if (parameters.IsUsingPitchShiftType)
                AudioManager.Instance.PlayOnLoop(audioClip, group, parameters.Volume, pitchShiftType);
            else
                AudioManager.Instance.PlayOnLoop(audioClip, group, parameters.Pitch, parameters.Volume);
        }

        private void PlayAtPositionButtonClicked(MethodParameters methodParameters)
        {
            PlayClipMethodParameters parameters = GetPlayClipMethodParameters(methodParameters, out AudioClip audioClip, out SoundType group, out PitchShiftType pitchShiftType);
            PlayClipAtPositionMethodParameters positionParameters = (PlayClipAtPositionMethodParameters)parameters;

            if (parameters.IsUsingPitchShiftType)
                AudioManager.Instance.PlayAudioClipAtPosition(audioClip, positionParameters.Position, group, parameters.Volume, positionParameters.Loop, pitchShiftType);
            else
                AudioManager.Instance.PlayAudioClipAtPosition(audioClip, positionParameters.Position, group, parameters.Pitch, parameters.Volume, positionParameters.Loop);
        }

        private void SetVolumeButtonClicked(MethodParameters methodParameters)
        {
            SetVolumeMethodParameters parameters = (SetVolumeMethodParameters)methodParameters;
            AudioManager.Instance.SetVolume(parameters.Volume, GetGroupFromName(parameters.GroupName));
        }

        private void FadeGroupVolumeButtonClicked(MethodParameters methodParameters)
        {
            FadeGroupVolumeMethodParameters parameters = (FadeGroupVolumeMethodParameters)methodParameters;
            AudioManager.Instance.FadeGroupVolume(parameters.FromTo.x, parameters.FromTo.y, parameters.Duration, GetGroupFromName(parameters.GroupName));
        }

        private void ReloadSceneButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private PlayClipMethodParameters GetPlayClipMethodParameters(MethodParameters methodParameters, out AudioClip audioClip, out SoundType group, out PitchShiftType pitchShiftType)
        {
            PlayClipMethodParameters parameters = (PlayClipMethodParameters)methodParameters;

            audioClip = uiInitialisor.GetAudioClipByName(parameters.AudioClipName);
            group = GetGroupFromName(parameters.GroupName);
            pitchShiftType = GetPitchShiftTypeFromName(parameters.PitchShiftTypeName);

            return parameters;
        }

        private SoundType GetGroupFromName(string groupName)
        {
            return Enum.Parse<SoundType>(groupName);
        }

        private PitchShiftType GetPitchShiftTypeFromName(string pitchShiftTypeName)
        {
            return Enum.Parse<PitchShiftType>(pitchShiftTypeName);
        }
    }
}