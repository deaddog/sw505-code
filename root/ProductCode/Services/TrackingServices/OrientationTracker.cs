using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Services.TrackingServices
{
    public class OrientationTracker
    {
        private ColorTracker front, rear;

        public OrientationTracker(Color front, Color rear)
        {
            this.front = new ColorTracker(front);
            this.rear = new ColorTracker(rear);
        }

        public void Track(Bitmap bmp)
        {
            front.Track(bmp);
            rear.Track(bmp);
        }
    }
}
