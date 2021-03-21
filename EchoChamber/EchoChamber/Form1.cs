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
        private string akun1;
        private string akun2;
        private string algoritma;

        public Form1()
        {
            InitializeComponent();
            comboBox4.Items.Add("BFS");
            comboBox4.Items.Add("DFS");
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
                {
                    comboBox1.Items.Add(v.Name);
                    comboBox2.Items.Add(v.Name);
                    comboBox3.Items.Add(v.Name);
                }

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
            Dictionary<Vertex, int> friends = G.FriendRec(G.FindVertex(akunMutual));
            List<Vertex> mutual;
            string s = "Friend Recommendation:\n";
            foreach (KeyValuePair<Vertex, int> v in friends)
            {
            
                s += v.Key.Name;
                s += "\n";
                s += v.Value.ToString();
                s += " Mutual friends: ";
                mutual = G.MutualFriend(v.Key, G.FindVertex(akunMutual));
                foreach (Vertex v1 in mutual)
                {
                    s += v1.Name;
                    s += " ";
                }
                s += "\n\n";
            }
            
            richTextBox1.Text = s;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Show();
            akun1 = comboBox2.SelectedItem.ToString();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Show();
            akun2 = comboBox3.SelectedItem.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string s;
            if (algoritma == "BFS")
            {
                s = G.BFSString(G.FindVertex(akun1), G.FindVertex(akun2));
            }
            else
            {
                s = G.DFSString(G.FindVertex(akun1), G.FindVertex(akun2));
            }

            richTextBox2.Text = s;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Show();
            algoritma = comboBox4.SelectedItem.ToString();
        }
    }
}
