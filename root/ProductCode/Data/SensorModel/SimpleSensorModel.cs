using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DTOs;
using CommonLib.Interfaces;

namespace Data.SensorModel
{
    public class SimpleSensorModel : AbstractSensorModel
    {

        public SimpleSensorModel()
            : base()
        {
        }

        protected override double GetProbabilityInAlphaRange(OccupancyGrid grid, IPose robotPose, ICoordinate cellCenter, double distance, byte sensorX)
        {
            return OCCUPIED_CELL_PROBABILITY;
        }
    }
}
