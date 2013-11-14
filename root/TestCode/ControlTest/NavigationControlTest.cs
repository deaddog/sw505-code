using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Control;

namespace ControlTest
{
    [TestClass]
    class NavigationControlTest
    {
        private static TestContext context;
        private NavigationControl nav;

        [ClassInitialize]
        public static void ClassInitializer(TestContext c)
        {
            context = c;
        }

        [ClassCleanup]
        public static void ClassCleaner()
        {
            context = null;
        }

        [TestInitialize]
        public void Initializer()
        {
            nav = new NavigationControl();
        }

        [TestCleanup]
        public void Cleaner()
        {
            nav = null;
        }
    }
}
