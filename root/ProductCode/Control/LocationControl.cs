using System;
using CommonLib.Interfaces;
using CommonLib.DTOs;
using Services.TrackingServices;
using System.Drawing;

namespace Control
{
    public class LocationControl
    {
        private ICoordinate defaultLocation = new Vector2D(5.0f,5.0f); 

        #region cTor Chain.


        /// <summary>
        /// Default cTor.
        /// </summary>
        public LocationControl() : this(new OrientationTracker(Color.Black, Color.White))  { }


        /// <summary>
        /// Master cTor.
        /// </summary>
        /// <param name="tracker">The tracker used to find robot.</param>
        public LocationControl(OrientationTracker tracker)
        {

        }


        #endregion

        public ICoordinate FindRobotLocation()
        {

            return defaultLocation;
        }

    }
}
