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

        public List<Vertex> Vertices { get { return this.vertices; } }

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
        public List<Vertex> FriendRec(Vertex v1)
        {
            List<Vertex> v = new List<Vertex>();
            foreach (Vertex ve in v1.Edges)
            {
                v = v.Union(ve.Edges).ToList();
            }
            v = v.Except(v1.Edges).ToList();
            v.Remove(v1);
            return v;
        }

        public List<Vertex> MutualFriend(Vertex v1, Vertex v2)
        {   
            return v1.Edges.Intersect(v2.Edges).ToList();
        }

        public List<Vertex> DFS(Vertex v, Vertex v1)
        {
            Stack <KeyValuePair<Vertex, List<Vertex>>> s = new Stack<KeyValuePair<Vertex, List<Vertex>>>();
            List<Vertex> v2, v3 = new List<Vertex>();

            s.Push(new KeyValuePair<Vertex, List<Vertex>>(v, new List<Vertex>()));

            KeyValuePair<Vertex, List<Vertex>> se = s.Pop();
            while (se.Key != v1)
            {
                v3.Add(se.Key);
                v2 = se.Key.Edges.Except(v3).ToList();
                foreach (Vertex ve in v2)
                {
                    List<Vertex> tempv = new List<Vertex>(se.Value);
                    tempv.Add(se.Key);

                    s.Push(new KeyValuePair<Vertex, List<Vertex>>(ve, tempv));
                }
                se = s.Pop();
            }
            
            return se.Value;
        }
        public List<Vertex> BFS(Vertex v, Vertex v1)
        {
            Queue<KeyValuePair<Vertex, List<Vertex>>> s = new Queue<KeyValuePair<Vertex, List<Vertex>>>();
            List<Vertex> v2, v3 = new List<Vertex>();

            s.Enqueue(new KeyValuePair<Vertex, List<Vertex>>(v, new List<Vertex>()));

            KeyValuePair<Vertex, List<Vertex>> se = s.Dequeue();
            while (se.Key != v1)
            {
                v3.Add(se.Key);
                v2 = se.Key.Edges.Except(v3).ToList();
                foreach (Vertex ve in v2)
                {
                    List<Vertex> tempv = new List<Vertex>(se.Value);
                    tempv.Add(se.Key);

                    s.Enqueue(new KeyValuePair<Vertex, List<Vertex>>(ve, tempv));
                }
                se = s.Dequeue();
            }

            return se.Value;
        }
    }
}
