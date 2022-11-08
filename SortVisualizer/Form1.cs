using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SortVisualizer
{
    public partial class Form1 : Form
    {
        int[] TheArray;
        Graphics g;
        BackgroundWorker bgWorker = null;
        bool Paused = false;
        public Form1()
        {
            InitializeComponent();
            PopulateDropdown();
        }

        private void PopulateDropdown()
        {
            List<string> ClassList = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(ISortEngine).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x.Name).ToList();
            ClassList.Sort();
            foreach (string Class in ClassList)
            {
                comboBox1.Items.Add(Class);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (TheArray == null) button1_Click(null, null);

            bgWorker = new BackgroundWorker();
            bgWorker.WorkerSupportsCancellation = true;
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerAsync(argument: comboBox1.SelectedItem);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (!Paused)
            {
                bgWorker.CancelAsync();
                Paused = true;
            }
            else
            {
                if(bgWorker.IsBusy) return;
                int NumEntries = panel1.Width;
                int MaxVal = panel1.Height;
                Paused = false;
                for(int i = 0; i < NumEntries; i++)
                {
                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), i, 0, 1, MaxVal);
                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i, MaxVal - TheArray[i], 1, MaxVal);
                }
                bgWorker.RunWorkerAsync(argument: comboBox1.SelectedItem);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g = panel1.CreateGraphics();
            int NumEntries = panel1.Width;
            int MaxVal = panel1.Height;
            TheArray = new int[NumEntries];
            g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), 0, 0, NumEntries, MaxVal);
            Random rnd = new Random();
            for(int i =0; i < NumEntries; i++)
            {
                TheArray[i] = rnd.Next(0, MaxVal);
            }
            for(int i = 0; i < NumEntries; i++)
            {
                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), i, MaxVal - TheArray[i], 1, MaxVal);
            }
        }

        #region Bacground

        public void bgWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            string SortTypeName = (string)e.Argument;
            Type type = Type.GetType("SortVisualizer." + SortTypeName);
            var ctors = type.GetConstructors();
            try
            {
                ISortEngine se = (ISortEngine)ctors[0].Invoke(new object[] { TheArray, g, panel1.Height });
                while(!se.IsSorted() && (!bgWorker.CancellationPending))
                {
                    se.NextStep();
                }
            }
            catch(Exception ex)
            {
                
            }
        }

        #endregion

        
    }
}
