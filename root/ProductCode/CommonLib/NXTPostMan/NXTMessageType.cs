using System;

namespace CommonLib.NXTPostMan
{
    public enum NXTMessageType
    {
        /// <summary>
        /// The robot requests location
        /// </summary>
        RobotRequestsLocation = 0,
        /// <summary>
        /// The robot has arrived at destination
        /// </summary>
        RobotHasArrivedAtDestination = 1,
        /// <summary>
        /// Request robot moves to position
        /// </summary>
        MoveToPos = 2
    }
}
