using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace island
{
    class Wall : Obj
    {
        public Wall(Vector2 newPosition)
            : base(newPosition)
        {
            //texture = newTexture;
            //position = newPosition;
            //name = "Wall";

            //rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            //exists = true;
            //isPassable = false;

            position = newPosition;
            spriteName = "tree";
        }
    }
}
