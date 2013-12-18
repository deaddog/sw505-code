using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridViewer
{
    public class StateGrid
    {
        private int columns;
        private int rows;

        private CellState[,] states;

        private StateGrid()
        {
        }
        public StateGrid(int columns, int rows)
        {
            this.columns = columns;
            this.rows = rows;

            this.states=new CellState[columns,rows];

            for (int x = 0; x < columns; x++)
                for (int y = 0; y < rows; y++)
                    states[x, y] = CellState.Unknown;
        }

        public static StateGrid Load(OccupancyGrid grid, Func<double, CellState> staterule)
        {
            StateGrid stateGrid = new StateGrid();
            stateGrid.columns = grid.Columns;
            stateGrid.rows = grid.Rows;
            stateGrid.states = new CellState[grid.Columns, grid.Rows];

            for (int x = 0; x < stateGrid.columns; x++)
                for (int y = 0; y < stateGrid.rows; y++)
                    stateGrid.states[x, y] = staterule(grid[x, y]);

            return stateGrid;
        }

        public int Columns
        {
            get { return columns; }
        }
        public int Rows
        {
            get { return rows; }
        }

        public CellState this[int x, int y]
        {
            get { return states[x, y]; }
            set { states[x, y] = value; }
        }
    }
}
