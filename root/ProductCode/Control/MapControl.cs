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

        public OccupancyGrid UpdateOccupancyGrid(OccupancyGrid map, ISensorModel model, ISensorData sensorReadings)
        {
            double[,] newMap = new double[map.Columns, map.Rows];

            for (int i = 0; i < map.Rows - 1; i++)
            {
                for (int j = 0; j < map.Columns - 1; j++)
                {
                    if (cellIsInPerceptualRange(new CellIndex(i, j), map.CellSize))
                    {
                        double newProbability = logOddsInverse(model.GetProbabilityUltrasonicSensorX(
                            map, robotPose, new CellIndex(i, j),
                            getCorrectSensorReading(new CellIndex(i, j), map.CellSize, sensorReadings)));
                        newMap[i, j] = logOdds(map[i, j]) + newProbability - logOdds(map.InitialProbability);
                    }
                }
            }

            return new OccupancyGrid(newMap, map.CellSize, map.X, map.Y);
        }

        private bool cellIsInPerceptualRange(IIndex mapCell, double cellSize)
        {
            int robotCellX = (int)Math.Floor(robotPose.X / cellSize);
            int robotCellY = (int)Math.Floor(robotPose.Y / cellSize);

            return mapCell.X == robotCellX || mapCell.Y == robotCellY;
        }

        private byte getCorrectSensorReading(IIndex mapCell, double cellSize, ISensorData sensorReadings)
        {
            int robotCellX = (int)Math.Floor(robotPose.X / cellSize);
            int robotCellY = (int)Math.Floor(robotPose.Y / cellSize);
            int robotAngle = (int)Math.Round(robotPose.Angle / 90.0) * 90;

            int relativeAngle = 0;
            if (mapCell.Y > robotCellY)
                relativeAngle = 90;
            else if (mapCell.Y < robotCellY)
                relativeAngle = 270;
            else if (mapCell.X < robotCellX)
                relativeAngle = 180;

            relativeAngle = Math.Abs(relativeAngle - robotAngle) % 360;

            if (relativeAngle == 0)
                return sensorReadings.SensorFront;
            else if (relativeAngle == 90)
                return sensorReadings.SensorLeft;
            else if (relativeAngle == 180)
                return sensorReadings.SensorBack;
            else
                return sensorReadings.SensorRight;
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
