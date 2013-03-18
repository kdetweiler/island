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
        public double g;
        public double f;

        public Node() { }

        public Node(int xPos, int yPos, Node[] listNeighbors, double[] listDistance) {
            point=new Vector2(xPos,yPos);
            neighbors = listNeighbors;
            distanceTo = listDistance;
            g = 0;
            f = 0;
        }

        public Node(Vector2 pointPos, Node[] listNeighbors, double[] listDistance) {
            point=pointPos;
            neighbors=listNeighbors;
            distanceTo=listDistance;
            g = 0;
            f = 0;
        }

        public double getTo(int k) {
            return distanceTo[k];
        }

        public double H(Node dest) 
        {
            return Math.Sqrt(Math.Pow((point.X - dest.point.X),2) - Math.Pow((point.Y - dest.point.Y),2));
        }

        public double G(Node dest) 
        {
            return 0;
        }

    }
}
