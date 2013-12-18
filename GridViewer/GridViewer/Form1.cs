using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemInterface.GUI.Controls;

namespace GridViewer
{
    public partial class Form1 : Form
    {
        private Size diffSize;
        private OccupancyGrid[] grids = new OccupancyGrid[0];
        private OccupancyGrid defaultGrid;

        public Form1()
        {
            InitializeComponent();

            diffSize = new Size(this.Width - gridControl.Width, this.Height - gridControl.Height);
            gridControl.Resize += gridControl_Resize;

            defaultGrid = new OccupancyGrid(30, 23, 10, -150, -115);
            gridControl.Grid = defaultGrid;
            gridControl.Image = Properties.Resources.emptyGrid;
        }
        private void gridControl_Resize(object sender, EventArgs e)
        {
            if (DesignMode)
                return;
            else
                this.Size = new Size(gridControl.Width + diffSize.Width, gridControl.Height + diffSize.Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SetPerfectGridForm form = new SetPerfectGridForm())
                form.ShowDialog();
        }

        private void gridControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length == 1 && files[0].EndsWith(".log"))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void gridControl_DragDrop(object sender, DragEventArgs e)
        {
            string file = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            this.grids = GridLogger.LoadGrids(file);

            this.trackBar1.Value = -1;
            this.trackBar1.Maximum = grids.Length - 1;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (trackBar1.Value == -1)
                gridControl.Grid = defaultGrid;
            else
                gridControl.Grid = grids[trackBar1.Value];
        }
    }
}
