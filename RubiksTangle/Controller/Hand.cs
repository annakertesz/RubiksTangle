using System.Collections;

namespace RubiksTangle
{
    public class Hand
    {
        private ArrayList inHand;

        public ArrayList InHand { get => inHand; private set => inHand = value; }
        public int Size { get => InHand.Count; }
        public Hand(ArrayList inHand)
        {
            this.InHand = inHand;
        }
       
        
        public Card Get()
        {
            return (Card)InHand[0];
        }


        public void Remove(Card card)
        {
            InHand.Remove(card);
            InHand.Remove(card.OtherSide);
        }


        public void Add(Card card)
        {
            card.ActualPosition = 0;
            card.OtherSide.ActualPosition = 0;
            InHand.Add(card);
            InHand.Add(card.OtherSide);
        }        
    }
}