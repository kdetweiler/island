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

        Vector2 position = new Vector2(50, 475);
        Vector2 velocity;

        Boolean lastDirectionUp = false;
        Boolean lastDirectionDown = false;
        Boolean lastDirectionHorizontalLeft = false;
        Boolean lastDirectionHorizontalRight = false;

        public Player()
        {

        }

        public Player(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
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
        }

        public override void Update()
        {
            position += velocity;
            // TODO: Add your update  
            //GamePad
            GamePadState gamepadstatus = GamePad.GetState(PlayerIndex.One);
            position.Y += (int)((gamepadstatus.ThumbSticks.Left.Y * 3) * -2);
            position.X += (int)((gamepadstatus.ThumbSticks.Left.X * 3) * -2);

            //KeyBoard
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Up) || (keyboard.IsKeyDown(Keys.W)))
                velocity.Y = -1f;
            else if (keyboard.IsKeyDown(Keys.Down) || (keyboard.IsKeyDown(Keys.S)))
                velocity.Y = +1f;
            else
                velocity.Y = 0f;

            if (keyboard.IsKeyDown(Keys.Left) || (keyboard.IsKeyDown(Keys.A)))
                velocity.X = -1f;
            else if (keyboard.IsKeyDown(Keys.Right) || (keyboard.IsKeyDown(Keys.D)))
                velocity.X = 1f;
            else
                velocity.X = 0f;
            if (lastDirectionDown == false && lastDirectionUp == false && lastDirectionHorizontalLeft == false && lastDirectionHorizontalRight == false)
            {
                animationPlayer.PlayAnimation(idleVerticalDownAnimation);
            }

            if (velocity.X < 0 && velocity.Y == 0)
            {
                animationPlayer.PlayAnimation(walkHorizontalLeftAnimation);
                lastDirectionHorizontalLeft = true;
                lastDirectionHorizontalRight = false;
                lastDirectionDown = false;
                lastDirectionUp = false;
            }
            else if (velocity.X > 0 && velocity.Y == 0)
            {
                animationPlayer.PlayAnimation(walkHorizontalRightAnimation);
                lastDirectionHorizontalRight = true;
                lastDirectionHorizontalLeft = false;
                lastDirectionDown = false;
                lastDirectionUp = false;
            }
            else if (velocity.X == 0 && lastDirectionHorizontalLeft == true)
                animationPlayer.PlayAnimation(idleHorizontalLeftAnimation);
            else if (velocity.X == 0 && lastDirectionHorizontalRight == true)
                animationPlayer.PlayAnimation(idleHorizontalRightAnimation);

            if (velocity.Y > 0 && velocity.X == 0)
            {
                animationPlayer.PlayAnimation(walkVerticalDownAnimation);
                lastDirectionDown = true;
                lastDirectionHorizontalLeft = false;
                lastDirectionHorizontalRight = false;
                lastDirectionUp = false;
            }
            else if (velocity.Y < 0 && velocity.X == 0)
            {
                animationPlayer.PlayAnimation(walkVerticalUpAnimation);
                lastDirectionUp = true;
                lastDirectionHorizontalLeft = false;
                lastDirectionHorizontalRight = false;
                lastDirectionDown = false;
            }
            else if (velocity.Y == 0 && velocity.X == 0 && lastDirectionDown == true)
            {
                animationPlayer.PlayAnimation(idleVerticalDownAnimation);
            }
            else if (velocity.Y == 0 && velocity.X == 0 && lastDirectionUp == true)
            {
                animationPlayer.PlayAnimation(idleVerticalUpAnimation);
            }
        }

        public void PutInStartPosition()
        {
            position.X = 40;
            position.Y = 40;
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
    }
}
