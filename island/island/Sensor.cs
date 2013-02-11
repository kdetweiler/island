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
        public float distance;

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

        public void Proximity(Player owner, int radius, List<NPC> npcs)
        {
            List<NPC> prox = new List<NPC>();
            Vector2 main = new Vector2(owner.rectangle.Center.X, owner.rectangle.Center.Y); // reference point 1

            foreach (NPC npc in npcs) 
            {
                Vector2 V2 = new Vector2(npc.rectangle.Center.X, npc.rectangle.Center.Y);
                Vector2 Distance = main - V2;
                
                distance = Vector2.Distance(main, V2);
                if (Vector2.Distance(main, V2) < radius)
                    prox.Add(npc);
            }
            owner.proxList = prox;
        }

        /*
        public void proximitySensor(Rectangle rectangle, int radius, List<Npc> npcs)
        {
            for(int k=0;k<=360;k+=45) {
                for each Npc in Npc
            }
            
        }*/

        public void WallsGetAll(Rectangle owner, Vector2 location, Vector2 direction, List<NPC> charlist, List<GameObject> objectList)
        {
            for (int k = 0; k < length; k++) { 
                WallScanner[k]=WallsDetect(owner, location, direction, charlist, objectList);
            }
        }

        public static float? WallsDetect(Rectangle owner, Vector2 location, Vector2 direction, List<NPC> charlist, List<GameObject> objectList)
        {
            Vector3 newLocation = new Vector3(location.X, location.Y, 0);
            Vector3 newDirection = new Vector3(direction.X, direction.Y, 0);
            float? ans = -1;
            Ray ray = new Ray(newLocation, newDirection);
            //look in direction for sightRange, if a wall detected, return how far away it is
            //else return -1, indicating that no wall is in that direction

            for (int k = 0; k < charlist.Count; k++) { 
                NPC temp=charlist[k];
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
