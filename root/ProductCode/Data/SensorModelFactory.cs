using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.SensorModel;

namespace Data
{
    public class SensorModelFactory
    {
        private static SimpleSensorModel simpleSensorModel;
        private static SensorModelFactory instance;

        public static SensorModelFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new SensorModelFactory();
            }
            return instance;
        }

        public SimpleSensorModel CreateSimpleSensorModel()
        {
            if(simpleSensorModel == null)
                return new SimpleSensorModel();
            else
                return simpleSensorModel;
        }
    }
}
