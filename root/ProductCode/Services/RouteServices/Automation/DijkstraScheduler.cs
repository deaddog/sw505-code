using CommonLib.DTOs;
using CommonLib.Interfaces;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.RouteServices.Automation
{
    public class DijkstraScheduler : ICellScheduler
    {
        private const double VISIT_WHEN_ADJECENT_IS_VALUE = 0.4;
        private const double IGNORE_CELLS_PAST_VALUE = 0.75;
        private const int MAXIMUM_CELLRANGE_FOR_KNOWLEDGE_CALCULATION = 17;

        public IEnumerable<CellIndex> GetIndexRoute(CellIndex robotLocation, OccupancyGrid grid)
        {
            DijkstraNode<CellIndex>[] nodes = dijkstraSearch(robotLocation, grid);
            double[,] knowledgegrid = buildKnowledgeGainGrid(grid);

            double highest = -1;
            Dictionary<CellIndex, double> knowledge = new Dictionary<CellIndex, double>();
            foreach (var node in nodes)
            {
                double know = calculateKnowledgeGain(node.Value, knowledgegrid);
                knowledge.Add(node.Value, know);
                if (know > highest)
                    highest = know;
            }

            DijkstraNode<CellIndex> destination = (from node in nodes
                                                   where knowledge[node.Value] == highest
                                                   orderby node.Weight descending
                                                   select node).FirstOrDefault();

            return RouteSimplifier.GetRoute(destination);
        }

        public bool DetermineIfRouteable(CellIndex robotLocation, OccupancyGrid grid)
        {
            if (!testVisitable(robotLocation, grid))
                return false;
            return dijkstraSearch(robotLocation, grid).Length > 0;
        }

        private DijkstraNode<CellIndex>[] dijkstraSearch(CellIndex robotLocation, OccupancyGrid grid)
        {
            return DijkstraSearch<CellIndex>.Search(cell => adjecentCells(cell, grid), distance, robotLocation);
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
                yield return new CellIndex(cell.X - 1, cell.Y);

            if (cell.Y > 1) yield return new CellIndex(cell.X, cell.Y - 1);
            if (cell.Y < grid.Rows - 2) yield return new CellIndex(cell.X, cell.Y + 1);

            if (cell.X < grid.Columns - 2)
                yield return new CellIndex(cell.X + 1, cell.Y);
        }

        private double[,] buildKnowledgeGainGrid(OccupancyGrid grid)
        {
            double[,] knowledge = new double[grid.Columns, grid.Rows];

            for (int y = 0; y < grid.Rows; y++)
                for (int x = 0; x < grid.Columns; x++)
                {
                    if (grid[x, y] > IGNORE_CELLS_PAST_VALUE)
                        knowledge[x, y] = -1;
                    else
                        knowledge[x, y] = 0.5 - Math.Abs(0.5 - grid[x, y]);
                }

            return knowledge;
        }

        private double calculateKnowledgeGain(CellIndex cell, double[,] knowledgegrid)
        {
            double knowledge = 0;

            int columns = knowledgegrid.GetLength(0);
            int rows = knowledgegrid.GetLength(1);

            for (int x = cell.X; x < Math.Min(columns, x + MAXIMUM_CELLRANGE_FOR_KNOWLEDGE_CALCULATION); x++)
            {
                if (knowledgegrid[x, cell.Y] == -1) break;
                knowledge += knowledgegrid[x, cell.Y];
            }
            for (int x = cell.X; x >= Math.Max(0, x - MAXIMUM_CELLRANGE_FOR_KNOWLEDGE_CALCULATION); x--)
            {
                if (knowledgegrid[x, cell.Y] == -1) break;
                knowledge += knowledgegrid[x, cell.Y];
            }

            for (int y = cell.Y; y < Math.Min(rows, y + MAXIMUM_CELLRANGE_FOR_KNOWLEDGE_CALCULATION); y++)
            {
                if (knowledgegrid[cell.X, y] == -1) break;
                knowledge += knowledgegrid[cell.X, y];
            }
            for (int y = cell.Y; y >= Math.Max(0, y - MAXIMUM_CELLRANGE_FOR_KNOWLEDGE_CALCULATION); y--)
            {
                if (knowledgegrid[cell.X, y] == -1) break;
                knowledge += knowledgegrid[cell.X, y];
            }

            return knowledge;
        }
    }
}
