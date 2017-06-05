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

        public Card Card { get => card; private set => card = value; }

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
                hand.add(Card);
            }
        }

        public int getIndexOfField() { return indexOfField; }

        public bool placeCard(Card cardToInsert, Hand hand)
        {

            int neededPosition = 4;
            if (topDependency != null)
            {
                int positionToTop = cardToInsert.isEdgesJoinable(topDependency.Card.getEdge(2), 0);
                if (positionToTop == -1) return false;
                neededPosition = positionToTop;
            }
            if (leftDependency != null)
            {
                int positionToLeft = cardToInsert.isEdgesJoinable(leftDependency.Card.getEdge(1), 3);
                if (neededPosition == 4 || neededPosition == positionToLeft) neededPosition = positionToLeft;
                else return false;
            }
            if (neededPosition < 0) return false;
            if (neededPosition == 4) neededPosition = 0;
            cardToInsert.ActualPosition = neededPosition;
            this.Card = cardToInsert;
            if (PlacedEvent != null)
            {
                PlacedEvent(this);
                Thread.Sleep(Game.speed);
            }
            
            clearHistory();
            hand.remove(cardToInsert);
            return true;
        }


        public bool isEmpty() { return Card == null; }


        public int getCardPosition()
        {
            return card.ActualPosition;
        }


        public void turnCard(int position)
        {
            card.ActualPosition = position;
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

       
    }
}