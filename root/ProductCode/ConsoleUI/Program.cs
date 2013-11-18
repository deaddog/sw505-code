using System;
using System.Collections.Generic;
using Control;
using CommonLib.DTOs;

namespace SystemInterface.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            NavigationControl nav = new NavigationControl();
            nav.TellRobotNavigateTo(new Vector2D(233.23f, 4234.53f));
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
