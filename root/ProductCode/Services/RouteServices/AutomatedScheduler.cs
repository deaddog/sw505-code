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
            throw new NotImplementedException();
        }
    }
}
