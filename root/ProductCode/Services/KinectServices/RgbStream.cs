using Microsoft.Kinect;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Services.KinectServices
{
    public class RgbStream
    {
        private KinectSensor kinectSensor;
        private Bitmap currentImage;
        private object updaterLock;

        private RgbStream(KinectSensor sensor)
        {
            this.kinectSensor = sensor;
            this.currentImage = null;
            this.updaterLock = new object();
        }

        private static Bitmap ImageToBitmap(ColorImageFrame Image)
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
