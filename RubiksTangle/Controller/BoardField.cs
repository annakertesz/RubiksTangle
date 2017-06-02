using System.Collections;
using System.Threading;

namespace RubiksTangle
{
    public class BoardField
    {
        private Card card;
        private BoardField topDependency;
        private BoardField leftDependency;
        private ArrayList formerNeighbours;
        public event FieldHandler PlacedEvent;
        public delegate void FieldHandler(BoardField field);
        private int indexOfField;


        public BoardField(BoardField topDependency, BoardField leftDependency, int index)
        {
            this.topDependency = topDependency;
            this.leftDependency = leftDependency;
            formerNeighbours = new ArrayList();
            indexOfField = index;
        }


        public void removeCard(Hand hand)
        {
            if (!isEmpty())
            {
                hand.add(card);
            }
        }

        public int getIndexOfField() { return indexOfField; }

        public bool placeCard(Card cardToInsert, Hand hand)
        {

            int neededPosition = 4;
            if (topDependency != null)
            {
                int positionToTop = cardToInsert.isEdgesJoinable(topDependency.card.getEdge(2), 0);
                if (positionToTop == -1) return false;
                neededPosition = positionToTop;
            }
            if (leftDependency != null)
            {
                int positionToLeft = cardToInsert.isEdgesJoinable(leftDependency.card.getEdge(1), 3);
                if (neededPosition == 4 || neededPosition == positionToLeft) neededPosition = positionToLeft;
                else return false;
            }
            if (neededPosition < 0) return false;
            if (neededPosition == 4) neededPosition = 0;
            cardToInsert.setPosition(neededPosition);
            this.card = cardToInsert;
            if (PlacedEvent != null)
            {
                PlacedEvent(this);
                Thread.Sleep(500);
            }
            
            clearHistory();
            hand.remove(cardToInsert);
            return true;
        }


        public bool isEmpty() { return card == null; }


        public int getCardPosition()
        {
            return card.getActualPosition();
        }


        public void turnCard(int position)
        {
            card.setPosition(position);
        }


        public bool wasNeighbour(Card card)
        {
            return formerNeighbours.Contains(card);
        }


        public void addNeighbourhood(Card card)
        {
            formerNeighbours.Add(card);
        }


        public void clearHistory()
        {
            formerNeighbours = new ArrayList();
        }


        public Card getCard()
        {
            return card;
        }
    }
}