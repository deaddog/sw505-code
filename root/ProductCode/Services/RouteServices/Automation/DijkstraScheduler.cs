using CommonLib.DTOs;
using CommonLib.Interfaces;
using Data;
using System;
using System.Collections.Generic;

namespace Services.RouteServices.Automation
{
    public class DijkstraScheduler : ICellScheduler
    {
        private const double VISIT_WHEN_ADJECENT_IS_VALUE = 0.4;

        public IEnumerable<CellIndex> GetIndexRoute(CellIndex robotLocation, OccupancyGrid grid)
        {
            DijkstraNode<CellIndex>[] nodes = DijkstraSearch<CellIndex>.Search(
                cell => adjecentCells(cell, grid),
                (cell1, cell2) => 1,
                robotLocation);

            double lowest = double.PositiveInfinity;
            Dictionary<CellIndex, double> knowledge = new Dictionary<CellIndex, double>();
            foreach (var node in nodes)
            {
                double know = calculateKnowledge(node.Value, grid);
                knowledge.Add(node.Value, know);
                if (know < lowest)
                    lowest = know;
            }
            throw new NotImplementedException();
        }

        public bool DetermineIfRouteable(CellIndex robotLocation, OccupancyGrid grid)
        {
            return DijkstraSearch<CellIndex>.Search(cell => adjecentCells(cell, grid), distance, robotLocation).Length > 0;
        }

        private uint distance(CellIndex c1, CellIndex c2)
        {
            double distx = c1.X - c2.X;
            double disty = c1.Y - c2.Y;
            return (uint)Math.Sqrt(distx * distx + disty * disty) * 1000;
        }

        private bool testVisitable(CellIndex cell, OccupancyGrid grid)
        {
            for (int x = cell.X - 1; x < cell.X + 2; x++)
                for (int y = cell.Y - 1; y < cell.Y + 2; y++)
                    if (grid[x, y] > VISIT_WHEN_ADJECENT_IS_VALUE)
                        return false;
            return true;
        }

        private IEnumerable<CellIndex> adjecentCells(CellIndex cell, OccupancyGrid grid)
        {
            foreach (var c in allAdjecentCells(cell, grid))
                if (testVisitable(c, grid))
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

        private double calculateKnowledge(CellIndex cell, OccupancyGrid grid)
        {
            double knowledge = 0;
            for (int x = cell.X; x < grid.Columns; x++)
                knowledge += Math.Abs(0.5 - grid[x, cell.Y]);
            for (int x = cell.X; x >= 0; x--)
                knowledge += Math.Abs(0.5 - grid[x, cell.Y]);
            for (int y = cell.Y; y < grid.Rows; y++)
                knowledge += Math.Abs(0.5 - grid[cell.X, y]);
            for (int y = cell.Y; y >= 0; y--)
                knowledge += Math.Abs(0.5 - grid[cell.X, y]);

            return knowledge;
        }
    }
}
