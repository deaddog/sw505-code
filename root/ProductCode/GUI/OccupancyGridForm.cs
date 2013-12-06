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
        private Size diffSize;
        public OccupancyGridForm(OccupancyGrid initialGrid)
        {
            InitializeComponent();

            diffSize = new Size(this.Width - gridControl.Width, this.Height - gridControl.Height);
            gridControl.Resize += gridControl_Resize;

            this.Grid = initialGrid;

            checkBoxShowBorders_CheckedChanged(null, null);
            checkBoxShowProbabilities_CheckedChanged(null, null);
            checkBoxHideUnexplored_CheckedChanged(null, null);
            checkBoxShowRulers_CheckedChanged(null, null);
        }
        public GUI.Controls.OccupancyGridControl.DrawCollection<CommonLib.Interfaces.ICoordinate> Coordinates
        {
            get { return gridControl.Coordinates; }
        }
        public GUI.Controls.OccupancyGridControl.DrawCollection<CommonLib.Interfaces.IPose> Poses
        {
            get { return gridControl.Poses; }
        }

        public OccupancyGrid Grid
        {
            get { return gridControl.Grid; }
            set { gridControl.Grid = value; }
        }

        public float AreaWidth
        {
            get { return gridControl.AreaWidth; }
            set { gridControl.AreaWidth = value; }
        }
        public float AreaHeight
        {
            get { return gridControl.AreaHeight; }
            set { gridControl.AreaHeight = value; }
        }

        private void checkBoxShowBorders_CheckedChanged(object sender, EventArgs e)
        {
            gridControl.GridShowBorders = checkBoxShowBorders.Checked;
        }

        private void checkBoxShowProbabilities_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxHideUnexplored.Enabled =
                gridControl.GridShowProbabilities =
                checkBoxShowProbabilities.Checked;
        }

        private void checkBoxHideUnexplored_CheckedChanged(object sender, EventArgs e)
        {
            gridControl.GridHideUnexplored = checkBoxHideUnexplored.Checked;
        }

        private void checkBoxShowRulers_CheckedChanged(object sender, EventArgs e)
        {
            gridControl.GridShowRuler = checkBoxShowRulers.Checked;
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
