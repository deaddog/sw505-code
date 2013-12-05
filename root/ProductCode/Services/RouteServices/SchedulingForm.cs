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
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SchedulingForm(new OccupancyGrid(30,23,10,-150,-115)));
        }

        private OccupancyGrid grid;

        private Vector2D point;

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
        

        public SchedulingForm(OccupancyGrid grid)
        {
            InitializeComponent();
            occupancyGridControl1.Grid = grid;
        }

        private void occupancyGridControl1_UpdatePoint(object sender, EventArgs e)
        {
            label1.Text = occupancyGridControl1.Point.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            point = occupancyGridControl1.Point;
            this.Close();
        }

        
    }
}
