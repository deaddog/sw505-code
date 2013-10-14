using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace TrackColorForm
{
    class ColorTracking
    {
        public ColorTracking()
        {
        }

        public static Image TrackColor(Bitmap src, double min, double max)
        {
            Bitmap output = new Bitmap(src.Width, src.Height);
            BitmapData bdSrc = src.LockBits(new Rectangle(0,0,src.Width, src.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitmapData bdOutput = output.LockBits(new Rectangle(0, 0, src.Width, src.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            
            int PixelSize = 3;
            unsafe
            {

                for (int i = 0; i < bdSrc.Height; i++)
                {
                    byte* row = (byte*)bdSrc.Scan0 + i * bdSrc.Stride;
                    byte* rowOutput = (byte*)bdOutput.Scan0 + i * bdOutput.Stride;

                    for (int j = 0; j < bdSrc.Width; j++)
                    {
                        Color c = Color.FromArgb(row[j*PixelSize+2],row[j*PixelSize+1], row[j*PixelSize]);
                        
                        double hue = c.GetHue();

                        c = hue > max || hue < min ? Color.Black : Color.White;

                        rowOutput[j * PixelSize + 2] = c.R;
                        rowOutput[j * PixelSize + 1] = c.G;
                        rowOutput[j * PixelSize] = c.B;
                        
                    }

                }
            }
            src.UnlockBits(bdSrc);
            output.UnlockBits(bdOutput);

            return output;
        }
    }
}
