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

        private Differentiater diff;

        public Form1()
        {
            if (!DesignMode)
            {
                //Getting sensor from the kinect
                this.k = (from sensor in KinectSensor.KinectSensors
                          where sensor.Status == KinectStatus.Connected
                          select sensor).FirstOrDefault();

                //Starting the sensor
                this.k.Start();
                //Starting the colorstream
                this.k.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);

                // Wait for the sensor to be ready
                while (!k.IsRunning) { }

                diff = new ColorTracker();
                diff.UpdatePicture1 += (s, e) => pictureBox1.Image = diff.Picture1;
                diff.UpdatePicture2 += (s, e) => pictureBox2.Image = diff.Picture2;
                diff.UpdatePicture3 += (s, e) => pictureBox3.Image = diff.Picture3;

                //Event for when a frame from the colorstream is ready
                this.k.ColorFrameReady += (s, e) =>
                    {
                        Bitmap bmp;
                        using (var frame = e.OpenColorImageFrame())
                            bmp = ImageToBitmap(frame);

                        if (bmp == null)
                            return;

                        diff.Start(bmp);
                    };
            }

            InitializeComponent();
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
