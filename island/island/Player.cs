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

namespace island
{
    class Player : Character
    {
        AnimationPlayer animationPlayer;
        Animation walkHorizontalRightAnimation;
        Animation walkHorizontalLeftAnimation;
        Animation walkVerticalDownAnimation;
        Animation walkVerticalUpAnimation;

        Animation idleHorizontalRightAnimation;
        Animation idleHorizontalLeftAnimation;
        Animation idleVerticalDownAnimation;
        Animation idleVerticalUpAnimation;

        Animation combatDown;
        Animation combatUp;
        Animation combatLeft;
        Animation combatRight;

        public List<NPC> proxList = new List<NPC>();
        public List<Wall> wallList = new List<Wall>();
        public List<NPC> npcList = new List<NPC>();
        public int[] quadrants = new int[4];
        public float[] wallSensors = new float[3];
        public float[] npcSensors = new float[3];

        public Vector2 velocity;
        public Rectangle rectangle;

        public int faceDirection = 180;

        public Sensor sensor;

        public Weapon rightHand;
        public Weapon leftHand;

        float lastTime = 0.0f;
        float animationTime = 0.0f;
        float lastAttackTime = 0.0f;

        bool attackCoolDown = false;

        public Player()
        {

        }


        public Player(Vector2 newPosition, String newName)
        {
            //texture = newTexture;
            position = newPosition;
            health = 100;
            sensor = new Sensor(100, 3);
            //rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            rectangle = new Rectangle((int)position.X, (int)position.Y, 50, 50);
            name = newName;
            rightHand = new Weapon("Sword", 20, new int[3] { 2, 3, 4 });
            leftHand = new Weapon("Shield", 0, new int[0] { });
        }

        public void Load(ContentManager Content)
        {
            walkHorizontalRightAnimation = new Animation(Content.Load<Texture2D>("walkHorizontalRight"), 50, 0.1f, true);
            walkHorizontalLeftAnimation = new Animation(Content.Load<Texture2D>("walkHorizontalLeft"), 50, 0.1f, true);
            walkVerticalDownAnimation = new Animation(Content.Load<Texture2D>("walkVerticalDown"), 50, 0.1f, true);
            walkVerticalUpAnimation = new Animation(Content.Load<Texture2D>("walkVerticalUp"), 50, 0.1f, true);

            idleHorizontalRightAnimation = new Animation(Content.Load<Texture2D>("idleHorizontalRight"), 50, 0.3f, false);
            idleHorizontalLeftAnimation = new Animation(Content.Load<Texture2D>("idleHorizontalLeft"), 50, 0.3f, false);
            idleVerticalDownAnimation = new Animation(Content.Load<Texture2D>("idleVerticalDown"), 50, .3f, false);
            idleVerticalUpAnimation = new Animation(Content.Load<Texture2D>("idleVerticalUp"), 50, .3f, false);

            combatUp = new Animation(Content.Load<Texture2D>("CombatUp"), 50, .1f, false);
            combatDown = new Animation(Content.Load<Texture2D>("CombatDown"), 50, .1f, false);
            combatLeft = new Animation(Content.Load<Texture2D>("CombatLeft"), 50, .1f, false);
            combatRight = new Animation(Content.Load<Texture2D>("CombatRight"), 50, .1f, false);

            // animationPlayer.PlayAnimation(idleVerticalDownAnimation);
        }

        public override void Update(GameTime gameTime)
        {
            animationTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (animationTime >= 1000.0f)
            {
                attackCoolDown = false;
                animationTime = 0.0f;
            }
            position += velocity;
            this.rectangle.X = (int)this.position.X;
            this.rectangle.Y = (int)this.position.Y;
            // TODO: Add your update  
            //GamePad
            GamePadState gamepadstatus = GamePad.GetState(PlayerIndex.One);
            position.Y += (int)((gamepadstatus.ThumbSticks.Left.Y * 3) * -2);
            position.X += (int)((gamepadstatus.ThumbSticks.Left.X * 3) * -2);

            //KeyBoard
            KeyboardState keyboard = Keyboard.GetState();

            //change the vertical velocity of the player by pressing keys W or S, or Up or Down
            if ((keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.W)) && keyboard.IsKeyUp(Keys.LeftShift))
                velocity.Y = -1f;
            else if (keyboard.IsKeyDown(Keys.Down) || (keyboard.IsKeyDown(Keys.S)))
                velocity.Y = +1f;
            else
                velocity.Y = 0f;


            //change the horizontal velocity of the player by pressing A or D, or left or right
            if (keyboard.IsKeyDown(Keys.Left) || (keyboard.IsKeyDown(Keys.A)))
                velocity.X = -1f;
            else if (keyboard.IsKeyDown(Keys.Right) || (keyboard.IsKeyDown(Keys.D)))
                velocity.X = 1f;
            else
                velocity.X = 0f;



            //rotate the sprite counter clockwise using the Q button
            if (keyboard.IsKeyDown(Keys.Q))
            {
                //GameTime gameTime = new GameTime();
                lastTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (lastTime > 125.0f)
                {
                    rotateCounterClockWise();
                    lastTime = 0;
                }
            }

            //rotate the sprite clockwise by pressing E
            if (keyboard.IsKeyDown(Keys.E))
            {
                lastTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (lastTime > 125.0f)
                {
                    rotateClockWise();
                    lastTime = 0;
                }
            }


