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
            foreach (var card in cards)
            {
                for (int i = 0; i < card.Quantity; i++)
                {
                    deck.AddCard(card.Card.ExportModel());
                }
                
            }

            return deck;
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