using System;
using System.Collections.Generic;
using Control;
using CommonLib.DTOs;
using Services.RobotServices;

namespace SystemInterface.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ScanningControl s = new ScanningControl();
            s.GetSensorData();
            //NavigationControl nav = new NavigationControl();
            //nav.TellRobotNavigateTo(new Vector2D(50f, 50f));
            //AndersEksempelPaaBrugAfDesignOgLag();
        }

        private static void AndersEksempelPaaBrugAfDesignOgLag()
        {
            // create controller
            ScanningControl scanner = new ScanningControl();

            // get data from sensorsweep
            SensorSweepDTO dto = scanner.FullSweep();

            //display data in console
            Console.WriteLine(dto.ToString());
        }
    }
}
