using System;

namespace CommonLib.NXTPostMan
{
    public enum NXTMessageType
    {
        /// <summary>
        /// Request robot moves to position
        /// </summary>
        MoveToPos = 0,
        /// <summary>
        /// The robot requests location
        /// </summary>
        RobotRequestsLocation = 1,
        /// <summary>
        /// The robot has arrived at destination
        /// </summary>
        RobotHasArrivedAtDestination = 2,
    }
}
