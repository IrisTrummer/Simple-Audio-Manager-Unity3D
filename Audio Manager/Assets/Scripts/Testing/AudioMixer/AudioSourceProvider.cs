using System;
using System.Linq;
using SimpleAudioManager;
using UnityEngine;

namespace Testing.AudioMixer
{
    public class AudioSourceProvider : MonoBehaviour
    {
        public AudioSource[] GetActiveAudioSources()
        {
            return AudioManager.Instance.transform.GetComponentsInChildren<AudioSource>().Where(a => a.isPlaying).ToArray();
        }

        public AudioSource[] GetActiveAudioSourcesForSoundType(SoundType soundType)
        {
            AudioSource[] sources = GetActiveAudioSources();
            return soundType == SoundType.Master ? sources : sources.Where(a => Enum.TryParse(a.outputAudioMixerGroup.name, out SoundType s) && s == soundType).ToArray();
        }
    }
}