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
        private const double initialProbability = 0.5;
        private int gridRows;
        private int gridColumns;

        private double[,] gridCells;

        public OccupancyGrid(int row, int column)
        {
            gridRows = row;
            gridColumns = column;

            gridCells = new double[row, column];
            InitializeGrid(row, column);
        }

        private void InitializeGrid(int row, int column)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    gridCells[i, j] = initialProbability;
                }
            }
        }

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

        public bool IsOccupied(int row, int column)
        {
            if (row > gridRows || row < 0)
            {
                throw new ArgumentOutOfRangeException("row", "Index not between the bounds of the grid cells");
            }
            if (column > gridColumns || column < 0)
            {
                throw new ArgumentOutOfRangeException("column", "Index not between the bounds of the grid cells");
            }

            if (gridCells[row, column] > .5d)
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