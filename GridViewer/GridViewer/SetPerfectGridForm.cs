using CommonLib.DTOs;
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
    public partial class SetPerfectGridForm : Form
    {
        private StateGrid grid;
        private Size diffSize;

        public SetPerfectGridForm()
        {
            InitializeComponent();

            diffSize = new Size(this.Width - gridControl.Width, this.Height - gridControl.Height);
            gridControl.Resize += gridControl_Resize;

            gridControl.Grid = new OccupancyGrid(30, 23, 10, -150, -115);
            gridControl.Image = Properties.Resources.emptyGrid;

            this.grid = new StateGrid(30, 23);
        }
        private void gridControl_Resize(object sender, EventArgs e)
        {
            if (DesignMode)
                return;
            else
                this.Size = new Size(gridControl.Width + diffSize.Width, gridControl.Height + diffSize.Height);
        }

        private void gridControl_MouseClick(object sender, MouseEventArgs e)
        {
        }
    }
}
