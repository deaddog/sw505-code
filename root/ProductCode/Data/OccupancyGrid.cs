using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// Struct representing an occupancy grid
    /// </summary>
    public struct OccupancyGrid
    {
        // Initialize each cell in grid with 0,5 probability
        private const double INITIAL_PROBABILITY = 0.5;
        // Cell size in centimeters
        public const int CELL_SIZE_CM = 30;

        // Rows in grid
        public int Rows;
        // Columns in grid
        public int Columns;

        // Array to represent the grid
        private double[,] gridCells;

        /// <summary>
        /// Constructor taking the number of rows and columns
        /// </summary>
        /// <param name="row">Number of rows in grid (the height)</param>
        /// <param name="column">Number of columns in grid (the width)</param>
        public OccupancyGrid(int row, int column)
        {
            Rows = row;
            Columns = column;

            // Array of grid cells
            gridCells = new double[column, row];
            // Initializes grid with default probability
            InitializeGrid(column, row);
        }

        /// <summary>
        /// Initializes the grid with an initial probability
        /// </summary>
        /// <param name="row">Number of rows in grid to initialize</param>
        /// <param name="column">Number of columns in grid to initialize</param>
        private void InitializeGrid(int row, int column)
        {
            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    gridCells[i, j] = INITIAL_PROBABILITY;
                }
            }
        }

        /// <summary>
        /// Sets the probability of a given cell in grid
        /// </summary>
        /// <param name="row">Row in the occupancy grid</param>
        /// <param name="column">Column in the occupancy grid</param>
        /// <param name="probability">The new probability of the cell in grid</param>
        /// <returns>True if probability is successfully updated, false otherwise</returns>
        public bool SetProbability(int row, int column, double probability)
        {
            checkBounds(row, column);

            if (probability < 0 || probability > 1)
                return false;

            gridCells[column, row] = probability;
            return true;
        }

        public double this[int column, int row]
        {
            get
            {
                checkBounds(row, column);

                return gridCells[column, row];
            }
            set
            {
                checkBounds(row, column);

                if (value < 0)
                    value = 0;
                if (value > 1)
                    value = 1;

                gridCells[column, row] = value;
            }
        }

        /// <summary>
        /// Checks if a cell in grid is occupied
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns>True if the probability of the given cell is above 0,5, false otherwise</returns>
        public bool IsOccupied(int row, int column)
        {
            checkBounds(row, column);

            if (gridCells[column, row] > .5d)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks the bounds of the occupancy grid
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        private void checkBounds(int row, int column)
        {
            if (row > Rows || row < 0)
            {
                throw new ArgumentOutOfRangeException("row", "Index not between the bounds of the grid cells");
            }
            if (column > Columns || column < 0)
            {
                throw new ArgumentOutOfRangeException("column", "Index not between the bounds of the grid cells");
            }
        }
    }
}