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
        /// <summary>
        /// The robot sends sensor data
        /// </summary>
        SendSensorData = 2,

        // ################################
        // # Outgoing Messages.           #
        // ################################
        
        /// <summary>
        /// Request robot moves to position
        /// </summary>
        MoveToPos = 50,

        /// <summary>
        /// Request the robot for sensor data
        /// </summary>
        GetSensorMeasurement = 51,

        /// <summary>
        /// Send postion to robot
        /// </summary>
        SendPostion = 52,
    }
}
