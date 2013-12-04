using CommonLib.DTOs;
using CommonLib.Interfaces;
using System;
using System.Drawing;

namespace Services.TrackingServices
{
    public class RobotLocation
    {
        private static RobotLocation instance;

        public static RobotLocation Instance
        {
            get
            {
                if (instance == null)
                    instance = new RobotLocation();

                return instance;
            }
        }

        private OrientationTracker tracker;

        private RobotLocation()
        {
            this.tracker = new OrientationTracker(Color.White, Color.Black);
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

        public void Update(Bitmap bitmap)
        {
            tracker.Update(bitmap);
        }
    }
}
