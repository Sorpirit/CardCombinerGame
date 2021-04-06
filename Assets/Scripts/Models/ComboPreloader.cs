using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "NewComboList", menuName = "CardComboGame/Create Combo list", order = 1)]
    public class ComboPreloader : ScriptableObject
    {

        [SerializeField] private List<Combo> combos;

        public CardCombinationManager ExportCombinations()
        {
            var comboManager = new CardCombinationManager();
            foreach (var combo in combos)
            {
                CardModel[] neededCards = combo.NeededCards.Select(cmb => cmb.ExportModel()).ToArray();
                comboManager.AddCombination(combo.ScorePoints,neededCards);
            }

            return comboManager;
        }
    }
}