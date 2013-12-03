using CommonLib.Interfaces;
using Data;
using Services.RobotServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control
{
    /// <summary>
    /// Controls the robot on the map
    /// </summary>
    public class MappingControl
    {
        IRobot robot;
        ISensorModel sensorModel;
        OccupancyGrid oldGrid;
        OccupancyGrid newGrid;
        MapControl mapper;
        ScanningControl scanner;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingControl"/> class.
        /// </summary>
        public MappingControl() : this(RobotFactory.GetInstance()) { }

        private MappingControl(RobotFactory factory)
        {
            robot = factory.CreateRobot();
            sensorModel = SensorModelFactory.GetInstance().CreateSimpleSensorModel();
            mapper = new MapControl();
            scanner = new ScanningControl();
        }

        public void MapAtDest(QueueControl queue)
        {
            GoToNext(queue);
        }

        private void GoToNext(QueueControl queue)
        {
            queue.Arrived += arrived;
            queue.StartQueue();
        }

        private void arrived(object sender, EventArgs e)
        {
            MapCurrent();
        }

        private void MapCurrent()
        {
            newGrid = mapper.UpdateOccupancyGrid(oldGrid, sensorModel, scanner.GetSensorData());
        }
    }
}
