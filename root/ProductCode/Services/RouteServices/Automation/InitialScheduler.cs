﻿using CommonLib.DTOs;
using CommonLib.Interfaces;
using Data;
using System.Collections.Generic;

namespace Services.RouteServices.Automation
{
    public class InitialScheduler : ICellScheduler
    {
        private bool initialScan;
        private CellIndex initialCell;
        private int lastNeighbour = -1;

        public IEnumerable<CellIndex> GetIndexRoute(IPose robotLocation, OccupancyGrid grid)
        {
            yield return GetDestination(robotLocation, grid);
        }
        private CellIndex GetDestination(IPose robotLocation, OccupancyGrid grid)
        {
            if (initialScan)
            {
                initialScan = false;
                initialCell = grid.GetIndex(robotLocation);
                return initialCell;
            }
            else
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
                        return GetDestination(robotLocation, grid);
                }
            }
        }
    }
}
