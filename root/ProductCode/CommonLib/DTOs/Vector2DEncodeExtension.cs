using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DTOs
{
    public static class Vector2DEncodeExtension
    {
        public static string GetNXTEncoding(this Vector2D vector)
        {
            return String.Format("{0}{1}{2}", vector.X, vector.Y, (float)vector.Angle);
        }
    }
}
