using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLib.DTOs;

namespace Services.TrackingServices
{
    public class PointConverter
    {
        private int imageSizeX, imageSizeY;
        private float cameraAngleX, cameraAngleY, actualSizeX, actualSizeY;
        public PointConverter(int imageSizeX, int imageSizeY, float cameraAngleX,
            float cameraAngleY, float actualSizeX, float actualSizeY)
        {
            this.imageSizeX = imageSizeX;
            this.imageSizeY = imageSizeY;
            this.cameraAngleX = cameraAngleX;
            this.cameraAngleY = cameraAngleY;
            this.actualSizeX = actualSizeX;
            this.actualSizeY = actualSizeY;
        }

        public Coordinate ConvertPointToCoordinate(Vector2D point)
        {
            float oldx = point.X;
            float oldy = point.Y;
            float wideAngleX = calcWideAngle(this.cameraAngleX);
            float wideAngleY = calcWideAngle(this.cameraAngleY);

            float cameraToActualDist = calcDist(actualSizeX, wideAngleX);
            float cameraToImageDist = calcDist(imageSizeX, wideAngleX);

            float angleX = calcAngle(cameraToImageDist, oldx);
            float angleY = calcAngle(cameraToImageDist, oldy);

            float x = calcActual(angleX, cameraToActualDist);
            float y = calcActual(angleY, cameraToActualDist);
            return new Coordinate(x, y);
        }

        private float calcWideAngle(float cameraAngle)
        {
            return 90 - (cameraAngle / 2);
        }

        private float calcDist(float sizeX, float wideAngleX)
        {
            return (sizeX / (float)Math.Sin(degToRad(this.cameraAngleX / 2))) * (float)Math.Sin(degToRad(wideAngleX));
        }

        private float calcAngle(float cameraToImageDist, float pointDim)
        {
            return (float)Math.Tanh(degToRad(pointDim / cameraToImageDist));
        }

        private float calcActual(float angle, float cameraToActualDist)
        {
            return (float)Math.Tanh(degToRad(angle)) * cameraToActualDist;
        }

        private float degToRad(float degrees)
        {
            return degrees * ((float)Math.PI / 180);
        }
    }
}
