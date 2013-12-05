using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.Interfaces;
using Data;

namespace Services.RouteServices
{
    public class SchedulingService
    {
        private static SchedulingService instance;

        private SchedulingService()
        {

        }

        public static SchedulingService Instance
        {
            get
            {
                if (instance == null)
                    instance = new SchedulingService();
                return instance;
            }
        }
        public IEnumerable<ICoordinate> GetRoute(OccupancyGrid grid)
        {
            throw new NotImplementedException();
        }


    }
}
