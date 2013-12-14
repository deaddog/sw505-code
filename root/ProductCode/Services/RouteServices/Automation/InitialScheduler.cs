using CommonLib.Interfaces;
using Data;
using System.Collections.Generic;

namespace Services.RouteServices.Automation
{
    public class InitialScheduler : IScheduler
    {
        public IEnumerable<ICoordinate> GetRoute(IPose robotLocation, OccupancyGrid grid)
        {
            throw new System.NotImplementedException();
        }
    }
}
