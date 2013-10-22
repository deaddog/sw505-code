using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackColorForm
{
    public class PointInPlane
    {
        public static PointF PointInPlaneMapping(int x1, int y1, double v, double d)
        { 
            float y2 = (float) Math.Tan(v - 90)* (float)(y1/Math.Sin(v-90) + d);

            return new PointF((float)x1, y2);
        }

        public static PointF PointInPlaneMapping(Point p, double v, double d)
        {
            return PointInPlaneMapping(p.X, p.Y, v, d);
        }
    }
}
