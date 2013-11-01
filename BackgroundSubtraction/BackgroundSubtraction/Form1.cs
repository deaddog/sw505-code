using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackgroundSubtraction
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = Properties.Resources.snapshot2;
            pictureBox2.Image = Properties.Resources.snapshot3;

            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.CenterImage;

            

            Bitmap diff =FrameDifferencing.GetDiff(Properties.Resources.snapshot3, Properties.Resources.snapshot2);
            pictureBox4.Image = diff;
            pictureBox3.Image = FrameDifferencing.FindContour(diff, Properties.Resources.snapshot3, 3000, 5000);
        }

    }
}
