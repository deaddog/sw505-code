using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using CommonLib.DTOs;

namespace Services.TrackingServices
{
    public class ColorTracker
    {
        private const float DEFAULT_THRESHOLD = 50;
        public const int BOUNDS_INFLATE = 5;
        private const int BOUNDS_MAX = 100000;

        private float threshold;
        private Color originalColor;
        private Color targetColor;

        private Rectangle bounds;
        private Vector2D center;

        public ColorTracker(Color color)
        {
            this.bounds = new Rectangle(0, 0, BOUNDS_MAX, BOUNDS_MAX);
            this.threshold = DEFAULT_THRESHOLD;
            this.originalColor = this.targetColor = color;
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
        public Vector2D Center
        {
            get { return center; }
        }
        public Rectangle Bounds
        {
            get { return bounds; }
        }

        public void Track(Bitmap bitmap)
        {
            Rectangle oldBounds = bounds;
            oldBounds.Inflate(BOUNDS_INFLATE, BOUNDS_INFLATE);
            oldBounds.Intersect(new Rectangle(0, 0, bitmap.Width, bitmap.Height));

            Bitmap bmp = ColorTracking.TrackColor(bitmap, oldBounds, targetColor, threshold);
            filterNoise(bmp);
            Point p = findBrightestPixel(bmp);

            Color newTarget = bitmap.GetPixel(p.X + oldBounds.X, p.Y + oldBounds.Y);

            double dist = distance(originalColor, newTarget);
            if (dist < 50)
                targetColor = newTarget;

            Rectangle newBounds = ColorTracking.FindBounds(bmp);
            bounds = new Rectangle(oldBounds.X + newBounds.X, oldBounds.Y + newBounds.Y, newBounds.Width, newBounds.Height);

            PointF newPoint;
            if (FindCenter(bounds, out newPoint))
                center = (Vector2D)newPoint;
            else
            {
                bounds = new Rectangle(0, 0, BOUNDS_MAX, BOUNDS_MAX);
                targetColor = originalColor;
            }

            bmp.Dispose();
        }

        unsafe private static void filterNoise(Bitmap src)
        {
            if (src.PixelFormat != PixelFormat.Format8bppIndexed)
                throw new ArgumentException("Bitmap must be 8bpp greyscale.");

            List<Point> clearPoints = new List<Point>();

            BitmapData bdSrc = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            int stride = bdSrc.Stride;
        start:
            for (int y = 0; y < bdSrc.Height; y++)
            {
                byte* row = (byte*)bdSrc.Scan0 + y * stride;
                for (int x = 0; x < bdSrc.Width; x++)
                {
                    byte* ptr = row + x;
                    if (ptr[0] > 0 && countSet(bdSrc.Height, bdSrc.Width, stride, y, x, ptr) < 4)
                        clearPoints.Add(new Point(x, y));
                }
            }
            if (clearPoints.Count > 0)
            {
                while (clearPoints.Count > 0)
                {
                    ((byte*)bdSrc.Scan0 + clearPoints[0].Y * stride + clearPoints[0].X)[0] = 0;
                    clearPoints.RemoveAt(0);
                }
                goto start;
            }
            src.UnlockBits(bdSrc);
        }
        unsafe private static int countSet(int height, int width, int stride, int y, int x, byte* ptr)
        {
            int set = 0;

            if (x > 0)
            {
                if (y > 0 && ptr[-1 - stride] > 0) set++;
                if (ptr[-1] > 0) set++;
                if (y < height - 1 && ptr[-1 + stride] > 0) set++;
            }

            if (y > 0 && ptr[-stride] > 0) set++;
            //Don't test the center itself
            if (y < height - 1 && ptr[+stride] > 0) set++;

            if (x < width - 1)
            {
                if (y > 0 && ptr[1 - stride] > 0) set++;
                if (ptr[1] > 0) set++;
                if (y < height - 1 && ptr[1 + stride] > 0) set++;
            }

            return set;
        }


        /// <summary>
        /// Finds the brightest pixel in a bitmap.
        /// </summary>
        /// <param name="src">The source bitmap.</param>
        /// <returns>The point that had the brightest value in the bitmap.</returns>
        unsafe static Point findBrightestPixel(Bitmap src)
        {
            if (src.PixelFormat != PixelFormat.Format8bppIndexed)
                throw new ArgumentException("Bitmap must be 8bpp greyscale.");

            int max = -1;
            Point p = new Point(-1, -1);

            BitmapData bdSrc = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            int stride = bdSrc.Stride;
            for (int y = 0; y < bdSrc.Height; y++)
            {
                byte* row = (byte*)bdSrc.Scan0 + y * stride;
                for (int x = 0; x < bdSrc.Width; x++)
                {
                    byte val = (row + x)[0];
                    if (val > max)
                    {
                        max = val;
                        p = new Point(x, y);
                    }
                }
            }
            src.UnlockBits(bdSrc);

            return p;
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
    }
}
