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

/* NOTES
 * entitys = npcs.Cast<Entity>().Concat(walls.Cast<Entity>()).ToList(); // how to concat two children lists into one parent list
 * pointless change
 * 
 * 
 * 
 * */
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
        private GamePadState gamepadstatus;
        private KeyboardState keyboard;
        private Rectangle TitleSafe;
        private List<NPC> npcs = new List<NPC>();
        private List<GameObject> gameObjects = new List<GameObject>();
        private List<Character> characters = new List<Character>();
        private List<Wall> walls = new List<Wall>();
        private List<Entity> entitys = new List<Entity>();
        public List<Vector2> path = new List<Vector2>();
        NPC npcMover = new NPC();
        private bool hit = false;

        Point startPoint = new Point(0,0);
        Point endPoint = new Point(6, 7);

        GameObject pathEndPoint;
        CombatController combat;
        

        public Vector2 textBox = new Vector2(600, 0);

        public string test;
        Map myMap = new Map();

        Pathfinding pathfinding;


        //comment above NodeGraph
        private NodeGraph map;


        private static Node[] funNodes = new Node[35];
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
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
            pathfinding = new Pathfinding(myMap);
            path = pathfinding.FindPath(startPoint, endPoint);

            map = new NodeGraph(funNodes);

            player = new Player(new Vector2(400, 300), "Player1");
            Vector2 start = new Vector2(startPoint.X*50, startPoint.Y*50);
            Vector2 end = new Vector2(endPoint.X * 50, endPoint.Y * 50);

            map = new NodeGraph(funNodes);
            npcMover = new NPC(start, "NPC");
            combat = new CombatController(player);

            pathEndPoint = new GameObject(Content.Load<Texture2D>("endPoint"),end);

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
            //officeBackground = Content.Load<Texture2D>("tempStartArea");
            List<Texture2D> textures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("grass"),
                Content.Load<Texture2D>("tree"),
            };
            myMap.SetTextures(textures);

            player.Load(Content);

            npcs.Add(new NPC(Content.Load<Texture2D>("npc1"), new Vector2(100, 150), "NPC1", new Node(), new Vector2(50, 50)));
            npcs.Add(new NPC(Content.Load<Texture2D>("npc2"), new Vector2(200, 150), "NPC2", new Node(), new Vector2(50, 50)));

            npcMover.Load(Content);

            //walls.Add(new Wall(Content.Load<Texture2D>("horizontalBox"), new Vector2(250, 400)));

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

            //player.sensor.proximitySensor(player.rectangle, 100, npcs);
            //run player update
            player.Update(gameTime);

            foreach (NPC c in npcs)
            {
                c.WithinRange(player.position);
            }
            
            npcMover.Update(gameTime, path);
            //player.sensor.Proximity(player, 100, npcs);
            //player.sensor.WallScan(player, walls);
            hit = player.sensor.WeaponSensor(player, npcs[1], player.rightHand.range);
            //hit = false;
            
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
            //spriteBatch.Draw(officeBackground, GraphicsDevice.Viewport.Bounds, Color.White);            
            myMap.Draw(spriteBatch);
            spriteBatch.End();
             
            // start rendering sprites
            spriteBatch.Begin();
            //foreach (Vector2 point in path)
            //{
            //    //System.Diagnostics.Debug.WriteLine(point);
            //    test = "Path: (" + point.X + ", " + point.Y + ")";
            //    spriteBatch.DrawString(font, test, textBox, Color.Black);
            //    textBox.Y += 20;
            //}
            textBox.Y = 0;

            foreach (NPC c in npcs)
            {
                if (c.withinRange == true)
                    spriteBatch.DrawString(font, "Within Range!", new Vector2(550, 0), Color.Black);
            }
            if (hit)
                spriteBatch.DrawString(font, "HIT!", new Vector2(600, 100), Color.Black);

            spriteBatch.DrawString(font, player.toString(), new Vector2(50, 0), Color.Black);

            pathEndPoint.Draw(gameTime, spriteBatch);
            //draw walls
            foreach (Wall wall in walls)
                wall.Draw(gameTime, spriteBatch);
            
            //draw players
            player.DrawAnimation(gameTime, spriteBatch);
            npcMover.DrawAnimation(gameTime, spriteBatch);

            //draw all npc's on screen
            foreach (NPC npc in npcs)
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
