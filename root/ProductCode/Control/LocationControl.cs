using System;
using CommonLib.Interfaces;
using CommonLib.DTOs;
using Services.TrackingServices;
using System.Drawing;

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
            get { return defaultPose; }
        }
    }
}
