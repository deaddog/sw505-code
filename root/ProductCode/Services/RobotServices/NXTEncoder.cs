﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
                floatToPaddedString(coordinate.X),
                floatToPaddedString(coordinate.Y));
        }

        public static string Encode(IPose pose)
        {

            return String.Format("{0}{1}{2}", 
                floatToPaddedString(pose.X),
                floatToPaddedString(pose.Y),
                floatToPaddedString((float)pose.Angle)
                );
        }

        private static string floatToPaddedString(float f)
        {
            string str = f.ToString("F2", CultureInfo.InvariantCulture);
            bool isNeg = str[0] == '-';

            if (isNeg)
                str = str.Substring(1);

            str = str.PadLeft(7, '0');

            if (isNeg)
                str = "-" + str.Substring(1);

            return str;
        }
    }
}
