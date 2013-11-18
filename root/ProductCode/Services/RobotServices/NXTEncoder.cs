using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Interfaces;

namespace Services.RobotServices
{
    public static class NXTEncoder
    {
        public static string Encode(ICoordinate coordinate)
        {
            return String.Format("{0}{1}", coordinate.X, coordinate.Y);
        }

        public static string Encode(IPose pose)
        {
            return String.Format("{0}{1}{2}", pose.X, pose.Y, (float)pose.Angle);
        }
    }
}
