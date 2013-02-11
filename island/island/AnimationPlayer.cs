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
    struct AnimationPlayer
    {
        private float timer;
        
        Animation animation;

        int frameIndex; 

        public Animation Animation
        {
            get { return animation; }
        }
        
        public int FrameIndex
        {
            get { return frameIndex; }
            set { frameIndex = value;}
        }

        public Vector2 Origin
        {
            get { return new Vector2(animation.frameWidth / 2, animation.FrameHeight); } 
        }

        public void PlayAnimation(Animation newAnimation)
        {
            if (animation == newAnimation)
                return;

            animation = newAnimation;
            frameIndex = 0;
            timer = 0;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            if (Animation == null)
                throw new NotSupportedException("No animation selected");

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (timer >= animation.FrameTime)
            {
                timer -= animation.FrameTime;

                if (animation.IsLooping)
                    frameIndex = (frameIndex + 1) % animation.frameCount;
                else
                    frameIndex = Math.Min(frameIndex + 1, animation.frameCount - 1);
            }

            Rectangle rectangle = new Rectangle(frameIndex * Animation.frameWidth, 0, Animation.frameWidth, Animation.FrameHeight);

            spriteBatch.Draw(Animation.Texture, position, rectangle, Color.White, 0f, Origin, 1f, spriteEffects, 0f);
        }
    }
}
