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
 * derp
 * 
 * */
namespace island
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        static int[,] levelOneLayout = new int[,]
        {
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
            { 1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            { 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1}, 
            { 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 0, 1}, 
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1},
            { 1, 0, 1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0, 1}, 
            { 1, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 2}, 
            { 1, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1}, 
            { 1, 1, 1, 1, 0, 1, 0, 1, 0, 0, 1, 0, 0, 0, 1, 1}, 
            { 1, 4, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1}, 
            { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1}, 
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
        };

        static public int[,] levelTwoLayout = new int[,]
        {
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 1}, 
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
            { 3, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 1}, 
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, 
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
        };
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        private Texture2D officeBackground;
        private Player player;
        private GamePadState gamepadstatus;
        private KeyboardState keyboard;
        private Rectangle TitleSafe;

        String health;
        
        private List<NPC> npcs = new List<NPC>();
        private List<GameObject> gameObjects = new List<GameObject>();
        private List<Character> characters = new List<Character>();
        private List<Wall> walls = new List<Wall>();
        private List<Entity> entitys = new List<Entity>();
        public List<Vector2> path = new List<Vector2>();
        public List<Vector2> newPath = new List<Vector2>();


        NPC npcMover = new NPC();
        private bool hit = false;
        private int currentLevel = 1;

        Point startPoint = new Point(1,1);
        Point endPoint = new Point(6, 7);
        Point TSP = new Point(1, 9);
        //Point TEP = new Point(3, 9);

        CombatController combat;
        

        public Vector2 textBox = new Vector2(600, 0);

        public string test;
        Map level1 = new Map(levelOneLayout);
        Map level2 = new Map(levelTwoLayout);

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
            player = new Player(new Vector2(400, 300), "Player1");
            ListHolder.Instance.NPCList = new List<NPC>();
            ListHolder.Instance.WallList = new List<Wall>();
            ListHolder.Instance.setGame(this);
            ListHolder.Instance.player = player;

            //initialize map npc spawns
            MakeNPCList(levelOneLayout);
            pathfinding = new Pathfinding(level1);
            ListHolder.Instance.setPathFinder(pathfinding);
            //path = pathfinding.FindPath(startPoint, endPoint);
            //newPath = pathfinding.FindPath(TSP, TEP);

            
            Vector2 start = new Vector2(startPoint.X*50, startPoint.Y*50);
            Vector2 end = new Vector2(endPoint.X * 50, endPoint.Y * 50);
            npcMover = new NPC(start, "NPC");


           // pathEndPoint = new GameObject(Content.Load<Texture2D>("endPoint"),end);


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
            List<Texture2D> mapTextures = new List<Texture2D>()
            {
                Content.Load<Texture2D>("grass"),
                Content.Load<Texture2D>("tree"),
                Content.Load<Texture2D>("doorRight"),
                Content.Load<Texture2D>("doorLeft"),
                Content.Load<Texture2D>("npc1"),
            };
            level1.SetTextures(mapTextures);
            level2.SetTextures(mapTextures);

            player.Load(Content);

            //npcs.Add(new NPC(Content.Load<Texture2D>("npc1"), new Vector2(100, 150), "NPC1", new Node(), new Vector2(50, 50)));
            //npcs.Add(new NPC(Content.Load<Texture2D>("npc2"), new Vector2(200, 150), "NPC2", new Node(), new Vector2(50, 50)));

            npcMover.Load(Content);
            ListHolder.Instance.NPCList[0].Load(Content);

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
            LoadLevel();
            health="Health: "+player.health;

            Point npcEnd = new Point(player.rectangle.X / 50, player.rectangle.Y / 50);
            newPath = pathfinding.FindPath(TSP, npcEnd);
            //ListHolder.Instance.NPCList[0].Update(gameTime, newPath);

            for (int k = 0; k < ListHolder.Instance.NPCList.Count; k++) 
            {
                ListHolder.Instance.NPCList[k].Update(gameTime,newPath);
            }
            
            //npcMover.Update(gameTime, path);
            //Point npcStart = new Point(10, 1);
            //Point npcEnd = new Point(player.rectangle.X / 50, player.rectangle.Y / 50);
            //newPath = pathfinding.FindPath(TSP, npcEnd);
            //ListHolder.Instance.NPCList[0].Update(gameTime, newPath);

            //player.sensor.Proximity(player, 100, npcs);
            //player.sensor.WallScan(player, walls);
            //hit = player.sensor.WeaponSensor(player, npcs[1], player.rightHand.range);
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

            if (currentLevel == 1)
                level1.Draw(spriteBatch);
            else if (currentLevel == 2)
                level2.Draw(spriteBatch);

            spriteBatch.End();
             
            // start rendering sprites
            spriteBatch.Begin();
            textBox.Y = 0;
            spriteBatch.DrawString(font, "Health: "+player.health, new Vector2(550, 0), Color.Black);
            spriteBatch.DrawString(font, player.toString(), new Vector2(50, 0), Color.Black);

            //draw walls
            foreach (Wall wall in walls)
                wall.Draw(gameTime, spriteBatch);
            
            //draw players
            player.DrawAnimation(gameTime, spriteBatch);
            npcMover.DrawAnimation(gameTime, spriteBatch);
            ListHolder.Instance.NPCList[0].DrawAnimation(gameTime, spriteBatch);
                        
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

        protected void LoadLevel()
        {
            if (currentLevel == 1 && player.rectangle.Center.X >= 780 && player.rectangle.Center.Y >= 249 && player.position.Y <= 301)
            {
                currentLevel = 2;
                player.position.X = 30;
                player.position.Y = 300;
            }
            else if (currentLevel == 2 && player.rectangle.Center.X <= 20 && player.position.Y >= 249 && player.position.Y <= 301)
            {
                currentLevel = 1;
                player.position.X = 730;
                player.position.Y = 300;
            }
        }

        
        public void MakeNPCList(int[,] layout)
        {
            for(int i = 0; i < 12; i++)
                for (int j = 0; j < 16; j++)
                {
                    if (layout[i, j] == 4)
                        ListHolder.Instance.addNPC(new NPC(new Vector2(j * 50, i * 50), "NPC1", new Node()));
                }

        }
    }
}
