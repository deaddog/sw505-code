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
        /// <summary>
        /// Order the robot to send a test message to the PC.
        /// </summary>
        SendTestMessageFromRobot = 3
    }
}
