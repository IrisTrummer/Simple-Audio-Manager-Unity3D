using System;
using UnityEngine;

namespace SimpleAudioManager
{
    [Serializable]
    public class SoundEffect
    {
        [SerializeField]
        private AudioClip audioClip;

        public AudioClip Clip => audioClip;

        [SerializeField, Min(0)]
        private float volume;

        public float Volume => volume;
    }
}
