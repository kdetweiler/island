﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections;

namespace island
{
    class Sensor
    {
        //Wall Sensors to save
        public static float[] WallScanner;
        public static int length;
        public float angle;
        public float angle2;

        //Collision Sensors
        const int proximityMargin = 5;

        //Adjacency(Proximity) Sensors

        //The owner of this WallSensor
        public static int sightRange; //how far this particular owner can see

        public Sensor(int range, int pi_division) 
        {
            sightRange = range;
            WallScanner = new float[pi_division];
            length = pi_division;
        }

        public int getSightRange() 
        {
            return sightRange;
        }

        //Proximity Sensor
        public void Proximity(Player owner, int radius, List<NPC> npcs)
        {
            List<NPC> prox = new List<NPC>();
            int[] quadrant = new int[4];
            Vector2 main = new Vector2(owner.rectangle.Center.X, owner.rectangle.Center.Y); // reference point 1

            foreach (NPC npc in npcs) 
            {
                Vector2 V2 = new Vector2(npc.rectangle.Center.X, npc.rectangle.Center.Y);
                Vector2 Distance = main - V2;

                if (Vector2.Distance(main, V2) < radius)
                {
                    prox.Add(npc);
                    float radians = (float)Math.Atan2(V2.Y - main.Y, V2.X - main.X);

                    angle = MathHelper.ToDegrees(radians);

                    if (owner.rectangle.Center.Y > V2.Y)
                        angle = (owner.faceDirection - angle) % 360;
                    else
                        angle = (360- angle + owner.faceDirection)%360;

                    if (angle >= 0 && angle < 90)
                        quadrant[0]++;
                    else if (angle >= 90 && angle < 180)
                        quadrant[1]++;
                    else if (angle >= 180 && angle < 270)
                        quadrant[2]++;
                    else
                        quadrant[3]++;
                }
            }
            owner.proxList = prox;
            owner.quadrants = quadrant;
        }

        //Wall Sensors
        public void WallsScan(Player owner, List<Wall> wallList) 
        {
            int ray = owner.faceDirection;
            
            //if player is facing up
            if (ray == 0) {
                for (int k = 0; k < owner.sensor.getSightRange(); k++) { 
                    
                }
            }
            
        }

        public float detectWalls() {

            return -1;
        }
    }
}
