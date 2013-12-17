using System;
using CommonLib;
using CommonLib.Interfaces;

namespace Data.SensorModel
{
    public class GaussianSensorModel : AbstractSensorModel
    {
        protected const double RHO = 0.1;
       
        public override double GetProbability(OccupancyGrid grid, CommonLib.Interfaces.IPose robot, CommonLib.DTOs.CellIndex cell, byte sensorX)
        {
            if (sensorX < 20)
                return initialProbability;

            ICoordinate c = grid.GetCellCenter(cell);
            double r = Math.Abs(c.X - robot.X + c.Y - robot.Y);

            if (r < MINIMIM_SENSOR_RANGE_CM)
                return NEAR_CELL_PROBABILITY;
            else if (r > Math.Min(MAXIMUM_SENSOR_RANGE_CM, sensorX + AVERAGE_OBSTACLE_DEPTH_CM / 2))
                return initialProbability;
            else if (sensorX - AVERAGE_OBSTACLE_DEPTH_CM / 2 <= r && r <= sensorX + AVERAGE_OBSTACLE_DEPTH_CM / 2)
            {
                Func<double, double> gaussianPDF = x => 
                    Math.Pow(Math.E, -Math.Pow((x - sensorX),2) / (2 * Math.Pow((AVERAGE_OBSTACLE_DEPTH_CM / 6), 2))) 
                    / Math.Sqrt(2 * Math.PI * Math.Pow((AVERAGE_OBSTACLE_DEPTH_CM / 6), 2));

                double eta = calcEta(sensorX, gaussianPDF);
                return eta * ExtendedMath.DefIntegrate(gaussianPDF, r - RHO, r + RHO);
            }
            else
                return FREE_CELL_PROBABILITY;
        }

        private double calcEta(byte sensorX, Func<double, double> gaussianPDF)
        {
            double pmiddle = ExtendedMath.DefIntegrate(gaussianPDF, sensorX - RHO, sensorX + RHO);

            return OCCUPIED_CELL_PROBABILITY / pmiddle;
        }


        //protected double constantdenominator = 2 * Math.Pow(AVERAGE_OBSTACLE_DEPTH_CM / 2, 2);
        //protected double constantfactor = 1 /(Math.Sqrt(2 * Math.PI * Math.Pow((AVERAGE_OBSTACLE_DEPTH_CM / 6), 2)));


        //public double NormalDistribution(byte sensor, double distance)
        //{
        //    return constantfactor * Math.Pow(Math.E,-(Math.Pow((distance - sensor), 2)/ constantdenominator ));
        //}

        //public override double GetProbability(OccupancyGrid grid, CommonLib.Interfaces.IPose robot, CommonLib.DTOs.CellIndex cell, byte sensorX)
        //{
        //    if (sensorX < 20)
        //        return initialProbability;

        //    ICoordinate c = grid.GetCellCenter(cell);
        //    double r = Math.Abs(c.X - robot.X + c.Y - robot.Y);
        //    double eta = calcEta(sensorX);

        //    if (r < MINIMIM_SENSOR_RANGE_CM)
        //        return NEAR_CELL_PROBABILITY;
        //    else if (r > Math.Min(MAXIMUM_SENSOR_RANGE_CM, sensorX + AVERAGE_OBSTACLE_DEPTH_CM / 2))
        //        return initialProbability;
        //    else if (sensorX - AVERAGE_OBSTACLE_DEPTH_CM / 2 <= r && r <= sensorX + AVERAGE_OBSTACLE_DEPTH_CM / 2)
        //    {
        //        Func<double, double> NormalDist = x => NormalDistribution(sensorX, x);
        //        return eta * ExtendedMath.DefIntegrate(NormalDist, r - RHO, r + RHO);
        //    }
        //    else
        //        return FREE_CELL_PROBABILITY;
        //}
    }
}




