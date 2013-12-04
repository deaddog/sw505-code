using Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemInterface.GUI
{
    public partial class SelectColorsForm : Form
    {
        private Bitmap image;
        private bool first;

        public SelectColorsForm()
        {
            InitializeComponent();

            first = true;
            image = DisplayControl.Instance.Bitmap;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X >= 0 && e.Y >= 0 && e.X < image.Width && e.Y < image.Height)
                color_new.BackColor = image.GetPixel(e.X, e.Y);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (first)
            {
                color1.BackColor = color_new.BackColor;
                color_new.Location = new Point(color_new.Location.X, color2.Location.Y);
            }
            else
            {
                color2.BackColor = color_new.BackColor;
                color_new.Location = new Point(color_new.Location.X, color1.Location.Y);
            }

            first = !first;
        }
    }
}
