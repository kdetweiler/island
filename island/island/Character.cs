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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Character : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected Texture2D texture;
        protected Rectangle spriteRectangle;
        protected Vector2 position;

        //Width and Heigh of sprite in texture
        protected const int charWidth = 21;
        protected const int charHeight = 37;

        //Screen Area
        protected Rectangle screenBounds;

        public Character(Game game, ref Texture2D theTexture)
            : base(game)
        {
            // TODO: Construct any child components here
            texture = theTexture;
            position = new Vector2();

            //Create Source rectangle
            spriteRectangle = new Rectangle(0, 0, charWidth, charHeight);
            screenBounds = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);

        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            //GamePad
            GamePadState gamepadstatus = GamePad.GetState(PlayerIndex.One);
            position.Y += (int)((gamepadstatus.ThumbSticks.Left.Y * 3) * -2);
            position.X += (int)((gamepadstatus.ThumbSticks.Left.X * 3) * -2);

            //KeyBoard
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Up) || (keyboard.IsKeyDown(Keys.W)))
            {
                position.Y -= 3;
            }
            if (keyboard.IsKeyDown(Keys.Down) || (keyboard.IsKeyDown(Keys.S)))
            {
                position.Y += 3;
            }
            if (keyboard.IsKeyDown(Keys.Left) || (keyboard.IsKeyDown(Keys.A)))
            {
                position.X -= 3;
            }
            if (keyboard.IsKeyDown(Keys.Right) || (keyboard.IsKeyDown(Keys.D)))
            {
                position.X += 3;
            }

            //Keep player on screen, boundries
            if (position.X < screenBounds.Left)
            {
                position.X = screenBounds.Left;
            }
            if (position.X > screenBounds.Width - charWidth)
            {
                position.X = screenBounds.Width - charWidth;
            }
            if (position.Y < screenBounds.Top)
            {
                position.Y = screenBounds.Top;
            }
            if (position.Y > screenBounds.Height - charHeight)
            {
                position.Y = screenBounds.Top - charHeight;
            }

            base.Update(gameTime);
        }

        public void PutInStartPosition()
        {
            position.X = 40;
            position.Y = 40;
        }

        public override void Draw(GameTime gameTime)
        {
            //Get the current spriteBatch
            SpriteBatch sBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

            // Draw the character
            sBatch.Draw(texture, position, spriteRectangle, Color.White);
            base.Draw(gameTime);
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, charWidth, charHeight);
        }
    }
}
