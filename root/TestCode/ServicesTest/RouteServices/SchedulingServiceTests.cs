using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.RouteServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;
namespace Services.RouteServices.Tests
{
    [TestClass()]
    public class SchedulingServiceTests
    {
        private SchedulingService s;

        [TestInitialize]
        public void Initializer()
        {
            s = SchedulingService.Instance;
        }

        [TestMethod()]
        public void GetRouteTest()
        {
            OccupancyGrid g = new OccupancyGrid(30, 23, 10, -150, -115);
            s.GetRoute(g);
            int k = s.test();

            Assert.AreEqual(10, k);
        }
    }
}
