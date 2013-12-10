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

        private void occupancyGridControl1_MouseMove(object sender, MouseEventArgs e)
        {
            var point = occupancyGridControl1.GetPoint(e.Location);
            labelMouse.Text = point.X.ToString("0.0") + " ; " + point.Y.ToString("0.0");
        }

        private void occupancyGridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                AddLabel(e.Location, false);
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                RemoveLabel();
        }

        private void occupancyGridControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                RemoveLabel();
                AddLabel(e.Location, true);
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                RemoveLabel();
        }

        private void AddLabel(Point point, bool newRoute)
        {
            Vector2D p = occupancyGridControl1.GetPoint(point);

            if (newRoute)
                occupancyGridControl1.AddNewRoute(p);
            else
                occupancyGridControl1.AddPointToRoute(p);

            Label label = new Label()
            {
                Text = p.X.ToString("0.0") + " ; " + p.Y.ToString("0.0"),
                ForeColor = (newRoute || flowLayoutPanel1.Controls.Count == 0) ? Color.Red : Color.Black
            };
            flowLayoutPanel1.Controls.Add(label);
        }
        private void RemoveLabel()
        {
            if (flowLayoutPanel1.Controls.Count == 0)
                return;

            occupancyGridControl1.RemoveLastPoint();

            Control ctrl = flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1];
            flowLayoutPanel1.Controls.Remove(ctrl);
            ctrl.Dispose();
        }
    }
}
