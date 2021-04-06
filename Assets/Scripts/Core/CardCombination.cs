namespace Core
{
    public struct CardCombination
    {
        public CardModel[] combinations;
        public int bonusPoints;

        public CardCombination(CardModel[] combinations, int bonusPoints)
        {
            this.combinations = combinations;
            this.bonusPoints = bonusPoints;
        }

        public bool Contains(int id, out int index)
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

        public bool Contains(out int[] indexes, params int[] ids)
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