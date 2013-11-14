using System;

namespace CommonLib.DTOs
{
    /// <summary>
    /// Describes a two-dimensional vector
    /// </summary>
    public struct Vector2D
    {
        private static Vector2D zero = new Vector2D(0, 0);
        private static Vector2D one = new Vector2D(1, 1);

        /// <summary>
        /// Gets a <see cref="Vector2D"/> with X and Y both equal to 0.
        /// </summary>
        public static Vector2D Zero
        {
            get { return zero; }
        }
        /// <summary>
        /// Gets a <see cref="Vector2D"/> with X and Y both equal to 1.
        /// </summary>
        public static Vector2D One
        {
            get { return one; }
        }

        private float x, y;

        /// <summary>
        /// Gets the X-coordinate.
        /// </summary>
        public float X
        {
            get { return x; }
        }
        /// <summary>
        /// Gets the Y-coordinate.
        /// </summary>
        public float Y
        {
            get { return y; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> struct.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The x-coordinate.</param>
        public Vector2D(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Joins two vectors using addition.
        /// </summary>
        /// <param name="v1">The first vector in the addition.</param>
        /// <param name="v2">The second vector in the addition.</param>
        /// <returns>A new <see cref="Vector2D"/> which is the sum of the two parameter vectors.</returns>
        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.x + v2.x, v1.y + v2.y);
        }
        /// <summary>
        /// Joins two vectors using subtraction.
        /// </summary>
        /// <param name="v1">The first vector in the subtraction.</param>
        /// <param name="v2">The second vector in the subtraction.</param>
        /// <returns>A new <see cref="Vector2D"/> which is the difference between the two parameter vectors.</returns>
        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.x - v2.x, v1.y - v2.y);
        }
        /// <summary>
        /// Multiplies a vector with a scale value.
        /// </summary>
        /// <param name="v1">The vector.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>A new <see cref="Vector2D"/> where both coordinates are multiplied by the scale value.</returns>
        public static Vector2D operator *(Vector2D v1, float scale)
        {
            return new Vector2D(v1.x * scale, v1.y * scale);
        }
        /// <summary>
        /// Divides a vector with a scale value.
        /// </summary>
        /// <param name="v1">The vector.</param>
        /// <param name="scale">The scale.</param>
        /// <returns>A new <see cref="Vector2D"/> where both coordinates are divided by the scale value.</returns>
        public static Vector2D operator /(Vector2D v1, float scale)
        {
            return new Vector2D(v1.x / scale, v1.y / scale);
        }

        /// <summary>
        /// Converts a <see cref="Vector2D"/> to a <see cref="System.Drawing.PointF"/>.
        /// </summary>
        /// <param name="vector">The vector to convert.</param>
        /// <returns>A new <see cref="System.Drawing.PointF"/> with coordinates equal to the coordinates of the vector.</returns>
        public static explicit operator System.Drawing.PointF(Vector2D vector)
        {
            return new System.Drawing.PointF(vector.x, vector.y);
        }
        /// <summary>
        /// Converts a <see cref="Vector2D"/> to a <see cref="System.Drawing.SizeF"/>.
        /// </summary>
        /// <param name="vector">The vector to convert.</param>
        /// <returns>A new <see cref="System.Drawing.SizeF"/> with size equal to the coordinates of the vector.</returns>
        public static explicit operator System.Drawing.SizeF(Vector2D vector)
        {
            return new System.Drawing.SizeF(vector.x, vector.y);
        }
        /// <summary>
        /// Converts a <see cref="System.Drawing.PointF"/> to a <see cref="Vector2D"/>.
        /// </summary>
        /// <param name="point">The point to convert.</param>
        /// <returns>A new <see cref="Vector2D"/> with coordinates equal to the coordinates of the point.</returns>
        public static explicit operator Vector2D(System.Drawing.PointF point)
        {
            return new Vector2D(point.X, point.Y);
        }
        /// <summary>
        /// Converts a <see cref="System.Drawing.SizeF"/> to a <see cref="Vector2D"/>.
        /// </summary>
        /// <param name="point">The point to convert.</param>
        /// <returns>A new <see cref="Vector2D"/> with coordinates equal to the size of the 'size'.</returns>
        public static explicit operator Vector2D(System.Drawing.SizeF size)
        {
            return new Vector2D(size.Width, size.Height);
        }

        /// <summary>
        /// Normalizes this <see cref="Vector2D"/>.
        /// </summary>
        /// <returns>A new <see cref="Vector2D"/> with lenght 1.</returns>
        public Vector2D Normalize()
        {
            double len = Math.Sqrt(x * x + y * y);
            return this / (float)len;
        }
    }
}
