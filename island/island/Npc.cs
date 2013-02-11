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
    class Npc : Entity
    {
        AnimationPlayer animationPlayer;
        Animation npcIdle;

        public Npc()
        {

        }

        public Npc(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;

            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Load(ContentManager Content, Texture2D newTexture)
        {
            npcIdle = new Animation(newTexture, 32, 0.1f, true);

            animationPlayer.PlayAnimation(npcIdle);
        }

        public override void Update(GameTime gameTime)
        {
  

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
