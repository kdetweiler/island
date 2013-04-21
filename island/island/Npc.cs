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
        public List<Vector2> path;
        public int[] quadrants = new int[4];
        public float[] wallSensors = new float[3];

        public Vector2 velocity;
        public Rectangle rectangle;
        public Node location;
        public Node shortDest;
        public Node finalDest;
        public static List<Node> nodepath;

        public int faceDirection;

        public Sensor sensor;

        float lastTime = 0.0f;
        float speed;

        public Boolean withinRange;
        Vector2 attackRange;

        //void Update(float elapsed);
        
        public NPC()
        {

        }

        public NPC(Vector2 newPosition, String newName)
        {
            //texture = newTexture;
            position = newPosition;
            sensor=new Sensor(100,3);
            //rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            rectangle = new Rectangle((int)position.X, (int)position.Y, 34, 57);
            name = newName;
            health = 100;
            maxHealth = 100;
            strength = 10;
            defense = 5;
            isAlive = true;
        }

        public NPC(Texture2D newTexture, Vector2 newPosition, String newName)
        {
            texture = newTexture;
            position = newPosition;
            name = newName;
            sensor = new Sensor(100, 3);
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            health = 100;
            maxHealth = 100;
            strength = 10;
            defense = 5;
            isAlive = true;
        }

        public NPC(Vector2 newPosition, String newName, Node spawnNode) 
        {
            position = newPosition;
            name = newName;
            sensor = new Sensor(100, 3);
            //rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            rectangle = new Rectangle((int)position.X, (int)position.Y, 34, 57);
            location = spawnNode;
            health = 100;
            maxHealth = 100;
            strength = 10;
            defense = 5;
            isAlive = true;
        }


        public NPC(Texture2D newTexture, Vector2 newPosition, String newName, Node spawnNode)
        {
            texture = newTexture;
            position = newPosition;
            name = newName;
            sensor = new Sensor(100, 3);
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            location = spawnNode;
            health = 100;
            maxHealth = 100;
            strength = 10;
            defense = 5;
            isAlive = true;
        }

        public NPC(Texture2D newTexture, Vector2 newPosition, String newName, Node spawnNode, Vector2 newAttackRange)
        {
            texture = newTexture;
            position = newPosition;
            name = newName;
            sensor = new Sensor(100, 3);
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            location = spawnNode;
            health = 100;
            maxHealth = 100;
            strength = 10;
            defense = 5;
            isAlive = true;

            attackRange = newAttackRange;
            withinRange = false;
        }

        public void Load(ContentManager Content)
        {
            walkHorizontalRightAnimation = new Animation(Content.Load<Texture2D>("walkHorizontalRight"), 32, 0.1f, true);
            walkHorizontalLeftAnimation = new Animation(Content.Load<Texture2D>("walkHorizontalLeft"), 32, 0.1f, true);
            walkVerticalDownAnimation = new Animation(Content.Load<Texture2D>("walkVerticalDown"), 34, 0.1f, true);
            walkVerticalUpAnimation = new Animation(Content.Load<Texture2D>("walkVerticalUp"), 34, 0.1f, true);

            idleHorizontalRightAnimation = new Animation(Content.Load<Texture2D>("idleHorizontalRight"), 31, 0.3f, false);
            idleHorizontalLeftAnimation = new Animation(Content.Load<Texture2D>("idleHorizontalLeft"), 31, 0.3f, false);
            idleVerticalDownAnimation = new Animation(Content.Load<Texture2D>("idleVerticalDown"), 34, .3f, false);
            idleVerticalUpAnimation = new Animation(Content.Load<Texture2D>("idleVerticalUp"), 34, .3f, false);

            animationPlayer.PlayAnimation(idleVerticalDownAnimation);
        }

        /*
        public void Load(ContentManager Content, Texture2D newTexture)
        {
            npcIdle = new Animation(newTexture, 32, 0.1f, true);

            animationPlayer.PlayAnimation(npcIdle);
        }*/
        
        public override void Update(GameTime gameTime)
        {
           // position += velocity;

            //this.rectangle.X = (int)this.position.X;
            //this.rectangle.Y = (int)this.position.Y;
            if (shortDest.point.X == this.rectangle.X && shortDest.point.Y == this.rectangle.Y) {
                //A* shit goes here
                location = shortDest;
                //calculate using finalDest and the new location A* path
                
            }

        }

        public void takeAction() 
        {
            if (isAlive)
            {
                if (!isHostile)
                {
                    stand();
                }
                else
                {
                    if (withinRange)
                    {
                        //if(its been two seconds since last attack)
                        //attack
                    }
                    else { seek(); }
                }
            }
            else 
            { 
                //if dead want something to happen
            }
        }

        //not hostile actions
        public void patrol() { }

        public void stand() 
        {
            velocity = Vector2.Zero;
        }

        //hostile actions

        public void seek() { }

        public void attack() 
        { 
            //tell Game1 CombatController to do stuff
            CombatController.Instance.confirmedHit(this, ListHolder.Instance.getPlayer());
        }

        public List<Node> nodeMove(NodeGraph nodegraph, Node desiredEnd) 
        {
            List<Node> answer=nodegraph.AStar(location, desiredEnd);
            shortDest = answer[1];
            return answer;
        }

        public void Update(GameTime gameTime, List<Vector2> path)
        {
            lastTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (path.Count > 0)
            {
                if (lastTime > 1)
                {
                    if (path.Count > 0 && MoveTowardsPoint(path[0], lastTime))
                        path.RemoveAt(0);

                    lastTime = 0;
                }

            }
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

        public void die() 
        {
            isAlive = false;
            //change animation

        }

        private bool MoveTowardsPoint(Vector2 goal, float elapsed)
        {
            // If we're already at the goal return immediatly
            if (position == goal) return true;

            // Find direction from current position to goal
            Vector2 direction = Vector2.Normalize(goal - position);

            //animation stuff
            if (direction.X > 0)
            {
                animationPlayer.PlayAnimation(walkHorizontalRightAnimation);
            }
            else if (direction.X < 0)
            {
                animationPlayer.PlayAnimation(walkHorizontalLeftAnimation);
            }
            else if (direction.Y > 0)
            {
                animationPlayer.PlayAnimation(walkVerticalDownAnimation);
            }
            else if (direction.Y < 0)
            {
                animationPlayer.PlayAnimation(walkVerticalUpAnimation);
            }

            // Move in that direction
            //position += direction * speed * elapsed;

            // If we moved PAST the goal, move it back to the goal
            if (Math.Abs(Vector2.Dot(direction, Vector2.Normalize(goal - position)) + 1) < 0.1f)
                this.position = goal;

            

            // Return whether we've reached the goal or not
            //this.position = goal;
            
            if (this.position.X != goal.X)
            {
                if (this.position.X < goal.X)
                    this.position.X += 1;
                else
                    this.position.X -= 1;
            }

            if (this.position.Y != goal.Y)
            {
                if (this.position.Y < goal.Y)
                    this.position.Y += 1;
                else
                    this.position.Y -= 1;
            }
            
            return position == goal;
            
        }

        public void WithinRange(Vector2 PlayerPosition)
        {
            if (Math.Abs(this.position.X - PlayerPosition.X) <= this.attackRange.X && Math.Abs(this.position.Y - PlayerPosition.Y) <= (this.attackRange.Y))
            {
                this.withinRange = true;
            }
            else this.withinRange = false;
        }
    }
}
