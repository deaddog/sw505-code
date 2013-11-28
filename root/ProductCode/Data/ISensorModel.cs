using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Interfaces;

namespace Data
{
    public interface ISensorModel
    {
       double GetProbabilityUltrasonicSensorX(OccupancyGrid grid, IPose robot, IIndex cell,  byte sensorX);
    }
}
