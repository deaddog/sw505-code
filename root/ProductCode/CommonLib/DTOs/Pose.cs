using CommonLib.Interfaces;

namespace CommonLib.DTOs
{
    /// <summary>
    /// Describes a pose in terms of it values.
    /// </summary>
    public struct Pose : IPose
    {
        private float x, y;
        private double angle;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pose"/> struct.
        /// </summary>
        /// <param name="x">The x-coordinate of the pose.</param>
        /// <param name="y">The y-coordinate of the pose.</param>
        /// <param name="angle">The angle of the pose.</param>
        public Pose(float x, float y, double angle)
        {
            this.x = x;
            this.y = y;
            this.angle = angle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pose"/> struct.
        /// </summary>
        /// <param name="position">The position of the pose.</param>
        /// <param name="direction">A <see cref="Vector2D"/> expressing the direction of the pose.</param>
        public Pose(Vector2D position, Vector2D direction)
        {
            this.x = position.X;
            this.y = position.Y;
            this.angle = direction.Angle;
        }

        /// <summary>
        /// Gets the angle associated with the <see cref="Pose"/>.
        /// </summary>
        public double Angle
        {
            get { return angle; }
        }

        /// <summary>
        /// Gets the x-coordinate associated with the <see cref="Pose"/>.
        /// </summary>
        public float X
        {
            get { return x; }
        }

        /// <summary>
        /// Gets the y-coordinate associated with the <see cref="Pose"/>.
        /// </summary>
        public float Y
        {
            get { return y; }
        }
    }
}
