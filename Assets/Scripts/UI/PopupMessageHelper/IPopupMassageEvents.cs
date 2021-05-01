using System;
using UnityEngine;

namespace UI.PopupMessageHelper
{
    public interface IPopupMassageEvents
    {
        void Popup(PopupMessage popup);
        void Close(PopupMessage popup);

        Action<PopupMessage> OnPopupComplete { get; set; }
        Action<PopupMessage> OnCloseComplete { get; set; }
    }
}