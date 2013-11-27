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
        private const double INITIAL_PROBABILITY = 0.5;

        private int rows;
        public int Rows
        {
            get { return rows; }
        }

        private int columns;
        public int Columns
        {
            get { return columns; }
        }

        private float cellsize;
        public float CellSize
        {
            get { return cellsize; }
        }

        private float xOffset;
        public float X
        {
            get { return xOffset; }
        }

        private float yOffset;
        public float Y
        {
            get { return yOffset; }
        }

        private double[,] gridCells;

        /// <summary>
        /// Constructor taking the number of rows and columns
        /// </summary>
        /// <param name="columns">Number of columns in grid (the width)</param>
        /// <param name="rows">Number of rows in grid (the height)</param>
        public OccupancyGrid(int columns, int rows, float cellsize, float xOffset, float yOffset)
        {
            this.rows = rows;
            this.columns = columns;

            this.cellsize = cellsize;
            this.xOffset = xOffset;
            this.yOffset = yOffset;

            gridCells = new double[columns, rows];

            for (int x = 0; x < columns; x++)
                for (int y = 0; y < rows; y++)
                    gridCells[x, y] = INITIAL_PROBABILITY;
        }

        public OccupancyGrid(float[,] grid, float cellsize, float xOffset, float yOffset)
        {
            this.rows = grid.GetLength(0);
            this.columns = grid.GetLength(1);

            this.cellsize = cellsize;
            this.xOffset = xOffset;
            this.yOffset = yOffset;

            gridCells = (double[,])grid.Clone();
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