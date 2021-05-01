using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public delegate void UpdatePickedCardState(CardContainer card);

    public class CardPicker : MonoBehaviour
    {
        [SerializeField] private LayerMask targetLayer;
        
        public event Action<CardContainer> OnPicked;
        public event Action<CardContainer> OnDroped;
        public UpdatePickedCardState ChangeCardState;
        
        private EventSystem _eventSystem;
        private Vector2 cursorPosition => Input.mousePosition;

        private CardContainer pickedCard;

        private void Start()
        {
            _eventSystem = EventSystem.current;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Pick();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Drop();
            }

            if (pickedCard != null)
            {
                pickedCard.Body.position = cursorPosition;
                ChangeCardState?.Invoke(pickedCard);
            }
        }

        private void Pick()
        {
            var pointerEventData = new PointerEventData(_eventSystem) {position = cursorPosition};
            var raycastResults = new List<RaycastResult>();
            _eventSystem.RaycastAll(pointerEventData, raycastResults);


            if (raycastResults.Count == 0 ||
                (targetLayer.value & (1 << raycastResults[0].gameObject.layer)) <= 0) return;
            
            
            pickedCard = raycastResults[0].gameObject.GetComponent<CardContainer>();
            if (!pickedCard.IsCardEmpty)
            {
                OnPicked?.Invoke(pickedCard);
                return;
            }

            pickedCard = null;
        }

        private void Drop()
        {
            if (pickedCard == null)
                return;

            OnDroped?.Invoke(pickedCard);
            pickedCard = null;
        }
    }
}