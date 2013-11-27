using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public struct OccupancyGrid
    {
        // Cell size in centimeters
        private const int cellSizeCentimeters = 20;
        private int gridRows;
        private int gridColumns;

        private double[,] gridCells;

        public int GridRows { get { return gridRows; } }

        public int GridColumns { get { return gridColumns; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="OccupancyGrid"/> struct.
        /// </summary>
        /// <param name="row">The number of rows.</param>
        /// <param name="column">The number of columns.</param>
        public OccupancyGrid(int row, int column, double initialProbability = 0.5)
        {
            gridRows = row;
            gridColumns = column;

            gridCells = new double[row, column];
            InitializeGrid(initialProbability);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OccupancyGrid"/> struct with the content of the specified array
        /// </summary>
        /// <param name="grid">The array containing values to fill the occupancy grid with</param>
        /// <exception cref="System.ArgumentException">Content of occupancy must be a probability (value between 0 and 1)</exception>
        public OccupancyGrid(double[,] grid)
        {
            gridRows = grid.GetLength(0);
            gridColumns = grid.GetLength(1);

            foreach (double item in grid)
            {
                if (item > 1 || item < 0)
                    throw new ArgumentException("Content of occupancy must be a probability (value between 0 and 1)");
            }

            gridCells = grid;
        }


        /// <summary>
        /// Initializes the grid with the specified initial probabilty
        /// </summary>
        /// <param name="initialProbability">The initial probability.</param>
        private void InitializeGrid(double initialProbability)
        {
            for (int i = 0; i < gridRows; i++)
            {
                for (int j = 0; j < gridColumns; j++)
                {
                    gridCells[i, j] = initialProbability;
                }
            }
        }

        /// <summary>
        /// Sets the probability of the specified cell with the specified probability
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="probability">The probability.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">row;Index not between the bounds of the grid cells
        /// or
        /// column;Index not between the bounds of the grid cells</exception>
        public bool SetProbability(int row, int column, double probability)
        {
            if (row > gridRows || row < 0)
                throw new ArgumentOutOfRangeException("row", "Index not between the bounds of the grid cells");

            if (column > gridColumns || column < 0)
                throw new ArgumentOutOfRangeException("column", "Index not between the bounds of the grid cells");

            if (probability < 0 || probability > 1)
                return false;

            gridCells[row, column] = probability;
            return true;
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Double"/> with the specified row.
        /// </summary>
        /// <value>
        /// The <see cref="System.Double"/>.
        /// </value>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// row;Index not between the bounds of the grid cells
        /// or
        /// column;Index not between the bounds of the grid cells
        /// or
        /// row;Index not between the bounds of the grid cells
        /// or
        /// column;Index not between the bounds of the grid cells
        /// </exception>
        public double this[int row, int column]
        {
            get
            {
                if (row > gridRows || row < 0)
                    throw new ArgumentOutOfRangeException("row", "Index not between the bounds of the grid cells");

                if (column > gridColumns || column < 0)
                    throw new ArgumentOutOfRangeException("column", "Index not between the bounds of the grid cells");

                return gridCells[row, column];
            }
            set
            {
                if (row > gridRows || row < 0)
                    throw new ArgumentOutOfRangeException("row", "Index not between the bounds of the grid cells");

                if (column > gridColumns || column < 0)
                    throw new ArgumentOutOfRangeException("column", "Index not between the bounds of the grid cells");

                if (value < 0)
                    value = 0;
                if (value > 1)
                    value = 1;

                gridCells[row, column] = value;
            }
        }

        /// <summary>
        /// Determines whether the specified cell is occupied.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        public bool IsOccupied(int row, int column)
        {
            return IsOpThreshold(row, column, (x, y) => x > y, 0.5d);
        }

        /// <summary>
        /// Determines whether the specified cell is empty.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        public bool IsEmpty(int row, int column)
        {
            return IsOpThreshold(row, column, (x, y) => x < y, 0.5d);
        }
        /// <summary>
        /// Determines whether the specified cell has a value that satisfies the <paramref name="op"/> in respect to the specified threshold
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="op">The operation to compare the cell value and the threshold value</param>
        /// <param name="threshold">The threshold.</param>
        /// <returns>If the cell value satisfies the <paramref name="op"/> in respect to the specified threshold</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// row;Index not between the bounds of the grid cells
        /// or
        /// column;Index not between the bounds of the grid cells
        /// </exception>
        private bool IsOpThreshold(int row, int column, Func<double, double, bool> op, double threshold)
        {
            if (row > gridRows || row < 0)
            {
                throw new ArgumentOutOfRangeException("row", "Index not between the bounds of the grid cells");
            }
            if (column > gridColumns || column < 0)
            {
                throw new ArgumentOutOfRangeException("column", "Index not between the bounds of the grid cells");
            }

            if (op( gridCells[row, column], threshold))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}