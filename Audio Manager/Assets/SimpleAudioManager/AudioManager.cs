using System;
using System.Collections;
using System.Collections.Generic;
using RangePrimitive;
using Timing;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;
using UnityEngine.Pool;

namespace SimpleAudioManager
{
    public partial class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer audioMixer;

        public AudioMixer AudioMixer => audioMixer;

        private ObjectPool<AudioSource> audioSources;
        private readonly Dictionary<AudioClip, AudioSource> loopingAudioSources = new();

        public IReadOnlyDictionary<AudioClip, AudioSource> LoopingClips => loopingAudioSources;

        private void Awake()
        {
#if !NO_SINGLETON_AUDIO_MANAGER
            InitializeSingleton(this);
#endif

            SetupAudioSourcePool();

            Assert.IsNotNull(audioMixer, $"You need to specify an {nameof(AudioMixer)} for the {nameof(AudioManager)}!");

#if UNITY_EDITOR
            foreach (SoundType soundType in Enum.GetValues(typeof(SoundType)))
            {
                Assert.IsTrue(TryGetAudioMixerGroupForSoundType(soundType, out _),
                    $"The audio group {soundType} is not present on the specified {nameof(AudioMixer)}. You can add groups by going to Window > Audio > AudioMixer.");
            }
#endif
        }

        private void OnDestroy()
        {
            StopAllLoopingAudioSources();
            StopAllCoroutines();
        }

        /// <summary>
        /// Plays the given audio clip with the specified parameters once.
        /// </summary>
        public void PlayOnce(AudioClip clip, SoundType soundType, float pitch, float volume = 1f)
        {
            if (clip == null)
                return;

            Play(SetupAudioSource(clip, Vector3.zero, soundType, volume, false, false, pitch));
        }

        /// <summary>
        /// <inheritdoc cref="PlayOnce(UnityEngine.AudioClip,SimpleAudioManager.SoundType,float,float)"/>
        /// If not set to "None", the pitch is taken randomly from the corresponding allowed range.
        /// </summary>
        public void PlayOnce(AudioClip clip, SoundType soundType, float volume = 1f, PitchShiftType pitchShiftType = PitchShiftType.None)
        {
            PlayOnce(clip, soundType, GetRandomPitchShift(pitchShiftType), volume);
        }

        /// <summary>
        /// Plays the given audio clip with the specified parameters on loop.
        /// To stop the clip again, use <see cref="StopAudioClip"/>.
        /// </summary>
        public void PlayOnLoop(AudioClip clip, SoundType soundType, float pitch, float volume = 1f)
        {
            if (clip == null || loopingAudioSources.ContainsKey(clip))
                return;

            AudioSource audioSource = SetupAudioSource(clip, Vector3.zero, soundType, volume, false, true, pitch);
            Play(audioSource);
        }

        /// <summary>
        /// <inheritdoc cref="PlayOnLoop(UnityEngine.AudioClip,SimpleAudioManager.SoundType,float,float)"/>
        /// If not set to "None", the pitch is taken randomly from the corresponding allowed range.
        /// </summary>
        public void PlayOnLoop(AudioClip clip, SoundType soundType, float volume = 1f, PitchShiftType pitchShiftType = PitchShiftType.None)
        {
            PlayOnLoop(clip, soundType, GetRandomPitchShift(pitchShiftType), volume);
        }

        /// <summary>
        /// Plays the given audio clip with the specified parameters at the given position.
        /// </summary>
        public void PlayAudioClipAtPosition(AudioClip clip, Vector3 position, SoundType soundType, float pitch, float volume = 1f, bool loop = false)
        {
            if (clip == null)
                return;

            Play(SetupAudioSource(clip, position, soundType, volume, true, loop, pitch));
        }