            if (attackCoolDown == false)
            {
                //check sprite velocity and run horizontal walk animations
                //direction flags are tripped to remember which way sprite is facing
                if (velocity.X < 0 && velocity.Y == 0)//walk left
                {
                    animationPlayer.PlayAnimation(walkHorizontalLeftAnimation);
                    faceDirection = 270;
                }
                else if (velocity.X > 0 && velocity.Y == 0)//walk right
                {
                    animationPlayer.PlayAnimation(walkHorizontalRightAnimation);
                    faceDirection = 90;
                }
                else if (velocity.X == 0 && faceDirection == 270)//stand still facing left
                    animationPlayer.PlayAnimation(idleHorizontalLeftAnimation);
                else if (velocity.X == 0 && faceDirection == 90)//stand still facing right
                    animationPlayer.PlayAnimation(idleHorizontalRightAnimation);

                //check sprite velocity and run vertical walk animations
                //direction flags are tripped to remember which way sprite is facing
                if (velocity.Y > 0 && velocity.X == 0)//walk down
                {
                    animationPlayer.PlayAnimation(walkVerticalDownAnimation);
                    faceDirection = 180;
                }
                else if (velocity.Y < 0 && velocity.X == 0)//walk up
                {
                    animationPlayer.PlayAnimation(walkVerticalUpAnimation);
                    faceDirection = 0;
                }
                else if (velocity.Y == 0 && velocity.X == 0 && faceDirection == 180)//stand still facing down
                {
                    animationPlayer.PlayAnimation(idleVerticalDownAnimation);
                }
                else if (velocity.Y == 0 && velocity.X == 0 && faceDirection == 0)//stand still facing up
                {
                    animationPlayer.PlayAnimation(idleVerticalUpAnimation);
                }
            }

            if (keyboard.IsKeyDown(Keys.Space))
            {
                lastAttackTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (lastAttackTime > 125.0f)
                {
                    //check direction
                    if (faceDirection == 0)
                        animationPlayer.PlayAnimation(combatUp);
                    else if (faceDirection == 90)
                        animationPlayer.PlayAnimation(combatRight);
                    else if (faceDirection == 180)
                        animationPlayer.PlayAnimation(combatDown);
                    else if (faceDirection == 270)
                        animationPlayer.PlayAnimation(combatLeft);
                    uponAttack(ListHolder.Instance.NPCList);

                    lastAttackTime = 0;
                    attackCoolDown = true;
                }
            }
        }

        public Point getClosestNode(int level) 
        {

            return new Point(0, 0);
        }

        public void rotateCounterClockWise()
        {
            if (faceDirection == 0)
            {
                animationPlayer.PlayAnimation(idleHorizontalLeftAnimation);
                faceDirection = 270;
            }
            else if (faceDirection == 270)
            {
                animationPlayer.PlayAnimation(idleVerticalDownAnimation);
                faceDirection = 180;
            }
            else if (faceDirection == 180)
            {
                animationPlayer.PlayAnimation(idleHorizontalRightAnimation);
                faceDirection = 90;
            }
            else if (faceDirection == 90)
            {
                animationPlayer.PlayAnimation(idleVerticalUpAnimation);
                faceDirection = 0;
            }
        }

        public void rotateClockWise()
        {
            if (faceDirection == 0)
            {
                animationPlayer.PlayAnimation(idleHorizontalRightAnimation);
                faceDirection = 90;
            }
            else if (faceDirection == 270)
            {
                animationPlayer.PlayAnimation(idleVerticalUpAnimation);
                faceDirection = 0;
            }
            else if (faceDirection == 180)
            {
                animationPlayer.PlayAnimation(idleHorizontalLeftAnimation);
                faceDirection = 270;
            }
            else if (faceDirection == 90)
            {
                animationPlayer.PlayAnimation(idleVerticalDownAnimation);
                faceDirection = 180;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public void DrawAnimation(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects flip = SpriteEffects.None;

            animationPlayer.Draw(gameTime, spriteBatch, position, flip);
        }


        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, rectangle.Height, rectangle.Width);
        }

        public String toString()
        {
            String togo = "Player Center: " + this.rectangle.Center + " Direction: " + this.faceDirection
                + "\nProx List: ";
            /*
            foreach (NPC npc in proxList)
            {
                togo += "(" + npc.name + ": " + npc.rectangle.Center.X + ", " + npc.rectangle.Center.Y + ") " + this.sensor.npcAngle;
            }

            for (int i = 0; i < 4; i++)
            {
                togo += "\nQuadrant " + (i + 1) + ": " + this.quadrants[i];
            }
            
            foreach (Wall wall in wallList)
            {
                togo += "\n" + wall.name + ": " + this.sensor.wallAngle;
            }

            for (int i = 0; i < 3; i++)
            {
                togo += "\nWall " + (i + 1) + ": (" + this.wallSensors[i] + ") " + this.sensor.wallAngle;
            }*/
            foreach (NPC npc in npcList)
                togo += "\n" + npc.name + ": " + this.sensor.npcAngle;

            for (int i = 0; i < 3; i++)
            {
                togo += "\nNPC " + (i + 1) + ": (" + this.npcSensors[i] + ") " + this.sensor.npcAngle;
            }

            return togo;
        }

        public void uponAttack(List<NPC> entities) 
        {
            List<int> targets = attackList(entities);
            CombatController.Instance.attack(this, entities);
        }



        public List<int> attackList(List<NPC> entities)
        {
            List<int> targets=new List<int>();
            for (int k = 0; k < entities.Count; k++)
            {
                //check to see if entities[k] is hittable; if so return it
                if(sensor.WeaponSensor(this,entities[k],rightHand.range)) 
                {
                    targets.Add(k);
                }
            }
            return targets;
        }

        public bool wallCollision(Player player, List<Wall> walls)
        {
            foreach (Wall w in walls)
            {
                if (player.rectangle.Intersects(w.rectangle))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
