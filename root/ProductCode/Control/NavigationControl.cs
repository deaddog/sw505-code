using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using Services.RobotServices;
using CommonLib.Interfaces;

namespace Control
{
    public class NavigationControl
    {
        private IRobot robot;

        #region cTor Chain.

        public NavigationControl() : this(RobotFactory.GetInstance()) { }

        public NavigationControl(RobotFactory factory)
        {
            robot = factory.CreateRobot();
        }

        #endregion

        public void TellRobotNavigateTo(ICoordinate location)
        {
            robot.MoveToPosition(location);
        }

        public void SendRobotToNextLocation()
        {
            throw new NotImplementedException();
        }
    }
}
