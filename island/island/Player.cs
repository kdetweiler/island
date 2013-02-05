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
        protected Rectangle screenBounds;
        protected Rectangle spriteRectangle;

        //Width and Heigh of sprite in texture
        protected const int CHARWIDTH = 21;
        protected const int CHARHEIGHT = 37;

        public Player(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
        }

        public override void Update()
        {
            // TODO: Add your update  
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
        }

        public void PutInStartPosition()
        {
            position.X = 40;
            position.Y = 40;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, CHARWIDTH, CHARHEIGHT);
        }
    }
}
