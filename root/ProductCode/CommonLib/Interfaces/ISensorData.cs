﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Interfaces
{
    public interface ISensorData
    {
        byte SensorADistance { get; }
        byte SensorBDistance { get; }
        //byte[] Distance { get; }

    }
}