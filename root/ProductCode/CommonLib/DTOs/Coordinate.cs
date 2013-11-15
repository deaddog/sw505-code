using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DTOs
{
    public struct Coordinate
    {
        private float x, y;

        public float X { get { return x; } }

        public float Y { get { return y; } }

        public Coordinate(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public string Encode()
        {
            throw new NotImplementedException();
        }
    }
}
