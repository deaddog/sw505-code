using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RobotServices
{
    public interface IRobot
    {

        void TurnRobot(uint degrees);
        void TurnSensor(uint degrees);
        void Drive(bool forward, uint distanceInMM);
        ISensorData MeasureDistanceUsingSensor();
    }
}
