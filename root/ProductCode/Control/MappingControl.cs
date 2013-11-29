using CommonLib.Interfaces;
using Data;
using Services.RobotServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control
{
    /// <summary>
    /// Controls the robot on the map
    /// </summary>
    public class MappingControl
    {
        IRobot robot;
        OccupancyGrid grid;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingControl"/> class.
        /// </summary>
        public MappingControl() : this(RobotFactory.GetInstance()) { }

        private MappingControl(RobotFactory factory)
        {
            robot = factory.CreateRobot();
        }
    }
}
