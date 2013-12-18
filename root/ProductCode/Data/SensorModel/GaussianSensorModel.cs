using System;
using CommonLib;
using CommonLib.Interfaces;
using CommonLib.DTOs;

namespace Data.SensorModel
{
    public class GaussianSensorModel : AbstractSensorModel
    {
        private const double RHO = 0.1;
        private readonly double low_deviation;
        private readonly double high_deviation;
        private readonly double center;

        private readonly double low_a;
        private readonly double high_a;

        private double calculateIntegrate(double sensorX)
        {
            Func<double, double> gaussianPDF = x =>
                Math.Pow(Math.E, -Math.Pow((x - sensorX), 2) / (2 * Math.Pow((AVERAGE_OBSTACLE_DEPTH_CM / 6), 2)))
                / Math.Sqrt(2 * Math.PI * Math.Pow((AVERAGE_OBSTACLE_DEPTH_CM / 6), 2));

            return ExtendedMath.DefIntegrate(gaussianPDF, sensorX - RHO, sensorX + RHO);
        }

        public GaussianSensorModel()
        {
            center = calculateIntegrate(0);
            low_deviation = calculateIntegrate(-AVERAGE_OBSTACLE_DEPTH_HALF);
            high_deviation = calculateIntegrate(AVERAGE_OBSTACLE_DEPTH_HALF);

            low_a = (OCCUPIED_CELL_PROBABILITY - INITIAL_CELL_PROBABILITY) / (center - high_deviation);
            high_a = (OCCUPIED_CELL_PROBABILITY - FREE_CELL_PROBABILITY) / (center - low_deviation);
        }

        protected override double GetProbabilityInAlphaRange(OccupancyGrid grid, IPose robotPose, ICoordinate cellCenter, double distance, byte sensorX)
        {
            double proba = calculateIntegrate(sensorX);

            if (sensorX < distance)
                return low_a * (proba - center) + OCCUPIED_CELL_PROBABILITY;
            else
                return high_a * (proba - center) + OCCUPIED_CELL_PROBABILITY;
        }
    }
}
