using System;
using System.Collections.Generic;
using CommonLib.Interfaces;

namespace Services.RobotServices
{
    public interface IRobot
    {
        void TurnRobot(uint degrees, bool clockwise);
        void TurnSensor(uint degrees, bool clockwise);
        void Drive(bool forward, uint distanceInMM);
        void MoveToPosition(string position);
        ISensorData MeasureDistanceUsingSensor();
    }
}
