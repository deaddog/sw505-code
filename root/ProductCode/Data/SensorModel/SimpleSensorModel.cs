using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DTOs;
using CommonLib.Interfaces;

namespace Data.SensorModel
{
    public class SimpleSensorModel : AbstractSensorModel
    {
        private readonly double average_obstacle_depth_half;

        public SimpleSensorModel()
            : base()
        {
            this.average_obstacle_depth_half = AVERAGE_OBSTACLE_DEPTH_CM / 2;
        }
        /// <summary>
        /// Gets the probability from ultrasonic sensor X.
        /// </summary>
        /// <param name="robot">The robots coordinates.</param>
        /// <param name="cell">The cells coordinates.</param>
        /// <param name="sensorX">The sensor X's length measured.</param>
        /// <returns></returns>
        public override double GetProbability(OccupancyGrid grid, IPose robot, CellIndex cell, byte sensorX)
        {
            ICoordinate c = grid.GetCellCenter(cell);
            double r = Math.Abs(c.X - robot.X + c.Y - robot.Y);

            

            if (r < ROBOT_RADIUS_CM)
                return NEAR_CELL_PROBABILITY;
            else if (r > Math.Min(MAXIMUM_SENSOR_RANGE_CM, sensorX + average_obstacle_depth_half))
                return initialProbability;
            else if (sensorX - average_obstacle_depth_half <= r && r <= sensorX + average_obstacle_depth_half)
                return sensorX < MINIMIM_SENSOR_RANGE_CM ? SMALL_MEASUREMENT_PROBABILITY : OCCUPIED_CELL_PROBABILITY;
            else
                return sensorX < MINIMIM_SENSOR_RANGE_CM ? SMALL_MEASUREMENT_PROBABILITY : FREE_CELL_PROBABILITY;
        }
    }
}
