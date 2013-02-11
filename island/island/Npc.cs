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
        Animation npc1Idle;

        public Npc()
        {

        }

        public Npc(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;

            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Load(ContentManager Content)
        {
            npc1Idle = new Animation(Content.Load<Texture2D>("npc1Idle"), 32, 0.1f, true);

            //animationPlayer.PlayAnimation(idleVerticalDownAnimation);
        }
    }
}
