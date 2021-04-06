using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CardIconContainer : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Image higlight;

        public void SetIcon(Sprite sprite)
        {
            icon.sprite = sprite;
            higlight.sprite = sprite;
        }

        public void HighlightIcon(bool state)
        {
            if (state)
            {
                higlight.DOFillAmount(1, .3f);
            }
            else
            {
                higlight.DOFillAmount(0, .3f);
            }
        }
    }
}