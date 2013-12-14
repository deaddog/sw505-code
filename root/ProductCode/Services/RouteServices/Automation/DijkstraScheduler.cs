using CommonLib.DTOs;
using CommonLib.Interfaces;
using Data;
using System;
using System.Collections.Generic;

namespace Services.RouteServices.Automation
{
    public class DijkstraScheduler : ICellScheduler
    {
        public IEnumerable<CellIndex> GetIndexRoute(IPose robotLocation, OccupancyGrid grid)
        {
            throw new NotImplementedException();
        }
    }
}
