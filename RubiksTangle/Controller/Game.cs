using System;
using System.Collections;
using System.Threading;
using System.Windows.Forms;

namespace RubiksTangle
{
    public class Game
    {
        private Board board;
        private Hand hand;
        private int currentField;
        private Form1 form;
        public static int Speed;

        public event CardHandler TryCardEvent;
        public event RemoveHandler RemoveEvent;
        public event CardTurnHandler TurnEvent;
        public delegate void CardHandler(Card card);
        public delegate void RemoveHandler(int indexOfField);
        public delegate void CardTurnHandler(BoardField field);
        
        

        public Game(BoardField[] fields, Form1 runningForm)
        {
            this.board = new Board(fields);
            this.hand = new Hand(SetUpCards());
            this.currentField = 1;
            Speed = 200;
            form = runningForm;
            SubscribeToSpeedChange();
        }


        private void SubscribeToSpeedChange()
        {
            form.ChangeSpeedEvent += ChangeSpeed;
        }


        public void StartNewGame()
        {
            ThreadPool.QueueUserWorkItem(delegate { MakePuzzle(); });
        }

        private void ChangeSpeed(int diff)
        {
            Speed = Speed + diff * 100 <= 500 && Speed + diff * 100 >= 0 ? Speed + diff * 100 : Speed;
        }


        public void MakePuzzle()
        {
            Console.WriteLine("I start makePuzzle");
            while (hand.Size > 0)
            {
                if (currentField > 1)
                {
                    board.GetField(currentField - 1).RemoveCard(hand);
                    if (RemoveEvent != null)
                    {
                        RemoveEvent(currentField - 1);
                        Thread.Sleep(Speed);
                    }
                }
                if (PlaceCard(currentField - 1)) break;
            }
        }


        private bool PlaceCard(int indexOfField)
        {
            currentField = indexOfField;
            if (currentField == 9) return true;
            if (indexOfField == 0)
            {
                if (!PlaceFirstCard()) return false;
                return PlaceCard(indexOfField + 1);
            }
            else
            {
                foreach (Card card in hand.InHand)
                {
                    if (board.GetField(indexOfField - 1).WasNeighbour(card)) continue;
                    if (TryCardEvent != null)
                    {
                        TryCardEvent(card);
                        Thread.Sleep(Game.Speed/4);
                    }
                    if (board.GetField(indexOfField).PlaceCard(card, hand))
                    {
                        board.GetField(indexOfField - 1).AddNeighbourhood(card);

                        return PlaceCard(indexOfField + 1);
                    }
                }
                return false;
            }
        }


        private bool PlaceFirstCard()
        {
            BoardField firstField = board.GetField(0);
            if (firstField.IsEmpty())
            {
                firstField.PlaceCard(hand.Get(), hand);
                hand.Remove(firstField.Card);
                return true;
            }
            int position = firstField.CardPosition;
            if (position == 3)
            {
                firstField.RemoveCard(hand);
                if (board.TriedAllPossibilities()) return false;
                foreach (Card card in hand.InHand)
                {
                    if (board.WasOnFirstPlace(card)) continue;
                    firstField.PlaceCard(card, hand);
                    board.AddToHistory(card);
                    firstField.ClearHistory();
                    break;
                }
            }
            else
            {
                firstField.TurnCard(position + 1);
                if (TurnEvent != null)
                {
                    TurnEvent(firstField);
                    Thread.Sleep(Speed/2);
                }
            }
            return true;
        }


        private ArrayList SetUpCards()
        {
            Card firstA = new Card(new Color[] { Color.B, Color.Y, Color.R, Color.G, Color.B, Color.G, Color.Y, Color.R }, "firstA");
            Card secondA = new Card(new Color[] { Color.G, Color.B, Color.R, Color.Y, Color.G, Color.Y, Color.B, Color.R }, "secondA");
            Card thirdA = new Card(new Color[] { Color.B, Color.Y, Color.G, Color.R, Color.B, Color.R, Color.Y, Color.G }, "thirdA");
            Card fourthA = new Card(new Color[] { Color.R, Color.Y, Color.B, Color.G, Color.R, Color.G, Color.Y, Color.B }, "fourthA");
            Card fifthA = new Card(new Color[] { Color.B, Color.R, Color.G, Color.Y, Color.B, Color.Y, Color.R, Color.G }, "fifthA");
            Card sixthA = new Card(new Color[] { Color.Y, Color.B, Color.G, Color.R, Color.Y, Color.R, Color.B, Color.G }, "sixthA");
            Card seventhA = new Card(new Color[] { Color.Y, Color.B, Color.R, Color.G, Color.Y, Color.G, Color.B, Color.R }, "seventhA");
            Card eighthA = new Card(new Color[] { Color.G, Color.R, Color.B, Color.Y, Color.G, Color.Y, Color.R, Color.B }, "eighthA");
            Card ninthA = new Card(new Color[] { Color.G, Color.Y, Color.B, Color.R, Color.G, Color.R, Color.Y, Color.B }, "ninthA");

            Card firstB = new Card(new Color[] { Color.G, Color.Y, Color.R, Color.B, Color.G, Color.B, Color.Y, Color.R }, "firstB");
            Card secondB = new Card(new Color[] { Color.Y, Color.G, Color.R, Color.B, Color.Y, Color.B, Color.G, Color.R }, "secondB");
            Card thirdB = new Card(new Color[] { Color.Y, Color.R, Color.G, Color.B, Color.Y, Color.B, Color.R, Color.G }, "thirdB");
            Card fourthB = new Card(new Color[] { Color.R, Color.G, Color.B, Color.Y, Color.R, Color.Y, Color.G, Color.B }, "fourthB");
            Card fifthB = new Card(new Color[] { Color.R, Color.Y, Color.G, Color.B, Color.R, Color.B, Color.Y, Color.G }, "fifthB");
            Card sixthB = new Card(new Color[] { Color.R, Color.B, Color.G, Color.Y, Color.R, Color.Y, Color.B, Color.G }, "sixthB");
            Card seventhB = new Card(new Color[] { Color.B, Color.G, Color.R, Color.Y, Color.B, Color.Y, Color.G, Color.R }, "seventhB");
            Card eighthB = new Card(new Color[] { Color.Y, Color.G, Color.B, Color.R, Color.Y, Color.R, Color.G, Color.B }, "eighthB");
            Card ninthB = new Card(new Color[] { Color.Y, Color.R, Color.B, Color.G, Color.Y, Color.G, Color.R, Color.B }, "ninthB");


            firstA.OtherSide = firstB;
            secondA.OtherSide = secondB;
            thirdA.OtherSide = thirdB;
            fourthA.OtherSide = fourthB;
            fifthA.OtherSide = fifthB;
            sixthA.OtherSide = sixthB;
            seventhA.OtherSide = seventhB;
            eighthA.OtherSide = eighthB;
            ninthA.OtherSide = ninthB;
            return new ArrayList(new ArrayList(new Card[] {
                firstA, secondA, thirdA, fourthA, fifthA, sixthA, seventhA, eighthA, ninthA,
                firstB, secondB, thirdB, fourthB, fifthB, sixthB, seventhB, eighthB, ninthB}));
        }
    }
}

        