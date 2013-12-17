using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DTOs;
using CommonLib.Interfaces;

namespace Data.SensorModel
{
    public abstract class AbstractSensorModel :ISensorModel
    {
        protected const double OCCUPIED_CELL_PROBABILITY = .6;
        protected const double FREE_CELL_PROBABILITY = .4;
        protected const double NEAR_CELL_PROBABILITY = .1;
        protected const double SMALL_MEASUREMENT_PROBABILITY = .55;

        protected const double MAXIMUM_SENSOR_RANGE_CM = 170;
		protected const double MINIMIM_SENSOR_RANGE_CM = 25;
        protected const double ROBOT_RADIUS_CM = 10;
        protected const double AVERAGE_OBSTACLE_DEPTH_CM = 30;
        protected readonly double AVERAGE_OBSTACLE_DEPTH_HALF;

        protected readonly double initialProbability;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleSensorModel"/> class.
        /// </summary>
        /// <param name="cellDepthCM">The cell depth cm.</param>
        public AbstractSensorModel()
        {
            this.initialProbability = OccupancyGrid.INITIAL_PROBABILITY;
            this.AVERAGE_OBSTACLE_DEPTH_HALF = AVERAGE_OBSTACLE_DEPTH_CM / 2;
        }

        public double GetProbability(OccupancyGrid grid, IPose robot, CellIndex cell, byte sensorX)
        {
            ICoordinate c = grid.GetCellCenter(cell);
            double r = Math.Abs(c.X - robot.X + c.Y - robot.Y);

            if (r < ROBOT_RADIUS_CM)
                return NEAR_CELL_PROBABILITY;
            else if (r > Math.Min(MAXIMUM_SENSOR_RANGE_CM, sensorX + AVERAGE_OBSTACLE_DEPTH_HALF))
                return initialProbability;
            else if (sensorX - AVERAGE_OBSTACLE_DEPTH_HALF <= r && r <= sensorX + AVERAGE_OBSTACLE_DEPTH_HALF)
                return sensorX < MINIMIM_SENSOR_RANGE_CM ? SMALL_MEASUREMENT_PROBABILITY : GetProbabilityInAlphaRange(grid, robot, c, r, sensorX);
            else
                return sensorX < MINIMIM_SENSOR_RANGE_CM ? SMALL_MEASUREMENT_PROBABILITY : FREE_CELL_PROBABILITY;
        }

        protected abstract double GetProbabilityInAlphaRange(OccupancyGrid grid, IPose robotPose, ICoordinate cellCenter, double distance, byte sensorX);
    }
}
