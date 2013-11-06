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
        private static unsafe float GetHue(byte* p)
        {
            byte min = Math.Min(p[0], Math.Min(p[1], p[2]));
            byte max = Math.Max(p[0], Math.Max(p[1], p[2]));

            if (max == min)
                return 0;
            else if (max == p[2])
                return (60f * (p[1] - p[0]) / (max - min) + 360f) % 360f;
            else if (max == p[1])
                return (60f * (p[0] - p[2]) / (max - min) + 120f + 360f) % 360f;
            else if (max == p[0])
                return (60f * (p[2] - p[1]) / (max - min) + 240f + 360f) % 360f;

            throw new Exception("Calculation error in GetHue.");
        }

        public enum Evaluators
        {
            Hue,
            RGBVector
        }
        private static unsafe PixelEvaluator GetEvaluator(Evaluators e)
        {
            switch (e)
            {
                case Evaluators.Hue:
                    return HueEvaluator;
                case Evaluators.RGBVector:
                    return RGBVectorEvaluator;
                default:
                    throw new NotImplementedException();
            }
        }

        private unsafe delegate float PixelEvaluator(byte* ptr);

        private static unsafe float HueEvaluator(byte* ptr)
        {
            float hue = GetHue(ptr);

            return hue > 220 || hue < 260 ? 0f : 1f;
        }

        private static unsafe float RGBVectorEvaluator(byte* ptr)
        {
            // B,G,R
            float r = 255, g = 0, b = 0;

            float x = ptr[2] - r;
            float y = ptr[1] - g;
            float z = ptr[1] - b;

            float v = (float)Math.Sqrt(x * x + y * y + z * z);

            float upperMax = 16581375f;
            upperMax = 255;

            if (v > upperMax)
                v = upperMax;
            v /= upperMax;

            return v;
        }

        public unsafe static Image TrackColor(Bitmap src, Evaluators evaluator)
        {
            Bitmap output = new Bitmap(src.Width, src.Height);
            BitmapData bdSrc = src.LockBits(new Rectangle(0, 0, src.Width, src.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitmapData bdOutput = output.LockBits(new Rectangle(0, 0, src.Width, src.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            int PixelSize = 3;
            for (int i = 0; i < bdSrc.Height; i++)
            {
                byte* row = (byte*)bdSrc.Scan0 + i * bdSrc.Stride;
                byte* rowOutput = (byte*)bdOutput.Scan0 + i * bdOutput.Stride;

                for (int j = 0; j < bdSrc.Width; j++)
                {
                    float weight = GetEvaluator(evaluator)(row + j * PixelSize);
                    if (weight > 1) weight = 1;
                    if (weight < 0) weight = 0;
                    byte c = (byte)(weight * 255);

                    rowOutput[j * PixelSize + 2] = c;
                    rowOutput[j * PixelSize + 1] = c;
                    rowOutput[j * PixelSize] = c;
                }
            }
            src.UnlockBits(bdSrc);
            output.UnlockBits(bdOutput);

            return output;
        }
    }
}
