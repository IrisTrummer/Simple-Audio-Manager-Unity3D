using ColorBindings;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Testing
{
    [RequireComponent(typeof(TMP_Text))]
    public class HyperlinkHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private string link;

        [SerializeField]
        private BindableColor hoverColor;

        [SerializeField]
        private BindableColor defaultColor;
        
        private TMP_Text text;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);

            if (linkIndex == -1)
                return;
            
            Application.OpenURL(link);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            text.color = hoverColor.Value;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            text.color = defaultColor.Value;
        }
    }
}