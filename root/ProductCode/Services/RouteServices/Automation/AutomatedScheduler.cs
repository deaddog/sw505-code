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
        private ICellScheduler current;
        private ICellScheduler next;

        internal AutomatedScheduler()
        {
            current = new InitialScheduler();
            next = new DijkstraScheduler();
        }

        public IEnumerable<ICoordinate> GetRoute(IPose robotLocation, OccupancyGrid grid)
        {
            if (next != null && next.DetermineIfRouteable(robotLocation, grid))
            {
                current = next;
                next = null;
            }
            return current.GetRoute(robotLocation, grid);
        }
    }
}
