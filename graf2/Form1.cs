using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace graf2
{
    public partial class Form1 : Form
    {
        private Dictionary<string, Dictionary<string, int>> graph;
        XmlDocument gxl = new XmlDocument();
        public Form1()
        {
            InitializeComponent();
        }

        private void button_open_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "GXL Files|*.gxl";
            openFileDialog1.Title = "Select a GXL File";
            Dictionary<string,Dictionary<string,int>> colors = new Dictionary<string, Dictionary<string, int>>();

            graph = new Dictionary<string, Dictionary<string, int>>();

            Dictionary<string, int> colorNumber = new Dictionary<string, int>();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                gxl.Load(openFileDialog1.OpenFile());

                XmlNodeList elemList = gxl.GetElementsByTagName("edge");
                for (int i = 0; i < elemList.Count; i++)
                {
                    string from = elemList[i].Attributes["from"].Value;
                    string to = elemList[i].Attributes["to"].Value;

                    if (!graph.ContainsKey(from))
                    {
                        graph.Add(from, new Dictionary<string, int>());
                        graph[from].Add(to, 0);
                    }
                    else
                    {
                        if (!graph[from].ContainsKey(to))
                        {
                            graph[from].Add(to, 0);
                        }
                    }

                    if (!graph.ContainsKey(to))
                    {
                        graph.Add(to, new Dictionary<string, int>());
                        graph[to].Add(from, 0);
                    }
                    else
                    {
                        if (!graph[to].ContainsKey(from))
                        {
                            graph[to].Add(from, 0);
                        }
                    }
                }
            }
        }

        private void button_count_Click(object sender, EventArgs e)
        {
            int max_color = 0;
            foreach (var node in graph)
            {
                int color = node.Value.Count + 1;
                if (color > max_color) max_color = color;
            }

            label1.Text = max_color.ToString();v
        }
    }
}
