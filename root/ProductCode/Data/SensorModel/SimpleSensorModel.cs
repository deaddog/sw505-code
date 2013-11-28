﻿using System;
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

        private double l_0(OccupancyGrid grid, IIndex index)
        {
            double prior = grid[index.X,index.Y];
            return Math.Log10(prior / (1 - prior));
        }

        private CellCoordinate getCoordinateFromCellIndex(OccupancyGrid grid, IIndex index)
        {
            float cellRadius = grid.CellSize / 2;
            return new CellCoordinate(grid.CellSize * index.X + cellRadius, grid.CellSize * index.Y + cellRadius);
        }

        /// <summary>
        /// Gets the probability from ultrasonic sensor X.
        /// </summary>
        /// <param name="robot">The robots coordinates.</param>
        /// <param name="cell">The cells coordinates.</param>
        /// <param name="sensorX">The sensor X's length measured.</param>
        /// <returns></returns>
        public double GetProbabilityUltrasonicSensorX(OccupancyGrid grid, IPose robot, IIndex cell, byte sensorX)
        {
            float cellRadius = grid.CellSize / 2;

            CellCoordinate c = getCoordinateFromCellIndex(grid, cell);
            double r = Math.Abs((c.X - robot.X) + (c.Y - robot.Y));

            if (r > Math.Min(MAXIMUM_SENSOR_RANGE_CM, sensorX + cellRadius))
            {
                return l_0(grid,cell);
            }
            else if (sensorX - cellRadius <= r && r <= sensorX + cellRadius)
            {
                return OCCUPIED_CELL_VALUE;
            }
            else
                return FREE_CELL_VALUE;
        }
    }
}
