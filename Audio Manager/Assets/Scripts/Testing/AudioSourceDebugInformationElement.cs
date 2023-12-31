using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Testing
{
    public class AudioSourceDebugInformationElement : DebugInformationElement
    {
        [SerializeField]
        private TMP_Text timeText;

        [SerializeField]
        private float minDisplayLength = 0.1f;
        
        [SerializeField]
        private Image loopIndicator;
        
        [SerializeField]
        private TMP_Text pitchText;

        [SerializeField]
        private TMP_Text volumeText;

        private AudioSource audioSource;

        protected override void Update()
        {
            base.Update();
            
            if (ShouldBeKilled)
            {
                timeText.SetTextBetweenTags("-");
                return;
            }
            
            if (audioSource != null)
                UpdateTime(audioSource.clip.length, audioSource.time);
        }

        public void SetReferenceAudioSource(AudioSource audioSource)
        {
            this.audioSource = audioSource;
            loopIndicator.enabled = audioSource.loop;
            pitchText.SetTextBetweenTags(audioSource.pitch.ToString("#0.00"));
            volumeText.SetTextBetweenTags(audioSource.volume.ToString("#0.00"));
        }

        private void UpdateTime(float clipLengthSeconds, float playTimeSeconds)
        {
            float remainingTime = (clipLengthSeconds - playTimeSeconds) * 10;
            string remainingTimeText = audioSource.clip.length >= minDisplayLength ? remainingTime.ToString("00.0") : "< 0.1s";

            timeText.SetTextBetweenTags(remainingTimeText);
        }
    }
}