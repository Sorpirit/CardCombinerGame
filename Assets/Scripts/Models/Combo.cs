using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public struct Combo
    {
        [SerializeField] private int scorePoints;
        [SerializeField] private Card[] neededCards;

        public int ScorePoints => scorePoints;
        public Card[] NeededCards => neededCards;
    }
}