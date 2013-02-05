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
    class Entity
    {
        public Texture2D texture;
        public Rectangle rectangle;

        protected Vector2 position;

        public virtual void Update()
        {

        }

        public void Draw(SpriteBatch sBatch)
        {
            // Draw the character
            sBatch.Draw(texture, position, rectangle, Color.White);
            //base.Draw(gameTime);
        }
    }
}
