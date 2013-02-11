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
        const int penetrationMargin = 5;



        //Wall Sensors
        public static int[] piScanner;
        public static int length;

        //The owner of this WallSensor
        public int sightRange; //how far this particular owner can see

        public Sensor(int range, int pi_division) {
            sightRange = range;
            piScanner = new int[pi_division];
            length = pi_division;
        }

        public static void getAllWalls() {
            for (int k = 0; k < length; k++) { 
                //piScanner[k]=detectWalls
                //some sort of array of sensors

                //public static int[] any walls?

                /*
                 * Take in an int[] with length for how many pis to divide by. Then, run detectWalls in all of those directions
                 * The array will end up storing the distances walls are in every direction indicated by the pi
                 * 
                 * */
            }
        }

        /// <summary>
        /// checks a direction and returns how far away the next object is in that direction
        /// </summary>
        /// <param name="r1"></param>
        /// <returns></returns>
        public static int detectWalls(Rectangle owner, Vector2 direction)
        {
            //look in direction for sightRange, if a wall detected, return how far away it is
            //else return -1, indicating that no wall is in that direction
            return -1;
        }

        public static int detectWalls(Rectangle owner, Vector2 direction, int howFar)
        {
            //look in direction for a certain amount of spaces, if a wall detected, return how far away it is
            //else return -1, indicating that no wall is in that direction
            return -1;
        }
    }
}
