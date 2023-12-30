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

            float arrowPosition = -Mathf.Lerp(MaxTopOffset, -MaxTopOffset, linearVolume / 10f);
            arrow.offsetMax = new Vector2(arrow.offsetMax.x, Mathf.Lerp(arrow.offsetMax.y, arrowPosition, Time.deltaTime * volumeSampleAdaptionSpeed));

            AudioListener.GetOutputData(samples, 0);
            AdjustBarHeight(volume, volumeSampleRectLeft);
            
            AudioListener.GetOutputData(samples, 1);
            AdjustBarHeight(volume, volumeSampleRectRight);
        }
        
        private float GetDecibelGroupVolume()
        {
            AudioManager.Instance.AudioMixer.GetFloat($"{soundType}Volume", out float volume);
            return volume;
        }

        private void AdjustBarHeight(float volume, RectTransform rectTransform)
        {
            float top = -Mathf.Lerp(MaxTopOffset, 0, samples.Max() + (volume + 80) / MaxTopOffset);
            top = Mathf.Lerp(rectTransform.offsetMax.y, top, Time.deltaTime * volumeSampleAdaptionSpeed);

            rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, top);
        }
    }
}