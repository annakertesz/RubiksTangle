using RubiksTangle.Properties;
using System;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace RubiksTangle
{
    public partial class Form1 : Form
    {

        private BoardField[] fields;
        private Game game;
        private PictureBox[] pictureBoxList;
        private static ResourceManager rm = Resources.ResourceManager;

        public event SpeedHandler ChangeSpeedEvent;
        public delegate void SpeedHandler(int diff);
        
        
        public Form1()
        {
            InitializeComponent();

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            setUpPictureBoxes();
            fields = getMyBoard();
            setInitialImages();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            clearBoardImages();
            game = new Game(fields, this);
            Subscribes();
            game.startNewGame();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (ChangeSpeedEvent != null)
            {
                ChangeSpeedEvent(-1);
            }

        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (ChangeSpeedEvent != null)
            {
                ChangeSpeedEvent(1);
            }
        }
                         
  
        private void PlaceCard(BoardField field)
        {
            Image flipimage = (Bitmap)rm.GetObject(field.Card.Filename);
            for (int i = 0; i < 4 - field.CardPosition; i++)
            {
                flipimage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            pictureBoxList[field.IndexOfField].Image = flipimage;
        }


        private void RemoveCard(int indexOfField)
        {
            pictureBoxList[indexOfField].Image = null;
        }


        private void ShowTriedCard(Card card)
        {
           
            Image image = (Bitmap)rm.GetObject(card.Filename);
            fieldForNewCard.Image = image;
        }
        
            
        private void setUpPictureBoxes()
        {
            pictureBoxList = new PictureBox[] { field1, field2, field3, field4, field5, field6, field7, field8, field9 };
            foreach (PictureBox pictureBox in pictureBoxList)
            {
                Controls.Add(pictureBox);
            }
        }
        

        private void Subscribes()
        {
            foreach (BoardField field in fields)
            {
                field.PlacedEvent += PlaceCard;
            }
            game.TurnEvent += PlaceCard;
            game.TryCardEvent += ShowTriedCard;
            game.RemoveEvent += RemoveCard;
        }
        
        
        private BoardField[] getMyBoard()
        {
            BoardField[] fields = new BoardField[9];
            fields[0] = new BoardField(null, null, 0);
            fields[1] = new BoardField(null, fields[0], 1);
            fields[2] = new BoardField(null, fields[1], 2);
            fields[3] = new BoardField(fields[0], null, 3);
            fields[4] = new BoardField(fields[1], fields[3], 4);
            fields[5] = new BoardField(fields[2], fields[4], 5);
            fields[6] = new BoardField(fields[3], null, 6);
            fields[7] = new BoardField(fields[4], fields[6], 7);
            fields[8] = new BoardField(fields[5], fields[7], 8);
            return fields;
        }


        private void setInitialImages()
        {

            Image image = (Bitmap)rm.GetObject("semaS.jpg");

            foreach (PictureBox pictureBox in pictureBoxList)
            {
                pictureBox.Image = image;
            }
            fieldForNewCard.Image= image;
        }


        private void clearBoardImages()
        {
            foreach (PictureBox pictureBox in pictureBoxList)
            {
                pictureBox.Image = null;
            }
        }

    }
}
