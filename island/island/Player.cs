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

        public List<NPC> proxList = new List<NPC>();
        public int[] quadrants = new int[4];
        public float[] wallSensors = new float[3];

        public Vector2 velocity;
        public Rectangle rectangle;
        
        public int faceDirection;

        public Sensor sensor;

        float lastTime = 0.0f;
                
        public Player()
        {

        }

        
        public Player(Vector2 newPosition, String newName)
        {
            //texture = newTexture;
            position = newPosition;
            sensor=new Sensor(500,3);
            //rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            rectangle = new Rectangle((int)position.X, (int)position.Y, 34, 57);
            name = newName;
        }

        public void Load(ContentManager Content)
        {
            walkHorizontalRightAnimation = new Animation(Content.Load<Texture2D>("walkHorizontalRight"), 32, 0.1f, true);
            walkHorizontalLeftAnimation = new Animation(Content.Load<Texture2D>("walkHorizontalLeft"), 32, 0.1f, true);
            walkVerticalDownAnimation = new Animation(Content.Load<Texture2D>("walkVerticalDown"), 34, 0.1f, true);
            walkVerticalUpAnimation = new Animation(Content.Load<Texture2D>("walkVerticalUp"), 34, 0.1f, true);

            idleHorizontalRightAnimation = new Animation(Content.Load<Texture2D>("idleHorizontalRight"), 31, 0.3f, false);
            idleHorizontalLeftAnimation = new Animation(Content.Load<Texture2D>("idleHorizontalLeft"), 31, 0.3f, false);
            idleVerticalDownAnimation = new Animation(Content.Load<Texture2D>("idleVerticalDown"), 34, .3f, false);
            idleVerticalUpAnimation = new Animation(Content.Load<Texture2D>("idleVerticalUp"), 34, .3f, false);

           // animationPlayer.PlayAnimation(idleVerticalDownAnimation);
        }

        public override void Update(GameTime gameTime)
        {
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
            else if (velocity.X == 0 && faceDirection==270)//stand still facing left
                animationPlayer.PlayAnimation(idleHorizontalLeftAnimation);
            else if (velocity.X == 0 && faceDirection==90)//stand still facing right
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
            else if (velocity.Y == 0 && velocity.X == 0 && faceDirection==180)//stand still facing down
            {
                animationPlayer.PlayAnimation(idleVerticalDownAnimation);
            }
            else if (velocity.Y == 0 && velocity.X == 0 && faceDirection==0)//stand still facing up
            {
                animationPlayer.PlayAnimation(idleVerticalUpAnimation);
            }
            
        }

        public void rotateCounterClockWise()
        {
            if (faceDirection==0)
            {
                animationPlayer.PlayAnimation(idleHorizontalLeftAnimation);
                faceDirection = 270;
            }
            else if (faceDirection==270)
            {
                animationPlayer.PlayAnimation(idleVerticalDownAnimation);
                faceDirection = 180;
            }
            else if (faceDirection==180)
            {
                animationPlayer.PlayAnimation(idleHorizontalRightAnimation);
                faceDirection = 90;
            }
            else if (faceDirection==90)
            {
                animationPlayer.PlayAnimation(idleVerticalUpAnimation);
                faceDirection = 0;
            }
        }

        public void rotateClockWise()
        {
            if (faceDirection==0)
            {
                animationPlayer.PlayAnimation(idleHorizontalRightAnimation);
                faceDirection = 90;
            }
            else if (faceDirection==270)
            {
                animationPlayer.PlayAnimation(idleVerticalUpAnimation);
                faceDirection = 0;
            }
            else if (faceDirection==180)
            {
                animationPlayer.PlayAnimation(idleHorizontalLeftAnimation);
                faceDirection = 270;
            }
            else if (faceDirection==90)
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
            String togo = "Player Center: " + this.rectangle.Center
                + "\nProx List: ";
            foreach (NPC npc in proxList) {
                togo += "(" + npc.name + ": " + npc.rectangle.Center.X + ", " + npc.rectangle.Center.Y + ")";
            }
            for (int i = 0; i < 4; i++)
            {
                togo += "\nQuadrant " + (i + 1) + ": " + this.quadrants[i];
            }
            
            return togo;
        }
    }
}
