using Core;
using TMPro;
using UnityEngine;

namespace Testing
{
    public class AudioSourceDebugInformationElement : DebugInformationElement
    {
        [SerializeField]
        private TMP_Text timeText;

        [SerializeField]
        private float minDisplayLength = 0.1f;

        private AudioSource audioSource;

        private void Update()
        {
            if (audioSource != null)
                UpdateTime(audioSource.clip.length, audioSource.time);
        }

        public void TrackTimeOfAudioSource(AudioSource audioSource)
        {
            this.audioSource = audioSource;
        }

        private void UpdateTime(float clipLengthSeconds, float playTimeSeconds)
        {
            float remainingTime = (clipLengthSeconds - playTimeSeconds) * 10;
            string remainingTimeText = audioSource.clip.length >= minDisplayLength ? remainingTime.ToString("00:0") : "< 0.1s";

            timeText.SetTextBetweenTags(remainingTimeText);
        }
    }
}