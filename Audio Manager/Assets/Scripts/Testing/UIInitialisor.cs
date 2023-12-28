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

        [SerializeField]
        private PlayClipMethodConfigurationMenu playOnce;

        [SerializeField]
        private AudioClip[] audioClips;

        private void Start()
        {
            Initialise();
        }

        public AudioClip GetAudioClipByName(string name)
        {
            return audioClips.FirstOrDefault(c => c.name == name);
        }

        private void Initialise()
        {
            stopClip.InitialiseSelf();
            stopClip.InitialiseClips(GetAudioClipNames());

            playOnce.InitialiseSelf();
            playOnce.Initialise(GetAudioClipNames(), GetAudioGroupNames());
        }

        private List<string> GetAudioClipNames()
        {
            return audioClips.Select(c => c.name).ToList();
        }

        private List<string> GetAudioGroupNames()
        {
            List<string> names = new List<string>();

            foreach (SoundType soundType in Enum.GetValues(typeof(SoundType)))
                names.Add(soundType.ToString());

            return names;
        }
    }
}