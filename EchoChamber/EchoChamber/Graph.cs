using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoChamber
{
    class Graph
    {
        List<Vertex> vertices;

        public Graph()
        {
            vertices = new List<Vertex>();
        }

        public void AddVertex(Vertex v)
        {
            vertices.Add(v);
        }

        public void AddEdge(Vertex v1, Vertex v2)
        {
            bool found1=false;
            bool found2 = false;
            Vertex temp1 =v1;
            Vertex temp2 = v2;
            int i=0;

            while (i < this.vertices.Count)
            {
                if (v1.Name== this.vertices[i].Name){
                    found1 = true;
                    temp1 = this.vertices[i];
                }
                else if (v2.Name == this.vertices[i].Name){
                    found2 = true;
                    temp2 = this.vertices[i];
                }
                i++;
            }
            if (!found1)
            {
                this.AddVertex(v1);
            }
            if (!found2)
            {
                this.AddVertex(v2);
            }
            temp1.AddEdge(temp2);
            temp2.AddEdge(temp1);
        }

        public void AddEdge(string n1, string n2)
        {
            Vertex v1 = new Vertex(n1);
            Vertex v2 = new Vertex(n2);
            this.AddEdge(v1, v2);
        }

        public void PrintAll()
        {
            foreach (Vertex v in vertices)
            {
                v.Print();
                v.PrintEdge();
            }
        }

        public Vertex FindVertex(string name)
        {
            foreach(Vertex v in vertices)
            {
                if (v.Name == name)
                {
                    return v;
                }
            }
            return null;
        }

        public List<Vertex> GetNDegree(Vertex v1, int n)
        {
            if (n == 0)
            {
                return v1.Edges;
            }
            else
            {
                List<Vertex> v = new List<Vertex>();
                foreach(Vertex ve in v1.Edges)
                {
                    v = v.Union(GetNDegree(ve, n - 1)).ToList();
                }
                return v;
            }
        }
    }
}
