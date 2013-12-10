using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;
using CommonLib.DTOs;

namespace Services.RouteServices
{
    public partial class SchedulingForm : Form
    {
        private OccupancyGrid grid;
        private Size diffSize;
        private Vector2D point;

        public SchedulingForm(OccupancyGrid grid)
        {
            InitializeComponent();
            
            diffSize = new Size(this.Width - occupancyGridControl1.Width, this.Height - occupancyGridControl1.Height);

            occupancyGridControl1.Grid = grid;
        }

        public Vector2D Point
        {
            get { return point; }
            set { point = value; }
        }
        
        public OccupancyGrid Grid
        {
            get { return grid; }
            set { grid = value; }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void occupancyGridControl1_Resize(object sender, EventArgs e)
        {
            if (DesignMode)
                return;
            else
                 this.Size = new Size(occupancyGridControl1.Width + diffSize.Width, occupancyGridControl1.Height + diffSize.Height);
        }
        
    }
}
