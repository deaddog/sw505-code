using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Interfaces
{
    public interface ISensorData
    {
        byte SensorFront { get; set; }
        byte SensorBack { get; set; }
        byte SensorLeft { get; set; }
        byte SensorRight { get; set; }
    }
}
