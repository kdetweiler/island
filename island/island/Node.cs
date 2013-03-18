using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace island
{
    public class Node
    {
        public Vector2 point;
        public Node[] neighbors;
        public double[] distanceTo;

        public Node() { }

        public Node(int xPos, int yPos, Node[] listNeighbors, double[] listDistance) {
            point=new Vector2(xPos,yPos);
            neighbors = listNeighbors;
            distanceTo = listDistance;
        }

        public Node(Vector2 pointPos, Node[] listNeighbors, double[] listDistance) {
            point=pointPos;
            neighbors=listNeighbors;
            distanceTo=listDistance;
        }

        public double getTo(Vector2 dest)
        {
            for(int k=0;k<neighbors.Length;k++) {
                if (neighbors[k].point == dest) return distanceTo[k];
            }
            return -1;
        }

        public double getTo(int x, int y) {
            Vector2 thatPoint = new Vector2(x, y);
            return getTo(thatPoint);
        }

    }
}
