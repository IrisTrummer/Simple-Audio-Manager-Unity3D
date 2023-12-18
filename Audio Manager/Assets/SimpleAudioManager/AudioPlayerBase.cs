using Timing;
using UnityEngine;

namespace SimpleAudioManager
{
    /// <summary>
    /// Acts as a proxy for the <see cref="AudioManager"/>.
    /// </summary>
    public abstract class AudioPlayerBase : MonoBehaviour
    {
        [SerializeField]
        private AudioClip clip;

        [SerializeField]
        public bool playAtTarget;

        [SerializeField]
        public Transform target;

        [SerializeField]
        private bool playOnStart;

        [SerializeField]
        private bool loop;

        [SerializeField]
        private SoundType soundType = SoundType.Master;

        [SerializeField]
        private float volume = 1f;

        [SerializeField]
        private PitchShiftType pitchShift = PitchShiftType.None;

        [SerializeField]
        public bool fadeGroup;

        [SerializeField]
        [HideInInspector]
        public float fadeDuration = 1f;

        protected abstract AudioManager AudioManager { get; }

        private void Start()
        {
            if (playOnStart)
                PlaySound();
        }

        public void PlaySound()
        {
            // TODO get target group volume (might be overriden by "settings/init" method)
            // if (fadeGroup)
            //     audioManager.FadeGroup(0, targetVolume, fadeDuration, soundType);

            if (playAtTarget)
                AudioManager.PlayAudioClipAtPosition(clip, target.position, soundType, volume, loop, pitchShift);
            else if (loop)
                AudioManager.PlayOnLoop(clip, soundType, volume, pitchShift);
            else
                AudioManager.PlayOnce(clip, soundType, volume, pitchShift);
        }

        /// <summary>
        /// Stops the audio clip if it is looping.
        /// </summary>
        public void Stop(bool suppressFading = false)
        {
            if (fadeGroup && !suppressFading)
            {
                AudioManager.FadeGroupVolumeTo(0, fadeDuration, soundType);
                Delay.Create(fadeDuration, () => AudioManager.StopAudioClip(clip));
            }
            else
            {
                AudioManager.StopAudioClip(clip);
            }
        }

        /// <summary>
        /// Fades the audio clip's group to the given percentage.
        /// </summary>
        public void FadeGroupVolumeTo(float targetValue, float duration)
        {
            AudioManager.FadeGroupVolumeTo(targetValue, duration, soundType);
        }
    }
}