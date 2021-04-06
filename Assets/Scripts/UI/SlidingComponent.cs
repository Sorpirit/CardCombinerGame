using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class SlidingComponent : MonoBehaviour
    {
        [SerializeField] private float duration = .5f;
        [SerializeField] private Ease ease;
        
        private RectTransform myTransform;

        private bool isHiden = false;
        private Tween anim;

        public void Slide()
        {
            if (isHiden)
            {
                ShowPanel();
            }
            else
            {
                HidePanel();
            }
        }
        
        public void HidePanel()
        {
            if (myTransform == null)
                myTransform = GetComponent<RectTransform>();

            if(isHiden)
                return;

            anim = myTransform.DOMove(myTransform.position + Vector3.up * myTransform.rect.height * 2, duration)
                    .SetAutoKill(false).OnComplete(() => Debug.Log("Finish1"));
            isHiden = true;
        }

        public void ShowPanel()
        {
            if (myTransform == null)
                myTransform = GetComponent<RectTransform>();

            if(!isHiden)
                return;

            anim = myTransform.DOMove(myTransform.position - Vector3.up * myTransform.rect.height * 2, duration)
                    .SetAutoKill(false).OnComplete(() => Debug.Log("Finish2"));
            
            isHiden = false;
        }
    }
}