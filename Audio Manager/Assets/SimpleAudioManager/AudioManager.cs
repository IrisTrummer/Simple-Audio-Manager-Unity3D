using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

namespace SimpleAudioManager
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer audioMixer;

        [Header("Scene Loading")]
        [SerializeField]
        [Tooltip("Indicates the duration for fading in sounds when loading levels.")]
        private float fadeInDuration = 2f;

        [SerializeField]
        [Tooltip("Indicates the duration for fading out sounds when loading levels.")]
        private float fadeOutDuration = 1f;

        private ObjectPool<AudioSource> audioSources;
        private readonly Dictionary<AudioClip, AudioSource> loopingAudioSources = new();

        private void Awake()
        {
            audioSources = new ObjectPool<AudioSource>(Create, OnGet, OnRelease);

            AudioSource Create()
            {
                GameObject go = new GameObject("AudioSource");
                go.transform.SetParent(transform);
                
                return go.AddComponent<AudioSource>();
            }

            void OnGet(AudioSource audioSource)
            {
                audioSource.enabled = true;
                audioSource.transform.SetParent(transform);
            }

            void OnRelease(AudioSource audioSource)
            {
                audioSource.enabled = false;
                audioSource.transform.SetParent(transform);
            }
        }

        private void Start()
        {
            // TODO test fading on scene change, fade out playing sources and stop all looping ones
            // TODO allow to load volume from init method
        }

        public void PlayOnce(AudioClip clip, SoundType soundType, float volume = 1f, PitchShiftType pitchShiftType = PitchShiftType.None)
        {
            if (clip == null)
                return;

            Play(SetupAudioSource(clip, Vector3.zero, soundType, volume, false, false, GetRandomPitchShift(pitchShiftType)), clip);
        }

        public void PlayOnce(AudioClip clip, SoundType soundType, float volume, float pitch)
        {
            if (clip == null)
                return;

            Play(SetupAudioSource(clip, Vector3.zero, soundType, volume, false, false, pitch), clip);
        }

        public void PlayOnLoop(AudioClip clip, SoundType soundType, float volume = 1f, PitchShiftType pitchShiftType = PitchShiftType.None)
        {
            if (loopingAudioSources.ContainsKey(clip) || clip == null)
                return;

            AudioSource audioSource = SetupAudioSource(clip, Vector3.zero, soundType, volume, false, true, GetRandomPitchShift(pitchShiftType));
            Play(audioSource, clip);

            loopingAudioSources.Add(clip, audioSource);
        }

        public void PlayAudioClipAtPosition(AudioClip clip, Vector3 position, SoundType soundType, float volume = 1f, bool loop = false, PitchShiftType pitchShiftType = PitchShiftType.None)
        {
            if (clip == null)
            {
                throw new ArgumentNullException(nameof(clip), "There is no audio clip to play!");
            }

            Play(SetupAudioSource(clip, position, soundType, volume, true, loop, GetRandomPitchShift(pitchShiftType)), clip);
        }

        private AudioSource SetupAudioSource(AudioClip clip, Vector3 position, SoundType soundType, float volume, bool is3D, bool loop, float pitch)
        {
            AudioSource audioSource = audioSources.Get();
            ConfigureAudioSource(ref audioSource, clip, position, volume, soundType, is3D, loop, pitch);
            return audioSource;
        }

        private void ConfigureAudioSource(ref AudioSource audioSource, AudioClip clip, Vector3 position, float volume, SoundType soundType, bool is3D, bool loop, float pitch)
        {
            audioSource.transform.position = position;
            audioSource.clip = clip;
            audioSource.spatialBlend = is3D ? 1f : 0f;
            audioSource.outputAudioMixerGroup = GetAudioMixerGroupForSoundType(soundType);
#if MUTE_AUDIO_MANAGER
            audioSource.volume = 0;
#else
            audioSource.volume = volume;
#endif
            audioSource.loop = loop;
            audioSource.pitch = pitch;
        }

        private void Play(AudioSource audioSource, AudioClip audioClip)
        {
            audioSource.Play();

            if (!audioSource.loop)
            {
                // TODO delay package
                // Delay.Create(audioSource.clip.length, () => ReturnAudioSourceToPool(audioSource));
            }
        }

        private void ReturnAudioSourceToPool(AudioSource audioSource)
        {
            audioSources.Release(audioSource);
        }

        public void StopAudioClip(AudioClip audioClip)
        {
            if (audioClip == null || !loopingAudioSources.ContainsKey(audioClip))
                return;

            AudioSource audioSource = loopingAudioSources[audioClip];
            loopingAudioSources.Remove(audioClip);

            StopAudioSource(audioSource);
        }

        private void StopAllLoopingAudioSources(float delay = 0)
        {
            foreach (var loopingAudioSource in loopingAudioSources)
            {
                if (Mathf.Approximately(delay, 0))
                    StopAudioSource(loopingAudioSource.Value);
                // TODO delay package
                // else
                //     Delay.Create(delay, () => StopAudioSource(loopingAudioSource.Value), true);
            }

            loopingAudioSources.Clear();
        }

        private void StopAudioSource(AudioSource audioSource)
        {
            if (audioSource == null)
                return;

            audioSource.Stop();
            ReturnAudioSourceToPool(audioSource);
        }

        // TODO investigate range package
        // private Range<float> GetPitchRangeForPitchShiftType(PitchShiftType pitchShiftType)
        // {
        //     switch (pitchShiftType)
        //     {
        //         case PitchShiftType.None:
        //             return new Range<float>(1f, 1f);
        //         case PitchShiftType.Small:
        //             return new Range<float>(0.9f, 1.1f);
        //         case PitchShiftType.Medium:
        //             return new Range<float>(0.75f, 1.25f);
        //         case PitchShiftType.Large:
        //             return new Range<float>(0.5f, 1.5f);
        //         default:
        //             throw new ArgumentOutOfRangeException(nameof(pitchShiftType), pitchShiftType, null);
        //     }
        // }

        private float GetRandomPitchShift(PitchShiftType pitchShiftType)
        {
            // TODO investigate range package
            return 1;

            // return GetPitchRangeForPitchShiftType(pitchShiftType).Random();
        }

        private string GetNameFromSoundType(SoundType soundType)
        {
            switch (soundType)
            {
                case SoundType.Master:
                    return "Master";
                case SoundType.Background:
                    return "Background";
                case SoundType.Effects:
                    return "Effects";
                case SoundType.UI:
                    return "UI";
                default:
                    throw new ArgumentOutOfRangeException(nameof(soundType), soundType, null);
            }
        }

        private AudioMixerGroup GetAudioMixerGroupForSoundType(SoundType soundType)
        {
            return audioMixer.FindMatchingGroups(GetNameFromSoundType(soundType))[0];
        }

        public void SetVolume(float value, SoundType soundType)
        {
            float decibel = DecibelHelper.LinearToDecibel(value);
            audioMixer.SetFloat(GetNameFromSoundType(soundType), decibel);
        }

        private void FadeAllGroupsTo(float value, float duration)
        {
            foreach (SoundType soundType in (SoundType[]) Enum.GetValues(typeof(SoundType)))
            {
                FadeGroupTo(value, duration, soundType);
            }
        }

        public void FadeGroupTo(float value, float duration, SoundType soundType)
        {
            // TODO check if coroutine for group runs already; maybe we can use tweens?
            audioMixer.GetFloat(GetNameFromSoundType(soundType), out float start);
            start = DecibelHelper.DecibelToLinear(start);

            FadeGroup(start, value, duration, soundType);
        }

        public void FadeGroup(float startValue, float endValue, float duration, SoundType soundType)
        {
            if (duration == 0)
            {
                SetVolume(endValue, soundType);
                return;
            }

            StartCoroutine(FadeRoutine(startValue, endValue, duration, soundType));
        }

        private IEnumerator FadeRoutine(float startValue, float endValue, float duration, SoundType soundType)
        {
            float timeSinceStart = 0;
            while (timeSinceStart < duration)
            {
                timeSinceStart += Time.unscaledDeltaTime;

                float volume = Mathf.Lerp(startValue, endValue, timeSinceStart / duration);
                SetVolume(volume, soundType);

                yield return null;
            }
        }

        private void OnDestroy()
        {
            StopAllLoopingAudioSources();

            StopAllCoroutines();
        }
    }
}