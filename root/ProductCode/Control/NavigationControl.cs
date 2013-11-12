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

        public NavigationControl() : this(RobotFactory.getInstance()) { }

        public NavigationControl(RobotFactory factory)
        {
            robot = factory.createRobot();
        }

        public void TellRobotNavigateTo()
        {
            robot.MoveToPosition();
        }
    }
}
