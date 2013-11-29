using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.RobotServices.Mindsqualls;

namespace Services.RobotServices
{
    public class RobotFactory
    {
        private static RobotFactory instance;
        private static MSQRobot robot;

        /// <summary>
        /// Singleton Instance Handler.
        /// </summary>
        /// <returns>Singleton Instance of object.</returns>
        public static RobotFactory GetInstance()
        {

            if (instance == null)
            {
                instance = new RobotFactory();
            }
            return instance;
        }

        /// <summary>
        /// Default cTor.
        /// </summary>
        private RobotFactory()
        {

        }


        public IRobot CreateRobot()
        {
            if (robot == null)
                return new PmanRobot();
            else return robot;
        }
    }
}
