using CommonLib.Interfaces;
using CommonLib.DTOs;
using Services.TrackingServices;
using System;
using System.Drawing;
using System.Threading;

namespace Control
{
    /// <summary>
    /// Handles locating a robot using color tracking.
    /// </summary>
    public class LocationControl
    {
        private static readonly IPose defaultPose = new Pose(0.0f, 0.0f, 0.0);

        private OrientationTracker tracker;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationControl"/> class.
        /// </summary>
        public LocationControl()
            : this(new OrientationTracker(Color.Black, Color.White))
        {
        }
        private LocationControl(OrientationTracker tracker)
        {
            this.tracker = tracker;

            DisplayControl.Instance.ImageUpdated += (s, e) =>
                {
                    Bitmap bitmap = DisplayControl.Instance.Bitmap.Clone() as Bitmap;
                    Thread thread = new Thread(obj =>
                        {
                            Bitmap bmp = obj as Bitmap;
                            if (bmp == null)
                                return;

                            tracker.Update(bmp);
                            bmp.Dispose();
                        });
                    thread.Start(bitmap);
                };
        }

        public Color FrontColor
        {
            get { return tracker.FrontColor; }
            set { tracker.FrontColor = value; }
        }
        public Color RearColor
        {
            get { return tracker.RearColor; }
            set { tracker.RearColor = value; }
        }

        /// <summary>
        /// Gets the current pose of the tracked robot.
        /// </summary>
        public IPose RobotPose
        {
            get { return new Pose(tracker.Center, tracker.Orientation); }
        }
    }
}
