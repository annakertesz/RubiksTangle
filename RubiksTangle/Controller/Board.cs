using System.Collections;


namespace RubiksTangle
{
    public class Board
    {
        private BoardField[] fields;
        private ArrayList formerCardsOnFirstField;


        public Board(BoardField[] fields)
        {
            this.fields = fields;
            formerCardsOnFirstField = new ArrayList();
        }


        public BoardField GetField(int index)
        {
            return fields[index];
        }


        public bool WasOnFirstPlace(Card card)
        {
            return formerCardsOnFirstField.Contains(card);
        }


        public bool TriedAllPossibilities()
        {
            return formerCardsOnFirstField.Count == 18;
        }


        public void AddToHistory(Card card)
        {
            formerCardsOnFirstField.Add(card);
        }
    }
}