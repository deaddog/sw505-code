using CommonLib.Interfaces;
using Data;
using Services.RobotServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DTOs;

namespace Control
{
    /// <summary>
    /// Controls the robot on the map
    /// </summary>
    public class MappingControl
    {
        private const int LOG_ODDS_BASE = 10;

        private IPose robotPose;
        private IRobot robot;
        private ISensorModel sensorModel;
        OccupancyGrid grid;

        private static MappingControl instance;

        public static MappingControl GetInstance()
        {
            if (instance == null)
                instance = new MappingControl();
            return instance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingControl"/> class.
        /// </summary>
        private MappingControl() : this(RobotFactory.GetInstance()) { }

        private MappingControl(RobotFactory factory)
        {
            robot = factory.CreateRobot();
            sensorModel = SensorModelFactory.GetInstance().CreateSimpleSensorModel();
        }

        public void SendRobotToNextLocation()
        {
            throw new NotImplementedException();
        }

        public ISensorData GetSensorData()
        {
            return robot.GetSensorData();
        }

        /// <summary>
        /// Creates a new and updated occupancy grid.
        /// </summary>
        /// <param name="map">The old map.</param>
        /// <param name="model">The sensor model.</param>
        /// <param name="sensorReadings">The sensor readings.</param>
        /// <returns>An updated map</returns>
        public OccupancyGrid UpdateOccupancyGrid(OccupancyGrid map, ISensorModel model, ISensorData sensorReadings)
        {
            double[,] newMap = new double[map.Columns, map.Rows];

            for (int i = 0; i < map.Columns; i++)
            {
                for (int j = 0; j < map.Rows; j++)
                {
                    CellIndex cell = new CellIndex(i, j);
                    newMap[i, j] = map[i, j];
                    if (cellIsInPerceptualRange(cell, map.CellSize))
                    {
                        byte sensorReading = getCorrectSensorReading(
                            cell, map.CellSize, sensorReadings
                            );
                        double newProbability = model.GetProbability(
                            map, robotPose, cell, sensorReading
                            );
                        newMap[i, j] = logOddsInverse(
                            logOdds(map[i, j]) + logOdds(newProbability) - logOdds(map.InitialProbability)
                            );
                    }
                }
            }

            return new OccupancyGrid(newMap, map.CellSize, map.X, map.Y);
        }

        private bool cellIsInPerceptualRange(CellIndex mapCell, double cellSize)
        {
            //Calculate in which the robot is located
            int robotCellX = (int)Math.Floor(robotPose.X / cellSize);
            int robotCellY = (int)Math.Floor(robotPose.Y / cellSize);

            //If current map cell is in either same row or column as robot, return true
            return mapCell.X == robotCellX || mapCell.Y == robotCellY;
        }

        private byte getCorrectSensorReading(CellIndex mapCell, double cellSize, ISensorData sensorReadings)
        {
            //Calculate in which the robot is located
            int robotCellX = (int)Math.Floor(robotPose.X / cellSize);
            int robotCellY = (int)Math.Floor(robotPose.Y / cellSize);
            int robotAngle = (int)Math.Round(robotPose.Angle / 90.0) * 90;

            //Sets the relative position of the current cell as an angle, where angle 0 is x > 0 and y = 0
            int relativeAngle = 0;
            if (mapCell.Y > robotCellY)
                relativeAngle = 90;
            else if (mapCell.Y < robotCellY)
                relativeAngle = 270;
            else if (mapCell.X < robotCellX)
                relativeAngle = 180;

            //Make up for robot pose angle
            if (robotAngle > relativeAngle)
                relativeAngle = 360 - (Math.Abs(relativeAngle - robotAngle));
            else
                relativeAngle -= robotAngle;

            //Return sensor reading based on relative position of cell and robot pose angle
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
