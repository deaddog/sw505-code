using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Interfaces;

namespace CommonLib.DTOs
{
    public class CellCoordinate : ICoordinate
    {
        public CellCoordinate(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        private float x;
        private float y;


        public float X
        {
            get { return x; }
        }

        public float Y
        {
            get { return y; }
        }
    }
}
