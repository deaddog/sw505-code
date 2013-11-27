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
            double[,] newMap = new double[map.Columns, map.Rows];

            for (int i = 0; i < map.Rows - 1; i++)
            {
                for (int j = 0; j < map.Columns - 1; j++)
                {
                    if (cellIsInPerceptualField(i, j, sensorReading))
                        break;
                }
            }

            return new OccupancyGrid(newMap, map.CellSize, map.X, map.Y);
        }

        private bool cellIsInPerceptualField(int cellX, int cellY, SensorSweepDTO sensorReading)
        {
            throw new NotImplementedException();
        }
    }
}
