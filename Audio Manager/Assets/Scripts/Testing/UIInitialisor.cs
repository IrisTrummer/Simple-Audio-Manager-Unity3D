using System;
using System.Collections.Generic;
using System.Linq;
using SimpleAudioManager;
using Testing.MethodConfigurationMenus;
using UnityEngine;

namespace Testing
{
    public class UIInitialisor : MonoBehaviour
    {
        [SerializeField]
        private StopClipMethodConfigurationMenu stopClip;

        public StopClipMethodConfigurationMenu StopClip => stopClip;

        [SerializeField]
        private PlayClipMethodConfigurationMenu playOnce;

        public PlayClipMethodConfigurationMenu PlayOnce => playOnce;

        [SerializeField]
        private PlayClipMethodConfigurationMenu playOnLoop;

        public PlayClipMethodConfigurationMenu PlayOnLoop => playOnLoop;

        [SerializeField]
        private PlayClipAtPositionMethodConfigurationMenu playAtPosition;

        public PlayClipAtPositionMethodConfigurationMenu PlayAtPosition => playAtPosition;

        [SerializeField]
        private SetVolumeMethodConfigurationMenu setVolume;

        public SetVolumeMethodConfigurationMenu SetVolume => setVolume;

        [SerializeField]
        private FadeGroupVolumeMethodConfigurationMenu fadeGroupVolume;

        public FadeGroupVolumeMethodConfigurationMenu FadeGroupVolume => fadeGroupVolume;

        [SerializeField]
        private MethodButton reloadScene;

        public MethodButton ReloadScene => reloadScene;

        [SerializeField]
        private AudioClip[] audioClips;

        private const float VolumeDefaultValue = 1f;
        private const float PitchDefaultValue = 1f;

        private void Start()
        {
            Initialise();
        }

        public AudioClip GetAudioClipByName(string name)
        {
            return audioClips.FirstOrDefault(c => c != null && c.name == name);
        }

        private void Initialise()
        {
            stopClip.Initialise(nameof(AudioManager.StopAudioClip), GetAudioClipNames());
            InitialisePlayConfiguration(playOnce, nameof(AudioManager.PlayOnce));
            InitialisePlayConfiguration(playOnLoop, nameof(AudioManager.PlayOnLoop));
            InitialisePlayConfiguration(playAtPosition, nameof(AudioManager.PlayAudioClipAtPosition));
            setVolume.Initialise(nameof(AudioManager.SetVolume), GetAudioGroupNames(), VolumeDefaultValue);
            fadeGroupVolume.Initialise(nameof(AudioManager.FadeGroupVolume), GetAudioGroupNames(), new Vector2(0, 1), 1);
        }

        private List<string> GetAudioClipNames()
        {
            return audioClips.Select(c => c == null ? "NULL" : c.name).ToList();
        }

        private List<string> GetAudioGroupNames()
        {
            List<string> names = new List<string>();

            foreach (SoundType soundType in Enum.GetValues(typeof(SoundType)))
                names.Add(soundType.ToString());

            return names;
        }

        private void InitialisePlayConfiguration(PlayClipMethodConfigurationMenu configuration, string methodName)
        {
            configuration.Initialise(methodName, GetAudioClipNames(), GetAudioGroupNames(), VolumeDefaultValue, PitchDefaultValue);
        }
    }
}