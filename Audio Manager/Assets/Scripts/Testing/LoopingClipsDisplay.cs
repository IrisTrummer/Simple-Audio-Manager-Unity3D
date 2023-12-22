using System.Collections.Generic;
using System.Linq;
using SimpleAudioManager;
using UnityEngine;

namespace Testing
{
    public class LoopingClipsDisplay : MonoBehaviour
    {
        [SerializeField]
        private DebugInformationElement debugInformationElementPrefab;

        [SerializeField]
        private Transform elementParent;

        private readonly Dictionary<AudioClip, DebugInformationElement> spawnedElements = new();
        private readonly List<AudioClip> elementsToDelete = new();

        private void Awake()
        {
            for (int i = 0; i < elementParent.childCount; i++)
                Destroy(elementParent.GetChild(i).gameObject);
        }

        private void Update()
        {
            CreateMissingElements();
            RemoveExcessiveElements();
        }

        private void CreateMissingElements()
        {
            foreach (var loopingClip in AudioManager.Instance.LoopingClips)
            {
                if (!spawnedElements.ContainsKey(loopingClip.Key))
                    CreateNewElement(loopingClip.Key, loopingClip.Value);
            }
        }

        private void RemoveExcessiveElements()
        {
            foreach (var spawnedInformationElement in spawnedElements)
            {
                if (!AudioManager.Instance.LoopingClips.ContainsKey(spawnedInformationElement.Key))
                    elementsToDelete.Add(spawnedInformationElement.Key);
            }

            foreach (AudioClip audioClip in elementsToDelete)
                DeleteElement(audioClip);

            elementsToDelete.Clear();
        }

        private void CreateNewElement(AudioClip clip, AudioSource source)
        {
            // TODO max elements
            DebugInformationElement element = Instantiate(debugInformationElementPrefab, elementParent);
            element.Configure(spawnedElements.Count + 1, clip.name, source.outputAudioMixerGroup.name);
            element.ButtonPress += OnButtonPressed;

            spawnedElements.Add(clip, element);
        }

        private void DeleteElement(AudioClip clip)
        {
            if (!spawnedElements.TryGetValue(clip, out DebugInformationElement element))
                return;

            Destroy(element.gameObject);
            spawnedElements.Remove(clip);
            element.ButtonPress -= OnButtonPressed;

            foreach (KeyValuePair<AudioClip, DebugInformationElement> e in spawnedElements)
            {
                if (e.Value.Index >= element.Index)
                    e.Value.SetIndex(e.Value.Index - 1);
            }
        }

        private void OnButtonPressed(DebugInformationElement debugInformationElement)
        {
            AudioClip clip = spawnedElements.FirstOrDefault(e => e.Value == debugInformationElement).Key;

            if (clip == null)
                return;
            
            AudioManager.Instance.StopAudioClip(clip);
        }
    }
}