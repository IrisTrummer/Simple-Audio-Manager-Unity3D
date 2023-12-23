using ColorBindings;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Testing
{
    public class RawImageColorBinding : ColorBindingBase
    {
        [SerializeField]
        private BindableColor color;

        private RawImage image;

        protected override bool Initialize()
        {
            image = GetComponent<RawImage>();

            if (image == null)
            {
                Debug.LogError($"No {nameof(RawImage)} component found on '{gameObject.name}'", gameObject);
                return false;
            }

            return true;
        }

        protected override void SetColor()
        {
#if UNITY_EDITOR
            Undo.RecordObject(image, nameof(RawImage));
            PrefabUtility.RecordPrefabInstancePropertyModifications(image);
#endif

            image.color = color.Value;
        }

#if UNITY_EDITOR
        protected override void ClearColorInternal()
        {
            image.color = Color.clear;
        }
#endif
    }
}
