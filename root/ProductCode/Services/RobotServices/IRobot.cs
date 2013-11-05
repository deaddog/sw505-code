using System;
using System.Collections.Generic;
using CommonLib.Interfaces;

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
