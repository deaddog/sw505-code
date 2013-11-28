using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;
using CommonLib.DTOs;

namespace SystemInterface.GUI
{
    public partial class OccupancyGridForm : Form
    {
        public OccupancyGridForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            occupancyGridControl1.Grid = new OccupancyGrid(30, 23, 10, -150, -115);
        }

        private void checkBoxShowBorders_CheckedChanged(object sender, EventArgs e)
        {
            occupancyGridControl1.GridShowBorders = checkBoxShowBorders.Checked;
        }

        private void checkBoxShowProbabilities_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxHideUnexplored.Enabled =
                occupancyGridControl1.GridShowProbabilities =
                checkBoxShowProbabilities.Checked;
        }

        private void checkBoxHideUnexplored_CheckedChanged(object sender, EventArgs e)
        {
            occupancyGridControl1.GridHideUnexplored = checkBoxHideUnexplored.Checked;
        }

        private void checkBoxShowRulers_CheckedChanged(object sender, EventArgs e)
        {
            occupancyGridControl1.GridShowRuler = checkBoxShowRulers.Checked;
        }
    }
}
