using System;

namespace CommonLib.NXTPostMan
{
    public enum NXTMessageType
    {
        // ################################
        // # Incoming Messages.           #
        // ################################

        /// <summary>
        /// The robot requests location
        /// </summary>
        RobotRequestsLocation = 0,
        /// <summary>
        /// The robot has arrived at destination
        /// </summary>
        RobotHasArrivedAtDestination = 1,
        
        SendSensorData = 2,

        // ################################
        // # Outgoing Messages.           #
        // ################################
        
        /// <summary>
        /// Request robot moves to position
        /// </summary>
        MoveToPos = 50,


        GetSensorMeasurement = 51,

        /// <summary>
        /// Order the robot to send a test message to the PC.
        /// </summary>
        SendTestMessageFromRobot = 52


    }
}
