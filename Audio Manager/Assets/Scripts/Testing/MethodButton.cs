using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Testing
{
    public class MethodButton : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        public UnityAction ButtonClicked;

        private void Awake()
        {
            button.onClick.AddListener(RaiseButtonPressedEvent);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(RaiseButtonPressedEvent);
        }

        private void RaiseButtonPressedEvent()
        {
            ButtonClicked.Invoke();
        }
    }
}