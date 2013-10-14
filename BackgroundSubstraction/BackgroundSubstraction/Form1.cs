using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

namespace BackgroundSubstraction
{
    public partial class Form1 : Form
    {
        const double Threshold = 60;
        const double ContourThresh = 3;
        public Form1()
        {

            InitializeComponent();
            pictureBox1.Image = Properties.Resources.snapshot;
            pictureBox2.Image = Properties.Resources.snapshot__1_;

            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            
            //Image<Bgr, Byte> img = RGBImageMaskBGSubtraction(Properties.Resources.snapshot__1_, true).AbsDiff(RGBImageMaskBGSubtraction(Properties.Resources.snapshot__1_, false));
            //img = img.ThresholdBinary(new Bgr(threshold, threshold, threshold), new Bgr(255,255,255));

            //pictureBox3.Image = img.ToBitmap();

            Image<Bgr, Byte> Frame = new Image<Bgr, byte>(Properties.Resources.snapshot);
            Image<Bgr, Byte> Previous_Frame = new Image<Bgr, byte>(Properties.Resources.snapshot__1_);
            Image<Bgr, Byte> Difference;


            Difference = Previous_Frame.AbsDiff(Frame); //find the absolute difference 
            /*Play with the value 60 to set a threshold for movement*/
            Difference = Difference.ThresholdBinary(new Bgr(Threshold, Threshold, Threshold), new Bgr(255, 255, 255)); //if value > 60 set to 255, 0 otherwise 
            pictureBox2.Image = Difference.ToBitmap();

            pictureBox3.Image = subtract(new Bitmap(Properties.Resources.snapshot), new Bitmap(Properties.Resources.snapshot__1_));


            /*Previous_Frame = Frame.Copy(); //copy the frame to act as the previous frame
            using (MemStorage storage = new MemStorage()) //allocate storage for contour approximation
                //detect the contours and loop through each of them
                for (Contour<Point> contours = Difference.Convert<Gray, Byte>().FindContours(
                      Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
                      Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_LIST,
                      storage);
                   contours != null;
                   contours = contours.HNext)
                {
                    //Create a contour for the current variable for us to work with
                    Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.05, storage);

                    //Draw the detected contour on the image
                    if (currentContour.Area > ContourThresh) //only consider contours with area greater than 100 as default then take from form control
                    {
                        Frame.Draw(currentContour.BoundingRectangle, new Bgr(Color.Red), 2);
                    }
                }

            pictureBox3.Image = Frame.ToBitmap();*/


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

                    if (different(R1, R2) && different(G1, G2) && different(B1, B2))
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

        private Image<Bgr, Byte> RGBImageMaskBGSubtraction(Bitmap bmp, bool displayResult)
        {
            // create new image
            Image<Bgr, Byte> img = new Image<Bgr, Byte>(bmp);

            //convert to grayscale
            Image<Gray, Byte> gray = img.Convert<Gray, Byte>();

            //convert to binary image using the threshold
            gray = gray.ThresholdBinary(new Gray(Threshold), new Gray(255));

            // copy pixels from the original image where pixels in 
            // mask image is nonzero
            Image<Bgr, Byte> newimg = img.Copy(gray);
            
            // display result
            if (displayResult) this.pictureBox3.Image = newimg.ToBitmap();

            return newimg;

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
