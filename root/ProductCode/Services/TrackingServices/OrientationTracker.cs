using System;
using System.Drawing;

using CommonLib.DTOs;

namespace Services.TrackingServices
{
    public class OrientationTracker
    {
        private ColorTracker front, rear;

        public OrientationTracker(Color front, Color rear)
        {
            this.front = new ColorTracker(front);
            this.rear = new ColorTracker(rear);
        }

        public Vector2D Front
        {
            get { return front.Center; }
        }
        public Vector2D Rear
        {
            get { return rear.Center; }
        }

        public Vector2D Center
        {
            get { return front.Center + (rear.Center - front.Center) / 2f; }
        }
        public Vector2D Orientation
        {
            get { return front.Center - rear.Center; }
        }

        public void Track(Bitmap bmp)
        {
            front.Track(bmp);
            rear.Track(bmp);
        }
    }
}
