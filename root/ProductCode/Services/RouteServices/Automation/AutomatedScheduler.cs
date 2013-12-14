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
        internal AutomatedScheduler()
        {
            this.initialScan = true;
        }

        public IEnumerable<ICoordinate> GetRoute(IPose robotLocation, OccupancyGrid grid)
        {
            foreach (CellIndex index in getRoute(robotLocation, grid))
                yield return getCellCenter(index, grid);
        }
        private IEnumerable<CellIndex> getRoute(IPose robotLocation, OccupancyGrid grid)
        {
            if (initialScan)
            {
                initialScan = false;
                initialCell = getIndex(robotLocation, grid);
                yield return initialCell;
            }
            else
            {
                var route = getAutomatedRoute(robotLocation, grid).ToArray();

                if (route.Length > 0)
                {
                    foreach (var index in route)
                        yield return index;
                }
                else
                    yield return getNextInitialNeighbour();
            }
        }
    }
}
