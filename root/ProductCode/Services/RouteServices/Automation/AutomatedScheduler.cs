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
        private Queue<ICellScheduler> queue;

        internal AutomatedScheduler()
            : this(new DijkstraScheduler())
        {
        }
        internal AutomatedScheduler(params ICellScheduler[] schedulers)
        {
            current = new InitialScheduler();
            queue = new Queue<ICellScheduler>();
            foreach (var scheduler in schedulers)
                queue.Enqueue(scheduler);
        }

        public IEnumerable<ICoordinate> GetRoute(IPose robotLocation, OccupancyGrid grid)
        {
            if (queue.Count > 0 && queue.Peek().DetermineIfRouteable(robotLocation, grid))
                current = queue.Dequeue();

            return current.GetRoute(robotLocation, grid);
        }
    }
}
