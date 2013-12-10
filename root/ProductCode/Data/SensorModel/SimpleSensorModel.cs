using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DTOs;
using CommonLib.Interfaces;

namespace Data.SensorModel
{
    public class SimpleSensorModel : ISensorModel
    {
        // constants for the sensor model
        private const double DEFAULT_VALUE = 0.5;
        private const double OCCUPIED_CELL_VALUE = .6;
        private const double FREE_CELL_VALUE = .4;
        private const double MAXIMUM_SENSOR_RANGE_CM = 170;
        private const double HALF_AVERAGE_OBSTACLE_DEPTH_CM = 5;

        private readonly double initialLogOdds;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleSensorModel"/> class.
        /// </summary>
        /// <param name="cellDepthCM">The cell depth cm.</param>
        public SimpleSensorModel(double cellDepthCM = 20)
        {
            this.cellDepthCM = cellDepthCM;
            this.initialLogOdds = OccupancyGrid.INITIAL_PROBABILITY;
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

        private CellCoordinate getCoordinateFromCellIndex(OccupancyGrid grid, CellIndex index)
        {
            float cellRadius = grid.CellSize / 2;
            return new CellCoordinate(grid.X + grid.CellSize * index.X + cellRadius, grid.Y + grid.CellSize * index.Y + cellRadius);
        }

        /// <summary>
        /// Gets the probability from ultrasonic sensor X.
        /// </summary>
        /// <param name="robot">The robots coordinates.</param>
        /// <param name="cell">The cells coordinates.</param>
        /// <param name="sensorX">The sensor X's length measured.</param>
        /// <returns></returns>
        public double GetProbability(OccupancyGrid grid, IPose robot, CellIndex cell, byte sensorX)
        {
            CellCoordinate c = getCoordinateFromCellIndex(grid, cell);
            double r = Math.Abs(c.X - robot.X + c.Y - robot.Y);

            if (r > Math.Min(MAXIMUM_SENSOR_RANGE_CM, sensorX + HALF_AVERAGE_OBSTACLE_DEPTH_CM))
                return initialLogOdds;
            else if (sensorX - HALF_AVERAGE_OBSTACLE_DEPTH_CM <= r && r <= sensorX + HALF_AVERAGE_OBSTACLE_DEPTH_CM)
                return OCCUPIED_CELL_VALUE;
            else
                return FREE_CELL_VALUE;
        }
    }
}
