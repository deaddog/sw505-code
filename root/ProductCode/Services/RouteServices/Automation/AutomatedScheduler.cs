using CommonLib.DTOs;
using CommonLib.Interfaces;
using Data;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.RouteServices.Automation
{
    public class AutomatedScheduler : IScheduler
    {
        private static CellIndex getIndex(ICoordinate pose, OccupancyGrid grid)
        {
            int robotCellX = (int)Math.Floor((pose.X - grid.X) / grid.CellSize);
            int robotCellY = (int)Math.Floor((pose.Y - grid.Y) / grid.CellSize);
            return new CellIndex(robotCellX, robotCellY);
        }
        private static ICoordinate getCellCenter(CellIndex index, OccupancyGrid grid)
        {
            float cellRadius = grid.CellSize / 2;
            return new CellCoordinate(grid.X + grid.CellSize * index.X + cellRadius, grid.Y + grid.CellSize * index.Y + cellRadius);
        }

        private bool initialScan;
        private CellIndex initialCell;

        internal AutomatedScheduler()
        {
            this.initialScan = true;
        }

        public IEnumerable<ICoordinate> GetRoute(IPose robotLocation, OccupancyGrid grid)
        {
            foreach (CellIndex index in getRoute(robotLocation, grid))
                yield return getCellCenter(index, grid);
        }
        private IEnumerable<CellIndex> getRoute(IPose robotLocation, OccupancyGrid grid)
        {
            if (initialScan)
            {
                initialScan = false;
                initialCell = getIndex(robotLocation, grid);
                yield return initialCell;
            }
            else
            {
                var route = getAutomatedRoute(robotLocation, grid).ToArray();

                if (route.Length > 0)
                {
                    foreach (var index in route)
                        yield return index;
                }
                else
                    yield return getNextInitialNeighbour();
            }
        }

        private int lastNeighbour = -1;
        private CellIndex getNextInitialNeighbour()
        {
            lastNeighbour++;
            switch (lastNeighbour)
            {
                case 0:
                    return new CellIndex(initialCell.X - 1, initialCell.Y);
                case 1:
                    return new CellIndex(initialCell.X, initialCell.Y - 1);
                case 2:
                    return new CellIndex(initialCell.X + 1, initialCell.Y);
                case 3:
                    return new CellIndex(initialCell.X, initialCell.Y + 1);
                default:
                    lastNeighbour = -1;
                    return getNextInitialNeighbour();
            }
        }

        private IEnumerable<CellIndex> getAutomatedRoute(IPose robotLocation, OccupancyGrid grid)
        {
            DijkstraSearch<CellIndex>.Search(cell => adjecentCells(cell, grid), (cell1, cell2) => 1, getIndex(robotLocation, grid));
        }

        private bool testVisitable(CellIndex cell, OccupancyGrid grid)
        {
            return true;
        }

        private IEnumerable<CellIndex> adjecentCells(CellIndex cell, OccupancyGrid grid)
        {
            foreach (var c in allAdjecentCells(cell, grid))
                if (testVisitable(cell, grid))
                    yield return c;
        }
        private IEnumerable<CellIndex> allAdjecentCells(CellIndex cell, OccupancyGrid grid)
        {
            if (cell.X > 1)
            {
                if (cell.Y > 1) yield return new CellIndex(cell.X - 1, cell.Y - 1);
                yield return new CellIndex(cell.X - 1, cell.Y);
                if (cell.Y < grid.Rows - 2) yield return new CellIndex(cell.X - 1, cell.Y + 1);
            }

            if (cell.Y > 1) yield return new CellIndex(cell.X, cell.Y - 1);
            if (cell.Y < grid.Rows - 2) yield return new CellIndex(cell.X, cell.Y + 1);

            if (cell.X < grid.Columns - 2)
            {
                if (cell.Y > 1) yield return new CellIndex(cell.X + 1, cell.Y - 1);
                yield return new CellIndex(cell.X + 1, cell.Y);
                if (cell.Y < grid.Rows - 2) yield return new CellIndex(cell.X + 1, cell.Y + 1);
            }
        }
    }
}
