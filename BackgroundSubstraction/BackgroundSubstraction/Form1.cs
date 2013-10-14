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
        const double Threshold = 50;
        const double ContourThresh = 500;
        public Form1()
        {

            InitializeComponent();
            pictureBox1.Image = Properties.Resources.snapshot2;
            pictureBox2.Image = Properties.Resources.snapshot3;

            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;

            Image<Bgr, Byte> Frame = new Image<Bgr, byte>(Properties.Resources.snapshot3);
            Image<Bgr, Byte> Previous_Frame = new Image<Bgr, byte>(Properties.Resources.snapshot2);
            Image<Bgr, Byte> Difference;

            Difference = Previous_Frame.AbsDiff(Frame); //find the absolute difference 
            Difference = Difference.ThresholdBinary(new Bgr(Threshold, Threshold, Threshold), new Bgr(255, 255, 255));
            pictureBox4.Image = Difference.ToBitmap();

            Previous_Frame = Frame.Copy(); //copy the frame to act as the previous frame

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

            pictureBox3.Image = Frame.ToBitmap();
        }

    }
}
