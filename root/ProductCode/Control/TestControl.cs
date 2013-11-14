using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Services;
using Services.RobotServices;

namespace Control
{
    public class TestControl
    {
        IRobot robot;

        public TestControl() : this(RobotFactory.GetInstance()) { }

        public TestControl(RobotFactory factory)
        {
            robot = factory.CreateRobot();
        }

        public string TestRobotHasArrived()
        {
            return robot.CheckIncoming(IncomingCommand.RobotHasArrivedAtDestination);
        }
    }
}
