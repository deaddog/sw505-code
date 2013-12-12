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

        protected const double MAXIMUM_SENSOR_RANGE_CM = 170;
		protected const double MINIMIM_SENSOR_RANGE_CM = 10;
        protected const double AVERAGE_OBSTACLE_DEPTH_CM = 5;

        protected readonly double initialProbability;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleSensorModel"/> class.
        /// </summary>
        /// <param name="cellDepthCM">The cell depth cm.</param>
        public AbstractSensorModel(double cellDepthCM = 20)
        {
            this.cellDepthCM = cellDepthCM;
            this.initialProbability = OccupancyGrid.INITIAL_PROBABILITY;
        }


        protected double cellDepthCM;

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

        protected CellCoordinate getCoordinateFromCellIndex(OccupancyGrid grid, CellIndex index)
        {
            float cellRadius = grid.CellSize / 2;
            return new CellCoordinate(grid.X + grid.CellSize * index.X + cellRadius, grid.Y + grid.CellSize * index.Y + cellRadius);
        }

        public abstract double GetProbability(OccupancyGrid grid, CommonLib.Interfaces.IPose robot, CellIndex cell, byte sensorX);
    }
}
