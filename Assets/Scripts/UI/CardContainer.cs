using System.Collections;
using DG.Tweening;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CardContainer : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text label;
        [SerializeField] private TMP_Text reartyLabel;
        [SerializeField] private Image higlight;
        [SerializeField] private RectTransform cardBody;

        public RectTransform RectTransform => icon.rectTransform;
        public CardUIModel Model => _model;
        public bool IsCardEmpty => _model.Parent == Card.Empty;
        public RectTransform Body => cardBody;
        
        private CardUIModel _model;
        private Vector2 defaultPosition => transform.position;
        private bool dropIndicator;
        private bool grabIndicator;

        public bool DropIndicator
        {
            set
            {
                if (value && true != dropIndicator)
                {
                    dropIndicator = true;
                    StartCoroutine(ShowDropIndicator());
                }

                dropIndicator = value;
            }
        }

        public bool GrabIndicator
        {
            set
            {
                if (value && true != grabIndicator)
                {
                    grabIndicator = true;
                    StartCoroutine(ShowGrabIndicator());
                }

                grabIndicator = value;
            }
        }

        public void InitCointainer(CardUIModel model)
        {
            UpdateCardModel(model);

            higlight.gameObject.SetActive(false);
            
        }


        public void UpdateCardModel(CardUIModel model)
        {
            _model = model;
            if (model.Parent == Card.Empty)
            {
                icon.enabled = false;
                icon.sprite = model.Image;
                label.text = model.Lable;
                reartyLabel.text = "";
                return;
            }

            icon.enabled = true;
            icon.sprite = model.Image;
            label.text = model.Lable;
            reartyLabel.text = ((int) (model.Rearty * 100)) + "%";
            CancelAllTweens();
        }

        public void ResetCard()
        {
            cardBody.DOMove(defaultPosition, .25f).SetEase(Ease.OutBack);
            GrabIndicator = false;
            DropIndicator = false;
        }

        public IEnumerator HilightCard(Vector2 comboAnchor)
        {
            higlight.gameObject.SetActive(true);
            higlight.rectTransform.localScale = Vector3.zero;
            higlight.color = new Color(0, 0, 0, 0);

            Sequence s = DOTween.Sequence();
            s.Append(cardBody.transform.DOMove(new Vector3(cardBody.position.x, comboAnchor.y), .3f)
                .SetEase(Ease.OutQuart));
            s.Append(cardBody.transform.DOMove(comboAnchor, .4f).SetEase(Ease.OutCubic));
            s.Append(higlight.rectTransform.DOScale(Vector3.one * .8f, .3f));
            s.Join(higlight.DOColor(Color.white, .3f));
            s.Join(cardBody.transform.DOShakePosition(.3f, new Vector3(1, 1, 0) * 30, 60));
            s.Append(icon.DOFade(0, .26f));
            s.SetAutoKill(false);
            yield return s.WaitForCompletion();
            s.Rewind();
            s.Kill();
            
            cardBody.anchoredPosition = Vector3.zero;
            cardBody.rotation = Quaternion.identity;
            higlight.gameObject.SetActive(false);
        }


        public Tween PlayeApearAnimation(Vector2 startPos, float duration)
        {
            cardBody.position = startPos;
            cardBody.rotation = Quaternion.Euler(Vector3.forward * 45);
            Sequence s = DOTween.Sequence();
            Tween rotate = cardBody.DORotate(Vector3.zero, duration);
            Tween move = cardBody.DOMove(defaultPosition, duration).SetEase(Ease.OutCubic);
            s.Append(rotate);
            s.Join(move);
            return s;
        }

        private IEnumerator ShowDropIndicator()
        {
            icon.DOFade(.1f, .3f);
            while (dropIndicator)
            {
                yield return null;
            }

            icon.DOFade(1, .3f);
        }

        private IEnumerator ShowGrabIndicator()
        {
            icon.transform.DOScale(Vector3.one * 1.35f, .17f);
            while (grabIndicator)
            {
                yield return null;
            }

            icon.transform.DOScale(Vector3.one, .14f);
        }

        private void CancelAllTweens()
        {
            cardBody.DOComplete();
            icon.DOComplete();
            icon.transform.DOComplete();
        }
    }
}