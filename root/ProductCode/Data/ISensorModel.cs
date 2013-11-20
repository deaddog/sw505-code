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
        double GetProbability(ICoordinate robot, ICoordinate cell, ISensorData sensor);
    }
}
