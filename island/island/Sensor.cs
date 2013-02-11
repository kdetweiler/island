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
        public static float?[] WallScanner;
        public static int length;

        //Proximity Sensors
        const int proximityMargin = 5;

        //Adjacency Sensors

        //The owner of this WallSensor
        public static int sightRange; //how far this particular owner can see

        public Sensor(int range, int pi_division) {
            sightRange = range;
            WallScanner = new float?[pi_division];
            length = pi_division;
        }

        public static void WallsGetAll(Rectangle owner, Vector2 location, Vector2 direction, List<Npc> charlist, List<GameObject> objectList) {
            for (int k = 0; k < length; k++) { 
                WallScanner[k]=WallsDetect(owner, location, direction, charlist, objectList);
            }
        }

        public static float? WallsDetect(Rectangle owner, Vector2 location, Vector2 direction, List<Npc> charlist, List<GameObject> objectList)
        {
            Vector3 newLocation = new Vector3(location.X, location.Y, 0);
            Vector3 newDirection = new Vector3(direction.X, direction.Y, 0);
            float? ans = -1;
            Ray ray = new Ray(newLocation, newDirection);
            //look in direction for sightRange, if a wall detected, return how far away it is
            //else return -1, indicating that no wall is in that direction

            for (int k = 0; k < charlist.Count; k++) { 
                Npc temp=charlist[k];
                Vector3 tempMin = new Vector3(temp.position.X,temp.position.Y,0);
                BoundingBox tempBox = new BoundingBox(tempMin, tempMin);
                if(ray.Intersects(tempBox)<=sightRange && ray.Intersects(tempBox)<ans) {
                    ans= ray.Intersects(tempBox);
                }
            }
            for (int k = 0; k < objectList.Count; k++)
            {
                GameObject temp = objectList[k];
                Vector3 tempMin = new Vector3(temp.position.X, temp.position.Y, 0);
                BoundingBox tempBox = new BoundingBox(tempMin, tempMin);
                if (ray.Intersects(tempBox) <= sightRange && ray.Intersects(tempBox)<ans)
                {
                    ans= ray.Intersects(tempBox);
                }
            }
            //if (ray.Intersects()) { }
            return ans;
        }
    }
}
