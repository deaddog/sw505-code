using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Interfaces;

namespace CommonLib.DTOs
{
    /// <summary>
    /// Describes the (zero-based) index of a cell in a grid.
    /// </summary>
    public struct CellIndex : IEquatable<CellIndex>
    {
        private int x;
        private int y;

        /// <summary>
        /// Gets the x-coordinate of the cell.
        /// </summary>
        public int X
        {
            get { return x; }
        }
        /// <summary>
        /// Gets the y-coordinate of the cell.
        /// </summary>
        public int Y
        {
            get { return y; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CellIndex"/> struct.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        public CellIndex(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" }, is equal to this cell index.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this cell index.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this cell index; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is CellIndex)
                return Equals((CellIndex)obj);
            else
                return false;
        }
        /// <summary>
        /// Determines whether the current cell indx is equal to another cell index.
        /// </summary>
        /// <param name="other">A cell index to compare with this cell index.</param>
        /// <returns>
        /// true if the current cell index is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(CellIndex other)
        {
            return this.x == other.x && this.y == other.y;
        }
        /// <summary>
        /// Returns a hash code for this cell index.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return x ^ y;
        }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this cell index.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this cell index.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{{{0}; {1}}}", x, y);
        }
    }
}
