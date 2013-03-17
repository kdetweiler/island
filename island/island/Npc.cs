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
    class NPC : Character
    {
        AnimationPlayer animationPlayer;
        Animation npcIdle;
        Animation walkHorizontalRightAnimation;
        Animation walkHorizontalLeftAnimation;
        Animation walkVerticalDownAnimation;
        Animation walkVerticalUpAnimation;

        Animation idleHorizontalRightAnimation;
        Animation idleHorizontalLeftAnimation;
        Animation idleVerticalDownAnimation;
        Animation idleVerticalUpAnimation;

        public List<Character> proxList = new List<Character>();
        public List<Wall> wallList = new List<Wall>();
        public int[] quadrants = new int[4];
        public float[] wallSensors = new float[3];

        public Vector2 velocity;
        public Rectangle rectangle;

        public int faceDirection;

        public Sensor sensor;

        float lastTime = 0.0f;
        
        public NPC()
        {

        }

        public NPC(Texture2D newTexture, Vector2 newPosition, String newName)
        {
            texture = newTexture;
            position = newPosition;
            name = newName;
            sensor = new Sensor(100, 3);
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Load(ContentManager Content, Texture2D newTexture)
        {
            npcIdle = new Animation(newTexture, 32, 0.1f, true);

            animationPlayer.PlayAnimation(npcIdle);
        }

        public override void Update(GameTime gameTime)
        {
            position += velocity;
            this.rectangle.X = (int)this.position.X;
            this.rectangle.Y = (int)this.position.Y;

        }

        public void DrawAnimation(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects flip = SpriteEffects.None;

            animationPlayer.Draw(gameTime, spriteBatch, position, flip);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
