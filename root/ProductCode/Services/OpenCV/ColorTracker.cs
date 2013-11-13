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
        private Color originalColor;
        private Color targetColor;

        private PointF center;

        private DateTime lastUpdate;

        public ColorTracker(Color color)
        {
            this.threshold = DEFAULT_THRESHOLD;
            this.originalColor = this.targetColor = color;
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
                this.originalColor = this.targetColor = value;
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

            Color newTarget = bitmap.GetPixel(p.X, p.Y);

            double dist = distance(originalColor, newTarget);
            if (dist < 50)
                targetColor = newTarget;

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

        private double distance(Color c1, Color c2)
        {
            double r = c1.R - c2.R;
            double g = c1.G - c2.G;
            double b = c1.B - c2.B;

            return Math.Sqrt(r * r + g * g + b * b);
        }
        private double distance(PointF p1, PointF p2)
        {
            double x = p1.X - p2.X;
            double y = p1.Y - p2.Y;

            return Math.Sqrt(x * x + y * y);
        }
    }
}
