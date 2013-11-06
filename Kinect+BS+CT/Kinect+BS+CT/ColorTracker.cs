using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackColorForm;

namespace Kinect_BS_CT
{
    public class ColorTracker : Differentiater
    {
        private DateTime last = DateTime.Now;
        public override void Start(Bitmap bitmap)
        {
            TimeSpan ts = DateTime.Now - last;
            if (ts.TotalMilliseconds > 200)
            {
                Picture1 = bitmap;
                Picture2 = ColorTracking.TrackColor(bitmap, 200, 320);
                last = DateTime.Now;
            }
        }
    }
}
