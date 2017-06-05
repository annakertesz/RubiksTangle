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
        public static int speed;
        private Form1 form;

        public event CardHandler TryCardEvent;
        public event RemoveHandler RemoveEvent;
        public event CardTurnHandler TurnEvent;
        public delegate void CardHandler(Card card);
        public delegate void RemoveHandler(int indexOfField);
        public delegate void CardTurnHandler(BoardField field);
        
        

        public Game(BoardField[] fields, Form1 runningForm)
        {
            this.board = new Board(fields);
            this.hand = new Hand(setTwoSidedCards());
            this.currentField = 1;
            speed = 200;
            form = runningForm;
            SubscribeToSpeedChange();
        }

        private void SubscribeToSpeedChange()
        {
            form.ChangeSpeedEvent += changeSpeed;
        }

        public void startNewGame()
        {
            ThreadPool.QueueUserWorkItem(delegate { makePuzzle(); });
        }

        private void changeSpeed(int diff)
        {
            speed = speed + diff * 100 <= 500 && speed + diff * 100 >= 0 ? speed + diff * 100 : speed;
        }


        public void makePuzzle()
        {
            Console.WriteLine("I start makePuzzle");
            while (hand.size() > 0)
            {
                if (currentField > 1)
                {
                    board.getField(currentField - 1).removeCard(hand);
                    if (RemoveEvent != null)
                    {
                        RemoveEvent(currentField - 1);
                        Thread.Sleep(speed);
                    }
                }
                if (placeCard(currentField - 1)) break;
            }
        }


        private bool placeCard(int indexOfField)
        {
            currentField = indexOfField;
            if (currentField == 9) return true;
            if (indexOfField == 0)
            {
                if (!placeFirstCard()) return false;
                return placeCard(indexOfField + 1);
            }
            else
            {
                foreach (Card card in hand.getHand())
                {
                    if (board.getField(indexOfField - 1).wasNeighbour(card)) continue;
                    if (TryCardEvent != null)
                    {
                        TryCardEvent(card);
                        Thread.Sleep(Game.speed/4);
                    }
                    if (board.getField(indexOfField).placeCard(card, hand))
                    {
                        board.getField(indexOfField - 1).addNeighbourhood(card);

                        return placeCard(indexOfField + 1);
                    }
                }
                return false;
            }
        }


        private bool placeFirstCard()
        {
            BoardField firstField = board.getField(0);
            if (firstField.isEmpty())
            {
                firstField.placeCard(hand.get(), hand);
                hand.remove(firstField.Card);
                return true;
            }
            int position = firstField.getCardPosition();
            if (position == 3)
            {
                firstField.removeCard(hand);
                if (board.triedAllPossibilities()) return false;
                foreach (Card card in hand.getHand())
                {
                    if (board.wasOnFirstPlace(card)) continue;
                    firstField.placeCard(card, hand);
                    board.addToHistory(card);
                    firstField.clearHistory();
                    break;
                }
            }
            else
            {
                firstField.turnCard(position + 1);
                if (TurnEvent != null)
                {
                    TurnEvent(firstField);
                    Thread.Sleep(speed/2);
                }
            }
            return true;
        }


        private ArrayList setTwoSidedCards()
        {
            Card firstA = new Card(new Color[] { Color.B, Color.Y, Color.R, Color.G, Color.B, Color.G, Color.Y, Color.R }, "firstA");
            Card secondA = new Card(new Color[] { Color.G, Color.B, Color.R, Color.Y, Color.G, Color.Y, Color.B, Color.R }, "secondA");
            Card thirdA = new Card(new Color[] { Color.B, Color.Y, Color.G, Color.R, Color.B, Color.R, Color.Y, Color.G }, "thirdA");
            Card forthA = new Card(new Color[] { Color.R, Color.Y, Color.B, Color.G, Color.R, Color.G, Color.Y, Color.B }, "fouthA");
            Card fifthA = new Card(new Color[] { Color.B, Color.R, Color.G, Color.Y, Color.B, Color.Y, Color.R, Color.G }, "fifthA");
            Card sixthA = new Card(new Color[] { Color.Y, Color.B, Color.G, Color.R, Color.Y, Color.R, Color.B, Color.G }, "sixthA");
            Card seventhA = new Card(new Color[] { Color.Y, Color.B, Color.R, Color.G, Color.Y, Color.G, Color.B, Color.R }, "seventhA");
            Card eighthA = new Card(new Color[] { Color.G, Color.R, Color.B, Color.Y, Color.G, Color.Y, Color.R, Color.B }, "eighthA");
            Card ninthA = new Card(new Color[] { Color.G, Color.Y, Color.B, Color.R, Color.G, Color.R, Color.Y, Color.B }, "ninthA");

            Card firstB = new Card(new Color[] { Color.G, Color.Y, Color.R, Color.B, Color.G, Color.B, Color.Y, Color.R }, "firstB");
            Card secondB = new Card(new Color[] { Color.Y, Color.G, Color.R, Color.B, Color.Y, Color.B, Color.G, Color.R }, "secondB");
            Card thirdB = new Card(new Color[] { Color.Y, Color.R, Color.G, Color.B, Color.Y, Color.B, Color.R, Color.G }, "thirdB");
            Card forthB = new Card(new Color[] { Color.R, Color.G, Color.B, Color.Y, Color.R, Color.Y, Color.G, Color.B }, "fouthB");
            Card fifthB = new Card(new Color[] { Color.R, Color.Y, Color.G, Color.B, Color.R, Color.B, Color.Y, Color.G }, "fifthB");
            Card sixthB = new Card(new Color[] { Color.R, Color.B, Color.G, Color.Y, Color.R, Color.Y, Color.B, Color.G }, "sixthB");
            Card seventhB = new Card(new Color[] { Color.B, Color.G, Color.R, Color.Y, Color.B, Color.Y, Color.G, Color.R }, "seventhB");
            Card eighthB = new Card(new Color[] { Color.Y, Color.G, Color.B, Color.R, Color.Y, Color.R, Color.G, Color.B }, "eighthB");
            Card ninthB = new Card(new Color[] { Color.Y, Color.R, Color.B, Color.G, Color.Y, Color.G, Color.R, Color.B }, "ninthB");


            firstA.OtherSide = firstB;
            secondA.OtherSide = secondB;
            thirdA.OtherSide = thirdB;
            forthA.OtherSide = forthB;
            fifthA.OtherSide = fifthB;
            sixthA.OtherSide = sixthB;
            seventhA.OtherSide = seventhB;
            eighthA.OtherSide = eighthB;
            ninthA.OtherSide = ninthB;
            return new ArrayList(new ArrayList(new Card[] {
                firstA, secondA, thirdA, forthA, fifthA, sixthA, seventhA, eighthA, ninthA,
                firstB, secondB, thirdB, forthB, fifthB, sixthB, seventhB, eighthB, ninthB}));
        }
    }
}

        