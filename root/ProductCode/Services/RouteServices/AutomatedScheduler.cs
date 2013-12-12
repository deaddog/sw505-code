using CommonLib.DTOs;
using CommonLib.Interfaces;
using Data;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.RouteServices
{
    public class AutomatedScheduler : IScheduler
    {
        internal AutomatedScheduler()
        {
        }

        public IEnumerable<ICoordinate> GetRoute(IPose robotLocation, OccupancyGrid grid)
        {
            foreach (CellIndex index in getRoute(robotLocation, grid))
                yield return getCellCenter(index, grid);
        }

        private IEnumerable<CellIndex> getRoute(IPose robotLocation, OccupancyGrid grid)
        {
            throw new NotImplementedException();
        }

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
    }
}
