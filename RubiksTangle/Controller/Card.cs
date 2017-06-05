using System;

namespace RubiksTangle
{
    public class Card
    {
        private Color[,] actualEdges;
        private Color[] colorsInZeroPosition;
        private Card otherSide;
        private int actualPosition;
        private String filename;

        public Card OtherSide
        {
            get => otherSide;
            set
            {
                otherSide = value;
                if (value.OtherSide == null) value.OtherSide = this;
            }
        }
        public string Filename { get => filename; private set => filename = value; }
        public int ActualPosition
        {
            get => actualPosition;
            set
            {
                ActualEdges = SetEdges(value);
                actualPosition = value;
            }
        }
        public Color[,] ActualEdges { get => actualEdges; set => actualEdges = value; }


        public Card(Color[] colors, String name)
        {
            colorsInZeroPosition = colors;
            ActualEdges = SetEdges(0);
            ActualPosition = 0;
            Filename = name;
        }


        public Color[] GetEdge(int edge)
        {
            return new Color[] { ActualEdges[edge, 0], ActualEdges[edge, 1] };
        }


        private Color[,] SetEdges(int positionValue)
        {
            Color[,] edges = new Color[4, 2];
            int y = (positionValue % 4) * 2;
            for (int i = 0; i < 4; i++)
            {
                if (y > 7) y = 0;
                edges[i, 0] = colorsInZeroPosition[y];
                y++;
                if (y > 7) y = 0;
                edges[i, 1] = colorsInZeroPosition[y];
                y++;
            }
            return edges;
        }


        public int IsEdgesJoinable(Color[] edgeToJoin, int neededEdge)
        {
            for (int i = 0; i < 4; i++)
            {
                if (ActualEdges[i, 0] == edgeToJoin[1] && ActualEdges[i, 1] == edgeToJoin[0])
                {
                    int difference = i - neededEdge;
                    return difference >= 0 ? difference : 4 + difference;
                }
            }
            return -1;
        }
    }
}
