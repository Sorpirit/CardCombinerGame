using System.Collections.Generic;
using Models;

namespace Core
{
    public class GameMaster
    {
        public readonly int HandSize;
        public readonly int TableSize;
        
        public List<CardModel> Hand => _hand;

        public CardModel[] Table => _table;

        public int Score => _score;
        public bool IsGameEnded => _deck.isEmpty;
        public bool IsTurnFinished => _isCardDroped && _isCardPicked;
        public int CardCount => _deck.CardsLeft;
        
        private Deck _deck;
        private CardCombinationManager _combinationManager;

        private List<CardModel> _hand;
        private CardModel[] _table;

        private int _score;
        
        private bool _isCardDroped;
        private bool _isCardPicked;
        

        public GameMaster(GamePreset preset)
        {
            _deck = preset.DeckPreload.ExportDeck();
            _combinationManager = preset.ComboPreloader.ExportCombinations();

            HandSize = preset.HandSize;
            TableSize = preset.TabelSize;
            
            _hand = new List<CardModel>(HandSize);
            _table = new CardModel[TableSize];

            DelfaultTabel();
        }
        
        public void StartGame()
        {
            _deck.Shuffle();

            _score = 0;
            _isCardDroped = false;
            _isCardPicked = false;

            ClearHand();
            _hand.Add(_deck.Pick());
            
            DelfaultTabel();
            GenerateTable();
        }

        public List<(CardModel[] comboination, int score)>  GetComboInfo()
        {
            return _combinationManager.GetCominationsInfo();
        }
        
        public bool NextTurn()
        {

            if (!IsTurnFinished || IsGameEnded)
                return false;

            GenerateTable();
                
            _isCardDroped = false;
            _isCardPicked = false;

            return true;
        }
        
        public bool PickCard(int i)
        {
            if (_isCardPicked || i >= TableSize || i < 0 || _table[i].Equals(CardModel.Empty))
                return false;
            
            if (_hand.Count == HandSize)
                _hand.RemoveAt(0);
            
            _hand.Add(_table[i]);
            _table[i] = CardModel.Empty;
            _isCardPicked = true;
            
            return true;
        }

        public bool DropCard(int i)
        {
            if (_isCardDroped || i >= TableSize || i < 0 || _table[i].Equals(CardModel.Empty))
                return false;

            _table[i] = CardModel.Empty;
            _isCardDroped = true;
            
            return true;
        }

        public bool LookForCombinations(out int[] cardCombosIndexes)
        {
            bool containsCombination = _combinationManager.ContainsCombination(out int points,out cardCombosIndexes, _hand.ToArray());
            if (containsCombination)
            {
                ClearHand();
                _score += points;
            }

            return containsCombination;
        }
        
        private void ClearHand()
        {
            _hand.Clear();
        }
        
        private void GenerateTable()
        {
            for (int i = 0; i < _table.Length; i++)
            {
                if(_deck.isEmpty)
                    return;
                
                if(_table[i].Equals(CardModel.Empty))
                    _table[i] = _deck.Pick();
            }
        }

        private void DelfaultTabel()
        {
            for (int i = 0; i < _table.Length; i++)
            {
                _table[i] = CardModel.Empty;
            }
        }
        
    }
}