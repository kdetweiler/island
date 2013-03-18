using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace island
{
    class JasonQ
    {
        private List<Node> list;
        private int size;

        public JasonQ() 
        {
            list = new List<Node>();
            size = 0;
        }

        public JasonQ(List<Node> graph) {
            size = graph.Count;
            for (int k = 0; k < size; k++) { 
                
            }
        }

        public void add(Node node) {
            
            for (int k = 0; k < size; k++) {
                if (node.f < list[k].f) { 
                    list.Insert(k, node);
                    size++;
                    return;
                }
            }
            size++;
            list.Add(node);
        }

        public Node dequeue() {
            Node zero = list[0];
            list.RemoveAt(0);
            size--;
            return zero;
        }

        public int Size() { return size; }

        public Node peek() 
        {
            if (size == 0)
            {
                throw new Exception("Please check that priorityQueue is not empty before dequeing");
            }
            else {
                if (size > 0)
                {
                    return list[0];
                }
                else throw new Exception("Weird size issues");
            }
        }

    }
}
