using Services.KinectServices;
using System;
using System.Drawing;

namespace Control
{
    /// <summary>
    /// Retrieves rgb images from a Microsoft Kinect.
    /// </summary>
    public class DisplayControl
    {
        private RgbStream stream;

        private static DisplayControl instance;
        /// <summary>
        /// Gets the singleton instance of <see cref="DisplayControl"/>.
        /// </summary>
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

        /// <summary>
        /// Occurs when the <see cref="DisplayControl.Bitmap"/> property is updated with a new image.
        /// After this event occurs, the previous image is automatically disposed.
        /// </summary>
        public event EventHandler ImageUpdated;
        /// <summary>
        /// Gets the last bitmap captured.
        /// </summary>
        public Bitmap Bitmap
        {
            get { return stream.Bitmap; }
        }

        /// <summary>
        /// Gets or sets the image format used by this <see cref="DisplayControl"/>.
        /// </summary>
        public RgbImageFormats ImageFormat
        {
            get { return stream.ImageFormat; }
            set { stream.ImageFormat = value; }
        }
    }
}
