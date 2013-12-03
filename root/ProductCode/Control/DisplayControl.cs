using Services.KinectServices;
using System;
using System.Drawing;

namespace Control
{
    public class DisplayControl
    {
        private RgbStream stream;

        private static DisplayControl instance;
        public static DisplayControl Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DisplayControl();
                }
                return instance;
            }
        }

        private DisplayControl()
        {
            this.stream = RgbStream.Instance;
            this.stream.ImageFormat = RgbImageFormats.Resolution640x480Fps30;

            this.stream.ImageUpdated += (s, e) =>
            {
                if (ImageUpdated != null)
                    ImageUpdated(this, EventArgs.Empty);
            };
        }

        public event EventHandler ImageUpdated;
        public Bitmap Bitmap
        {
            get { return stream.Bitmap; }
        }
    }
}
