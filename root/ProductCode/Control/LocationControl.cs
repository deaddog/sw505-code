﻿using CommonLib.Interfaces;
using CommonLib.DTOs;
using System;
using System.Drawing;
using System.Threading;
using Services.KinectServices;
using Services.TrackingServices;

namespace Control
{
    /// <summary>
    /// Handles locating a robot using color tracking.
    /// </summary>
    public class LocationControl
    {
        private RobotLocation robLocation;

        private static LocationControl instance;
        public static LocationControl Instance
        {
            get
            {
                if (instance == null)
                    instance = new LocationControl();

                return instance;
            }
        }

        private object updateLock = new object();
        private Thread updateThread = null;

        private LocationControl()
        {
            this.robLocation = RobotLocation.Instance;

            RgbStream.Instance.ImageUpdated += (s, e) =>
                {
                    lock (updateLock)
                        if (updateThread == null || !updateThread.IsAlive)
                        {
                            Bitmap bitmap = RgbStream.Instance.Bitmap.Clone() as Bitmap;
                            updateThread = new Thread(UpdateLocation);
                            updateThread.Start(bitmap);
                        }
                };
        }

        private void UpdateLocation(object obj)
        {
            Bitmap bmp = obj as Bitmap;
            if (bmp == null)
                return;

            robLocation.Update(bmp);
            if (RobotPoseChanged != null)
                RobotPoseChanged(this, EventArgs.Empty);

            bmp.Dispose();
        }

        public void SetImageSize(int width, int height)
        {
            robLocation.SetImageSize(width, height);
        }
        public void SetActualSize(float width, float height)
        {
            robLocation.SetActualSize(width, height);
        }

        public Vector2D Front { get { return robLocation.Front; } }
        public Vector2D Rear { get { return robLocation.Rear; } }

        public Color FrontColor
        {
            get { return robLocation.FrontColor; }
            set { robLocation.FrontColor = value; }
        }
        public Color RearColor
        {
            get { return robLocation.RearColor; }
            set { robLocation.RearColor = value; }
        }

        public event EventHandler RobotPoseChanged;

        /// <summary>
        /// Gets the current pose of the tracked robot.
        /// </summary>
        public IPose RobotPose
        {
            get { return robLocation.RobotPose; }
        }
    }
}