        /// <summary>
        /// <inheritdoc cref="PlayAudioClipAtPosition(UnityEngine.AudioClip,UnityEngine.Vector3,SimpleAudioManager.SoundType,float,float,bool)"/>
        /// If not set to "None", the pitch is taken randomly from the corresponding allowed range.
        /// </summary>
        public void PlayAudioClipAtPosition(AudioClip clip, Vector3 position, SoundType soundType, float volume = 1f, bool loop = false, PitchShiftType pitchShiftType = PitchShiftType.None)
        {
            PlayAudioClipAtPosition(clip, position, soundType, GetRandomPitchShift(pitchShiftType), volume, loop);
        }

        /// <summary>
        /// Stops the given audio clip. Only works for looping clips.
        /// </summary>
        /// <param name="audioClip"></param>
        public void StopAudioClip(AudioClip audioClip)
        {
            if (audioClip == null || !loopingAudioSources.ContainsKey(audioClip))
                return;

            AudioSource audioSource = loopingAudioSources[audioClip];
            loopingAudioSources.Remove(audioClip);

            StopAudioSource(audioSource);
        }

        /// <summary>
        /// Sets the volume of the audio group corresponding to the sound type.
        /// </summary>
        public void SetVolume(float value, SoundType soundType)
        {
            value = Mathf.Max(0, value);
            float decibel = DecibelHelper.LinearToDecibel(value);

            if (!TryGetExposedParameterNameFromSoundType(soundType, out string parameterName))
            {
                Debug.LogError($"Tried to set volume on invalid soundType: {soundType}. You need to configure the corresponding group in the audio mixer first.");
                return;
            }

            audioMixer.SetFloat(parameterName, decibel);
        }

        /// <summary>
        /// Smoothly fades the volume of the whole group corresponding to the given sound type to the specified value.
        /// </summary>
        public void FadeGroupVolumeTo(float value, float duration, SoundType soundType)
        {
            if (!TryGetNameFromSoundType(soundType, out string groupName))
            {
                Debug.LogError($"Tried to fade volume of invalid group: {soundType}. You need to configure the corresponding group in the audio mixer first.");
                return;
            }

            audioMixer.GetFloat(groupName, out float start);
            start = DecibelHelper.DecibelToLinear(start);

            FadeGroupVolume(start, value, duration, soundType);
        }

        /// <summary>
        /// Smoothly fades the volume of the whole group corresponding to the given sound type to the specified value, starting from the given start value.
        /// If you want to start from the current volume of the group, use <see cref="FadeGroupVolume"/> instead.
        /// </summary>
        public void FadeGroupVolume(float startValue, float endValue, float duration, SoundType soundType)
        {
            if (duration == 0)
            {
                SetVolume(endValue, soundType);
                return;
            }

            // TODO check if coroutine for group runs already; maybe we can use tweens?
            StartCoroutine(FadeRoutine(startValue, endValue, duration, soundType));
        }

        private void SetupAudioSourcePool()
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

        private AudioSource SetupAudioSource(AudioClip clip, Vector3 position, SoundType soundType, float volume, bool is3D, bool loop, float pitch)
        {
            AudioSource audioSource = audioSources.Get();
            ConfigureAudioSource(audioSource, clip, position, volume, soundType, is3D, loop, pitch);

            return audioSource;
        }

        private void ConfigureAudioSource(AudioSource audioSource, AudioClip clip, Vector3 position, float volume, SoundType soundType, bool is3D, bool loop, float pitch)
        {
            if (!TryGetAudioMixerGroupForSoundType(soundType, out AudioMixerGroup audioMixerGroup))
            {
                Debug.LogError($"Tried to play audio clip with invalid soundType: {soundType}. You need to configure the corresponding group in the audio mixer first.");
                audioMixerGroup = GetDefaultAudioMixerGroup();
            }

            audioSource.clip = clip;
            audioSource.outputAudioMixerGroup = audioMixerGroup;
            audioSource.loop = loop;
            audioSource.pitch = pitch;
            audioSource.transform.position = position;
            audioSource.spatialBlend = is3D ? 1f : 0f;

#if MUTE_AUDIO_MANAGER
            audioSource.volume = 0;
#else
            audioSource.volume = volume;
#endif
        }

