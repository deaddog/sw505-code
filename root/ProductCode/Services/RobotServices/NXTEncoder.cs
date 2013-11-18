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
            return String.Format("{0}{1}",
                BitConverter.ToString(BitConverter.GetBytes(coordinate.X)),
                BitConverter.ToString(BitConverter.GetBytes(coordinate.Y))
                ).Replace("-", "");
        }

        public static string Encode(IPose pose)
        {

            return String.Format("{0}{1}{2}",
                BitConverter.ToString(BitConverter.GetBytes(pose.X)),
                BitConverter.ToString(BitConverter.GetBytes(pose.Y)),
                BitConverter.ToString(BitConverter.GetBytes((float)pose.Angle))
                ).Replace("-", ""); ;
        }
    }
}
