using System.Collections;

namespace RubiksTangle
{
    public class Hand
    {
        private ArrayList inHand;


        public Hand(ArrayList inHand)
        {
            this.inHand = inHand;
        }


        public ArrayList getHand()
        {
            return inHand;
        }


        public int size()
        {
            return inHand.Count;
        }


        public Card get()
        {
            return (Card)inHand[0];
        }


        public void remove(Card card)
        {

            inHand.Remove(card);
            inHand.Remove(card.getOtherSide());
        }


        public void add(Card card)
        {
            card.setPosition(0);
            card.getOtherSide().setPosition(0);
            inHand.Add(card);
            inHand.Add(card.getOtherSide());
        }


    }
}