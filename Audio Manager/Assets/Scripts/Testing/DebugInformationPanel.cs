using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Testing
{
    public abstract class DebugInformationPanel : MonoBehaviour
    {
        [SerializeField]
        private DebugInformationElement debugInformationElementPrefab;

        [SerializeField]
        private Transform elementParent;

        [SerializeField]
        private int maxElementCount = 5;

        [SerializeField]
        private Image overflowIndicator;

        protected readonly Dictionary<AudioSource, DebugInformationElement> SpawnedElements = new();
        
        private readonly List<AudioSource> elementsToDelete = new();

        private void Awake()
        {
            for (int i = 0; i < elementParent.childCount; i++)
                Destroy(elementParent.GetChild(i).gameObject);

            overflowIndicator.gameObject.SetActive(false);
        }

        private void Update()
        {
            CreateMissingElements();
            RemoveExcessiveElements();
        }
        
        protected abstract AudioSource[] GetReferencesSources();
        
        protected virtual void OnButtonPressed(DebugInformationElement debugInformationElement)
        {
        }

        private void CreateMissingElements()
        {
            foreach (var audioSource in GetReferencesSources())
            {
                if (!SpawnedElements.ContainsKey(audioSource))
                    CreateNewElement(audioSource);
            }
        }

        private void RemoveExcessiveElements()
        {
            foreach (var spawnedInformationElement in SpawnedElements)
            {
                if (!GetReferencesSources().Contains(spawnedInformationElement.Key))
                    elementsToDelete.Add(spawnedInformationElement.Key);
            }

            foreach (AudioSource element in elementsToDelete)
                DeleteElement(element);

            elementsToDelete.Clear();
        }

        protected virtual DebugInformationElement CreateNewElement(AudioSource audioSource)
        {
            DebugInformationElement element = Instantiate(debugInformationElementPrefab, elementParent);
            element.Configure(SpawnedElements.Count + 1, audioSource.clip.name, audioSource.outputAudioMixerGroup.name);
            element.ButtonPress += OnButtonPressed;

            SpawnedElements.Add(audioSource, element);

            if (SpawnedElements.Count > maxElementCount)
            {
                overflowIndicator.gameObject.SetActive(true);
                element.gameObject.SetActive(false);
            }

            return element;
        }

        private void DeleteElement(AudioSource audioSource)
        {
            if (!SpawnedElements.TryGetValue(audioSource, out DebugInformationElement element))
                return;

            Destroy(element.gameObject);
            SpawnedElements.Remove(audioSource);
            element.ButtonPress -= OnButtonPressed;

            foreach (var e in SpawnedElements)
            {
                if (e.Value.Index >= element.Index)
                    e.Value.SetIndex(e.Value.Index - 1);

                e.Value.gameObject.SetActive(e.Value.Index <= maxElementCount);
            }

            overflowIndicator.gameObject.SetActive(SpawnedElements.Count > maxElementCount);
        }
    }
}