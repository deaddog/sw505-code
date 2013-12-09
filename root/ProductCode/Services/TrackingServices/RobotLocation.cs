using CommonLib;
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
        private CoordinateConverter converter;
        private bool hasbeenupdated;

        private RobotLocation()
        {
            this.converter = new CoordinateConverter(1, 1, 1, 1);
            this.tracker = new OrientationTracker(Color.White, Color.Black, converter);
        }

        public void SetImageSize(int width, int height)
        {
            this.converter.SetPixelSize(width, height);
        }
        public void SetActualSize(float width, float height)
        {
            this.converter.SetActualSize(width, height);
        }

        public Vector2D Front { get { return tracker.Front; } }
        public Vector2D Rear { get { return tracker.Rear; } }

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
            get { while (!hasbeenupdated) { } return new Pose(tracker.Center, tracker.Orientation); }
        }

        public void Update(Bitmap bitmap)
        {
            tracker.Update(bitmap);
            hasbeenupdated = true;
        }
    }
}
