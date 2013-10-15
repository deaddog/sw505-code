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

namespace Kinect_BS_CT
{
    public partial class Form1 : Form
    {
        private KinectSensor k;
        public Form1()
        {

            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.k = potentialSensor;
                    break;
                }
            }

            this.k.Start();
            this.k.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);

            this.k.ColorFrameReady += k_ColorFrameReady;

            InitializeComponent();

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private Bitmap current;
        private Bitmap oldFrame;
        private bool isCurrent = false;
        void k_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {

            if (e.OpenColorImageFrame() != null)
            {
                if (isCurrent == false)
                {
                    oldFrame = ImageToBitmap(e.OpenColorImageFrame());
                    if (oldFrame != null)
                    {
                        isCurrent = true;
                    }
                }

             current = ImageToBitmap(e.OpenColorImageFrame());


                if (oldFrame != null && current != null)
                {
                    pictureBox1.Image = oldFrame;
                    pictureBox2.Image = FrameDifferencing.FindContour(FrameDifferencing.Diff(oldFrame, current), current, 3000, 5000);
                }
            }

        }

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
