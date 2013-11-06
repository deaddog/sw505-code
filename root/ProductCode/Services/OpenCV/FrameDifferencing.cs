using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Services.OpenCV
{
    class FrameDifferencing
    {
        public FrameDifferencing() { }

        //Finds the absolute difference and thresholds it
        public static Bitmap GetDiff(Bitmap current, Bitmap previous, int threshold = 50)
        {
            Image<Bgr, Byte> frame = new Image<Bgr, byte>(current);
            Image<Bgr, Byte> previousFrame = new Image<Bgr, byte>(previous);
            Image<Bgr, Byte> diff;

            diff = frame.AbsDiff(previousFrame);
            diff = diff.ThresholdBinary(new Bgr(threshold, threshold, threshold), new Bgr(255, 255, 255));

            return diff.ToBitmap();
        }
    }
}
