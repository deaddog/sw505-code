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
        private const int LOG_ODDS_BASE = 10;

        IRobot robot;
        IPose robotPose;

        public MapControl() : this(RobotFactory.GetInstance()) { }

        public MapControl(RobotFactory factory)
        {
            robot = factory.CreateRobot();
        }

        public OccupancyGrid UpdateOccupancyGrid(OccupancyGrid map, ISensorModel model, ISensorData sensorReading)
        {
            double[,] newMap = new double[map.Columns, map.Rows];

            for (int i = 0; i < map.Rows - 1; i++)
            {
                for (int j = 0; j < map.Columns - 1; j++)
                {
                    if (cellIsInPerceptualField())
                        newMap[i, j] = logOddsInverse(
                            model.GetProbabilityUltrasonicSensorX(
                                map, robotPose, new CellIndex(i, j), getCorrectSensorReading()));
                }
            }

            return new OccupancyGrid(newMap, map.CellSize, map.X, map.Y);
        }

        private bool cellIsInPerceptualField()
        {
            throw new NotImplementedException();
        }

        private byte getCorrectSensorReading()
        {
            throw new NotImplementedException();
        }

        private double logOdds(double cellPropability)
        {
            return Math.Log(cellPropability / (1 - cellPropability), LOG_ODDS_BASE);
        }

        private double logOddsInverse(double cellLogOdds)
        {
            return 1 - (1 / (1 + Math.Pow(LOG_ODDS_BASE, cellLogOdds)));
        }
    }
}
