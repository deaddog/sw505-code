using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DTOs;

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

        protected readonly double initialProbability;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleSensorModel"/> class.
        /// </summary>
        /// <param name="cellDepthCM">The cell depth cm.</param>
        public AbstractSensorModel()
        {
            this.initialProbability = OccupancyGrid.INITIAL_PROBABILITY;
        }

        public abstract double GetProbability(OccupancyGrid grid, CommonLib.Interfaces.IPose robot, CellIndex cell, byte sensorX);
    }
}
