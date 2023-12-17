using System.Linq;
using UnityEngine;

#if !NO_SINGLETON_AUDIO_MANAGER
namespace SimpleAudioManager
{
    public partial class AudioManager
    {
        private static AudioManager instance;

        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    InitializeSingleton(FindObjectOfType<AudioManager>());

                    if (instance == null)
                        Debug.LogError($"The {nameof(AudioManager)} you were trying to access was not part of the scene! Add the component to a game object in the scene and try again.");
                }

                return instance;
            }
        }

        private static void InitializeSingleton(AudioManager audioManager)
        {
            if (audioManager == null)
                return;

            audioManager.CheckObjectSetup();

            if (instance == null)
            {
                instance = audioManager;
                DontDestroyOnLoad(audioManager.gameObject);
            }

            if (instance != audioManager)
            {
                Destroy(audioManager.gameObject);
            }
        }

        private void CheckObjectSetup()
        {
            if (transform.childCount > 0)
                Debug.LogWarning($"The {nameof(AudioManager)} component should be placed on a game object without child game objects", gameObject);

            if (GetComponents<Component>().Any(c => c.GetType() != typeof(AudioManager) && c.GetType() != typeof(Transform)))
                Debug.LogWarning($"The {nameof(AudioManager)} component should be the only component on a game object", gameObject);
            
            if(GetComponents<AudioManager>().Length > 1)
                Debug.LogError($"Only one {nameof(AudioManager)} component should be placed on a game object", gameObject);
        }
    }
}
#endif