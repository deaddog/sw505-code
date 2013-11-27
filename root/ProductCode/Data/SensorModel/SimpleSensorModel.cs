using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using CommonLib.DTOs;
//using CommonLib.Interfaces;

namespace Data.SensorModel
{
    public class SimpleSensorModel : ISensorModel
    {
        // constants for the sensor model
        private const double DEFAULT_VALUE = 0.5;
        private const double OCCUPIED_CELL_VALUE = 1;
        private const double FREE_CELL_VALUE = 0;
        private const double MAXIMUM_SENSOR_RANGE_CM = 170;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleSensorModel"/> class.
        /// </summary>
        /// <param name="cellDepthCM">The cell depth cm.</param>
        public SimpleSensorModel(double cellDepthCM = 20)
        {
            this.cellDepthCM = cellDepthCM;
        }


        private double cellDepthCM;

        /// <summary>
        /// Gets the cell depth cm.
        /// </summary>
        /// <value>
        /// The cell depth cm.
        /// </value>
        public double CellDepthCM
        {
            get { return cellDepthCM; }
        }

        public float CellRadius
       {
           get { return OccupancyGrid.CELL_SIZE_CM / 2; }
        }

        private float l_0()
        {
            return 0;
        }
        /*
        private CellCoordinate getCoordinateFromCellIndex(IIndex index)
        {
            
            return new CellCoordinate(OccupancyGrid.CELL_SIZE_CM * index.X + CellRadius, OccupancyGrid.CELL_SIZE_CM * index.Y + CellRadius);
        }
        
        /// <summary>
        /// Gets the probability from ultrasonic sensor X.
        /// </summary>
        /// <param name="robot">The robots coordinates.</param>
        /// <param name="cell">The cells coordinates.</param>
        /// <param name="sensorX">The sensor X's length measured.</param>
        /// <returns></returns>
        public double GetProbabilityUltrasonicSensorX(ICoordinate robot, IIndex cell, byte sensorX)
        {
            
            double r = Math.Abs((cell.X - robot.X) + (cell.Y - robot.Y));

            if (r > Math.Min(MAXIMUM_SENSOR_RANGE_CM, sensorX + CellRadius))
            {
                return l_0();
            }
            else if (sensorX - CellRadius <= r && r <= sensorX + CellRadius)
            {
                return OCCUPIED_CELL_VALUE;
            }
            else
                return FREE_CELL_VALUE;
        }*/
    }
}
