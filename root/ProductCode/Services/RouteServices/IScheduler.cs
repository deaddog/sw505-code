using CommonLib.Interfaces;
using Data;

using System.Collections.Generic;

namespace Services.RouteServices
{
    /// <summary>
    /// Defines a route service, capable of determining a route for a robot.
    /// </summary>
    public interface IScheduler
    {
        /// <summary>
        /// Gets the next route for a robot, such that the robot scans at the end of the route.
        /// </summary>
        /// <param name="robotLocation">The current robot location.</param>
        /// <param name="grid">The grid in which the robot moves.</param>
        /// <returns>A collection of <see cref="ICoordinate"/>s through which the robot should move.</returns>
        IEnumerable<ICoordinate> GetRoute(IPose robotLocation, OccupancyGrid grid);
    }
}
