using UnityEngine;
using UnityEngine.UI;

namespace Testing
{
    [RequireComponent(typeof(RawImage))]
    public class ScrollingUIImage : MonoBehaviour
    {
        [SerializeField]
        private Vector2 scrollSpeed = Vector2.one;
        
        private RawImage image;

        private void Awake()
        {
            image = GetComponent<RawImage>();
        }

        private void Update()
        {
            image.uvRect = new Rect(image.uvRect.position + scrollSpeed * Time.deltaTime, image.uvRect.size);
        }
    }
}
