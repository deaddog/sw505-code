using Microsoft.Kinect;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Services.KinectServices
{
    /// <summary>
    /// Provides a stream of <see cref="Bitmap"/> images from a Microsoft Kinect.
    /// </summary>
    public class RgbStream
    {
        private const int CONNECT_SLEEP_TIME = 1000;

        private KinectSensor kinectSensor;
        private Bitmap currentImage;
        private object updaterLock;

        /// <summary>
        /// Gets the last bitmap captured by the Kinect.
        /// Notice that the bitmap is automatically disposed when a new image is ready - use <see cref="ImageUpdated"/> for notifications.
        /// </summary>
        public Bitmap Bitmap
        {
            get { return currentImage; }
        }

        private static RgbStream instance;
        /// <summary>
        /// Gets the singleton instance of <see cref="RgbStream"/>.
        /// </summary>
        public static RgbStream Instance
        {
            get
            {
                if (instance == null)
                {
                    KinectSensor sensor;

                    var sensors = from s in KinectSensor.KinectSensors
                                  where s.Status == KinectStatus.Connected
                                  select s;

                    while ((sensor = sensors.FirstOrDefault()) == null)
                        System.Threading.Thread.Sleep(CONNECT_SLEEP_TIME);

                    sensor.Start();

                    instance = new RgbStream(sensor);
                }

                return instance;
            }
        }

        private RgbImageFormats format;
        /// <summary>
        /// Gets or sets the image format used by this <see cref="RgbStream"/>.
        /// </summary>
        public RgbImageFormats ImageFormat
        {
            get { return format; }
            set
            {
                kinectSensor.ColorStream.Disable();

                this.format = value;

                kinectSensor.ColorStream.Enable((ColorImageFormat)this.format);
            }
        }

        /// <summary>
        /// Occurs when the <see cref="RgbStream.Bitmap"/> property is updated with a new image.
        /// After this event occurs, the previous image is automatically disposed.
        /// </summary>
        public event EventHandler ImageUpdated;

        private RgbStream(KinectSensor sensor)
        {
            this.kinectSensor = sensor;
            this.currentImage = null;
            this.updaterLock = new object();

            //Start the sensor and wait for it to be ready
            this.kinectSensor.Start();
            while (!this.kinectSensor.IsRunning) { }

            //Event for when a frame from the colorstream is ready
            this.kinectSensor.ColorFrameReady += (s, e) =>
            {
                lock (updaterLock)
                {
                    Bitmap old = currentImage;

                    using (var frame = e.OpenColorImageFrame())
                        currentImage = ImageToBitmap(frame);

                    if (ImageUpdated != null)
                        ImageUpdated(this, EventArgs.Empty);

                    if (old != null)
                    {
                        old.Dispose();
                        old = null;
                    }
                }
            };
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
