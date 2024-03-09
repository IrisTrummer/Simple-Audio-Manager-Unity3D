using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Testing
{
    public class HoverListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public UnityEvent OnHover;
        public UnityEvent OnUnhover;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnHover.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnUnhover.Invoke();
        }
    }
}
