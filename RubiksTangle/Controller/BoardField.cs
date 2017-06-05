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
        private int indexOfField;

        public event FieldHandler PlacedEvent;
        public delegate void FieldHandler(BoardField field);
       
        public Card Card { get => card; private set => card = value; }
        public int IndexOfField { get => indexOfField; private set => indexOfField = value; }
        public int CardPosition { get => card.ActualPosition; }

        public BoardField(BoardField topDependency, BoardField leftDependency, int index)
        {
            this.topDependency = topDependency;
            this.leftDependency = leftDependency;
            formerNeighbours = new ArrayList();
            IndexOfField = index;
            
        }


        public void RemoveCard(Hand hand)
        {
            if (!IsEmpty())
            {
                hand.Add(Card);
            }
        }
        

        public bool PlaceCard(Card cardToInsert, Hand hand)
        {

            int neededPosition = 4;
            if (topDependency != null)
            {
                int positionToTop = cardToInsert.IsEdgesJoinable(topDependency.Card.GetEdge(2), 0);
                if (positionToTop == -1) return false;
                neededPosition = positionToTop;
            }
            if (leftDependency != null)
            {
                int positionToLeft = cardToInsert.IsEdgesJoinable(leftDependency.Card.GetEdge(1), 3);
                if (neededPosition == 4 || neededPosition == positionToLeft) neededPosition = positionToLeft;
                else return false;
            }
            if (neededPosition < 0) return false;
            if (neededPosition == 4) neededPosition = 0;
            cardToInsert.ActualPosition = neededPosition;
            Card = cardToInsert;
            if (PlacedEvent != null)
            {
                PlacedEvent(this);
                Thread.Sleep(Game.Speed);
            }
            
            ClearHistory();
            hand.Remove(cardToInsert);
            return true;
        }


        public bool IsEmpty()
        {
            return Card == null;
        }

       
        public void TurnCard(int position)
        {
            card.ActualPosition = position;
        }


        public bool WasNeighbour(Card card)
        {
            return formerNeighbours.Contains(card);
        }


        public void AddNeighbourhood(Card card)
        {
            formerNeighbours.Add(card);
        }


        public void ClearHistory()
        {
            formerNeighbours = new ArrayList();
        }       
    }
}