using BackgroundSubtraction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinect_BS_CT
{
    public class FrameDiff : Differentiater
    {
        private Bitmap first = null;
        private Bitmap current = null;

        private bool hasFirst { get { return first != null; } }
        public override void Start(Bitmap bitmap)
        {
            if (!hasFirst)
            {
                first = bitmap;
                if (hasFirst)
                    Picture1 = first;
            }
            else
                current = bitmap;


            if (first != null && current != null)
            {
                Bitmap bmp = FrameDifferencing.GetDiff(first, current);
                Picture3 = bmp;
                Picture2 = FrameDifferencing.FindContour(bmp, current, 500, 5000);
            }
        }
    }
}
