using System.Linq;
using Core;
using SimpleAudioManager;
using TMPro;
using UnityEngine;

namespace Testing.AudioMixer
{
    public class AudioMixerGroup : MonoBehaviour
    {
        [SerializeField]
        private SoundType soundType;

        [SerializeField]
        private TMP_Text groupNameText;

        [SerializeField]
        private TMP_Text activeClipCountText;

        [SerializeField]
        private AudioSourceProvider audioSourceProvider;

        [Header("Arrow")]
        [SerializeField]
        private TMP_Text volumeText;

        [SerializeField]
        private RectTransform arrow;

        [Header("Bars")]
        [SerializeField]
        private RectTransform volumeSampleRectLeft;

        [SerializeField]
        private RectTransform volumeSampleRectRight;

        [SerializeField]
        private float volumeSampleAdaptionSpeed = 5f;

        private readonly float[] decibelPositionLookupTable = { 1, 0.745f, 0.431f, 0.257f, 0.129f, 0f };

        private readonly float[] samples = new float[1024];
        private const float MaxTopOffset = 360;

        private void Awake()
        {
            groupNameText.SetTextBetweenTags(soundType.ToString());
        }

        private void Update()
        {
            float decibelGroupVolume = GetDecibelGroupVolume();

            volumeText.SetTextBetweenTags(DecibelHelper.DecibelToLinear(decibelGroupVolume).ToString("#0.##"));
            AdjustRectPositionToVolume(arrow, decibelGroupVolume, MaxTopOffset, -MaxTopOffset);

            AudioSource[] audioSources = audioSourceProvider.GetActiveAudioSourcesForSoundType(soundType);

            AdjustBarHeight(volumeSampleRectLeft, audioSources, 0, decibelGroupVolume);
            AdjustBarHeight(volumeSampleRectRight, audioSources, 1, decibelGroupVolume);
        }

        private void AdjustBarHeight(RectTransform rectTransform, AudioSource[] audioSources, int channel, float decibelGroupVolume)
        {
            float sourceVolume = ComputeSourceVolume(audioSources, channel, decibelGroupVolume);
            AdjustRectPositionToVolume(rectTransform, sourceVolume);
        }

        private float GetDecibelGroupVolume()
        {
            AudioManager.Instance.AudioMixer.GetFloat($"{soundType}Volume", out float volume);
            return volume;
        }

        private void AdjustRectPositionToVolume(RectTransform rectTransform, float volume, float bottomPosition = MaxTopOffset, float topPosition = 0)
        {
            float top = -Mathf.Lerp(bottomPosition, topPosition, GetHeightFactorForDecibelVolume(volume));
            float lerpedTop = Mathf.Lerp(rectTransform.offsetMax.y, top, Time.deltaTime * volumeSampleAdaptionSpeed);

            rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, lerpedTop);
        }

        private float GetHeightFactorForDecibelVolume(float volume)
        {
            float lower = decibelPositionLookupTable[Mathf.Clamp(Mathf.CeilToInt((volume - 20.01f) / -20f), 0, decibelPositionLookupTable.Length - 1)];
            float upper = decibelPositionLookupTable[Mathf.Clamp(Mathf.FloorToInt((volume - 20.01f) / -20f), 0, decibelPositionLookupTable.Length - 1)];

            float t = ((volume + 80) / 20).Decimals();
            if (Mathf.Approximately(t, 0))
                t = 1f;

            return Mathf.Lerp(lower, upper, t);
        }

        private float ComputeSourceVolume(AudioSource[] audioSources, int channel, float channelVolumeDecibel)
        {
            float poweredSum = 0;

            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.GetOutputData(samples, channel);
                poweredSum += samples.Sum(s => Mathf.Pow(s, 2));
            }

            float rms = Mathf.Sqrt(poweredSum / (audioSources.Length * samples.Length));
            return DecibelHelper.LinearToDecibel(rms * DecibelHelper.DecibelToLinear(channelVolumeDecibel));
        }
    }
}