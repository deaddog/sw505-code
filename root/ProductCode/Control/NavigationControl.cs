using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using Services.RobotServices;

namespace Control
{
    public class NavigationControl
    {
        private IRobot robot;

        public NavigationControl() : this(RobotFactory.GetInstance()) { }

        public NavigationControl(RobotFactory factory)
        {
            robot = factory.CreateRobot();
        }

        public void TellRobotNavigateTo()
        {
            robot.MoveToPosition();
        }
    }
}
