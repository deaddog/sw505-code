using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using CommonLib.DTOs;
using CommonLib;

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

        private CoordinateConverter converter;

        private Rectangle bounds;
        private Vector2D center;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorTracker"/> class.
        /// </summary>
        /// <param name="color">The color that should be tracked.</param>
        public ColorTracker(Color color, CoordinateConverter converter)
        {
            this.bounds = new Rectangle(0, 0, BOUNDS_MAX, BOUNDS_MAX);
            this.threshold = DEFAULT_THRESHOLD;
            this.originalColor = this.targetColor = color;

            this.converter = converter;
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

            WeightGrid grid = trackColor(bitmap, oldBounds, targetColor, threshold);
            grid = WeightGrid.RemoveNoise(grid);
            Point p = grid.HeaviestPoint;

            Color newTarget = bitmap.GetPixel(p.X + oldBounds.X, p.Y + oldBounds.Y);

            double dist = distance(originalColor, newTarget);
            if (dist < threshold)
                targetColor = newTarget;

            Rectangle newBounds = grid.Bounds;
            bounds = new Rectangle(oldBounds.X + newBounds.X, oldBounds.Y + newBounds.Y, newBounds.Width, newBounds.Height);

            PointF newPoint;
            if (findCenter(bounds, out newPoint))
                center = converter.ConvertPixelToActual((Vector2D)newPoint);
            else
            {
                bounds = new Rectangle(0, 0, BOUNDS_MAX, BOUNDS_MAX);
                targetColor = originalColor;
            }
        }

        unsafe private static WeightGrid trackColor(Bitmap src, Rectangle clippingRectangle, Color track, float threshold)
        {
            if (src.PixelFormat != PixelFormat.Format24bppRgb && src.PixelFormat != PixelFormat.Format32bppRgb && src.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ArgumentException("Bitmap must be 24bpp or 32bpp.");

            float[,] wGrid = new float[clippingRectangle.Width, clippingRectangle.Height];

            BitmapData bdSrc = src.LockBits(clippingRectangle, ImageLockMode.ReadOnly, src.PixelFormat);

            int PixelSize = src.PixelFormat == PixelFormat.Format24bppRgb ? 3 : 4;
            for (int i = 0; i < bdSrc.Height; i++)
            {
                byte* row = (byte*)bdSrc.Scan0 + i * bdSrc.Stride;

                for (int j = 0; j < bdSrc.Width; j++)
                {
                    float weight = getPixelWeight(row + j * PixelSize, track, threshold);
                    if (weight > 1) weight = 1;
                    if (weight < 0) weight = 0;

                    wGrid[j, i] = weight;
                }
            }
            src.UnlockBits(bdSrc);

            return new WeightGrid(clippingRectangle.X, clippingRectangle.Y, wGrid);
        }
        unsafe private static float getPixelWeight(byte* ptr, Color track, float threshold)
        {
            float r = ptr[2] - track.R;
            float g = ptr[1] - track.G;
            float b = ptr[0] - track.B;

            float v = (float)Math.Sqrt(r * r + g * g + b * b);

            float upperMax = 441.672955930063709849498817084f; //Maximum distance between colors (for ref) sqrt(3 • 255 • 255)
            upperMax = threshold;

            if (v > upperMax)
                v = upperMax;
            v /= upperMax;

            return 1 - v;
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

        private static double distance(Color c1, Color c2)
        {
            double r = c1.R - c2.R;
            double g = c1.G - c2.G;
            double b = c1.B - c2.B;

            return Math.Sqrt(r * r + g * g + b * b);
        }

        private struct WeightGrid
        {
            private int xOffset, yOffset;
            private int height, width;
            private float[,] grid;

            public int X
            {
                get { return xOffset; }
            }
            public int Y
            {
                get { return yOffset; }
            }
            public int Height
            {
                get { return height; }
            }
            public int Width
            {
                get { return width; }
            }

            public float this[int x, int y]
            {
                get { return grid[x, y]; }
            }

            public WeightGrid(int xoffset, int yoffset, float[,] grid)
            {
                this.xOffset = xoffset;
                this.yOffset = yoffset;
                this.grid = (float[,])grid.Clone();
                this.width = grid.GetLength(0);
                this.height = grid.GetLength(1);
            }

            public int CountNeighbours(int x, int y)
            {
                int set = 0;

                if (x > 0)
                {
                    if (y > 0 && grid[x - 1, y - 1] > 0) set++;
                    if (grid[x - 1, y] > 0) set++;
                    if (y < height - 1 && grid[x - 1, y + 1] > 0) set++;
                }

                if (y > 0 && grid[x, y - 1] > 0) set++;
                //Don't test the center itself
                if (y < height - 1 && grid[x, y + 1] > 0) set++;

                if (x < width - 1)
                {
                    if (y > 0 && grid[x + 1, y - 1] > 0) set++;
                    if (grid[x + 1, y] > 0) set++;
                    if (y < height - 1 && grid[x + 1, y + 1] > 0) set++;
                }

                return set;
            }

            public static WeightGrid RemoveNoise(WeightGrid grid)
            {
                WeightGrid newGrid = new WeightGrid(grid.xOffset, grid.yOffset, grid.grid);
                bool changed = false;

                for (int x = 0; x < grid.Width; x++)
                    for (int y = 0; y < grid.Height; y++)
                        if (grid[x, y] > 0 && grid.CountNeighbours(x, y) < 4)
                        {
                            newGrid.grid[x, y] = 0;
                            changed = true;
                        }

                return changed ? RemoveNoise(newGrid) : newGrid;
            }

            public Point HeaviestPoint
            {
                get
                {
                    float max = -1;
                    Point p = new Point(-1, -1);

                    for (int x = 0; x < this.Width; x++)
                        for (int y = 0; y < this.Height; y++)
                        {
                            float val = grid[x, y];
                            if (val > max)
                            {
                                max = val;
                                p = new Point(x, y);
                            }
                        }

                    return p;
                }
            }
            public Rectangle Bounds
            {
                get
                {
                    int x1 = int.MaxValue, y1 = int.MaxValue, x2 = int.MinValue, y2 = int.MinValue;

                    for (int x = 0; x < this.width; x++)
                        for (int y = 0; y < this.height; y++)
                            if (grid[x, y] > 0)
                            {
                                if (x < x1) x1 = x;
                                if (y < y1) y1 = y;
                                if (x > x2) x2 = x;
                                if (y > y2) y2 = y;
                            }

                    if (x1 == int.MaxValue || y1 == int.MaxValue || x2 == int.MinValue || y2 == int.MinValue)
                        return Rectangle.Empty;
                    else
                        return Rectangle.FromLTRB(x1, y1, x2, y2);
                }
            }
        }
    }
}
