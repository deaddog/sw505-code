using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLib.DTOs;

namespace Services.TrackingServices
{
    /// <summary>
    /// Converts image coordinates to actual coordinates, based on given dimensions of image and actual sizes
    /// </summary>
    public class CoordinateConverter
    {
        private readonly float actualSizeX, actualSizeY;
        private readonly float scaleX, scaleY;

        /// <summary>
        /// Creates a new instance, with specified image and actual dimensions
        /// </summary>
        /// <param name="imageSizeX">First dimension in the size of the input image</param>
        /// <param name="imageSizeY">Second dimension in the size of the input image</param>
        /// <param name="actualSizeX">First dimension in the actual size</param>
        /// <param name="actualSizeY">Second dimension in the actual size</param>
        public CoordinateConverter(int imageSizeX, int imageSizeY, float actualSizeX, float actualSizeY)
        {
            this.actualSizeX = actualSizeX / 2f;
            this.actualSizeY = actualSizeY / 2f;
            this.scaleX = actualSizeX / ((float)imageSizeX / 2f);
            this.scaleY = actualSizeY / ((float)imageSizeY / 2f);
        }

        /// <summary>
        /// Converts the given <typeparamref name="Vector2D"/> to a new one, scaled according to given dimensions
        /// </summary>
        /// <param name="point">The point to convert from an image size coordinate to an actual size coordinate</param>
        /// <returns></returns>
        public Vector2D ConvertPixelToActual(Vector2D point)
        {
            return new Vector2D(scaleX * point.X - actualSizeX, scaleY * point.Y - actualSizeY);
        }
    }
}
