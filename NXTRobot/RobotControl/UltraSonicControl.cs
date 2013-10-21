using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NKH.MindSqualls;
using NKH.MindSqualls.MotorControl;

namespace NXTRobot
{
    class UltraSonicControl
    {
        private McNxtBrick brick;
        private McNxtMotor motor;
        private NxtUltrasonicSensor sensor;
        private UltraSonicData sensorData;

        private sbyte readPower = 6;
        private sbyte rewindPower = -50;
        private int sleepBeforeRewind = 6800;

        public UltraSonicControl(McNxtBrick _brick, McNxtMotor _motor, NxtUltrasonicSensor _sensor, UltraSonicData _data)
        {
            this.brick = _brick;
            this.motor = _motor;
            this.sensor = _sensor;
            this.sensorData = _data;

            this.motor.PollInterval = 1;
            //this.motor.OnPolled += motor_OnPolled;

            this.sensor.PollInterval = 1;
            this.sensor.OnPolled += sensor_OnPolled;
        }

        void motor_OnPolled(NxtPollable polledItem)
        {
            Console.WriteLine("Tacho: " + motor.TachoCount + "\tDistance: " + sensor.DistanceCm);
            // Save value from sensor each time a tacho change is registered
            sensorData.SetByteValueAtDegree((int)motor.TachoCount, (byte)sensor.DistanceCm);
        }

        void sensor_OnPolled(NxtPollable polledItem)
        {
            Console.WriteLine("Tacho: " + motor.TachoCount + "\tDistance: " + sensor.DistanceCm);
            // Save value from sensor each time a new value is registered
            sensorData.SetByteValueAtDegree((int)motor.TachoCount, (byte)sensor.DistanceCm);
        }

        public void Get360DegreeData()
        {
            // Ensure value at current sensor position is read
            sensor.Poll();
            sensorData.SetByteValueAtDegree(1, (byte)sensor.DistanceCm);
            motor.Run(readPower, 360);

            // Sleep to ensure next commands are recognized by MotorControl
            System.Threading.Thread.Sleep(sleepBeforeRewind);

            // Reverse back to start positioin
            motor.Run(rewindPower, 360);

            // Infer readings in datastructure to ensure that each degree has a value
            //sensorData.NormalizeValues();            
        }
    }
}
