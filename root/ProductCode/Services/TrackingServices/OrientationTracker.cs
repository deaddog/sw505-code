using System;
using System.Drawing;

using CommonLib.DTOs;

namespace Services.TrackingServices
{
    /// <summary>
    /// Handles tracking of two points and the orientation they define
    /// </summary>
    public class OrientationTracker
    {
        private ColorTracker front, rear;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrientationTracker"/> class.
        /// </summary>
        /// <param name="front">The color of the tracked 'front'.</param>
        /// <param name="rear">The color of the tracked 'rear'.</param>
        public OrientationTracker(Color front, Color rear)
        {
            this.front = new ColorTracker(front);
            this.rear = new ColorTracker(rear);
        }

        /// <summary>
        /// Gets the last front-location tracked.
        /// </summary>
        public Vector2D Front
        {
            get { return front.Center; }
        }
        /// <summary>
        /// Gets the last rear-location tracked.
        /// </summary>
        public Vector2D Rear
        {
            get { return rear.Center; }
        }

        /// <summary>
        /// Gets the last center-location tracked. This is the midpoint between the front and the rear.
        /// </summary>
        public Vector2D Center
        {
            get { return front.Center + (rear.Center - front.Center) / 2f; }
        }
        /// <summary>
        /// Gets the last orientation tracked.
        /// </summary>
        public Vector2D Orientation
        {
            get { return front.Center - rear.Center; }
        }

        /// <summary>
        /// Updates the state of the <see cref="OrientationTracker"/> from a bitmap.
        /// </summary>
        /// <param name="bitmap">The bitmap from which this <see cref="OrientationTracker"/> should update its state.</param>
        public void Update(Bitmap bmp)
        {
            front.Update(bmp);
            rear.Update(bmp);
        }
    }
}
