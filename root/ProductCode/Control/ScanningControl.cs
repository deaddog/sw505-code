using System;
using CommonLib.Interfaces;
using CommonLib.DTOs;
using Services;
using Services.RobotServices;

namespace Control
{
    public class ScanningControl
    {
        private IRobot rob;
        private const ushort DEGREES_IN_CIRCLE = 360;
        private const ushort DEGREES_TO_SCAN = DEGREES_IN_CIRCLE / 2;
        private const ushort FULLSWEEP_DEGREE_INTERVAL = 5;
        private const bool CLOCKWISE = true;

        #region cTor Chain

        // Default cTor.
        public ScanningControl() : this(RobotFactory.GetInstance()) { }

        // Master cTor.
        public ScanningControl(RobotFactory factory)
        {
            rob = factory.CreateRobot();
            
        }

        #endregion

        public void GetSensorData()
        {
            rob.GetSensorData();
        }
    }
}
