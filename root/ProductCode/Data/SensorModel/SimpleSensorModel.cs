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

            if (sensorX < 20)
                return initialProbability;

            if (r < MINIMIM_SENSOR_RANGE_CM)
                return NEAR_CELL_PROBABILITY;
            else if (r > Math.Min(MAXIMUM_SENSOR_RANGE_CM, sensorX + AVERAGE_OBSTACLE_DEPTH_CM / 2))
                return initialProbability;
            else if (sensorX - AVERAGE_OBSTACLE_DEPTH_CM / 2 <= r && r <= sensorX + AVERAGE_OBSTACLE_DEPTH_CM / 2)
                return OCCUPIED_CELL_PROBABILITY;
            else
                return FREE_CELL_PROBABILITY;
        }
    }
}
