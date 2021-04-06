using Models;

namespace Core
{
    public struct CardModel
    {
        public static readonly CardModel Empty = new CardModel(-1, "[ - ]",Card.Empty);
        private int id;
        private string label;
        private Card parent;

        public string Label => label;
        public int ID => id;

        public Card Parent => parent;

        public CardModel(int id,string lb, Card parent)
        {
            this.id = id;
            this.label = lb;
            this.parent = parent;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CardModel))
                return false;
            CardModel card = (CardModel) obj;


            return card.id == id;
        }

        public bool Equals(CardModel other)
        {
            return id == other.id;
        }

        public override int GetHashCode()
        {
            return id;
        }
        
        
    }
}