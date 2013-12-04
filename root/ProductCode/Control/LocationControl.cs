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

        private LocationControl()
        {
            this.robLocation = RobotLocation.Instance;

            RgbStream.Instance.ImageUpdated += (s, e) =>
                {
                    Bitmap bitmap = RgbStream.Instance.Bitmap.Clone() as Bitmap;
                    Thread thread = new Thread(obj =>
                        {
                            Bitmap bmp = obj as Bitmap;
                            if (bmp == null)
                                return;

                            robLocation.Update(bmp);

                            bmp.Dispose();
                        });
                    thread.Start(bitmap);
                };
        }

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

        /// <summary>
        /// Gets the current pose of the tracked robot.
        /// </summary>
        public IPose RobotPose
        {
            get { return robLocation.RobotPose; }
        }
    }
}
