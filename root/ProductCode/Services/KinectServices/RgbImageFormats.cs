using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.KinectServices
{
    public enum RgbImageFormats
    {
        /// <summary>
        /// RGB data (32 bits per pixel, layout corresponding to PixelFormats.Bgr32).
        /// Resolution of 640 by 480 at 30 Frames per second.
        /// </summary>
        Resolution640x480Fps30 = 1,
        /// <summary>
        /// RGB data (32 bits per pixel, layout corresponding to PixelFormats.Bgr32).
        /// Resolution of 1280 by 960 at 12 Frames per second.
        /// </summary>
        Resolution1280x960Fps12 = 2,
    }
}
