using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Interfaces;

namespace CommonLib.DTOs
{
    public struct CellIndex
    {
        private int x;
        private int y;
        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        public CellIndex(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
