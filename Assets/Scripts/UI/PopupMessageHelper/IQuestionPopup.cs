using System;

namespace UI.PopupMessageHelper
{
    public interface IQuestionPopup
    {
        void Init(PopupMessage popupMessage,string yesOption,string noOption);
        void Respond(bool answer);
        Action<bool> OnRespond { get; set; }
    }
}