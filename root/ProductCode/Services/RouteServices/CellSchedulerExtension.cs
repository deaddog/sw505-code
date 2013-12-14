using CommonLib.DTOs;
using CommonLib.Interfaces;
using Data;
using System;
using System.Collections.Generic;

namespace Services.RouteServices
{
    public static class CellSchedulerExtension
    {
        public static IEnumerable<ICoordinate> GetRoute(this ICellScheduler scheduler, IPose robotLocation, OccupancyGrid grid)
        {
            foreach (CellIndex index in scheduler.GetIndexRoute(robotLocation, grid))
                yield return grid.GetCellCenter(index);
        }
    }
}
