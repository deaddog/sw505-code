using System;
using CommonLib.Interfaces;
using CommonLib.DTOs;
using Services.TrackingServices;
using System.Drawing;

namespace Control
{
    public class LocationControl
    {
        private static readonly IPose defaultPose = new Pose(0.0f, 0.0f, 0.0);

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationControl"/> class.
        /// </summary>
        public LocationControl() : this(new OrientationTracker(Color.Black, Color.White)) { }

        private LocationControl(OrientationTracker tracker)
        {

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
