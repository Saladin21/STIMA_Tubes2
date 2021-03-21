using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EchoChamber
{
    public partial class Form1 : Form
    {
        
        private Graph G;
        private string akunMutual;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var FD = new System.Windows.Forms.OpenFileDialog();
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileToOpen = FD.FileName;

                System.IO.FileInfo File = new System.IO.FileInfo(FD.FileName);

                string[] s2 = FD.FileName.Split('\\');
                textBox1.AppendText(s2.Last());
                //OR

                System.IO.StreamReader reader = new System.IO.StreamReader(fileToOpen);
                //etc
                string n = reader.ReadLine();
                int n1 = Int32.Parse(n);

                G = new Graph();

                //create a viewer object 
                Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
                //create a graph object 
                Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
                //create the graph content 

                for (int i = 0; i < n1; i++)
                {
                    string[] edge = reader.ReadLine().Split(' ');
                    G.AddEdge(edge[0], edge[1]);
                    var Edge = graph.AddEdge(edge[0], edge[1]);
                    Edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                    Edge.Attr.ArrowheadAtSource = Microsoft.Msagl.Drawing.ArrowStyle.None;

                }

                
                
                //bind the graph to the viewer 
                viewer.Graph = graph;
                //associate the viewer with the form 
                pictureBox1.SuspendLayout();
                viewer.Dock = System.Windows.Forms.DockStyle.Fill;
                pictureBox1.Controls.Add(viewer);
                pictureBox1.ResumeLayout();
                //show the form 



                foreach (Vertex v in G.Vertices)
                    comboBox1.Items.Add(v.Name);

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

     

    

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Show();
            akunMutual = comboBox1.SelectedItem.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Vertex> friends = G.FriendRec(G.FindVertex(akunMutual));
            List<Vertex> mutual;
            string s = "Friend Recommendation:\n";
            foreach (Vertex v in friends)
            {
            
                s += v.Name;
                s += "\nMutual friends: ";
                mutual = G.MutualFriend(v, G.FindVertex(akunMutual));
                foreach (Vertex v1 in mutual)
                {
                    s += v1.Name;
                    s += " ";
                }
                s += "\n\n";
            }
            
            richTextBox1.Text = s;
        }
    }
}
