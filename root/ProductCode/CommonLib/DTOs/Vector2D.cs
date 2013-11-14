using System;

namespace CommonLib.DTOs
{
    /// <summary>
    /// Describes a two-dimensional vector
    /// </summary>
    public struct Vector2D
    {
        private float x, y;

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

        public Vector2D Normalize()
        {
            double len = Math.Sqrt(x * x + y * y);
            return this / (float)len;
        }
    }
}
