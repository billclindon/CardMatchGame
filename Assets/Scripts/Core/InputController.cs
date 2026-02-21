using UnityEngine;
using UnityEngine.EventSystems;
using CardMatch.Cards;

namespace CardMatch.Core
{
    public class InputController : MonoBehaviour, IPointerClickHandler
    {
        // [SerializeField] private Camera uiCamera;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.pointerPressRaycast.gameObject == null)
                return;

            Card card = eventData.pointerPressRaycast.gameObject.GetComponentInParent<Card>();
            if (card == null)
                return;

            card.RequestFlip();
        }
    }
}