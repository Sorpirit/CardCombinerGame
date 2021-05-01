using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class SlidingComponent : MonoBehaviour
    {
        [SerializeField] private float duration = .5f;
        [SerializeField] private bool startIn;
        
            
        public Action OnSlideIn;
        public Action OnSlideOut;
        
        private RectTransform myTransform;

        private bool isHiden;
        private Tween anim;

        private void Start()
        {
            if (startIn)
            {
                SlideIn();
                anim.Complete();
            }
        }

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
                .SetAutoKill(false);
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
                .SetAutoKill(false);

            isHiden = false;
            OnSlideOut?.Invoke();
        }
    }
}