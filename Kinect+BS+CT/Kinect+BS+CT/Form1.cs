using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Kinect;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using BackgroundSubtraction;
using TrackColorForm;
using System.Threading;

namespace Kinect_BS_CT
{
    public partial class Form1 : Form
    {
        private KinectSensor k;

        //used for selecting which tracking method is selected
        private bool frameDiferencing = true; //if true = frame differencing else colortracking

        public Form1()
        {

            //Getting sensor from the kinect
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.k = potentialSensor;
                    break;
                }
            }

            //Starting the sensor
            this.k.Start();
            //Starting the colorstream
            this.k.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);

            // Wait for the sensor to be ready
            Thread.Sleep(2000);

            //Event for when a frame from the colorstream is ready
            this.k.ColorFrameReady += k_ColorFrameReady;

            InitializeComponent();

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        //current and old frame
        private Bitmap currentFrame;
        private Bitmap oldFrame;

        //used for getting the first frame for frame differencing
        private bool isOldFrameAssigned = false;

        void k_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            if (e.OpenColorImageFrame() != null)
            {
                if (frameDiferencing)
                {
                    frameDifferencing(e);
                }
                else
                {
                    colorTracking(e);
                }
            }
        }

        //Colortracking method
        private void colorTracking(ColorImageFrameReadyEventArgs e)
        {
            currentFrame = ImageToBitmap(e.OpenColorImageFrame());
            if (currentFrame != null)
            {
                pictureBox1.Image = ColorTracking.TrackColor(currentFrame, 298, 320);
            }
        }

        //Frame differencing method
        private void frameDifferencing(ColorImageFrameReadyEventArgs e)
        {
            if (isOldFrameAssigned == false)
            {
                oldFrame = ImageToBitmap(e.OpenColorImageFrame());
                if (oldFrame != null)
                {
                    isOldFrameAssigned = true;
                }
            }
            else if (isOldFrameAssigned)
            {
                currentFrame = ImageToBitmap(e.OpenColorImageFrame());
            }


            if (oldFrame != null && currentFrame != null)
            {
                pictureBox1.Image = oldFrame;
                Bitmap bmp = FrameDifferencing.GetDiff(oldFrame, currentFrame);
                pictureBox3.Image = bmp;
                pictureBox2.Image = FrameDifferencing.FindContour(bmp, currentFrame, 500, 5000);
            }
        }

        //ColorImageFram to Bitmap - used for converting CólorImageFram to Bitmap
        Bitmap ImageToBitmap(ColorImageFrame Image)
        {
            if (Image != null)
            {
                byte[] pixeldata = new byte[Image.PixelDataLength];
                Image.CopyPixelDataTo(pixeldata);
                Bitmap bmap = new Bitmap(Image.Width, Image.Height, PixelFormat.Format32bppRgb);
                BitmapData bmapdata = bmap.LockBits(
                    new Rectangle(0, 0, Image.Width, Image.Height),
                    ImageLockMode.WriteOnly,
                    bmap.PixelFormat);
                IntPtr ptr = bmapdata.Scan0;
                Marshal.Copy(pixeldata, 0, ptr, Image.PixelDataLength);
                bmap.UnlockBits(bmapdata);
                return bmap;
            }
            else
            {
                return null;
            }
        }
    }
}
