using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class CardCombinationManager
    {

        private List<CardCombination> _combinations;

        public CardCombinationManager()
        {
            _combinations = new List<CardCombination>();
        }

        public void AddCombination(int bonusPoints,params CardModel[] cards)
        {
            CardCombination combination = new CardCombination(cards,bonusPoints);
            
            _combinations.Add(combination);
        }

        public bool ContainsCombination(out int points,out int[] cardIndex,CardModel[] cards)
        {
            int[] ids = GetIds(cards);
            points = 0;
            cardIndex = null;
            
            foreach (var combo in _combinations)
            {
                if (combo.bonusPoints > points && combo.Contains(out int[] tempCardIndex,ids))
                {
                    points = combo.bonusPoints;
                    cardIndex = tempCardIndex;
                }
            }

            return points != 0;
        }

        private int[] GetIds(CardModel[] cards) => cards.Select(card => card.ID).ToArray();

        public List<(CardModel[] comboination, int score)> GetCominationsInfo()
        {
            return _combinations.Select(combo =>
            {
                (CardModel[] comboination, int score) c;
                c.comboination = combo.combinations;
                c.score = combo.bonusPoints;
                return c;
            }).ToList();
        }
    }
    
}