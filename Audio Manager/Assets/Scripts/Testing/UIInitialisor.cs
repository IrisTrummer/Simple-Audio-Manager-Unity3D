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
        private AudioClip[] audioClips;

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
            // TODO add spaces for method names
            stopClip.InitialiseClips(nameof(AudioManager.StopAudioClip), GetAudioClipNames());
            InitialisePlayConfiguration(playOnce, nameof(AudioManager.PlayOnce));
            InitialisePlayConfiguration(playOnLoop, nameof(AudioManager.PlayOnLoop));
            InitialisePlayConfiguration(playAtPosition, nameof(AudioManager.PlayAudioClipAtPosition));
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
            configuration.Initialise(methodName, GetAudioClipNames(), GetAudioGroupNames(), 1, 1);
        }
    }
}