using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace island
{
    class Sensor
    {



        //Wall Sensors
        public static int[] WallScanner;
        public static int length;

        //Proximity Sensors
        const int proximityMargin = 5;

        //Adjacency Sensors

        //The owner of this WallSensor
        public int sightRange; //how far this particular owner can see

        public Sensor(int range, int pi_division) {
            sightRange = range;
            WallScanner = new int[pi_division];
            length = pi_division;
        }

        public static void WallsGetAll() {
            for (int k = 0; k < length; k++) { 

            }
        }

        public static int WallsDetect(Rectangle owner, Vector2 direction)
        {
            //look in direction for sightRange, if a wall detected, return how far away it is
            //else return -1, indicating that no wall is in that direction
            return -1;
        }
    }
}
