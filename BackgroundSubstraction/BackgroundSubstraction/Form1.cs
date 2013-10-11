using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackgroundSubstraction
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
            pictureBox1.Image = Properties.Resources.snapshot;
            pictureBox2.Image = Properties.Resources.snapshot__1_;

            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox3.SizeMode = PictureBoxSizeMode.AutoSize;

            pictureBox3.Image = subtract(new Bitmap(pictureBox1.Image), new Bitmap(pictureBox2.Image));
        }

        
        private Bitmap subtract(Bitmap bmp1, Bitmap bmp2)
        {
            Bitmap bmp = new Bitmap(bmp1.Width, bmp1.Height);
            for (int i = 0; i < bmp1.Width; i++)
            {
                for (int k = 0; k < bmp1.Height; k++)
                {
                    if (bmp1.GetPixel(i,k) == bmp2.GetPixel(i,k))
                    {
                        bmp.SetPixel(i, k, Color.White);
                    }
                    else
                    {
                        bmp.SetPixel(i, k, bmp2.GetPixel(i, k));
                    }
                }
            }
            return bmp;
        }
    }
}
