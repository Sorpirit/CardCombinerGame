using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private CardPicker _picker;
        [SerializeField] private CardContainer cardPrefab;
        [SerializeField] private TMP_Text score;

        [SerializeField] private int dropLine;
        [SerializeField] private int handLine;

        [SerializeField] private RectTransform handPivit;
        [SerializeField] private RectTransform tabelPivit;
        [SerializeField] private RectTransform cardApearPoint;
        [SerializeField] private RectTransform ComboAnchor;

        [SerializeField] private CombinationUIManager combinationUIManager;

        public event Action<int> OnAddCardToHand;
        public event Action<int> OnCardDroped;

        private bool handIsAnimating;
        
        private CardContainer[] tabel;
        private CardContainer[] hand;

        private void Awake()
        {
            _picker.OnDroped += DropCard;
            _picker.ChangeCardState = UpdatePickedCard;
        }

        public void Init(int tabelSize, int handSize)
        {
            tabel = new CardContainer[tabelSize];
            hand = new CardContainer[handSize];

            InstantiateTabel();
            InstantiateHand();
        }

        public void InitCombinationManager(List<CardUIModel[]> combos)
        {
            combinationUIManager.InitCombos(combos);
        }

        public void UpdateTabelVisuals(CardUIModel[] tabel)
        {
            Sequence anim = DOTween.Sequence();

            for (int i = 0; i < tabel.Length; i++)
            {
                if (!this.tabel[i].Model.Equals(tabel[i]))
                {
                    this.tabel[i].UpdateCardModel(tabel[i]);
                    anim.Append(this.tabel[i].PlayeApearAnimation(cardApearPoint.position, .5f));
                }
            }

            anim.Play();
        }

        public async void UpdateHandVisuals(CardUIModel[] handModels)
        {
            while (handIsAnimating)
            {
                await Task.Yield();
            }

            handIsAnimating = true;
            ClearComboHiglights();


            for (int i = 0; i < hand.Length; i++)
            {
                if (!hand[i].Model.Equals(handModels[i]))
                {
                    hand[i].UpdateCardModel(handModels[i]);
                }

                UpdateCombinationHigliht(hand[i].Model);
            }

            handIsAnimating = false;
        }

        public void UpdateScores(int val)
        {
            score.text = val.ToString();
        }

        public void UpdateCombinationHigliht(CardUIModel uiModel)
        {
            combinationUIManager.Highlight(uiModel);
        }

        public void ClearComboHiglights()
        {
            combinationUIManager.UnhighlightAll();
        }

        private void DropCard(CardContainer cardContainer)
        {
            int index = IndexOf(cardContainer, tabel);
            cardContainer.ResetCard();

            if (IsCradOnDropRegion(cardContainer))
            {
                OnCardDroped?.Invoke(index);
            }
            else if (IsCradOnGrabRegion(cardContainer))
            {
                OnAddCardToHand?.Invoke(index);
            }
        }

        private void InstantiateHand()
        {
            for (int i = 0; i < hand.Length; i++)
            {
                hand[i] = Instantiate(cardPrefab.gameObject, handPivit).GetComponent<CardContainer>();
                hand[i].InitCointainer(CardUIModel.Empty);
            }
        }

        private void InstantiateTabel()
        {
            for (int i = 0; i < tabel.Length; i++)
            {
                tabel[i] = Instantiate(cardPrefab.gameObject, tabelPivit).GetComponent<CardContainer>();
                tabel[i].InitCointainer(CardUIModel.Empty);
                tabel[i].gameObject.layer = gameObject.layer;
            }
        }

        private int IndexOf(CardContainer target, CardContainer[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == target)
                    return i;
            }

            return -1;
        }

        public void VisualiseCombo(int[] intCardIndexes)
        {
            StartCoroutine(HighlightCombo(intCardIndexes));
        }

        private IEnumerator HighlightCombo(int[] intCardIndexes)
        {
            while (handIsAnimating)
            {
                yield return null;
            }

            handIsAnimating = true;
            Coroutine[] highlight = new Coroutine[intCardIndexes.Length];
            for (int i = 0; i < intCardIndexes.Length; i++)
            {
                int cardIndex = intCardIndexes[i];
                highlight[i] = hand[cardIndex].StartCoroutine(hand[cardIndex].HilightCard(ComboAnchor.position));
            }

            for (int i = 0; i < intCardIndexes.Length; i++)
            {
                yield return highlight[i];
            }

            handIsAnimating = false;
        }

        private void UpdatePickedCard(CardContainer cardContainer)
        {
            if (IsCradOnDropRegion(cardContainer))
            {
                cardContainer.DropIndicator = true;
                cardContainer.GrabIndicator = false;
            }
            else if (IsCradOnGrabRegion(cardContainer))
            {
                cardContainer.GrabIndicator = true;
                cardContainer.DropIndicator = false;
            }
            else
            {
                cardContainer.DropIndicator = false;
                cardContainer.GrabIndicator = true;
            }
        }

        private bool IsCradOnDropRegion(CardContainer cardContainer)
            => cardContainer.Body.anchoredPosition.y >= dropLine;

        private bool IsCradOnGrabRegion(CardContainer cardContainer)
            => cardContainer.Body.anchoredPosition.y <= handLine;
    }
}