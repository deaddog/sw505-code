﻿using Microsoft.Kinect;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Services.KinectServices
{
    public class RgbStream
    {
        private const int CONNECT_SLEEP_TIME = 1000;

        private KinectSensor kinectSensor;
        private Bitmap currentImage;
        private object updaterLock;

        private Bitmap lastBitmap;
        private bool hasBeenLoaded;
        public Bitmap Bitmap
        {
            get { lock (updaterLock) { hasBeenLoaded = true; return lastBitmap; } }
        }

        private static RgbStream instance;
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
                    if (!hasBeenLoaded && lastBitmap != null)
                    {
                        lastBitmap.Dispose();
                        lastBitmap = null;
                    }

                    using (var frame = e.OpenColorImageFrame())
                        lastBitmap = ImageToBitmap(frame);

                    hasBeenLoaded = false;
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
