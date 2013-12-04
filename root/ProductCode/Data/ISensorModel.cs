using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DTOs;
using CommonLib.Interfaces;

namespace Data
{
    public interface ISensorModel
    {
        double GetProbability(OccupancyGrid grid, IPose robot, CellIndex cell, byte sensorX);
    }
}
