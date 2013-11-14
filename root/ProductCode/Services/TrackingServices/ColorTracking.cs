using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Services.TrackingServices
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
        public static Bitmap TrackColor(Bitmap src, Rectangle clippingRectangle, Color track, float threshold)
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
