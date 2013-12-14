using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DTOs;
using CommonLib.Interfaces;

namespace Data.SensorModel
{
    public class GaussianSensorModel : AbstractSensorModel
    {
        protected const double ETA = 1;

        public double NormalDistribution(byte sensor, double distance)
        {
            return 1 /
                (Math.Sqrt(2 * Math.PI * Math.Pow((AVERAGE_OBSTACLE_DEPTH_CM / 6), 2)))

                * Math.Pow(Math.E,
                -(Math.Pow((distance - sensor), 2)
                /
                (Math.Pow(AVERAGE_OBSTACLE_DEPTH_CM, 2) / 6)));
        }

        public override double GetProbability(OccupancyGrid grid, CommonLib.Interfaces.IPose robot, CommonLib.DTOs.CellIndex cell, byte sensorX)
        {
            ICoordinate c = grid.GetCellCenter(cell);
            double r = Math.Abs(c.X - robot.X + c.Y - robot.Y);

            if (sensorX < 20)
                return initialProbability;

            if (r < MINIMIM_SENSOR_RANGE_CM)
                return NEAR_CELL_PROBABILITY;
            else if (r > Math.Min(MAXIMUM_SENSOR_RANGE_CM, sensorX + AVERAGE_OBSTACLE_DEPTH_CM/2))
                return initialProbability;
            else if (sensorX - AVERAGE_OBSTACLE_DEPTH_CM/2 <= r && r <= sensorX + AVERAGE_OBSTACLE_DEPTH_CM/2)
                throw new NotImplementedException();
            else
                return FREE_CELL_PROBABILITY;
        }
    }
}
