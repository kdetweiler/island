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
        NPC JasonMover=new NPC();

        Point startPoint = new Point(0,0);
        Point endPoint = new Point(6, 7);

        GameObject pathEndPoint;
        public CombatController combat;
        

        public Vector2 textBox = new Vector2(600, 0);

        public string test;
        Map myMap = new Map();

        Pathfinding pathfinding;


        //comment above NodeGraph
        private NodeGraph map;

        

        //pure test data
        
        //public Node(Vector2 pointPos, Node[] listNeighbors, double[] listDistance) {
        //    point=pointPos;
        //    neighbors=listNeighbors;
        //    distanceTo=listDistance;
        //    g = 0;
        //    f = 0;
        //}

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

            

            setFunNodes();
            map = new NodeGraph(funNodes);

            player = new Player(new Vector2(400, 300), "Player1");
            Vector2 start = new Vector2(startPoint.X*50, startPoint.Y*50);
            Vector2 end = new Vector2(endPoint.X * 50, endPoint.Y * 50);

            map = new NodeGraph(funNodes);
            npcMover = new NPC(start, "NPC");
            JasonMover = new NPC(start, "NPC", map.graph[0]);
            combat = new CombatController(player);

            List<Node> tempNodeList = JasonMover.nodeMove(map, funNodes[22]);

            GameObject[] listPath=new GameObject[tempNodeList.Count];
            for (int k = 0; k < tempNodeList.Count; k++) 
            { 
                listPath[k]=new GameObject(Content.Load<Texture2D>("endpoint"),tempNodeList[k].point);
            }

            pathEndPoint = new GameObject(Content.Load<Texture2D>("endPoint"),end);

            base.Initialize();
        }

        public void setFunNodes() 
        {
            funNodes[0] = new Node((new Vector2(0, 0)));
            funNodes[1] = new Node((new Vector2(50, 0)));
            funNodes[2] = new Node((new Vector2(100, 0)));
            funNodes[3] = new Node((new Vector2(150, 0)));
            funNodes[4] = new Node((new Vector2(200, 0)));
            funNodes[5] = new Node((new Vector2(250, 0)));
            funNodes[6] = new Node((new Vector2(300, 0)));
            funNodes[7] = new Node((new Vector2(0, 50)));
            funNodes[8] = new Node((new Vector2(50, 50)));
            funNodes[9] = new Node((new Vector2(100, 50)));
            funNodes[10] = new Node((new Vector2(150, 50)));
            funNodes[11] = new Node((new Vector2(200, 50)));
            funNodes[12] = new Node((new Vector2(250, 50)));
            funNodes[13] = new Node((new Vector2(300, 50)));
            funNodes[14] = new Node((new Vector2(0, 50)));
            funNodes[15] = new Node((new Vector2(50, 50)));
            funNodes[16] = new Node((new Vector2(100, 50)));
            funNodes[17] = new Node((new Vector2(150, 50)));
            funNodes[18] = new Node((new Vector2(200, 50)));
            funNodes[19] = new Node((new Vector2(250, 50)));
            funNodes[20] = new Node((new Vector2(300, 50)));
            funNodes[0] = new Node((new Vector2(0,0)));
            funNodes[1] = new Node((new Vector2(50, 0)));
            funNodes[2] = new Node((new Vector2(100, 0)));
            funNodes[3] = new Node((new Vector2(150, 0)));
            funNodes[4] = new Node((new Vector2(200, 0)));
            funNodes[5] = new Node((new Vector2(250, 0)));
            funNodes[6] = new Node((new Vector2(300, 0)));
            funNodes[7] = new Node((new Vector2(0, 50)));
            funNodes[8] = new Node((new Vector2(50, 50)));
            funNodes[9] = new Node((new Vector2(100, 50)));
            funNodes[10] = new Node((new Vector2(150, 50)));
            funNodes[11] = new Node((new Vector2(200, 50)));
            funNodes[12] = new Node((new Vector2(250, 50)));
            funNodes[13] = new Node((new Vector2(300, 50)));
            funNodes[14] = new Node((new Vector2(0, 100)));
            funNodes[15] = new Node((new Vector2(50, 100)));
            funNodes[16] = new Node((new Vector2(100, 100)));
            funNodes[17] = new Node((new Vector2(150, 100)));
            funNodes[18] = new Node((new Vector2(200, 100)));
            funNodes[19] = new Node((new Vector2(250, 100)));
            funNodes[20] = new Node((new Vector2(300, 100)));
            funNodes[21] = new Node((new Vector2(0, 150)));
            funNodes[22] = new Node((new Vector2(50, 150)));
            funNodes[23] = new Node((new Vector2(100, 150)));
            funNodes[24] = new Node((new Vector2(150, 150)));
            funNodes[25] = new Node((new Vector2(200, 150)));
            funNodes[26] = new Node((new Vector2(250, 150)));
            funNodes[27] = new Node((new Vector2(300, 150)));
            funNodes[28] = new Node((new Vector2(0, 200)));
            funNodes[29] = new Node((new Vector2(50, 200)));
            funNodes[30] = new Node((new Vector2(100, 200)));
            funNodes[31] = new Node((new Vector2(150, 200)));
            funNodes[32] = new Node((new Vector2(200, 200)));
            funNodes[33] = new Node((new Vector2(250, 200)));
            funNodes[34] = new Node((new Vector2(300, 200)));

            //funNodes[0].testAdd(funNodes[7]); funNodes[0].testAdd(funNodes[1]);
            funAdd(0,new int[] {7,1});
            funAdd(1, new int[] { 0, 2 });
            funAdd(2, new int[] { 1,9,3 });
            funAdd(3, new int[] { 2,4 });
            funAdd(4, new int[] { 3,11,5 });
            funAdd(5, new int[] { 4,6 });
            funAdd(6, new int[] { 5,13});
            funAdd(7, new int[] { 0,14 });
            funAdd(8, new int[] { 1,7,9,15 });
            funAdd(9, new int[] { 2,16 });
            funAdd(10, new int[] { 3,9,17 });
            funAdd(11, new int[] { 4,18 });
            funAdd(12, new int[] { 5,13,19 });
            funAdd(13, new int[] { 6,20});
            funAdd(14, new int[] { 7,21 });
            funAdd(15, new int[] { 14,16});
            funAdd(16, new int[] { 9,17,23 });
            funAdd(17, new int[] { 16,18 });
            funAdd(18, new int[] { 17,19 });
            funAdd(19, new int[] { 18,20 });
            funAdd(20, new int[] { 13,19 });
            funAdd(21, new int[] { 14,28 });
            funAdd(22, new int[] { 29,23,21 });
            funAdd(23, new int[] { 29,30,16 });
            funAdd(24, new int[] { 17,23,31 });
            funAdd(25, new int[] { 18,32 });
            funAdd(26, new int[] { 19,33 });
            funAdd(27, new int[] { 20,34 });
            funAdd(28, new int[] { 21,29 });
            funAdd(29, new int[] { 28,30 });
            funAdd(30, new int[] { 29,31 });
            funAdd(31, new int[] { 30,32 });
            funAdd(32, new int[] { 31,33 });
            funAdd(33, new int[] { 32,34 });
            funAdd(34, new int[] { 33 });
            //funNodes[1].testAdd(funNodes[0]); funNodes[1].testAdd(funNodes[8]); funNodes[1].testAdd(funNodes[2]);
            //funNodes[2].testAdd(funNodes[1]); funNodes[2].testAdd(funNodes[9]); funNodes[2].testAdd(funNodes[3]);
            //funNodes[3].testAdd(funNodes[2]); funNodes[3].testAdd(funNodes[10]); funNodes[3].testAdd(funNodes[4]);
            //funNodes[4].testAdd(funNodes[3]); funNodes[4].testAdd(funNodes[11]); funNodes[4].testAdd(funNodes[5]);
            //funNodes[5].testAdd(funNodes[4]); funNodes[5].testAdd(funNodes[12]); funNodes[5].testAdd(funNodes[6]);
            //funNodes[6].testAdd(funNodes[5]); funNodes[6].testAdd(funNodes[13]);
            //funNodes[7].testAdd(funNodes[0]); funNodes[7].testAdd(funNodes[8]); funNodes[7].testAdd(funNodes[8]);
        }

        public static void funAdd(int x, int[] y) 
        {
            for (int k = 0; k < y.Length; k++) { 
                funNodes[x].testAdd(funNodes[y[k]]);
            }
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

            npcs.Add(new NPC(Content.Load<Texture2D>("npc1"), new Vector2(100, 150), "NPC1", new Node()));
            npcs.Add(new NPC(Content.Load<Texture2D>("npc2"), new Vector2(200, 150), "NPC2",new Node()));

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

            
            npcMover.Update(gameTime, path);
            JasonMover.Update(gameTime);
            player.sensor.Proximity(player, 100, npcs);
            player.sensor.WallScan(player, walls);
            
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
            foreach (Vector2 point in path)
            {
                //System.Diagnostics.Debug.WriteLine(point);
                test = "Path: (" + point.X + ", " + point.Y + ")";
                spriteBatch.DrawString(font, test, textBox, Color.Black);
                textBox.Y += 20;
            }
            textBox.Y = 0;

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
