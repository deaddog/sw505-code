using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Interfaces
{
    /// <summary>
    /// A simple coordinate
    /// </summary>
    public interface ICoordinate
    {
        /// <summary>
        /// The first dimension for the coordinate
        /// </summary>
        float X { get; }
        /// <summary>
        /// The second dimension for the coordinate
        /// </summary>
        float Y { get; }
    }
}
