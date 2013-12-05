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
            SetImage(DisplayControl.Instance.Bitmap);
        }

        private void SetImage(Bitmap image)
        {
            this.image = image;

            if (image != null)
            {
                Size diff = this.Size - pictureBox1.Size;
                this.Size = image.Size + diff;
            }

            this.pictureBox1.Image = image;
        }

        public Color Color1
        {
            get { return color1.BackColor; }
        }
        public Color Color2
        {
            get { return color2.BackColor; }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (image != null && e.X >= 0 && e.Y >= 0 && e.X < image.Width && e.Y < image.Height)
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

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            SetImage(DisplayControl.Instance.Bitmap);
        }
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
