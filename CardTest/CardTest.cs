using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RubiksTangle;

namespace CardTest
{
    [TestClass]
    public class CardTest
    {
        [TestMethod]
        public void TestSetEdgesToZeroPosition()
        {
            Card card = new Card(new Color[] { Color.B, Color.Y, Color.R, Color.G, Color.B, Color.G, Color.Y, Color.R }, "firstA");
            card.ActualPosition = 0;
            Color[,] actual = card.ActualEdges;
            Color[,] expected = new Color[4, 2] { { Color.B, Color.Y, }, { Color.R, Color.G }, { Color.B, Color.G }, { Color.Y, Color.R } };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSetEdgesToSecondPosition()
        {
            Card card = new Card(new Color[] { Color.B, Color.Y, Color.R, Color.G, Color.B, Color.G, Color.Y, Color.R }, "firstA");
            card.ActualPosition = 2;
            Color[,] actual = card.ActualEdges;
            Color[,] expected = new Color[4, 2] { { Color.B, Color.G }, { Color.Y, Color.R }, { Color.B, Color.Y, }, { Color.R, Color.G } };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSetEdgesToPositionFour()
        {
            Card card = new Card(new Color[] { Color.B, Color.Y, Color.R, Color.G, Color.B, Color.G, Color.Y, Color.R }, "firstA");
            card.ActualPosition = 4;
            Color[,] actual = card.ActualEdges;
            Color[,] expected = new Color[4, 2] { { Color.B, Color.Y, }, { Color.R, Color.G }, { Color.B, Color.G }, { Color.Y, Color.R } };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSetEdgesToPositionSix()
        {
            Card card = new Card(new Color[] { Color.B, Color.Y, Color.R, Color.G, Color.B, Color.G, Color.Y, Color.R }, "firstA");
            card.ActualPosition = 6;
            Color[,] actual = card.ActualEdges;
            Color[,] expected = new Color[4, 2] { { Color.B, Color.G }, { Color.Y, Color.R }, { Color.B, Color.Y, }, { Color.R, Color.G } };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSetEdgesJoinableNotJoinable()
        {
            Card cardA = new Card(new Color[] { Color.B, Color.Y, Color.R, Color.G, Color.B, Color.G, Color.Y, Color.R }, "firstA");
            Color[] edgeToJoin = new Color[] { Color.B, Color.G };
            Assert.AreEqual(-1, cardA.IsEdgesJoinable(edgeToJoin, 2));
        }

        [TestMethod]
        public void TestSetEdgesJoinableIsJoinable()
        {
            Card cardA = new Card(new Color[] { Color.B, Color.Y, Color.R, Color.G, Color.B, Color.G, Color.Y, Color.R }, "firstA");
            Color[] edgeToJoin = new Color[] { Color.G, Color.B };
            Assert.AreEqual(2, cardA.IsEdgesJoinable(edgeToJoin, 0));
        }
    }
}
