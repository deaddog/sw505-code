using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLib.DTOs;

namespace Services.TrackingServices
{
    public class PointConverter
    {
        private readonly float actualSizeX, actualSizeY;
        private readonly float scaleX, scaleY;

        public PointConverter(int imageSizeX, int imageSizeY, float actualSizeX, float actualSizeY)
        {
            this.actualSizeX = actualSizeX / 2f;
            this.actualSizeY = actualSizeY / 2f;
            this.scaleX = actualSizeX / ((float)imageSizeX / 2f);
            this.scaleY = actualSizeY / ((float)imageSizeY / 2f);
        }

        public Coordinate ConvertPointToCoordinate(Vector2D point)
        {
            return new Coordinate(scaleX * point.X - actualSizeX, scaleY * point.Y - actualSizeY);
        }
    }
}
