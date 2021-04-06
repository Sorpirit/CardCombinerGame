using System;
using System.Collections.Generic;
using Core;
using Models;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    
    public delegate void UpdatePickedCardState(CardContainer card);
    
    public class CardPicker : MonoBehaviour
    {
        private Camera _cam;
        private EventSystem _eventSystem;

        [SerializeField] private LayerMask targetLayer;
        
        private Vector2 cursorPosition => Input.mousePosition;

        private CardContainer pickedCard;

        public event Action<CardContainer> OnPicked; 
        public event Action<CardContainer> OnDroped;
        public UpdatePickedCardState ChangeCardState;
        
        private void Start()
        {
            _cam = Camera.main;
            _eventSystem = EventSystem.current;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Pick();
            } else if (Input.GetMouseButtonUp(0))
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
            var pointerEventData =new PointerEventData(_eventSystem){position = cursorPosition};
            var raycastResults = new List<RaycastResult>();
            _eventSystem.RaycastAll(pointerEventData, raycastResults);

            if(raycastResults.Count > 0)
            {
                foreach(var result in raycastResults)
                {
                    Debug.Log(result.gameObject.layer + " " + ((targetLayer.value & (1 << result.gameObject.layer)) > 0) + " ");
                    if ((targetLayer.value & (1 << result.gameObject.layer)) > 0)
                    {
                        pickedCard = result.gameObject.GetComponent<CardContainer>();
                        if (!pickedCard.IsCardEmpty)
                        {
                            OnPicked?.Invoke(pickedCard);
                            return;
                        }

                        pickedCard = null;
                    }
                }
            }
        }

        private void Drop()
        {
            if(pickedCard == null)
                return;
            
            OnDroped?.Invoke(pickedCard);
            pickedCard = null;
        }
    }
}