using SimpleAudioManager;
using UnityEngine;

namespace Testing
{
    public class SoundEffectTestPlayer : MonoBehaviour
    {
        [SerializeField]
        private SoundEffect soundEffect;

        private void Start()
        {
            AudioManager.Instance.PlayOnLoop(soundEffect, SoundType.Effects);
        }
    }
}
