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
        private float volumeSampleAdaptionSpeed = 4f;

        private readonly float[] samples = new float[64];
        private const float MaxTopOffset = 360;

        private void Awake()
        {
            groupNameText.SetTextBetweenTags(soundType.ToString());
        }

        private void Update()
        {
            // TODO WIP: calculations not correct
            float volume = GetDecibelGroupVolume();
            float linearVolume = DecibelHelper.DecibelToLinear(volume);
            volumeText.SetTextBetweenTags(linearVolume.ToString("#0.##"));

            float arrowPosition = -Mathf.Lerp(MaxTopOffset, -MaxTopOffset, GetHeightPercentageForDecibelVolume(volume));
            arrow.offsetMax = new Vector2(arrow.offsetMax.x, Mathf.Lerp(arrow.offsetMax.y, arrowPosition, Time.deltaTime * volumeSampleAdaptionSpeed));

            // TODO exchange; just for testing
            AudioSource audioSource = AudioManager.Instance.transform.GetChild(6).GetComponent<AudioSource>();

            float sourceVolume0 = ComputeSourceVolume(audioSource, 0);
            AdjustBarHeight(sourceVolume0, volumeSampleRectLeft);

            float sourceVolume1 = ComputeSourceVolume(audioSource, 1);
            AdjustBarHeight(sourceVolume1, volumeSampleRectRight);

            Debug.Log($"{sourceVolume0} | {sourceVolume1}");
        }

        private float ComputeSourceVolume(AudioSource audioSource, int channel)
        {
            audioSource.GetOutputData(samples, channel);
            
            // formula taken from: https://forum.unity.com/threads/how-to-accurately-calculate-audio-decibels.321764/
            float rms = Mathf.Sqrt(samples.Sum(s => Mathf.Pow(s, 2)) / samples.Length);
            return 10 * Mathf.Log10(rms);
        }

        private float GetDecibelGroupVolume()
        {
            AudioManager.Instance.AudioMixer.GetFloat($"{soundType}Volume", out float volume);
            return volume;
        }

        private void AdjustBarHeight(float volume, RectTransform rectTransform)
        {
            float t = GetHeightPercentageForDecibelVolume(volume, 1);
            float top = -Mathf.Lerp(MaxTopOffset, 0, t);
            top = Mathf.Lerp(rectTransform.offsetMax.y, top, Time.deltaTime * volumeSampleAdaptionSpeed);

            rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, top);
        }

        private float GetHeightPercentageForDecibelVolume(float volume, float percentageToUpper = 1f)
        {
            float[] lookupTable = { 1, 0.745f, 0.431f, 0.257f, 0.129f, 0f };

            float lower = lookupTable[Mathf.Clamp(Mathf.CeilToInt((volume - 20.01f) / -20f), 0, lookupTable.Length)];
            float upper = lookupTable[Mathf.Clamp(Mathf.FloorToInt((volume - 20.01f) / -20f), 0, lookupTable.Length)];

            float t = (volume + 80) / 20;
            t -= (int)t;
            t += -1 + percentageToUpper;
            return Mathf.Lerp(lower, upper, percentageToUpper);
        }
    }
}