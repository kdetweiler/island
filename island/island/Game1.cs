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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        private Texture2D officeBackground;
        private Player player;
        private Wall box1;
        private GameObject box2;
        private GamePadState gamepadstatus;
        private KeyboardState keyboard;
        private Rectangle TitleSafe;
        private List<Npc> npcs = new List<Npc>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);            
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player(new Vector2(400, 300));
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);

            font = Content.Load<SpriteFont>("myFont");
            officeBackground = Content.Load<Texture2D>("tempStartArea");
            player.Load(Content);
            //player = new Player(Content.Load<Texture2D>("idleVerticalDown"), new Vector2(400, 300));

            npcs.Add(new Npc(Content.Load<Texture2D>("npc1"), new Vector2(600, 150)));
            npcs.Add(new Npc(Content.Load<Texture2D>("npc2"), new Vector2(200, 150)));

            box1 = new Wall(Content.Load<Texture2D>("horizontalBox"), new Vector2(250, 400));

            TitleSafe = GetTitleSafeArea(.8f);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            gamepadstatus = GamePad.GetState(PlayerIndex.One);
            keyboard = Keyboard.GetState();
            
            // Allows the game to exit
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) || (keyboard.IsKeyDown(Keys.Escape)))
            {
                this.Exit();
            }

            //run player update
            player.Update(gameTime);            
            
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            //Draw background
            spriteBatch.Begin();
            spriteBatch.Draw(officeBackground, GraphicsDevice.Viewport.Bounds, Color.White);            
            spriteBatch.End();
             
            // start rendering sprites
            spriteBatch.Begin();
            spriteBatch.DrawString(font, player.toString(), new Vector2(50, 45), Color.Black);

            //draw walls
            box1.Draw(gameTime, spriteBatch);
            
            //draw players
            player.DrawAnimation(gameTime, spriteBatch);
            //player.Draw(spriteBatch);

            //draw all npc's on screen
            foreach (Npc npc in npcs)
                npc.Draw(spriteBatch);
                        
            //Draw the game components (Sprites included)
            base.Draw(gameTime);
            // end rendering sprites;
            spriteBatch.End();
        }

        protected Rectangle GetTitleSafeArea(float percent)
        {
            Rectangle retval = new Rectangle(graphics.GraphicsDevice.Viewport.X,
                graphics.GraphicsDevice.Viewport.Y,
                graphics.GraphicsDevice.Viewport.Width,
                graphics.GraphicsDevice.Viewport.Height);
          
            return retval;
        }
    }
}
