using System;
using CommonLib.DTOs;

namespace CommonLib
{
    /// <summary>
    /// Converts image coordinates to actual coordinates, based on given dimensions of image and actual sizes
    /// </summary>
    public class CoordinateConverter
    {
        private float imageWidth, imageHeight;
        private float actualWidth, actualHeight;
        private float scaleX, scaleY;

        /// <summary>
        /// Creates a new instance, with specified image and actual dimensions
        /// </summary>
        /// <param name="imageWidth">First dimension in the size of the input image</param>
        /// <param name="imageHeight">Second dimension in the size of the input image</param>
        /// <param name="actualWidth">First dimension in the actual size</param>
        /// <param name="actualHeight">Second dimension in the actual size</param>
        public CoordinateConverter(int imageWidth, int imageHeight, float actualWidth, float actualHeight)
        {
            this.actualWidth = actualWidth / 2f;
            this.actualHeight = actualHeight / 2f;
            this.imageWidth = imageWidth / 2f;
            this.imageHeight = imageHeight / 2f;

            this.scaleX = this.actualWidth / this.imageWidth;
            this.scaleY = this.actualHeight / this.imageHeight;
        }


        /// <summary>
        /// Sets a new actual size.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void SetActualSize(float width, float height)
        {
            this.actualWidth = width / 2f;
            this.actualHeight = height / 2f;

            this.scaleX = this.actualWidth / this.imageWidth;
            this.scaleY = this.actualHeight / this.imageHeight;
        }

        /// <summary>
        /// Sets a new image size.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void SetPixelSize(int width, int height)
        {
            this.imageWidth = width / 2f;
            this.imageHeight = height / 2f;

            this.scaleX = this.actualWidth / this.imageWidth;
            this.scaleY = this.actualHeight / this.imageHeight;
        }

        /// <summary>
        /// Converts the given <typeparamref name="Vector2D"/> to a new one, scaled according to given dimensions
        /// </summary>
        /// <param name="point">The point to convert from an image size coordinate to an actual size coordinate</param>
        /// <returns></returns>
        public Vector2D ConvertPixelToActual(Vector2D point)
        {
            return new Vector2D(scaleX * point.X - actualWidth, scaleY * point.Y - actualHeight);
        }

        /// <summary>
        /// Converts the given <typeparamref name="Vector2D"/> to a new one, scaled according to given dimensions
        /// </summary>
        /// <param name="point">The point to convert from an actual size coordinate to an image size coordinate</param>
        /// <returns></returns>
        public Vector2D ConvertActualToPixel(Vector2D point)
        {
            return new Vector2D((point.X + actualWidth) / scaleX, (point.Y + actualHeight) / scaleY);
        }
    }
}
