using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MED
{
    public partial class Form1 : Form
    {
        Dictionary<string, int> wordDistanceDict;
        string[] vocabulary;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            vocabulary = File.ReadAllLines("vocabulary_tr.txt");
            wordDistanceDict = new Dictionary<string, int>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            foreach (string word in vocabulary)
            {
                wordDistanceDict[word] = Distance(input, word);
            }
            var sortedDict = from entry in wordDistanceDict orderby entry.Value ascending select entry;
            foreach (KeyValuePair<string, int> item in sortedDict)
            {
                listBox1.Items.Add(item.Key + " -> " + item.Value);
            }
        }

        private static int Distance(string word1, string word2)
        {
            word1 = word1.ToLower();
            word2 = word2.ToLower();
            int[] costs = new int[word2.Length + 1];
            for (int i = 0; i < costs.Length; i++)
            {
                costs[i] = i;
            }
            for (int i = 1; i <= word1.Length; i++)
            {
                costs[0] = i;
                int nw = i - 1;
                for (int j = 1; j <= word2.Length; j++)
                {
                    int cj;
                    if (word1[i-1] == word2[j - 1])
                    {
                        cj = Math.Min(1 + Math.Min(costs[j], costs[j - 1]), nw);
                    }
                    else
                    {
                        cj = Math.Min(1 + Math.Min(costs[j], costs[j - 1]), nw + 1);
                    }
                    nw = costs[j];
                    costs[j] = cj;
                }
            }
            return costs[word2.Length];
        }
    }
}
