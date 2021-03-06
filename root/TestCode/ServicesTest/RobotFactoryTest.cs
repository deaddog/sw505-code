﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.RobotServices;
using CommonLib.Interfaces;

namespace ServicesTest
{
    [TestClass]
    public class RobotFactoryTest
    {
        private static TestContext context;
        private RobotFactory factory;

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
            factory = RobotFactory.GetInstance();
        }

        [TestCleanup]
        public void Cleaner()
        {
            // cleanup after test.
            factory = null;
        }

        [TestMethod]
        public void Template_StateUnderTest_ExpectedResult()
        {
            // Arrange


            // Act


            // Assert

            Assert.Inconclusive();
        }


        [TestMethod]
        public void getInstance_NoInputParams_ReturnInstanceOfRobotFactoryClass()
        {
           // Arrange
            string a;
            int x;

            // Act
            RobotFactory fac = RobotFactory.GetInstance();

            // Assert
            Assert.IsNotNull(fac);
            Assert.IsInstanceOfType(fac, typeof(RobotFactory));
            
            
        }


        [TestMethod]
        public void createRobot_NoInputParams_ReturnObjectImplementingIRobot()
        {
            //arrange


            // act
            IRobot rob = factory.CreateRobot();

            // assert.
            Assert.IsNotNull(rob);
            Assert.IsInstanceOfType(rob, typeof(IRobot));
        }




    }
}
