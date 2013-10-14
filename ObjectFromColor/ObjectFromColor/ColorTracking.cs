using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.UI;
using Emgu.Util;

namespace ObjectFromColor
{
    class ColorTracking
    {
        public ColorTracking()
        {
        }

        public static Image TrackColor(Bitmap src)
        {
            Bitmap output = new Bitmap(src.Width, src.Height);

            int width = src.Width;
            int height = src.Height;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color c = src.GetPixel(i, j);
                    double hue = c.GetHue();
                    if (hue > 230 || hue < 200)
                        output.SetPixel(i, j, Color.Black);
                    else output.SetPixel(i, j, Color.White);
                }

            }

            return output;
        }


    }
}
