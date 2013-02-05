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
    class Animation
    {
        public int FrameWidth;
        public int FrameCount;

        Texture2D texture;

        bool isLooping;

        float frameTime;

        public Animation(Texture2D newTexture, int newFrameWidth, float newFrameTime, bool newIsLooping)
        {
            texture = newTexture;
            FrameWidth = newFrameWidth;
            frameTime = newFrameTime;
            isLooping = newIsLooping;
            FrameCount = texture.Width / FrameWidth;
        }

        public Texture2D Texture
        {
            get { return texture; }
        }

        public int FrameHeight
        {
            get { return texture.Height; }
        }

        public float FrameTime
        {
            get { return frameTime; }
        }

        public bool IsLooping
        {
            get { return isLooping; }
        }
    }
}
