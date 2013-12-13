using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CommonLib.Tests
{
    [TestClass()]
    public class ExtendedMathTests
    {
        [TestMethod()]
        public void DefIntegrateTest()
        {
            double res = ExtendedMath.DefIntegrate(x=> 2*x+7, 10, 20);

            Assert.AreEqual(370, res);
        }
    }
}
