using CommonLib.Interfaces;
using Data;
using Services.RobotServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DTOs;
using Services.TrackingServices;
using Services.RouteServices;

namespace Control
{
    /// <summary>
    /// Controls the robot on the map
    /// </summary>
    public class MappingControl
    {
        private const int LOG_ODDS_BASE = 10;

        private IRobot robot;
        private IPose robotPose;
        private ISensorModel sensorModel;
        OccupancyGrid grid;
        Queue<ICoordinate> coordQueue;
        private int counter;

        private static MappingControl instance;

        public static MappingControl Instance
        {
            get
            {
                if (instance == null)
                    instance = new MappingControl();
                return instance;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingControl"/> class.
        /// </summary>
        private MappingControl()
        {
            robot = RobotFactory.GetInstance().CreateRobot();
            sensorModel = SensorModelFactory.GetInstance().CreateSimpleSensorModel();
            counter = 0;
        }

        /// <summary>
        /// Starts mapping, asks for route when needed
        /// </summary>
        public void Map(OccupancyGrid initialGrid)
        {
            this.grid = initialGrid;

            System.Threading.Thread mapThread = new System.Threading.Thread(mapAgain);
            mapThread.Start();
        }
        private void mapAgain()
        {
            if (counter < 3)
            {
                counter++;
                UpdateOccupancyGrid();
                coordQueue = new Queue<ICoordinate>(SchedulingService.Instance.GetRoute(grid));
            }
        }

        /// <summary>
        /// Sends the robot to next location, if any remain
        /// </summary>
        public void SendRobotToNextLocation()
        {
            if (coordQueue.Count > 0)
                robot.MoveToPosition(coordQueue.Dequeue());
            else
                this.mapAgain();
        }

        private ISensorData GetSensorData()
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
        public void UpdateOccupancyGrid()
        {
            robotPose = RobotLocation.Instance.RobotPose;
            ISensorData sensorReadings = GetSensorData();
            double[,] newMap = new double[grid.Columns, grid.Rows];

            for (int i = 0; i < grid.Columns; i++)
            {
                for (int j = 0; j < grid.Rows; j++)
                {
                    CellIndex cell = new CellIndex(i, j);
                    newMap[i, j] = grid[i, j];
                    if (cellIsInPerceptualRange(cell, grid.CellSize))
                    {
                        byte sensorReading = getCorrectSensorReading(
                            cell, grid.CellSize, sensorReadings
                            );
                        double newProbability = sensorModel.GetProbability(
                            grid, robotPose, cell, sensorReading
                            );
                        newMap[i, j] = logOddsInverse(
                            logOdds(grid[i, j]) + logOdds(newProbability) - logOdds(grid.InitialProbability)
                            );
                    }
                }
            }

            grid = new OccupancyGrid(newMap, grid.CellSize, grid.X, grid.Y);
            onGridUpdated(new GridUpdaterEventArgs(grid));
        }

        public delegate void GridUpdatedEventHandler(object sender, GridUpdaterEventArgs e);
        public event GridUpdatedEventHandler GridUpdated;

        protected virtual void onGridUpdated(GridUpdaterEventArgs e)
        {
            if (GridUpdated != null)
                GridUpdated(this, e);
        }

        public class GridUpdaterEventArgs : EventArgs
        {
            private readonly OccupancyGrid grid;

            public GridUpdaterEventArgs(OccupancyGrid grid)
            {
                this.grid = grid;
            }

            public OccupancyGrid Grid { get { return grid; } }
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
