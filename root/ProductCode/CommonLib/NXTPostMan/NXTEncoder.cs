using System;
using CommonLib.Interfaces;
using System.Globalization;
using CommonLib;

namespace CommonLib.NXTPostMan
{
    /// <summary>
    /// Used for encoding messages to be sent to the NXT via Bluetooth
    /// </summary>
    public static class NXTEncoder
    {
        /// <summary>
        /// Encodes the specified coordinate.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <returns>The encoded coordinate.</returns>
        public static string Encode(ICoordinate coordinate)
        {
            return String.Format("{0}{1}",
                floatToPaddedString(coordinate.X * 10),
                floatToPaddedString(coordinate.Y * 10));
        }

        /// <summary>
        /// Encodes the specified pose.
        /// </summary>
        /// <param name="pose">The pose.</param>
        /// <returns>The encoded pose.</returns>
        public static string Encode(IPose pose)
        {

            return String.Format("{0}{1}{2}",
                floatToPaddedString(pose.X * 10),
                floatToPaddedString(pose.Y * 10),
                floatToPaddedString((float)pose.Angle)
                );
        }

        private static string floatToPaddedString(float f)
        {
            //Converts float to string with one decimal
            string str = f.ToString("F1", CultureInfo.InvariantCulture);
            bool isNeg = str[0] == '-';

            //If f is negative, temporarily remove the '-'
            //Then pad with zeros and re-add '-' if needed
            if (isNeg)
                str = str.Substring(1);
            str = str.PadLeft(7, '0');
            if (isNeg)
                str = "-" + str.Substring(1);

            return str;
        }
  
        public static byte[] ByteEncode(ICoordinate cord) {

            return StringByteConverter.GetBytes((NXTEncoder.Encode(cord)));
        }

        public static byte[] ByteEncode(IPose pose) {
            return StringByteConverter.GetBytes((NXTEncoder.Encode(pose)));
        }



    }
}
