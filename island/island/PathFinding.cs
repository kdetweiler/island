using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace island
{
    public class Map
    {
        public const int tileSize = 50;
        public int[,] layout;


        private List<Texture2D> textures;

        // The width of the map.
        public int Width
        {
            get { return layout.GetLength(1); }
        }

        // The height of the map.
        public int Height
        {
            get { return layout.GetLength(0); }
        }

        public Map(int[,] newLayout)
        {
            layout = newLayout;
        }

        /// <summary>
        /// Sets the textures for the map to draw.
        /// </summary>
        public void SetTextures(List<Texture2D> textures)
        {
            this.textures = textures;
        }

        /// <summary>
        /// Returns the tile index for the given cell.
        /// </summary>
        public int GetIndex(int cellX, int cellY)
        {
            if (cellX < 0 || cellX > Width - 1 || cellY < 0 || cellY > Height - 1)
                return 0;

            return layout[cellY, cellX];
        }

        /// <summary>
        /// Draws the map.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (textures == null)
            {
                return;
            }

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int index = layout[y, x];

                    if (index == 4)
                        spriteBatch.Draw(textures[0], new Vector2(x, y) * tileSize, Color.White);

                    spriteBatch.Draw(textures[index], new Vector2(x, y) * tileSize, Color.White);
                }
            }
        }
    }

    public class SearchNode
    {
        public Point Position;
        public bool Walkable;
        public SearchNode[] Neighbors;
        public SearchNode Parent;
        public bool InOpenList;
        public bool InClosedList;
        public float DistanceToGoal;
        public float DistanceTraveled;
    }

    public class Pathfinding
    {
        private SearchNode[,] searchNodes;
        private int levelWidth;
        private int levelHeight;

        private List<SearchNode> openList = new List<SearchNode>();
        private List<SearchNode> closedList = new List<SearchNode>();

        public Pathfinding(Map map)
        {
            levelWidth = map.Width;
            levelHeight = map.Height;

            InitializeSearchNodes(map);
        }

        private float Heuristic(Point point1, Point point2)
        {
            return Math.Abs(point1.X - point2.X) +
                   Math.Abs(point1.Y - point2.Y);
        }

        private void InitializeSearchNodes(Map map)
        {
            searchNodes = new SearchNode[levelWidth, levelHeight];

            //Create a search node for each tile
            for (int x = 0; x < levelWidth; x++)
            {
                for (int y = 0; y < levelHeight; y++)
                {
                    //create search node for this tile
                    SearchNode node = new SearchNode();
                    node.Position = new Point(x, y);

                    //check if tile is walkable or not
                    node.Walkable = map.GetIndex(x, y) == 0;

                    //if walkable then store the node
                    if (node.Walkable == true)
                    {
                        node.Neighbors = new SearchNode[4];
                        searchNodes[x, y] = node;
                    }
                }
            }

            //connect search node to neighbor nodes
            for (int x = 0; x < levelWidth; x++)
            {
                for (int y = 0; y < levelHeight; y++)
                {
                    SearchNode node = searchNodes[x, y];

                    //check if node is walkable
                    if (node == null || node.Walkable == false)
                    {
                        continue;
                    }

                    //array of all the neighbors this node has
                    Point[] neighbors = new Point[]
                    {
                        new Point (x, y - 1), // The node above the current node
                        new Point (x, y + 1), // The node below the current node.
                        new Point (x - 1, y), // The node left of the current node.
                        new Point (x + 1, y), // The node right of the current node
                    };


                    for (int i = 0; i < neighbors.Length; i++)
                    {
                        Point position = neighbors[i];

                        if (position.X < 0 || position.X > levelWidth - 1 ||
                            position.Y < 0 || position.Y > levelHeight - 1)
                        {
                            continue;
                        }

                        SearchNode neighbor = searchNodes[position.X, position.Y];

                        if (neighbor == null || neighbor.Walkable == false)
                        {
                            continue;
                        }
                        node.Neighbors[i] = neighbor;
                    }
                }
            }
        }

        private void ResetSearchNodes()
        {
            openList.Clear();
            closedList.Clear();

            for (int x = 0; x < levelWidth; x++)
            {
                for (int y = 0; y < levelHeight; y++)
                {
                    SearchNode node = searchNodes[x, y];

                    if (node == null)
                    {
                        continue;
                    }

                    node.InOpenList = false;
                    node.InClosedList = false;

                    node.DistanceTraveled = float.MaxValue;
                    node.DistanceToGoal = float.MaxValue;
                }
            }
        }

        private List<Vector2> FindFinalPath(SearchNode startNode, SearchNode endNode)
        {
            closedList.Add(endNode);

            SearchNode parentTile = endNode.Parent;

            while (parentTile != startNode)
            {
                closedList.Add(parentTile);
                parentTile = parentTile.Parent;
            }

            List<Vector2> finalPath = new List<Vector2>();

            for (int i = closedList.Count - 1; i >= 0; i--)
            {
                finalPath.Add(new Vector2(closedList[i].Position.X * 50,
                                          closedList[i].Position.Y * 50));
            }

            return finalPath;
        }

        private SearchNode FindBestNode()
        {
            SearchNode currentTile = openList[0];

            float smallestDistanceToGoal = float.MaxValue;

            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].DistanceToGoal < smallestDistanceToGoal)
                {
                    currentTile = openList[i];
                    smallestDistanceToGoal = currentTile.DistanceToGoal;
                }
            }
            return currentTile;
        }

        public List<Vector2> FindPath(Point startPoint, Point endPoint)
        {
            if (startPoint == endPoint)
            {
                return new List<Vector2>();
            }
            ResetSearchNodes();

            SearchNode startNode = searchNodes[startPoint.X, startPoint.Y];
            SearchNode endNode = searchNodes[endPoint.X, endPoint.Y];

            startNode.InOpenList = true;

            startNode.DistanceToGoal = Heuristic(startPoint, endPoint);
            startNode.DistanceTraveled = 0;

            openList.Add(startNode);

            while (openList.Count > 0)
            {
                SearchNode currentNode = FindBestNode();

                if (currentNode == null)
                {
                    break;
                }

                if (currentNode == endNode)
                {
                    return FindFinalPath(startNode, endNode);
                }

                for (int i = 0; i < currentNode.Neighbors.Length; i++)
                {
                    SearchNode neighbor = currentNode.Neighbors[i];

                    if (neighbor == null || neighbor.Walkable == false)
                    {
                        continue;
                    }

                    float distanceTraveled = currentNode.DistanceTraveled + 1;

                    float heuristic = Heuristic(neighbor.Position, endPoint);

                    if (neighbor.InOpenList == false && neighbor.InClosedList == false)
                    {
                        neighbor.DistanceTraveled = distanceTraveled;
                        neighbor.DistanceToGoal = distanceTraveled + heuristic;
                        neighbor.Parent = currentNode;
                        neighbor.InOpenList = true;
                        openList.Add(neighbor);
                    }
                    else if (neighbor.InOpenList || neighbor.InClosedList)
                    {
                        if (neighbor.DistanceTraveled > distanceTraveled)
                        {
                            neighbor.DistanceTraveled = distanceTraveled;
                            neighbor.DistanceToGoal = distanceTraveled + heuristic;

                            neighbor.Parent = currentNode;
                        }
                    }
                }
                openList.Remove(currentNode);
                currentNode.InClosedList = true;
            }

            // No path found
            return new List<Vector2>();
        }
    }
}
