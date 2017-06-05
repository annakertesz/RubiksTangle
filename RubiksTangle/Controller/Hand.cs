using System.Collections;

namespace RubiksTangle
{
    public class Hand
    {
        private ArrayList inHand;

        public ArrayList InHand { get => inHand; private set => inHand = value; }

        public Hand(ArrayList inHand)
        {
            this.InHand = inHand;
        }


        public int size()
        {
            return InHand.Count;
        }


        public Card get()
        {
            return (Card)InHand[0];
        }


        public void remove(Card card)
        {

            InHand.Remove(card);
            InHand.Remove(card.OtherSide);
        }


        public void add(Card card)
        {
            card.ActualPosition = 0;
            card.OtherSide.ActualPosition = 0;
            InHand.Add(card);
            InHand.Add(card.OtherSide);
        }


    }
}