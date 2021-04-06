using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "NewDeck", menuName = "CardComboGame/Create Deck", order = 1)]
    public class DeckPreload : ScriptableObject
    {
        [SerializeField] private Card DefaultEmpty;
        [SerializeField] private List<DeckItem> cards;
        
        public Deck ExportDeck()
        {
            Card.Empty = DefaultEmpty;
            Deck deck = new Deck();
            int cardCount = CountCards();
            foreach (var card in cards)
            {
                for (int i = 0; i < card.Quantity; i++)
                {
                    card.Card.Rearty = (float) card.Quantity / cardCount;
                    card.Card.QuantiyInDeck = card.Quantity;
                    deck.AddCard(card.Card.ExportModel());
                }
                
            }

            return deck;
        }

        private int CountCards()
        {
            int count = 0;
            foreach (var card in cards)
            {
                count += card.Quantity;
            }
            return count;
        }

        [Serializable]
        private struct DeckItem
        {
            [SerializeField] private Card card;
            [SerializeField] private int quantity;

            public Card Card => card;
            public int Quantity => quantity;
        }
    }
}