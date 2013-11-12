using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace TrackColorForm
{
    public static class ColorTracking
    {
        private static unsafe float RGBVectorEvaluator(byte* ptr, Color track, float threshold)
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
        public static Image TrackColor(Bitmap src, Color track, float threshold)
        {
            if (src.PixelFormat != PixelFormat.Format24bppRgb && src.PixelFormat != PixelFormat.Format32bppRgb && src.PixelFormat != PixelFormat.Format32bppArgb)
                throw new ArgumentException("Bitmap must be 24bpp or 32bpp.");

            Bitmap output = new Bitmap(src.Width, src.Height, PixelFormat.Format8bppIndexed);

            ColorPalette pal = output.Palette;

            for (int i = 0; i < pal.Entries.Length; i++)
                pal.Entries[i] = Color.FromArgb(i, i, i);

            output.Palette = pal;

            BitmapData bdSrc = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadOnly, src.PixelFormat);
            BitmapData bdOutput = output.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            int PixelSize = src.PixelFormat == PixelFormat.Format24bppRgb ? 3 : 4;
            unsafe
            {
                for (int i = 0; i < bdSrc.Height; i++)
                {
                    byte* row = (byte*)bdSrc.Scan0 + i * bdSrc.Stride;
                    byte* rowOutput = (byte*)bdOutput.Scan0 + i * bdOutput.Stride;

                    for (int j = 0; j < bdSrc.Width; j++)
                    {
                        float weight = RGBVectorEvaluator(row + j * PixelSize, track, threshold);
                        if (weight > 1) weight = 1;
                        if (weight < 0) weight = 0;
                        byte c = (byte)(weight * 255);

                        rowOutput[j] = c;
                    }
                }
            }
            src.UnlockBits(bdSrc);
            output.UnlockBits(bdOutput);

            return output;
        }

        public static void Filter(Bitmap src)
        {
            if (src.PixelFormat != PixelFormat.Format8bppIndexed)
                throw new ArgumentException("Bitmap must be 8bpp greyscale.");

            List<Point> clearPoints = new List<Point>();

            BitmapData bdSrc = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            int stride = bdSrc.Stride;
            unsafe
            {
            start:
                for (int y = 0; y < bdSrc.Height; y++)
                {
                    byte* row = (byte*)bdSrc.Scan0 + y * stride;
                    for (int x = 0; x < bdSrc.Width; x++)
                    {
                        byte* ptr = row + x;
                        if (ptr[0] > 0)
                        {
                            int set = 0;
                            if (x > 0)
                            {
                                if (y > 0 && ptr[-1 - stride] > 0) set++;
                                if (ptr[-1] > 0) set++;
                                if (y < bdSrc.Height - 1 && ptr[-1 + stride] > 0) set++;
                            }

                            if (y > 0 && ptr[-stride] > 0) set++;
                            //Don't test the center itself
                            if (y < bdSrc.Height - 1 && ptr[+stride] > 0) set++;

                            if (x < bdSrc.Width - 1)
                            {
                                if (y > 0 && ptr[1 - stride] > 0) set++;
                                if (ptr[1] > 0) set++;
                                if (y < bdSrc.Height - 1 && ptr[1 + stride] > 0) set++;
                            }

                            if (set < 4) clearPoints.Add(new Point(x, y));
                        }
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
            }
            src.UnlockBits(bdSrc);
        }

        public static Rectangle FindBounds(Bitmap src)
        {
            int x = int.MaxValue, y = int.MaxValue, x2 = int.MinValue, y2 = int.MinValue;

            BitmapData bdSrc = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            unsafe
            {
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
            }
            src.UnlockBits(bdSrc);

            if (x == int.MaxValue || y == int.MaxValue || x2 == int.MinValue || y2 == int.MinValue)
                return Rectangle.Empty;
            else
                return Rectangle.FromLTRB(x, y, x2, y2);
        }
    }
}
