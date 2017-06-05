using System;
using System.Collections;


namespace RubiksTangle
{
    public class Board
    {
        public BoardField[] fields;
        public ArrayList formerCards;


        public Board(BoardField[] fields)
        {
            this.fields = fields;
            formerCards = new ArrayList();
        }


        public BoardField getField(int index)
        {
            return fields[index];
        }


        public bool wasOnFirstPlace(Card card)
        {
            return formerCards.Contains(card);
        }


        public bool triedAllPossibilities()
        {
            return formerCards.Count == 18;
        }


        public void addToHistory(Card card)
        {
            formerCards.Add(card);
        }
    }
}