using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BackgroundSubtraction;

namespace TrackColorForm
{
    public partial class Form1 : Form
    {
        double min = 200, max = 240;
        int thresholdmin = 500, thresholdmax = 1000;
        Image input;
        FrameDifferencing f;

        public Form1()
        {
            InitializeComponent();

            input = Bitmap.FromFile("snapshot.jpg");
            f = new FrameDifferencing();
            SBmax.Value = (int)max;
            SBmin.Value = (int)min;
            Nmin.Value = (decimal)min;
            Nmax.Value = (decimal)max;

            src.Image = input;


            CalculateImage();
        }

        private void CalculateImage()
        {
            output.Image = ColorTracking.TrackColor(input as Bitmap, min, max);
            contour.Image = FrameDifferencing.FindContour(output.Image as Bitmap, input as Bitmap, thresholdmin, thresholdmax);
        }

        private void SBmin_Scroll(object sender, ScrollEventArgs e)
        {
            min = e.NewValue;
            Nmin.Value = e.NewValue;
            CalculateImage();
        }

        private void SBmax_Scroll(object sender, ScrollEventArgs e)
        {
            max = e.NewValue;
            Nmax.Value = e.NewValue;
            CalculateImage();
        }

        private void Nmin_ValueChanged(object sender, EventArgs e)
        {
            min = (double)Nmin.Value;
            SBmin.Value = (int)Nmin.Value;
            CalculateImage();
        }

        private void Nmax_ValueChanged(object sender, EventArgs e)
        {
            max = (double)Nmax.Value;
            SBmax.Value = (int)Nmax.Value;
            CalculateImage();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            thresholdmin = (int)numericUpDown1.Value;
            CalculateImage();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            thresholdmax = (int)numericUpDown2.Value;
            CalculateImage();
        }



    }
}
