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

        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.x + v2.x, v1.y + v2.y);
        }
        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.x - v2.x, v1.y - v2.y);
        }
        public static Vector2D operator *(Vector2D v1, float scale)
        {
            return new Vector2D(v1.x * scale, v1.y * scale);
        }
        public static Vector2D operator /(Vector2D v1, float scale)
        {
            return new Vector2D(v1.x / scale, v1.y / scale);
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
