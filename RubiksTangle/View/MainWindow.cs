using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RubiksTangle
{
    public partial class Form1 : Form
    {

        private BoardField[] fields;
        private Game game;
        String ready;
        private PictureBox[] pictureBoxList;
        public event SpeedHandler ChangeSpeedEvent;
        public delegate void SpeedHandler(int diff);

        public Form1()     
        {
            InitializeComponent();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addFields();
            fields = getMediumBoard();
            game = new Game(fields, this);
            SubscribeToFields();
            ready = game.startNewGame();
        }
        
        
        private void PlaceCard(BoardField field)
        {
            Image flipimage = Image.FromFile(@"c:\users\hudejo\documents\RubiksTangle\RubiksTangle\Img\" + field.getCard().getFilename() + ".jpg");
            int turns = field.getIndexOfField() == 0 ?  field.getCardPosition()  : 4 - field.getCardPosition();
            for (int i = 0; i < turns; i++)
            {
                flipimage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            pictureBoxList[field.getIndexOfField()].Image = flipimage;
        }

        private void RemoveCard(int indexOfField)
        {
            pictureBoxList[indexOfField].Image = null;
        }

        private void SubscribeToFields()
        {
            foreach(BoardField field in fields)
            {
                field.PlacedEvent += PlaceCard;
            }

            game.RemoveEvent += RemoveCard;
        }

        private void addFields()
        {
            pictureBoxList = new PictureBox[] { field1, field2, field3, field4, field5, field6, field7, field8, field9 };
            foreach (PictureBox field in pictureBoxList)
            {
                Controls.Add(field);
            }
        }
        

        private BoardField[] getMediumBoard()
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (ChangeSpeedEvent != null)
            {
                ChangeSpeedEvent(1);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ChangeSpeedEvent != null)
            {
                ChangeSpeedEvent(-1);
            }
        }
    }
}
