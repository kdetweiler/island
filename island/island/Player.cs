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
        Animation walkAnimation;
        Animation idleAnimation;

        Vector2 position = new Vector2(50, 475);
        Vector2 velocity;

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
            walkAnimation = new Animation(Content.Load<Texture2D>("walkHorizontal"), 23, 0.1f, true);
            idleAnimation = new Animation(Content.Load<Texture2D>("idleHorizontal"), 23, 0.3f, true);
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

            if (velocity.X != 0)
                animationPlayer.PlayAnimation(walkAnimation);
            else if (velocity.X == 0)
                animationPlayer.PlayAnimation(idleAnimation);

        }

        public void PutInStartPosition()
        {
            position.X = 40;
            position.Y = 40;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects flip = SpriteEffects.None;

            if (velocity.X >= 0)
                flip = SpriteEffects.None;
            else if (velocity.X < 0)
                flip = SpriteEffects.FlipHorizontally;

            animationPlayer.Draw(gameTime, spriteBatch, position, flip);
        }
    }
}
