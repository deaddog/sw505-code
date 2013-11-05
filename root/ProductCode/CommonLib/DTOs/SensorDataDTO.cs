using System;
using CommonLib.Interfaces;

namespace CommonLib.DTOs
{
    public struct SensorDataDTO : ISensorData
    {
        private byte[] distance;

        public byte[] Distance
        {
            get { return distance; }
        }

        public SensorDataDTO(byte[] distance)
        {
            this.distance = distance;
        }

    }
}