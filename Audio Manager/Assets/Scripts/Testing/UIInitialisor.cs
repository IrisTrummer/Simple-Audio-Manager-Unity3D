using System.Collections.Generic;
using System.Linq;
using Testing.MethodConfigurationMenus;
using UnityEngine;

namespace Testing
{
    public class UIInitialisor : MonoBehaviour
    {
        [SerializeField]
        private StopClipMethodConfigurationMenu stopClip;

        [SerializeField]
        private AudioClip[] audioClips;
        
        private const string ClipFieldName = "Clip";

        private void Start()
        {
            Initialise();
        }

        public AudioClip GetAudioClipByName(string name)
        {
            return audioClips.FirstOrDefault(c => c.name == name);
        }

        private void Initialise()
        {
            stopClip.InitialiseSelf();
            stopClip.InitialiseClips(ClipFieldName, GetAudioClipNames());
        }

        private List<string> GetAudioClipNames()
        {
            return audioClips.Select(c => c.name).ToList();
        }
    }
}
