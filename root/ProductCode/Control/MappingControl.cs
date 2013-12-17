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
using CommonLib;

namespace Control
{
    /// <summary>
    /// Controls the robot on the map
    /// </summary>
    public class MappingControl
    {
        private const int AMOUNT_OF_POINTS = 1000;
        private const float DISTANCE_BETWEEN_SENSORS_AND_ROBOT_MID_IN_CM = 11f;

        private IRobot robot;
        private IPose robotPose;

        private ISensorModel sensorModel;
        private OccupancyGrid grid;

        private IScheduler scheduler;
        private Queue<ICoordinate> coordQueue;
        private int counter;

        private static MappingControl instance;

        public static MappingControl Instance
        {
            get
            {
                if (instance == null)
                {
                    IRobot robot = RobotFactory.GetInstance().CreateRobot();
                    ISensorModel sensorModel = SensorModelFactory.GetInstance().CreateSimpleSensorModel();
                    IScheduler scheduler = SchedulerFactory.Instance.GetAutomatedScheduler();
                    instance = new MappingControl(robot, sensorModel, scheduler);
                }
                return instance;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MappingControl"/> class.
        /// </summary>
        private MappingControl(IRobot robot, ISensorModel sensorModel, IScheduler scheduler)
        {
            this.robot = robot;
            this.sensorModel = sensorModel;
            this.scheduler = scheduler;
            counter = 0;
        }

        /// <summary>
        /// Starts mapping, asks for route when needed
        /// </summary>
        public void Map(OccupancyGrid initialGrid)
        {
            this.grid = initialGrid;

            new System.Threading.Thread(() =>
                {
                    coordQueue = new Queue<ICoordinate>(scheduler.GetRoute(RobotLocation.Instance.RobotPose, grid));
                    SendRobotToNextLocation();
                }).Start();
        }
        private void mapAgain()
        {
            if (counter < AMOUNT_OF_POINTS)
            {
                counter++;

                UpdateOccupancyGrid();
                UpdateOccupancyGrid();
                coordQueue = new Queue<ICoordinate>(scheduler.GetRoute(robotPose, grid));
                SendRobotToNextLocation();
            }

        }

        /// <summary>
        /// Sends the robot to next location, if any remain
        /// </summary>
        public void SendRobotToNextLocation()
        {
            if (coordQueue.Count > 0)
            {
                ICoordinate destination = coordQueue.Dequeue();
                onDestinationUpdated(new DestinationUpdatedEventArgs(destination));
                robot.MoveToPosition(destination);
            }
            else
                new System.Threading.Thread(() => this.mapAgain()).Start();
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
            ISensorData sensorReadings = GetSensorData();
            double[,] newMap = new double[grid.Columns, grid.Rows];

            robotPose = RobotLocation.Instance.RobotPose;
            double a = robotPose.Angle * (Math.PI / 180);
            robotPose = new Pose(robotPose.X + (float)Math.Cos(a) * DISTANCE_BETWEEN_SENSORS_AND_ROBOT_MID_IN_CM, robotPose.Y + (float)Math.Sin(a) * DISTANCE_BETWEEN_SENSORS_AND_ROBOT_MID_IN_CM, robotPose.Angle);

            for (int i = 0; i < grid.Columns; i++)
            {
                for (int j = 0; j < grid.Rows; j++)
                {
                    CellIndex cell = new CellIndex(i, j);
                    newMap[i, j] = grid[i, j];
                    if (cellIsInPerceptualRange(cell))
                    {
                        byte sensorReading = getCorrectSensorReading(cell, sensorReadings);
                        double newProbability = sensorModel.GetProbability(grid, robotPose, cell, sensorReading);
                        newMap[i, j] = ExtendedMath.logOddsInverse(
                            ExtendedMath.logOdds(grid[i, j]) + ExtendedMath.logOdds(newProbability) - ExtendedMath.logOdds(OccupancyGrid.INITIAL_PROBABILITY)
                            );
                    }
                }
            }

            grid = new OccupancyGrid(newMap, grid.CellSize, grid.X, grid.Y);
            onGridUpdated(new GridUpdatedEventArgs(grid));
        }

        public delegate void GridUpdatedEventHandler(object sender, GridUpdatedEventArgs e);
        public event GridUpdatedEventHandler GridUpdated;

        protected virtual void onGridUpdated(GridUpdatedEventArgs e)
        {
            if (GridUpdated != null)
                GridUpdated(this, e);
        }


        public delegate void DestinationUpdatedEventHandler(object sender, DestinationUpdatedEventArgs e);
        public event DestinationUpdatedEventHandler DestinationUpdated;

        protected virtual void onDestinationUpdated(DestinationUpdatedEventArgs e)
        {
            if (DestinationUpdated != null)
                DestinationUpdated(this, e);
        }

        public class GridUpdatedEventArgs : EventArgs
        {
            private readonly OccupancyGrid grid;

            public GridUpdatedEventArgs(OccupancyGrid grid)
            {
                this.grid = grid;
            }

            public OccupancyGrid Grid
            {
                get { return grid; }
            }
        }

        public class DestinationUpdatedEventArgs : EventArgs
        {
            private readonly ICoordinate destination;

            public DestinationUpdatedEventArgs(ICoordinate destination)
            {
                this.destination = destination;
            }

            public ICoordinate Destination
            {
                get { return destination; }
            }
        }

        private bool cellIsInPerceptualRange(CellIndex mapCell)
        {
            //Calculate in which the robot is located
            CellIndex robotCell = grid.GetIndex(robotPose);

            //If current map cell is in either same row or column as robot, return true
            return mapCell.X == robotCell.X || mapCell.Y == robotCell.Y;
        }

        private byte getCorrectSensorReading(CellIndex mapCell, ISensorData sensorReadings)
        {
            //Calculate in which the robot is located
            CellIndex robotCell = grid.GetIndex(robotPose);
            int robotAngle = (int)Math.Round(robotPose.Angle / 90.0) * 90;

            //Sets the relative position of the current cell as an angle, where angle 0 is x > 0 and y = 0
            int relativeAngle = 0;
            if (mapCell.Y > robotCell.Y)
                relativeAngle = 90;
            else if (mapCell.Y < robotCell.Y)
                relativeAngle = 270;
            else if (mapCell.X < robotCell.X)
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

    }
}
