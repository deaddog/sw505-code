using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RobotServices.Mindsqualls
{
    public class MSQRobot : IRobot
    {


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
