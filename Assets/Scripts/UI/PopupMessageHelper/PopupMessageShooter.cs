using System;
using DG.Tweening;
using UnityEngine;

namespace UI.PopupMessageHelper
{
    public class PopupMessageShooter : MonoBehaviour
    {
        [SerializeField] private PopupMessage massagePrefabOneButton;
        [SerializeField] private PopupMessage massagePrefabTwoButton;
        [SerializeField] private PopupMessage massagePrefabTrigger;
        [SerializeField] private GameObject context;

        public IPopupMassageEvents BasicScalePopup => new ScalePopup(.3f,Ease.OutCubic);

        public void ShootScalePopup(Vector3 pos,string title, string content)
        {
            
            GameObject messageObj = Instantiate(massagePrefabOneButton.gameObject,pos,Quaternion.identity,context.transform);
            messageObj.transform.GetComponent<RectTransform>().localPosition = pos;
            
            PopupMessage massage = messageObj.GetComponent<PopupMessage>();
            ScalePopup scalePopup = new ScalePopup(.5f,Ease.OutCubic);
            SimpleContentProvider simpleContentProvider = new SimpleContentProvider(title,content);
            massage.Init(scalePopup,simpleContentProvider);
            
            massage.Popup();
        }
        public void ShootSlidePopup(Vector3 startPos,Vector3 pos,string title, string content)
        {
            GameObject messageObj = Instantiate(massagePrefabOneButton.gameObject,pos,Quaternion.identity,context.transform);
            PopupMessage massage = messageObj.GetComponent<PopupMessage>();
            SlidePopup scalePopup = new SlidePopup(pos,startPos,.5f,Ease.OutCubic);
            SimpleContentProvider simpleContentProvider = new SimpleContentProvider(title,content);
            massage.Init(scalePopup,simpleContentProvider);
            
            massage.Popup();
        }

        public void ShootPopup(Vector3 pos,string title, string content,IPopupMassageEvents events)
        {
            
            GameObject messageObj = Instantiate(massagePrefabOneButton.gameObject,pos,Quaternion.identity,context.transform);
            messageObj.transform.GetComponent<RectTransform>().localPosition = pos;
            
            PopupMessage massage = messageObj.GetComponent<PopupMessage>();
            SimpleContentProvider simpleContentProvider = new SimpleContentProvider(title,content);
            massage.Init(events,simpleContentProvider);
            
            massage.Popup();
        }
        
        public void ShootQuestionPopup(Vector3 pos,string title, string content,Action<bool> takeAnswer,string yesOption,string noOption,IPopupMassageEvents events)
        {
            GameObject messageObj = Instantiate(massagePrefabTwoButton.gameObject,pos,Quaternion.identity,context.transform);
            messageObj.transform.GetComponent<RectTransform>().localPosition = pos;
            PopupMessage massage = messageObj.GetComponent<PopupMessage>();
            IQuestionPopup questionPopup = messageObj.GetComponent<IQuestionPopup>();
            SimpleContentProvider simpleContentProvider = new SimpleContentProvider(title,content);
            
            questionPopup.Init(massage,yesOption,noOption);
            massage.Init(events,simpleContentProvider);

            questionPopup.OnRespond += takeAnswer;
            
            massage.Popup();
        }

        public PopupMessage ShootTriggerPopup(Vector3 pos,string title, string content,IPopupMassageEvents events)
        {
            GameObject messageObj = Instantiate(massagePrefabTrigger.gameObject,pos,Quaternion.identity,context.transform);
            messageObj.transform.GetComponent<RectTransform>().localPosition = pos;
            PopupMessage massage = messageObj.GetComponent<PopupMessage>();
            SimpleContentProvider simpleContentProvider = new SimpleContentProvider(title,content);

            massage.Init(events,simpleContentProvider);

            massage.Popup();
            return massage;
        }
        
        public void DebugPopup()
        {
            //ShootScalePopup(Vector3.zero, "Debug!","Some debug message");
            ShootSlidePopup(Vector3.right * 500, Vector3.zero,"Debug!","Some debug message");
        }
    }
}