using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoChamber
{
    class Vertex
    {
         string name;
         List<Vertex> edges;

        public string Name { get; }
        public List<Vertex> Edges { get; set; }

        public Vertex(string v)
        {
            name = v;
            edges = new List<Vertex>();
        }

        public void AddEdge(Vertex v)
        {
            edges.Add(v);
        }

        public void Print()
        {
            Console.WriteLine(name);
        }
        
        public void PrintEdge()
        {

            foreach (Vertex v in edges)
            {
                Console.Write("Edge: ");
                Console.WriteLine(v.name);
            }
        }
    }
}
