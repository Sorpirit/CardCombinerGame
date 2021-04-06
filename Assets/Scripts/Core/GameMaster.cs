using System.Collections.Generic;
using Models;

namespace Core
{
    public class GameMaster
    {
        private Deck _deck;
        private CardCombinationManager _combinationManager;

        private List<CardModel> hand;
        private CardModel[] table;

        private int score;

        public readonly int HandSize;
        public readonly int TableSize;

        public GameMaster(GamePreset preset)
        {
            _deck = preset.DeckPreload.ExportDeck();
            _combinationManager = preset.ComboPreloader.ExportCombinations();

            HandSize = preset.HandSize;
            TableSize = preset.TabelSize;
            
            hand = new List<CardModel>(HandSize);
            table = new CardModel[TableSize];

            DelfaultTabel();
        }

        public List<CardModel> Hand => hand;

        public CardModel[] Table => table;

        public int Score => score;
        public bool isGameEnded => _deck.isEmpty;
        public bool isTurnFinished => isCardDroped && isCardPicked;
        
        private bool isCardDroped;
        private bool isCardPicked;
        

        public void StartGame()
        {
            _deck.Shuffle();
            _deck.Shuffle();
            _deck.Shuffle();

            score = 0;
            isCardDroped = false;
            isCardPicked = false;

            ClearHand();
            hand.Add(_deck.Pick());
            
            DelfaultTabel();
            GenerateTable();
        }

        public List<(CardModel[] comboination, int score)>  GetComboInfo()
        {
            return _combinationManager.GetCominationsInfo();
        }
        
        public bool NextTurn()
        {

            if (!isTurnFinished || isGameEnded)
                return false;

            GenerateTable();
                
            isCardDroped = false;
            isCardPicked = false;

            return true;
        }
        
        public bool PickCard(int i)
        {
            if (isCardPicked || i >= TableSize || i < 0 || table[i].Equals(CardModel.Empty))
                return false;
            
            if (hand.Count == HandSize)
                hand.RemoveAt(0);
            
            hand.Add(table[i]);
            table[i] = CardModel.Empty;
            isCardPicked = true;
            
            return true;
        }

        public bool DropCard(int i)
        {
            if (isCardDroped || i >= TableSize || i < 0 || table[i].Equals(CardModel.Empty))
                return false;

            table[i] = CardModel.Empty;
            isCardDroped = true;
            
            return true;
        }

        public bool LookForCombinations(out int[] cardCombosIndexes)
        {
            bool containsCombination = _combinationManager.ContainsCombination(out int points,out cardCombosIndexes, hand.ToArray());
            if (containsCombination)
            {
                ClearHand();
                score += points;
            }

            return containsCombination;
        }
        
        private void ClearHand()
        {
            hand.Clear();
        }
        
        private void GenerateTable()
        {
            for (int i = 0; i < table.Length; i++)
            {
                if(_deck.isEmpty)
                    return;
                
                if(table[i].Equals(CardModel.Empty))
                    table[i] = _deck.Pick();
            }
        }

        private void DelfaultTabel()
        {
            for (int i = 0; i < table.Length; i++)
            {
                table[i] = CardModel.Empty;
            }
        }
        
    }
}