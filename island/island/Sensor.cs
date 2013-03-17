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
using System.Collections;

namespace island
{
    class Sensor
    {
        //Wall Sensors to save
        public static float[] WallScanner;
        public static int length;
        public float npcAngle;
        public float wallAngle;
        public bool wallFound = false;

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

        //wall sensor
        /*
        public void WallScan(Player owner, List<Wall> walls)
        {
            List<Wall> wallsInFront = new List<Wall>();
            float[] wallSense = new float[3];
            Vector2 main = new Vector2(owner.rectangle.Center.X, owner.rectangle.Center.Y); // reference point 1

            if (owner.faceDirection >= 235 && owner.faceDirection <= 315)
            {
                for (int currSensorRange = owner.rectangle.Center.Y; (currSensorRange < (currSensorRange + sightRange)) && !wallFound; currSensorRange++)
                {
                    foreach (Wall wall in walls)
                    {
                        if ((owner.rectangle.Center.X >= wall.rectangle.Left && owner.rectangle.Center.Y <= wall.rectangle.Right) && currSensorRange == wall.rectangle.Y)
                        {
                            Vector2 V2 = new Vector2(owner.rectangle.Center.X, currSensorRange);
                            wallSense[1] = Vector2.Distance(main, V2);
                            wallFound = true;
                            break;
                        }
                    }
                }
            }
            owner.wallSensors = wallSense;
            owner.wallList = wallsInFront;
            
            /*
            foreach (Wall wall in walls)
            {
                Vector2 V2 = new Vector2(wall.rectangle.Center.X, wall.rectangle.Center.Y);
                //Vector2 Distance = main - V2;

                if (Vector2.Distance(main, V2) < sightRange)
                {
                    wallsInFront.Add(wall);
                    float radians = (float)Math.Atan2(V2.Y - main.Y, V2.X - main.X);

                    wallAngle = MathHelper.ToDegrees(radians);

                    if (owner.rectangle.Center.Y > V2.Y)
                        wallAngle = (owner.faceDirection - wallAngle) % 360;
                    else
                        wallAngle = (360 - wallAngle + owner.faceDirection) % 360;

                    if (wallAngle >= 45 && wallAngle < 67.5)
                    {
                        wallSense[0] = Vector2.Distance(main, V2);
                    }
                    else if (wallAngle >= 67.5 && wallAngle <= 115.5)
                    {
                        wallSense[1] = Vector2.Distance(main, V2);
                    }
                    else if (wallAngle > 115.5 && wallAngle < 135)
                        wallSense[2] = Vector2.Distance(main, V2);

                }
                owner.wallSensors = wallSense;
                owner.wallList = wallsInFront;
            }
        }*/
    

        
        public void WallScan(Player owner, List<Wall> walls)
        {
            List<Wall> wallsInFront = new List<Wall>();
            float[] wallSense = new float[3];
            Vector2 main = new Vector2(owner.rectangle.Center.X, owner.rectangle.Center.Y); // reference point 1

            foreach (Wall wall in walls)
            {
                Vector2 V2 = new Vector2(wall.rectangle.Center.X, wall.rectangle.Center.Y);
                //Vector2 Distance = main - V2;

                if (Vector2.Distance(main, V2) < sightRange)
                {
                    wallsInFront.Add(wall);
                    float radians = (float)Math.Atan2(V2.Y - main.Y, V2.X - main.X);

                    wallAngle = MathHelper.ToDegrees(radians);

                    if (owner.rectangle.Center.Y > V2.Y)
                        wallAngle = (owner.faceDirection - wallAngle) % 360;
                    else
                        wallAngle = (360 - wallAngle + owner.faceDirection) % 360;

                    if (wallAngle >= 45 && wallAngle < 67.5)
                    {
                        wallSense[0] = Vector2.Distance(main, V2);
                    }
                    else if (wallAngle >= 67.5 && wallAngle <= 115.5)
                    {
                        wallSense[1] = Vector2.Distance(main, V2);
                    }
                    else if (wallAngle > 115.5 && wallAngle < 135)
                        wallSense[2] = Vector2.Distance(main, V2);

                }
                owner.wallList = wallsInFront;
                owner.wallSensors = wallSense;
            }
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

                    npcAngle = MathHelper.ToDegrees(radians);

                    if (owner.rectangle.Center.Y > V2.Y)
                        npcAngle = (owner.faceDirection - npcAngle) % 360;
                    else
                        npcAngle = (360- npcAngle + owner.faceDirection)%360;

                    if (npcAngle >= 0 && npcAngle < 90)
                        quadrant[0]++;
                    else if (npcAngle >= 90 && npcAngle < 180)
                        quadrant[1]++;
                    else if (npcAngle >= 180 && npcAngle < 270)
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
