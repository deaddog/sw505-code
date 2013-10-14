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

            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;

            pictureBox3.Image = subtract(new Bitmap(Properties.Resources.snapshot), new Bitmap(Properties.Resources.snapshot__1_));
        }

        
        private Bitmap subtract(Bitmap bmp1, Bitmap bmp2)
        {
            Color pixelColor1, pixelColor2;
            byte R1, G1, B1, R2, G2, B2;

            Bitmap bmp = new Bitmap(bmp1.Width, bmp1.Height);
            for (int i = 0; i < bmp1.Width; i++)
            {
                for (int k = 0; k < bmp1.Height; k++)
                {
                    pixelColor1 = bmp1.GetPixel(i, k);
                    pixelColor2 = bmp2.GetPixel(i, k);

                    R1 = pixelColor1.R;
                    G1 = pixelColor1.G;
                    B1 = pixelColor1.B;

                    R2 = pixelColor2.R;
                    G2 = pixelColor2.G;
                    B2 = pixelColor2.B;

                    if (different(R1,R2) && different(G1,G2) && different(B1,B2))
                    {
                        bmp.SetPixel(i, k, Color.White);
                    }
                    else
                    {
                        bmp.SetPixel(i, k, Color.Black);
                    }
                }
            }
            return bmp;
        }

        private bool different(byte one, byte two)
        {
            if (Math.Abs(one - two) < 10)
            {
                return true;
            }
            else
                return false;
        }
    }
}
