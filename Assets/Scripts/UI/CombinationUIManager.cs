using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class CombinationUIManager : MonoBehaviour
    {
        [SerializeField] private CardIconContainer containerPrefab;
        [SerializeField] private RectTransform columnPrefab;
        [SerializeField] private RectTransform rowPrefab;

        private CardIconContainer[][] containerStorage;
        private Dictionary<CardUIModel, LinkedList<(int i, int j)>> cards;
        private RectTransform myTransform;

        public void InitCombos(List<CardUIModel[]> combos)
        {
            Transform colOne = Instantiate(columnPrefab.gameObject, transform).transform.GetChild(0);
            Transform colTow = Instantiate(columnPrefab.gameObject, transform).transform.GetChild(0);
            
            containerStorage = new CardIconContainer[combos.Count][];
            cards = new Dictionary<CardUIModel, LinkedList<(int i, int j)>>();

            for (int i = 0; i < combos.Count; i++)
            {
                containerStorage[i] = new CardIconContainer[combos[i].Length];
                var parentColumn = i % 2 == 0 ? colOne : colTow;
                containerStorage[i] = AddRow(i,combos[i], parentColumn);
            }
        }

        public void Highlight(CardUIModel cardUIModel)
        {
            if(!cards.ContainsKey(cardUIModel))
                return;

            foreach (var coordinates in cards[cardUIModel])
            {
                Highlight(coordinates.i,coordinates.j);
            }
        }
        
        public void Highlight(int comboIndex, int cardIndex)
        {
            containerStorage[comboIndex][cardIndex].HighlightIcon(true);
        }

        public void UnhgilightAll()
        {
            foreach (var row in containerStorage)
            {
                foreach (var cardIconContainer in row)
                {
                    cardIconContainer.HighlightIcon(false);
                }
            }
        }

        private CardIconContainer[] AddRow(int rowIndex,CardUIModel[] uiModels, Transform parent)
        {
            CardIconContainer[] iconRow = new CardIconContainer[uiModels.Length];
            Transform row = Instantiate(rowPrefab.gameObject, parent).transform.GetChild(0);

            for (var i = 0; i < uiModels.Length; i++)
            {
                if (!cards.ContainsKey(uiModels[i]))
                {
                    cards.Add(uiModels[i],new LinkedList<(int i, int j)>());
                }

                cards[uiModels[i]].AddLast((rowIndex,i));
                
                iconRow[i] = Instantiate(containerPrefab.gameObject, row).GetComponent<CardIconContainer>();
                iconRow[i].SetIcon(uiModels[i].Icon);
            }

            return iconRow;
        }
        
        
    }
}