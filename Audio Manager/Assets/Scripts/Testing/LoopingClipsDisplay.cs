using System.Linq;
using SimpleAudioManager;
using UnityEngine;

namespace Testing
{
    public class LoopingClipsDisplay : DebugInformationPanel
    {
        protected override AudioSource[] GetReferencesSources()
        {
            return AudioManager.Instance.LoopingClips.Select(c => c.Value).ToArray();
        }

        protected override void OnButtonPressed(DebugInformationElement debugInformationElement)
        {
            AudioSource audioSource = SpawnedElements.FirstOrDefault(e => e.Value == debugInformationElement).Key;

            if (audioSource == null)
                return;

            AudioManager.Instance.StopAudioClip(audioSource.clip);
        }
    }
}