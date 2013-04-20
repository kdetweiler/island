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
    class Obj
    {
        public Vector2 position;
        public float rotation = 0.0f;
        public Texture2D spriteIndex;
        public string spriteName = "";
        public float speed = 0.0f;
        public float scale = 1.0f;
        public bool alive = true;

        public Obj(Vector2 pos)
        {
            position = pos;
        }

        public Obj()
        {

        }

        public virtual void Update()
        {
            if (!alive) return;
            pushTo(speed, rotation);
        }

        public virtual void LoadContent(ContentManager content)
        {
            spriteIndex = content.Load<Texture2D>("sprites\\" + spriteName);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!alive) return;

            Rectangle size;
            Vector2 center = new Vector2(spriteIndex.Width/2, spriteIndex.Height/2);

            spriteBatch.Draw(spriteIndex, position, null, Color.White, MathHelper.ToRadians(rotation), center, scale, SpriteEffects.None, 0);
        }

        public void pushTo(float plx, float dir)
        {
            float newX = (float)Math.Cos(MathHelper.ToRadians(dir));
            float newY = (float)Math.Sin(MathHelper.ToRadians(dir));
            position.X += plx * (float)newX;
            position.Y += plx * (float)newY;
        }
    }
}
