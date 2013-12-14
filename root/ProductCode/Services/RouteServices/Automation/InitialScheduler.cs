using CommonLib.DTOs;
using CommonLib.Interfaces;
using Data;
using System.Collections.Generic;

namespace Services.RouteServices.Automation
{
    public class InitialScheduler : ICellScheduler
    {
        public IEnumerable<CellIndex> GetIndexRoute(IPose robotLocation, OccupancyGrid grid)
        {
            throw new System.NotImplementedException();
        }
    }
}
