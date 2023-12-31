using Testing.AudioMixer;
using UnityEngine;

namespace Testing
{
    public class ActiveAudioSourceDisplay : DebugInformationPanel
    {
        [SerializeField]
        private AudioSourceProvider audioSourceProvider;
        
        // TODO also display clip parameters (loop, pitch, volume, maybe position)

        protected override AudioSource[] GetReferencesSources()
        {
            return audioSourceProvider.GetActiveAudioSources();
        }

        protected override DebugInformationElement CreateNewElement(AudioSource audioSource)
        {
            AudioSourceDebugInformationElement element = (AudioSourceDebugInformationElement)base.CreateNewElement(audioSource);
            element.TrackTimeOfAudioSource(audioSource);

            return element;
        }
    }
}