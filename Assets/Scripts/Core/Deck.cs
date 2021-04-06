using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Deck
    {
        private List<CardModel> cards;
        private int cardCount;

        public int CardsLeft => cardCount;
        public bool isEmpty => cardCount == -1;
        
        public Deck()
        {
            cards = new List<CardModel>();
        }

        public void AddCard(CardModel card)
        {
            cards.Add(card);
            cardCount++;
        }

        public CardModel Pick()
        {
            if(isEmpty)
                return CardModel.Empty;

            var pickCard = cards[cardCount];
            
            cardCount--;

            return pickCard;
        }

        public void Shuffle()
        {
            cardCount = cards.Count - 1;

            for (int i = 0; i < cards.Count; i++)
            {
                int randIndex = Random.Range(0, cards.Count);
                var tempCard = cards[i];
                cards[i] = cards[randIndex];
                cards[randIndex] = tempCard;
            }
        }
    }
}