using DG.Tweening;
using UnityEngine;

namespace UI.PopupMessageHelper
{
    public class PopupMessageShooter : MonoBehaviour
    {
        [SerializeField] private PopupMessage massagePrefab;
        [SerializeField] private GameObject context;
        
        public void ShootScalePopup(Vector3 pos,string title, string content)
        {
            
            GameObject messageObj = Instantiate(massagePrefab.gameObject,pos,Quaternion.identity,context.transform);
            messageObj.transform.GetComponent<RectTransform>().localPosition = pos;
            
            PopupMessage massage = messageObj.GetComponent<PopupMessage>();
            ScalePopup scalePopup = new ScalePopup(.5f,Ease.OutCubic);
            SimpleContentProvider simpleContentProvider = new SimpleContentProvider(title,content);
            massage.Init(scalePopup,simpleContentProvider);
            
            massage.Popup();
        }

        public void ShootSlidePopup(Vector3 startPos,Vector3 pos,string title, string content)
        {
            GameObject messageObj = Instantiate(massagePrefab.gameObject,pos,Quaternion.identity,context.transform);
            PopupMessage massage = messageObj.GetComponent<PopupMessage>();
            SlidePopup scalePopup = new SlidePopup(pos,startPos,.5f,Ease.OutCubic);
            SimpleContentProvider simpleContentProvider = new SimpleContentProvider(title,content);
            massage.Init(scalePopup,simpleContentProvider);
            
            massage.Popup();
        }

        public void DebugPopup()
        {
            //ShootScalePopup(Vector3.zero, "Debug!","Some debug message");
            ShootSlidePopup(Vector3.right * 500, Vector3.zero,"Debug!","Some debug message");
        }
    }
}