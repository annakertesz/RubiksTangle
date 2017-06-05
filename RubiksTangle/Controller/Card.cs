using System;

namespace RubiksTangle
{
    public class Card
    {
        public Color[,] actualEdges;
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
                actualEdges = setEdges(value * 2);
                actualPosition = value;
            }
        }


        public Card(Color[] colors, String name)
        {
            colorsInZeroPosition = colors;
            actualEdges = setEdges(0);
            ActualPosition = 0;
            Filename = name;
        }

        public Color[] getEdge(int edge)
        {
            return new Color[] { actualEdges[edge, 0], actualEdges[edge, 1] };
        }


        private Color[,] setEdges(int positionValue)
        {
            Color[,] edges = new Color[4, 2];
            int y = positionValue;
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


        public int isEdgesJoinable(Color[] edgeToJoin, int neededEdge)
        {
            for (int i = 0; i < 4; i++)
            {
                if (actualEdges[i, 0] == edgeToJoin[1] && actualEdges[i, 1] == edgeToJoin[0])
                {
                    int difference = i - neededEdge;
                    return difference >= 0 ? difference : 4 + difference;
                }
            }
            return -1;
        }
    }
}
