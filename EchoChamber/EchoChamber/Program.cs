using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EchoChamber
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Graph graph = new Graph();
            List < Vertex > v = new List<Vertex>();
            graph.AddEdge("A", "B");
            graph.AddEdge("B", "C");
            //graph.PrintAll();
            graph.FindVertex("A").Print();
            v = graph.GetNDegree(graph.FindVertex("A"), 1);

            foreach(Vertex x in v)
            {
                Console.WriteLine("Hasil: ");
                x.Print();
            }
        }
    }
}
