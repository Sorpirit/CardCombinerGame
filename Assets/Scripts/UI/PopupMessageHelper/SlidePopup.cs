using DG.Tweening;
using UnityEngine;

namespace UI.PopupMessageHelper
{
    public class SlidePopup : IPopupMassageEvents
    {
        private Vector3 _targetPos;
        private Vector3 _slideInPos;
        private float _duration;
        private Ease _ease;
        private Vector3 _slideOutPos;

        public SlidePopup(Vector3 targetPos, Vector3 slideInPos,float duration,Ease ease)
        {
            _targetPos = targetPos;
            _slideInPos = slideInPos;
            _duration = duration;
            _ease = ease;
            _slideOutPos = slideInPos;
            
        }
        
        public SlidePopup(Vector3 targetPos, Vector3 slideInPos,Vector3 slideOutPos,float duration,Ease ease)
            : this(targetPos, slideInPos,duration,ease)
        {
            _slideOutPos = slideOutPos;
        }


        public void Popup(PopupMessage popup)
        {
            popup.Container.localPosition = _slideInPos;
            popup.Container.DOLocalMove(_targetPos,_duration).SetEase(_ease);
        }

        public void Close(PopupMessage popup)
        {
            popup.Container.DOLocalMove(_slideOutPos,_duration)
                .SetEase(_ease)
                .OnComplete(() => Object.Destroy(popup.gameObject));
        }
    }
}