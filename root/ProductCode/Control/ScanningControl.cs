﻿using System;
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
        private const ushort FULLSWEEP_DEGREE_INTERVAL = 1;
        private const bool CLOCKWISE = true;

        #region cTor Chain

        // Default cTor.
        public ScanningControl() : this(RobotFactory.getInstance()) { }

        // Master cTor.
        public ScanningControl(RobotFactory factory)
        {
            rob = factory.createRobot();
            
        }

        #endregion

        public SensorSweepDTO FullSweep()
        {
            ISensorData[] data = new ISensorData[DEGREES_IN_CIRCLE];

            for (ushort i = 0; i < DEGREES_IN_CIRCLE; i=(ushort)(i+FULLSWEEP_DEGREE_INTERVAL))
            {
                rob.TurnSensor(FULLSWEEP_DEGREE_INTERVAL, CLOCKWISE);
                data[i] = rob.MeasureDistanceUsingSensor();
            }

            return new SensorSweepDTO(data);
        }
    }
}
