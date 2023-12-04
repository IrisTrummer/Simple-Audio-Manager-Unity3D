using UnityEngine;

namespace SimpleAudioManager
{
    /// <summary>
    /// Acts as a proxy for the <see cref="AudioManager"/>.
    /// </summary>
    public class AudioPlayer : MonoBehaviour
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

        // TODO Zenject support
        private readonly AudioManager audioManager;

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
                audioManager.PlayAudioClipAtPosition(clip, target.position, soundType, volume, loop, pitchShift);
            else if(loop)
                audioManager.PlayOnLoop(clip, soundType, volume, pitchShift);
            else
                audioManager.PlayOnce(clip, soundType, volume, pitchShift);
        }

        /// <summary>
        /// Stops the audio clip if it is looping.
        /// </summary>
        public void Stop(bool suppressFading = false)
        {
            if (fadeGroup && !suppressFading)
            {
                audioManager.FadeGroupTo(0, fadeDuration, soundType);
                // TODO delay package
                // Delay.Create(fadeDuration, () => audioManager.StopAudioClip(clip));
            }
            else
            {
                audioManager.StopAudioClip(clip);
            }
        }

        /// <summary>
        /// Fades the audio clip's group to the given percentage.
        /// </summary>
        public void FadeGroupTo(float targetValue, float duration)
        {
            audioManager.FadeGroupTo(targetValue, duration, soundType);
        }
    }
}