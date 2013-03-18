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
        public float dTmp;
        public bool wallFound = false;

        public Vector2 main = new Vector2();
        public Vector2 wallCenter = new Vector2();
        public Vector2 wallLeft = new Vector2();
        public Vector2 wallRight = new Vector2();
        public Vector2 sightRangeCenter = new Vector2();
        public Vector2 sightRangeLeft = new Vector2();
        public Vector2 sightRangeRight = new Vector2();
        public Vector2 wallLeft45 = new Vector2();
        public Vector2 wallRight45 = new Vector2();
        public Vector2 distanceLeft = new Vector2();
        public Vector2 distanceRight = new Vector2();


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

        public void WallScan(Player owner, List<Wall> walls)
        {
            List<Wall> wallsInFront = new List<Wall>();
            float[] wallSense = new float[3];

            //If face down
            foreach (Wall wall in walls)
            {
                setWallDefaults(owner, wall);
                Vector2 distance = new Vector2();

                //straight down
                if (Intersects(main, sightRangeCenter, wallLeft, wallRight, out distance))
                {
                    owner.wallSensors[1] = Vector2.Distance(main, distance);
                }
                else
                {
                    owner.wallSensors[0] = 0;
                }

                //left 45 degrees
                if (Intersects(wallLeft45, sightRangeLeft, wallLeft, wallRight, out distance))
                {
                    owner.wallSensors[0] = Vector2.Distance(main, distanceLeft);
                }
                else
                {
                    owner.wallSensors[0] = 0;
                }


                //right 45 degrees
                if (Intersects(wallRight45, sightRangeRight, wallLeft, wallRight, out distance))
                {
                    owner.wallSensors[2] = Vector2.Distance(main, distanceRight);
                }
                else
                {
                    owner.wallSensors[2] = 0;
                }

                owner.wallList = wallsInFront;

            }
        }

        // a1 is line1 start, a2 is line1 end, b1 is line2 start, b2 is line2 end
        static bool Intersects(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2, out Vector2 intersection)
        {
            intersection = Vector2.Zero;

            Vector2 b = a2 - a1;
            Vector2 d = b2 - b1;
            float bDotDPerp = b.X * d.Y - b.Y * d.X;

            // if b dot d == 0, it means the lines are parallel so have infinite intersection points
            if (bDotDPerp == 0)
                return false;

            Vector2 c = b1 - a1;
            float t = (c.X * d.Y - c.Y * d.X) / bDotDPerp;
            if (t < 0 || t > 1)
                return false;

            float u = (c.X * b.Y - c.Y * b.X) / bDotDPerp;
            if (u < 0 || u > 1)
                return false;

            intersection = a1 + t * b;

            return true;
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
                        npcAngle = (360 - npcAngle + owner.faceDirection) % 360;

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
            if (ray == 0)
            {
                for (int k = 0; k < owner.sensor.getSightRange(); k++)
                {

                }
            }

        }

        public float detectWalls()
        {

            return -1;
        }

        public void setWallDefaults(Player owner, Wall wall)
        {
            if (owner.faceDirection <= 235 && owner.faceDirection >= 135)
            {
                main = new Vector2(owner.rectangle.Center.X, owner.rectangle.Bottom); // reference point 1
                wallLeft = new Vector2(wall.rectangle.Left, wall.rectangle.Top);
                wallRight = new Vector2(wall.rectangle.Right, wall.rectangle.Top);
                wallCenter = new Vector2(main.X, wall.rectangle.Top);
                dTmp = Vector2.Distance(main, wallCenter);
                wallLeft45 = new Vector2((float)(main.X - dTmp * Math.Tan(45)), main.Y);
                wallRight45 = new Vector2((float)(main.X + dTmp * Math.Tan(45)), main.Y);
                sightRangeCenter = new Vector2(main.X, (main.Y + sightRange));
                sightRangeLeft = new Vector2(wallLeft45.X, sightRangeCenter.Y);
                sightRangeRight = new Vector2(wallRight45.X, sightRangeCenter.Y);
                distanceLeft = new Vector2(wallLeft45.X, wall.rectangle.Top);
                distanceRight = new Vector2(wallRight45.X, wall.rectangle.Top);
            }
            else if (owner.faceDirection <= 270 && owner.faceDirection <= 45)
            {
                main = new Vector2(owner.rectangle.Center.X, owner.rectangle.Top); // reference point 1
                wallLeft = new Vector2(wall.rectangle.Left, wall.rectangle.Bottom);
                wallRight = new Vector2(wall.rectangle.Right, wall.rectangle.Bottom);
                wallCenter = new Vector2(main.X, wall.rectangle.Bottom);
                dTmp = Vector2.Distance(main, wallCenter);
                wallLeft45 = new Vector2((float)(main.X - dTmp * Math.Tan(45)), main.Y);
                wallRight45 = new Vector2((float)(main.X + dTmp * Math.Tan(45)), main.Y);
                sightRangeCenter = new Vector2(main.X, (main.Y - sightRange));
                sightRangeLeft = new Vector2(wallLeft45.X, sightRangeCenter.Y);
                sightRangeRight = new Vector2(wallRight45.X, sightRangeCenter.Y);
                distanceLeft = new Vector2(wallLeft45.X, wall.rectangle.Bottom);
                distanceRight = new Vector2(wallRight45.X, wall.rectangle.Bottom);
            }
            else if (owner.faceDirection <= 315 && owner.faceDirection >= 225)
            {
                main = new Vector2(owner.rectangle.Left, owner.rectangle.Center.Y); // reference point 1
                wallLeft = new Vector2(wall.rectangle.Right, wall.rectangle.Top);
                wallRight = new Vector2(wall.rectangle.Right, wall.rectangle.Bottom);
                sightRangeCenter = new Vector2(main.X - sightRange, main.Y);
                wallCenter = new Vector2(wall.rectangle.Right, main.Y);
                dTmp = Vector2.Distance(main, wallCenter);
                wallLeft45 = new Vector2(main.X, (float)(main.Y - dTmp * Math.Tan(45)));
                wallRight45 = new Vector2(main.X, (float)(main.Y + dTmp * Math.Tan(45)));
                sightRangeCenter = new Vector2(main.X - sightRange, main.Y);
                sightRangeLeft = new Vector2(sightRangeCenter.X, wallLeft45.Y);
                sightRangeRight = new Vector2(sightRangeCenter.X, wallRight45.Y);
                distanceLeft = new Vector2(wall.rectangle.Right, wallLeft45.Y);
                distanceRight = new Vector2(wall.rectangle.Right, wallRight45.Y);
            }
            else if (owner.faceDirection >= 45 && owner.faceDirection <= 135)
            {
                main = new Vector2(owner.rectangle.Right, owner.rectangle.Center.Y); // reference point 1
                wallLeft = new Vector2(wall.rectangle.Left, wall.rectangle.Top);
                wallRight = new Vector2(wall.rectangle.Left, wall.rectangle.Bottom);
                sightRangeCenter = new Vector2(main.X + sightRange, main.Y);
                wallCenter = new Vector2(wall.rectangle.Left, main.Y);
                dTmp = Vector2.Distance(main, wallCenter);
                wallLeft45 = new Vector2(main.X, (float)(main.Y - dTmp * Math.Tan(45)));
                wallRight45 = new Vector2(main.X, (float)(main.Y + dTmp * Math.Tan(45)));
                sightRangeCenter = new Vector2(main.X + sightRange, main.Y);
                sightRangeLeft = new Vector2(sightRangeCenter.X, wallLeft45.Y);
                sightRangeRight = new Vector2(sightRangeCenter.X, wallRight45.Y);
                distanceLeft = new Vector2(wall.rectangle.Left, wallLeft45.Y);
                distanceRight = new Vector2(wall.rectangle.Left, wallRight45.Y);

            }
        }
    }
}
