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

        //Collision Sensors
        const int proximityMargin = 5;

        //Adjacency(Proximity) Sensors

        //The owner of this WallSensor
        public static int sightRange; //how far this particular owner can see

        public Sensor(int range, int pi_division) {
            sightRange = range;
            WallScanner = new float?[pi_division];
            length = pi_division;
        }

        //Proximity Sensor
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

        
    }
}
