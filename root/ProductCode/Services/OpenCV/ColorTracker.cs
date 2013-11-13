using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace TrackColorForm
{
    public class ColorTracker
    {
        private const float DEFAULT_THRESHOLD = 50;

        private float threshold;
        private Color targetColor;
        private PointF center;

        private DateTime lastUpdate;

        public ColorTracker(Color color)
        {
            this.threshold = DEFAULT_THRESHOLD;
            this.targetColor = color;
        }


        public float Threshold
        {
            get { return threshold; }
        }
        public Color Color
        {
            get { return targetColor; }
            set
            {
                this.targetColor = value;
                threshold = DEFAULT_THRESHOLD;
            }
        }
        public PointF Center
        {
            get { return center; }
        }

        public void Track(Bitmap bitmap)
        {
            TimeSpan ts = DateTime.Now - lastUpdate;
            if (ts.TotalMilliseconds < 600)
                return;
            lastUpdate = DateTime.Now;

            Bitmap bmp = ColorTracking.TrackColor(bitmap, targetColor, threshold);
            ColorTracking.Filter(bmp);
            Point p = ColorTracking.FindStrongestPoint(bmp);

            PointF newPoint;
            Rectangle bounds = ColorTracking.FindBounds(bmp);
            if (FindCenter(bounds, out newPoint))
                center = newPoint;

            bmp.Dispose();
        }

        private static bool FindCenter(Rectangle bounds, out PointF center)
        {
            if (bounds.Width == 0 || bounds.Height == 0)
            {
                center = PointF.Empty;
                return false;
            }
            else
            {
                center = new PointF(bounds.X + bounds.Width / 2f, bounds.Y + bounds.Height / 2f);
                return true;
            }
        }
    }
}
