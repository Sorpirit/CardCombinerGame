using Core;
using UI;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "NewCard", menuName = "CardComboGame/Create Card", order = 0)]
    public class Card : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private string label; 
        [SerializeField] private Sprite image;
        [SerializeField] private Sprite icon;
        
        public static Card Empty;
        
        public CardModel ExportModel()
        {
            return new CardModel(id,label,this);
        }

        public CardUIModel ExportUI()
        {
            return new CardUIModel(image,icon,label,this);
        }
    }
}