using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class SlidingComponent : MonoBehaviour
    {
        [SerializeField] private float duration = .5f;

        public Action OnSlideIn;
        public Action OnSlideOut;
        
        private RectTransform myTransform;

        private bool isHiden;
        private Tween anim;

        public void Slide()
        {
            if (isHiden)
            {
                SlideOut();
            }
            else
            {
                SlideIn();
            }
        }

        public void SlideIn()
        {
            if (myTransform == null)
                myTransform = GetComponent<RectTransform>();

            if (isHiden)
                return;

            anim = myTransform.DOMove(myTransform.position + Vector3.up * myTransform.rect.height * 2, duration)
                .SetAutoKill(false).OnComplete(() => Debug.Log("Finish1"));
            isHiden = true;
            OnSlideIn?.Invoke();
        }

        public void SlideOut()
        {
            if (myTransform == null)
                myTransform = GetComponent<RectTransform>();

            if (!isHiden)
                return;

            anim = myTransform.DOMove(myTransform.position - Vector3.up * myTransform.rect.height * 2, duration)
                .SetAutoKill(false).OnComplete(() => Debug.Log("Finish2"));

            isHiden = false;
            OnSlideOut?.Invoke();
        }
    }
}