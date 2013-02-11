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
    class Player : Entity
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

        public Vector2 velocity;
        public Rectangle rectangle;

        Boolean directionUp = false;
        Boolean directionDown = false;
        Boolean directionLeft = false;
        Boolean directionRight = false;

        Sensor sensor;

        float lastTime = 0.0f;
                
        public Player()
        {

        }

        public Player(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            sensor=new Sensor(500,3);
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Load(ContentManager Content)
        {
            walkHorizontalRightAnimation = new Animation(Content.Load<Texture2D>("walkHorizontalRight"), 32, 0.1f, true);
            walkHorizontalLeftAnimation = new Animation(Content.Load<Texture2D>("walkHorizontalLeft"), 32, 0.1f, true);
            walkVerticalDownAnimation = new Animation(Content.Load<Texture2D>("walkVerticalDown"), 34, 0.1f, true);
            walkVerticalUpAnimation = new Animation(Content.Load<Texture2D>("walkVerticalUp"), 34, 0.1f, true);

            idleHorizontalRightAnimation = new Animation(Content.Load<Texture2D>("idleHorizontalRight"), 31, 0.3f, true);
            idleHorizontalLeftAnimation = new Animation(Content.Load<Texture2D>("idleHorizontalLeft"), 31, 0.3f, true);
            idleVerticalDownAnimation = new Animation(Content.Load<Texture2D>("idleVerticalDown"), 34, .3f, true);
            idleVerticalUpAnimation = new Animation(Content.Load<Texture2D>("idleVerticalUp"), 34, .3f, true);
            animationPlayer.PlayAnimation(idleVerticalDownAnimation);
        }

        public override void Update(GameTime gameTime)
        {
            position += velocity;
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
                directionLeft = true;
                directionRight = false;
                directionDown = false;
                directionUp = false;
            }
            else if (velocity.X > 0 && velocity.Y == 0)//walk right
            {
                animationPlayer.PlayAnimation(walkHorizontalRightAnimation);
                directionRight = true;
                directionLeft = false;
                directionDown = false;
                directionUp = false;
            }
            else if (velocity.X == 0 && directionLeft == true)//stand still facing left
                animationPlayer.PlayAnimation(idleHorizontalLeftAnimation);
            else if (velocity.X == 0 && directionRight == true)//stand still facing right
                animationPlayer.PlayAnimation(idleHorizontalRightAnimation);

            //check sprite velocity and run vertical walk animations
            //direction flags are tripped to remember which way sprite is facing
            if (velocity.Y > 0 && velocity.X == 0)//walk down
            {
                animationPlayer.PlayAnimation(walkVerticalDownAnimation);
                directionDown = true;
                directionLeft = false;
                directionRight = false;
                directionUp = false;
            }
            else if (velocity.Y < 0 && velocity.X == 0)//walk up
            {
                animationPlayer.PlayAnimation(walkVerticalUpAnimation);
                directionUp = true;
                directionLeft = false;
                directionRight = false;
                directionDown = false;
            }
            else if (velocity.Y == 0 && velocity.X == 0 && directionDown == true)//stand still facing down
            {
                animationPlayer.PlayAnimation(idleVerticalDownAnimation);
            }
            else if (velocity.Y == 0 && velocity.X == 0 && directionUp == true)//stand still facing up
            {
                animationPlayer.PlayAnimation(idleVerticalUpAnimation);
            }
            
        }

        public void rotateCounterClockWise()
        {
            if (directionUp == true)
            {
                animationPlayer.PlayAnimation(idleHorizontalLeftAnimation);
                directionUp = false;
                directionLeft = true;
            }
            else if (directionLeft == true)
            {
                animationPlayer.PlayAnimation(idleVerticalDownAnimation);
                directionLeft = false;
                directionDown = true;
            }
            else if (directionDown == true)
            {
                animationPlayer.PlayAnimation(idleHorizontalRightAnimation);
                directionDown = false;
                directionRight = true;
            }
            else if (directionRight == true)
            {
                animationPlayer.PlayAnimation(idleVerticalUpAnimation);
                directionRight = false;
                directionUp = true;
            }
        }

        public void rotateClockWise()
        {
            if (directionUp == true)
            {
                animationPlayer.PlayAnimation(idleHorizontalRightAnimation);
                directionUp = false;
                directionRight = true;
            }
            else if (directionLeft == true)
            {
                animationPlayer.PlayAnimation(idleVerticalUpAnimation);
                directionLeft = false;
                directionUp = true;
            }
            else if (directionDown == true)
            {
                animationPlayer.PlayAnimation(idleHorizontalLeftAnimation);
                directionDown = false;
                directionLeft = true;
            }
            else if (directionRight == true)
            {
                animationPlayer.PlayAnimation(idleVerticalDownAnimation);
                directionRight = false;
                directionDown = true;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects flip = SpriteEffects.None;

            /*
            if (velocity.X >= 0)
                flip = SpriteEffects.None;
            else if (velocity.X < 0)
                flip = SpriteEffects.FlipHorizontally;
            */

            animationPlayer.Draw(gameTime, spriteBatch, position, flip);
        }


        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, rectangle.Height, rectangle.Width);
        }

        public String toString()
        {
            return "Bottom: " + this.rectangle.Bottom + "\nRight: " + this.rectangle.Right;
        }
    }
}
