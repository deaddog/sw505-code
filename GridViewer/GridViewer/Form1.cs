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

namespace GridViewer
{
    public partial class Form1 : Form
    {
        private OccupancyGrid grid;
        private Size diffSize;

        public Form1()
        {
            InitializeComponent();
            grid = new OccupancyGrid(30, 23, 10, -150, -115);

            diffSize = new Size(this.Width - gridControl.Width, this.Height - gridControl.Height);
            gridControl.Resize += gridControl_Resize;

            gridControl.Grid = grid;
            gridControl.Image = Properties.Resources.emptyGrid;
        }
        private void gridControl_Resize(object sender, EventArgs e)
        {
            if (DesignMode)
                return;
            else
                this.Size = new Size(gridControl.Width + diffSize.Width, gridControl.Height + diffSize.Height);
        }
    }
}