        private bool TryGetNameFromSoundType(SoundType soundType, out string name)
        {
            switch (soundType)
            {
                case SoundType.Master:
                    name = "Master";
                    break;
                case SoundType.Background:
                    name = "Background";
                    break;
                case SoundType.Effects:
                    name = "Effects";
                    break;
                case SoundType.UI:
                    name = "UI";
                    break;
                default:
                    name = "";
                    return false;
            }

            return true;
        }

        private bool TryGetAudioMixerGroupForSoundType(SoundType soundType, out AudioMixerGroup audioMixerGroup)
        {
            audioMixerGroup = null;

            if (!TryGetNameFromSoundType(soundType, out string groupName))
                return false;

            AudioMixerGroup[] matchingGroups = audioMixer.FindMatchingGroups(groupName);

            if (matchingGroups.Length <= 0)
                return false;

            audioMixerGroup = matchingGroups[0];
            return true;
        }

        private AudioMixerGroup GetDefaultAudioMixerGroup()
        {
            return audioMixer.FindMatchingGroups("Master")[0];
        }

        private void Play(AudioSource audioSource)
        {
            audioSource.Play();

            if (audioSource.loop)
            {
                loopingAudioSources.Add(audioSource.clip, audioSource);
            }
            else
            {
                Delay.Create(audioSource.clip.length, () => ReturnAudioSourceToPool(audioSource));
            }
        }

        private void ReturnAudioSourceToPool(AudioSource audioSource)
        {
            audioSources.Release(audioSource);
        }

        private void StopAudioSource(AudioSource audioSource)
        {
            if (audioSource == null)
                return;

            audioSource.Stop();
            ReturnAudioSourceToPool(audioSource);
        }

        private void StopAllLoopingAudioSources(float delay = 0)
        {
            foreach (var loopingAudioSource in loopingAudioSources)
            {
                if (Mathf.Approximately(delay, 0))
                    StopAudioSource(loopingAudioSource.Value);
                else
                    Delay.Create(delay, () => StopAudioSource(loopingAudioSource.Value), true);
            }

            loopingAudioSources.Clear();
        }

        private bool TryGetPitchRangeForPitchShiftType(PitchShiftType pitchShiftType, out Range<float> pitchRange)
        {
            switch (pitchShiftType)
            {
                case PitchShiftType.None:
                    pitchRange = new Range<float>(1f, 1f);
                    break;
                case PitchShiftType.Small:
                    pitchRange = new Range<float>(0.9f, 1.1f);
                    break;
                case PitchShiftType.Medium:
                    pitchRange = new Range<float>(0.75f, 1.25f);
                    break;
                case PitchShiftType.Large:
                    pitchRange = new Range<float>(0.5f, 1.5f);
                    break;
                default:
                    pitchRange = new Range<float>(0, 0);
                    return false;
            }

            return true;
        }

        private float GetRandomPitchShift(PitchShiftType pitchShiftType)
        {
            if (!TryGetPitchRangeForPitchShiftType(pitchShiftType, out Range<float> pitchRange))
            {
                Debug.LogError($"Tried to get pitch range for invalid pitch shift type: {pitchShiftType}");
                return 0;
            }

            return pitchRange.Random();
        }

        private void FadeAllGroupsTo(float value, float duration)
        {
            foreach (SoundType soundType in Enum.GetValues(typeof(SoundType)))
            {
                FadeGroupVolumeTo(value, duration, soundType);
            }
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

        private bool TryGetExposedParameterNameFromSoundType(SoundType soundType, out string parameterName)
        {
            parameterName = String.Empty;

            if (!TryGetNameFromSoundType(soundType, out string groupName))
                return false;

            parameterName = $"{groupName}Volume";
            return true;
        }
    }
}