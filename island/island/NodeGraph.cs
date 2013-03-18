using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace island
{
    class NodeGraph
    {
        
        public Node[] graph;
        

        public NodeGraph(Node[] map) 
        {
            graph = map;
        }

        public List<Node> AStar(Node start, Node dest) 
        {

            for (int k = 0; k < graph.Length; k++) {
                graph[k].g = 0;
                graph[k].f = 0;
            }
            List <Node> path= new List<Node>();
            Node[] Closed = new Node[graph.Length];
            int closed_counter = 0;
            JasonQ Open = new JasonQ();
            JasonQ neighborSet;
            Open.add(start);
            for (int k = 0; k < graph.Length; k++) {
                if (graph[k] != start) {
                    Open.add(graph[k]);
                    graph[k].g = 0;
                    graph[k].f = 0;
                }
            }

            Node current;
            
            double g_score = 0;
            double f_score = g_score + start.H(dest);

            start.g = g_score;
            start.f = f_score;

            double tempg = 0;

            while (Open.Size() > 0) 
            {
                current = Open.dequeue();
                if (current == dest) 
                {
                    path.Add(current);
                    return path;
                }
                Closed[closed_counter++] = current;
                for (int k = 0; k < current.neighbors.Length; k++) {
                    tempg = current.g + current.getTo(k);
                }
            }

            return path;
        }
    }

    /*
     *  function A*(start,goal)
     closedset := the empty set    // The set of nodes already evaluated.
     openset := {start}    // The set of tentative nodes to be evaluated, initially containing the start node
     came_from := the empty map    // The map of navigated nodes.
 
     g_score[start] := 0    // Cost from start along best known path.
     // Estimated total cost from start to goal through y.
     f_score[start] := g_score[start] + heuristic_cost_estimate(start, goal)
 
     while openset is not empty
         current := the node in openset having the lowest f_score[] value
         if current = goal
             return reconstruct_path(came_from, goal)
 
         remove current from openset
         add current to closedset
         for each neighbor in neighbor_nodes(current)
             tentative_g_score := g_score[current] + dist_between(current,neighbor)
             if neighbor in closedset
                 if tentative_g_score >= g_score[neighbor]
                     continue
 
             if neighbor not in openset or tentative_g_score < g_score[neighbor] 
                 came_from[neighbor] := current
                 g_score[neighbor] := tentative_g_score
                 f_score[neighbor] := g_score[neighbor] + heuristic_cost_estimate(neighbor, goal)
                 if neighbor not in openset
                     add neighbor to openset
 
     return failure
 
 function reconstruct_path(came_from, current_node)
     if came_from[current_node] in set
         p := reconstruct_path(came_from, came_from[current_node])
         return (p + current_node)
     else
         return current_node
     * 
     * */

}

