using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Interfaces
{
    /// <summary>
    /// A simple pose, consisting of a coordinate
    /// </summary>
    public interface IPose : ICoordinate
    {
        /// <summary>
        /// The angle of the coordinate
        /// </summary>
        double Angle { get; }
    }
}
