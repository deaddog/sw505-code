using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RobotServices
{
    /// <summary>
    /// Code for outgoing commands
    /// </summary>
    public enum OutgoingCommand
    {
        /// <summary>
        /// Move to position
        /// </summary>
        MoveToPos = 0,
        /// <summary>
        /// Get sensor data
        /// </summary>
        GetSensorData = 1
    }
}
