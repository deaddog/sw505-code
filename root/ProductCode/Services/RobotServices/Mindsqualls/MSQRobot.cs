using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKH.MindSqualls;
using NKH.MindSqualls.MotorControl;

namespace Services.RobotServices.Mindsqualls
{
    public class MSQRobot : IRobot
    {
        private const byte SERIAL_PORT_NUMBER = 10;
        private NxtBrick brick = new NxtBrick(NxtCommLinkType.Bluetooth, SERIAL_PORT_NUMBER);

        public MSQRobot()
        {

        }
        

        public void TurnRobot(uint degrees)
        {
            throw new NotImplementedException();
        }

        public void TurnSensor(uint degrees)
        {
            throw new NotImplementedException();
        }

        public void Drive(bool forward, uint distanceInMM)
        {
            throw new NotImplementedException();
        }

        public ISensorData MeasureDistanceUsingSensor()
        {
            


            throw new NotImplementedException();
        }
    }
}
