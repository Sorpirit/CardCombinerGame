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

        public List<(int comboIndex, int cardIndex)> GetCintainsCombo(int cardId)
        {
            return null;
        }
        
        private struct CardCombination
        {
            public CardModel[] combinations;
            public int bonusPoints;

            public CardCombination(CardModel[] combinations, int bonusPoints)
            {
                this.combinations = combinations;
                this.bonusPoints = bonusPoints;
            }

            public bool Contains(int id,out int index)
            {
                index = -1;
                for (int i = 0; i < combinations.Length; i++)
                {
                    if (combinations[i].ID == id)
                    {
                        index = i;
                        return true;
                    }
                }
                return false;
            }

            public bool Contains(out int[] indexes,params int[] ids)
            {
                indexes = new int[combinations.Length];
                
                if (combinations.Length > ids.Length)
                    return false;
                
                int maches = combinations.Length;
                for (int i = 0; i < indexes.Length; i++)
                {
                    indexes[i] = -1;
                }

                for (int i = 0; i < ids.Length; i++)
                {
                    if (Contains(ids[i], out int index) && indexes[index] == -1)
                    {
                        indexes[index] = i;
                        maches--;

                        if (maches == 0)
                            return true;
                    }
                }

                return false;
            }
        }
    }
    
}