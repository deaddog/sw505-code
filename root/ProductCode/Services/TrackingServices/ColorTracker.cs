using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using CommonLib.DTOs;

namespace Services.TrackingServices
{
    /// <summary>
    /// Handles tracking of colors in images
    /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorTracker"/> class.
        /// </summary>
        /// <param name="color">The color that should be tracked.</param>
        public ColorTracker(Color color)
        {
            this.bounds = new Rectangle(0, 0, BOUNDS_MAX, BOUNDS_MAX);
            this.threshold = DEFAULT_THRESHOLD;
            this.originalColor = this.targetColor = color;
        }

        /// <summary>
        /// Gets or sets the color tracked by this <see cref="ColorTracker"/>.
        /// </summary>
        public Color Color
        {
            get { return targetColor; }
            set
            {
                this.originalColor = this.targetColor = value;
                threshold = DEFAULT_THRESHOLD;
            }
        }
        /// <summary>
        /// Gets the last location tracked by this <see cref="ColorTracker"/>.
        /// </summary>
        public Vector2D Center
        {
            get { return center; }
        }
        /// <summary>
        /// Gets the bounding box in which the last point was found when tracking.
        /// </summary>
        public Rectangle Bounds
        {
            get { return bounds; }
        }

        /// <summary>
        /// Updates the state of the <see cref="ColorTracker"/> from a bitmap.
        /// </summary>
        /// <param name="bitmap">The bitmap from which this <see cref="ColorTracker"/> should update its state.</param>
        public void Update(Bitmap bitmap)
        {
            Rectangle oldBounds = bounds;
            oldBounds.Inflate(BOUNDS_INFLATE, BOUNDS_INFLATE);
            oldBounds.Intersect(new Rectangle(0, 0, bitmap.Width, bitmap.Height));

            Bitmap bmp = trackColor(bitmap, oldBounds, targetColor, threshold);
            filterNoise(bmp);
            Point p = findBrightestPixel(bmp);

            Color newTarget = bitmap.GetPixel(p.X + oldBounds.X, p.Y + oldBounds.Y);

            double dist = distance(originalColor, newTarget);
            if (dist < 50)
                targetColor = newTarget;

            Rectangle newBounds = findBounds(bmp);
            bounds = new Rectangle(oldBounds.X + newBounds.X, oldBounds.Y + newBounds.Y, newBounds.Width, newBounds.Height);

            PointF newPoint;
            if (findCenter(bounds, out newPoint))
                center = (Vector2D)newPoint;
            else
            {
                bounds = new Rectangle(0, 0, BOUNDS_MAX, BOUNDS_MAX);
                targetColor = originalColor;
            }

            bmp.Dispose();
        }

        unsafe private static Bitmap trackColor(Bitmap src, Rectangle clippingRectangle, Color track, float threshold)
        {
            if (src.PixelFormat != PixelFormat.Format24bppRgb && src.PixelFormat != PixelFormat.Format32bppRgb && src.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ArgumentException("Bitmap must be 24bpp or 32bpp.");

            Bitmap output = new Bitmap(clippingRectangle.Width, clippingRectangle.Height, PixelFormat.Format8bppIndexed);

            ColorPalette pal = output.Palette;

            for (int i = 0; i < pal.Entries.Length; i++)
                pal.Entries[i] = Color.FromArgb(i, i, i);

            output.Palette = pal;

            BitmapData bdSrc = src.LockBits(clippingRectangle, ImageLockMode.ReadOnly, src.PixelFormat);
            BitmapData bdOutput = output.LockBits(new Rectangle(0, 0, clippingRectangle.Width, clippingRectangle.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            int PixelSize = src.PixelFormat == PixelFormat.Format24bppRgb ? 3 : 4;
            for (int i = 0; i < bdSrc.Height; i++)
            {
                byte* row = (byte*)bdSrc.Scan0 + i * bdSrc.Stride;
                byte* rowOutput = (byte*)bdOutput.Scan0 + i * bdOutput.Stride;

                for (int j = 0; j < bdSrc.Width; j++)
                {
                    float weight = getPixelWeight(row + j * PixelSize, track, threshold);
                    if (weight > 1) weight = 1;
                    if (weight < 0) weight = 0;
                    byte c = (byte)(weight * 255);

                    rowOutput[j] = c;
                }
            }
            src.UnlockBits(bdSrc);
            output.UnlockBits(bdOutput);

            return output;
        }
        unsafe private static float getPixelWeight(byte* ptr, Color track, float threshold)
        {
            float r = ptr[2] - track.R;
            float g = ptr[1] - track.G;
            float b = ptr[0] - track.B;

            float v = (float)Math.Sqrt(r * r + g * g + b * b);

            float upperMax = 16581375f;
            upperMax = threshold;

            if (v > upperMax)
                v = upperMax;
            v /= upperMax;

            return 1 - v;
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

        unsafe private static Rectangle findBounds(Bitmap src)
        {
            if (src.PixelFormat != PixelFormat.Format8bppIndexed)
                throw new ArgumentException("Bitmap must be 8bpp greyscale.");

            int x = int.MaxValue, y = int.MaxValue, x2 = int.MinValue, y2 = int.MinValue;

            BitmapData bdSrc = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            for (int i = 0; i < bdSrc.Height; i++)
            {
                byte* row = (byte*)bdSrc.Scan0 + i * bdSrc.Stride;

                for (int j = 0; j < bdSrc.Width; j++)
                    if (row[j] > 0)
                    {
                        if (j < x) x = j;
                        if (i < y) y = i;
                        if (j > x2) x2 = j;
                        if (i > y2) y2 = i;
                    }
            }
            src.UnlockBits(bdSrc);

            if (x == int.MaxValue || y == int.MaxValue || x2 == int.MinValue || y2 == int.MinValue)
                return Rectangle.Empty;
            else
                return Rectangle.FromLTRB(x, y, x2, y2);
        }
        private static bool findCenter(Rectangle bounds, out PointF center)
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

        unsafe private static Point findBrightestPixel(Bitmap src)
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
        private static double distance(Color c1, Color c2)
        {
            double r = c1.R - c2.R;
            double g = c1.G - c2.G;
            double b = c1.B - c2.B;

            return Math.Sqrt(r * r + g * g + b * b);
        }
    }
}
