using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace island
{
    public class Player : Character
    {

        public Player(Game1 game)
            : base(Character)
        {

        }


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
       
    }
}
