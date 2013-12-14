using CommonLib.DTOs;
using CommonLib.Interfaces;
using Data;
using System.Collections.Generic;

namespace Services.RouteServices
{
    /// <summary>
    /// Defines a route service, capable of determining a route for a robot using a collection of cell indices.
    /// </summary>
    public interface ICellScheduler
    {
        /// <summary>
        /// Gets the next route for a robot, such that the robot scans at the end of the route.
        /// </summary>
        /// <param name="robotLocation">The current robot location.</param>
        /// <param name="grid">The grid in which the robot moves.</param>
        /// <returns>A collection of <see cref="CellIndex"/>s through which the robot should move.</returns>
        IEnumerable<CellIndex> GetIndexRoute(CellIndex robotLocation, OccupancyGrid grid);
        /// <summary>
        /// Determines if the scheduler can calculate a route from a grid.
        /// </summary>
        /// <param name="robotLocation">The robot location where the route should start.</param>
        /// <param name="grid">The grid in which to search for a route.</param>
        /// <returns>true, if scheduler can determine a route in the grid; otherwise, false.</returns>
        bool DetermineIfRouteable(CellIndex robotLocation, OccupancyGrid grid);
    }
}
