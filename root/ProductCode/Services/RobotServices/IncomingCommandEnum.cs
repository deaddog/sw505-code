﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RobotServices
{
    /// <summary>
    /// Codes for incoming commands
    /// </summary>
    public enum IncomingCommand
    {
        /// <summary>
        /// The robot requests location
        /// </summary>
        RobotRequestsLocation = 48,
        /// <summary>
        /// The robot has arrived at destination
        /// </summary>
        RobotHasArrivedAtDestination = 49,
        /// <summary>
        /// The robot sends sensor data
        /// </summary>
        GetSensorData = 50
    }
}
