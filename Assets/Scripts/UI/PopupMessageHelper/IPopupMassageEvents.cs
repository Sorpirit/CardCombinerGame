using UnityEngine;

namespace UI.PopupMessageHelper
{
    public interface IPopupMassageEvents
    {
        void Popup(PopupMessage popup);
        void Close(PopupMessage popup);
    }
}