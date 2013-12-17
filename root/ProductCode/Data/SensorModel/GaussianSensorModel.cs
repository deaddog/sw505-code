using System;
using CommonLib;
using CommonLib.Interfaces;
using CommonLib.DTOs;

namespace Data.SensorModel
{
    public class GaussianSensorModel : AbstractSensorModel
    {
        protected const double RHO = 0.1;
       
        protected override double GetProbabilityInAlphaRange(OccupancyGrid grid, IPose robotPose, ICoordinate cellCenter, double distance, byte sensorX)
        {
            Func<double, double> gaussianPDF = x =>
                Math.Pow(Math.E, -Math.Pow((x - sensorX), 2) / (2 * Math.Pow((AVERAGE_OBSTACLE_DEPTH_CM / 6), 2)))
                / Math.Sqrt(2 * Math.PI * Math.Pow((AVERAGE_OBSTACLE_DEPTH_CM / 6), 2));

            double eta = calcEta(sensorX, gaussianPDF);
            return eta * ExtendedMath.DefIntegrate(gaussianPDF, distance - RHO, distance + RHO);
        }

        private double calcEta(byte sensorX, Func<double, double> gaussianPDF)
        {
            double pmiddle = ExtendedMath.DefIntegrate(gaussianPDF, sensorX - RHO, sensorX + RHO);

            return OCCUPIED_CELL_PROBABILITY / pmiddle;
        }
    }
}




