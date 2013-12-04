using System;
using System.Collections.Generic;
using CommonLib.Interfaces;

namespace Services.RobotServices
{
    public interface IRobot
    {
        void MoveToPosition(ICoordinate destination);
        ISensorData GetSensorData();
    }
}
