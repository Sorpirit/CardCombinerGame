using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PopupMessageHelper
{
    public class QuestionPopup : MonoBehaviour,IQuestionPopup
    {
        [SerializeField] private Button yesBtn;
        [SerializeField] private Button noBtn;
        [SerializeField] private TMP_Text yesOptionLb;
        [SerializeField] private TMP_Text noOptionLb;
        
        public Action<bool> OnRespond { get; set; }
        private PopupMessage _popupMessage;
        
        public void Init(PopupMessage popupMessage,string yesOption = "yes", string noOption = "no")
        {
            _popupMessage = popupMessage;

            yesOptionLb.text = yesOption;
            noOptionLb.text = noOption;
            
            yesBtn.onClick.AddListener(()=> Respond(true));
            noBtn.onClick.AddListener(()=> Respond(false));

        }

        public void Respond(bool answer)
        {
            OnRespond?.Invoke(answer);
            _popupMessage.Close();
        }

    }
}