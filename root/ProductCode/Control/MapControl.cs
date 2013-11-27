using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.RobotServices;
using Data;
using CommonLib.DTOs;
using CommonLib.Interfaces;

namespace Control
{
    public class MapControl
    {
        IRobot robot;
        IPose robotPose;

        public MapControl() : this(RobotFactory.GetInstance()) { }

        public MapControl(RobotFactory factory)
        {
            robot = factory.CreateRobot();
        }

        public OccupancyGrid UpdateOccupancyGrid(OccupancyGrid map, ISensorModel model, SensorSweepDTO sensorReading)
        {
            for (int i = 0; i < map.GridRows - 1; i++)
            {
                for (int j = 0; j < map.GridColumns - 1; j++)
                {
                    if (cellIsInPerceptualField(i, j, sensorReading))
                        break;
                }
            }

            throw new NotImplementedException();
        }

        private bool cellIsInPerceptualField(int cellX, int cellY, SensorSweepDTO sensorReading)
        {
            throw new NotImplementedException();
        }
    }
}
