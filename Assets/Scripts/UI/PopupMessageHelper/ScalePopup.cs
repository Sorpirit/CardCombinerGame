using System;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI.PopupMessageHelper
{
    public class ScalePopup : IPopupMassageEvents
    {

        private float _duration;
        private Ease _animEase;
        
        public Action<PopupMessage> OnPopupComplete { get; set; }
        public Action<PopupMessage> OnCloseComplete { get; set; }

        public ScalePopup(float duration, Ease animEase)
        {
            _duration = duration;
            _animEase = animEase;
        }

        public void Popup(PopupMessage popup)
        {
            popup.Container.localScale = Vector3.zero;
            popup.Container.DOScale(Vector3.one, _duration)
                .SetEase(_animEase).OnComplete(() =>
                {
                    OnPopupComplete?.Invoke(popup);   
                });
            
        }

        public void Close(PopupMessage popup)
        {
            popup.Container.DOScale(Vector3.zero, _duration)
                .SetEase(_animEase)
                .OnComplete(() =>
                {
                    OnCloseComplete?.Invoke(popup);
                    Object.Destroy(popup.gameObject);
                });
            
        }

        
    }
}