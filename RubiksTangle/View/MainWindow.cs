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

        public Form1()     
        {
            InitializeComponent();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addFields();
            fields = getMediumBoard();
            game = new Game(fields);
            SubscribeToFields();
            ready = game.startNewGame();
           // MessageBox.Show(ready);
        }
        

        private void Place1(Card card)
        {
            //field1.ImageLocation = @"c:\users\hudejo\documents\RubiksTangle\RubiksTangle\Img\" + card.getFilename() + ".jpg";
            Image flipimage = Image.FromFile(@"c:\users\hudejo\documents\RubiksTangle\RubiksTangle\Img\" + card.getFilename() + ".jpg");
            for (int i = 0; i < card.getActualPosition()+1; i++)
            {
                flipimage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }            
            field1.Image = flipimage;
        }

        private void Place2(Card card)
        {
            Image flipimage = Image.FromFile(@"c:\users\hudejo\documents\RubiksTangle\RubiksTangle\Img\" + card.getFilename() + ".jpg");
            for (int i = 0; i < card.getActualPosition()+2; i++)
            {
                flipimage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            field2.Image = flipimage;
        }

        private void Place3(Card card)
        {
            Image flipimage = Image.FromFile(@"c:\users\hudejo\documents\RubiksTangle\RubiksTangle\Img\" + card.getFilename() + ".jpg");
            for (int i = 0; i < card.getActualPosition(); i++)
            {
                flipimage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            field3.Image = flipimage;

        }

        private void Place4(Card card)
        {
            Image flipimage = Image.FromFile(@"c:\users\hudejo\documents\RubiksTangle\RubiksTangle\Img\" + card.getFilename() + ".jpg");
            for (int i = 0; i < card.getActualPosition(); i++)
            {
                flipimage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            field4.Image = flipimage;
        }

        private void Place5(Card card)
        {
            Image flipimage = Image.FromFile(@"c:\users\hudejo\documents\RubiksTangle\RubiksTangle\Img\" + card.getFilename() + ".jpg");
            for (int i = 0; i < card.getActualPosition(); i++)
            {
                flipimage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            field5.Image = flipimage;
        }

        private void Place6(Card card)
        {
            Image flipimage = Image.FromFile(@"c:\users\hudejo\documents\RubiksTangle\RubiksTangle\Img\" + card.getFilename() + ".jpg");
            for (int i = 0; i < card.getActualPosition()+2; i++)
            {
                flipimage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            field6.Image = flipimage;
        }

        private void Place7(Card card)
        {
            Image flipimage = Image.FromFile(@"c:\users\hudejo\documents\RubiksTangle\RubiksTangle\Img\" + card.getFilename() + ".jpg");
            for (int i = 0; i < card.getActualPosition(); i++)
            {
                flipimage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            field7.Image = flipimage;
        }

        private void Place8(Card card)
        {
            Image flipimage = Image.FromFile(@"c:\users\hudejo\documents\RubiksTangle\RubiksTangle\Img\" + card.getFilename() + ".jpg");
            for (int i = 0; i < card.getActualPosition(); i++)
            {
                flipimage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            field8.Image = flipimage;
        }

        private void Place9(Card card)
        {
            Image flipimage = Image.FromFile(@"c:\users\hudejo\documents\RubiksTangle\RubiksTangle\Img\" + card.getFilename() + ".jpg");
            for (int i = 0; i < card.getActualPosition(); i++)
            {
                flipimage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }
            field9.Image = flipimage;
        }

        private void SubscribeToFields()
        {
            fields[0].PlacedEvent += Place1; // subscibe to the event
            fields[1].PlacedEvent += Place2;
            fields[2].PlacedEvent += Place3;
            fields[3].PlacedEvent += Place4; // subscibe to the event
            fields[4].PlacedEvent += Place5;
            fields[5].PlacedEvent += Place6;
            fields[6].PlacedEvent += Place7;
            fields[7].PlacedEvent += Place8;
            fields[8].PlacedEvent += Place9;
            //m.Stuff -= HeardIt; // unsubscibe to the event
        }

        private void addFields()
        {
            Controls.Add(field1);
            Controls.Add(field2);
            Controls.Add(field3);
            Controls.Add(field4);
            Controls.Add(field5);
            Controls.Add(field6);
            Controls.Add(field7);
            Controls.Add(field8);
            Controls.Add(field9);
        }
        

        private BoardField[] getMediumBoard()
        {
            BoardField[] fields = new BoardField[9];
            fields[0] = new BoardField(null, null);
            fields[1] = new BoardField(null, fields[0]);
            fields[2] = new BoardField(null, fields[1]);
            fields[3] = new BoardField(fields[0], null);
            fields[4] = new BoardField(fields[1], fields[3]);
            fields[5] = new BoardField(fields[2], fields[4]);
            fields[6] = new BoardField(fields[3], null);
            fields[7] = new BoardField(fields[4], fields[6]);
            fields[8] = new BoardField(fields[5], fields[7]);
            return fields;
        }
    }
}
