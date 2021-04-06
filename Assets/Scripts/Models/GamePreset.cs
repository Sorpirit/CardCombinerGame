using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "NewGamePreset", menuName = "CardComboGame/Create Game Preset", order = 1)]
    public class GamePreset : ScriptableObject
    {
        [SerializeField] private DeckPreload _deckPreload;
        [SerializeField] private ComboPreloader _comboPreloader;

        [SerializeField] private int tabelSize = 3;
        [SerializeField] private int handSize = 4;

        public DeckPreload DeckPreload => _deckPreload;
        public ComboPreloader ComboPreloader => _comboPreloader;
        public int TabelSize => tabelSize;
        public int HandSize => handSize;
    }
}