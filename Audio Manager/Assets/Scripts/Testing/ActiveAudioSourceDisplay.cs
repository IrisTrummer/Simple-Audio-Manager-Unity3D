using Testing.AudioMixer;
using UnityEngine;

namespace Testing
{
    public class ActiveAudioSourceDisplay : DebugInformationPanel
    {
        [SerializeField]
        private AudioSourceProvider audioSourceProvider;
        
        protected override AudioSource[] GetReferencesSources()
        {
            return audioSourceProvider.GetActiveAudioSources();
        }

        protected override DebugInformationElement CreateNewElement(AudioSource audioSource)
        {
            AudioSourceDebugInformationElement element = (AudioSourceDebugInformationElement)base.CreateNewElement(audioSource);
            element.SetReferenceAudioSource(audioSource);

            return element;
        }
    }
}