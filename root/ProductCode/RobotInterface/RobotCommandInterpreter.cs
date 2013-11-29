﻿using System;
using CommonLib;
using CommonLib.NXTPostMan;
using CommonLib.Interfaces;
using CommonLib.DTOs;
using System.Threading;
using Control;

namespace SystemInterface.RobotInterface
{
    public class RobotCommandInterpreter
    {
        private INXTPostMan postman;
        private bool RUNNING = true;
        private const int THREAD_SLEEP_INTERVAL_IN_MILLISECONDS = 10000;

        #region cTor Chain.

        /// <summary>
        /// Default cTor.
        /// </summary>
        public RobotCommandInterpreter() : this(PostMan.getInstance()) { }

        /// <summary>
        /// Master cTor.
        /// </summary>
        public RobotCommandInterpreter(PostMan p)
        {
            postman = p;
            new Thread(new ThreadStart(this.listener)).Start();
        }

        #endregion

        private void listener()
        {
            while(RUNNING) {

                if (postman.HasMessageArrived())
                {

                    NXTMessage msg = postman.RetrieveMessage();

                    switch(msg.MessageType) {

                        case(NXTMessageType.RobotRequestsLocation):
                            
                            RobotRequestLocation(msg);
                            break;
                        case(NXTMessageType.RobotHasArrivedAtDestination):

                            RobotHasArrivedAtDestination(msg);
                            break;
                        case(NXTMessageType.SendSensorData):

                            SendSensorData(msg);
                            break;
                        default:
                            throw new Exception("Dont know what to do ???");
                    }

                }
                Thread.Sleep(THREAD_SLEEP_INTERVAL_IN_MILLISECONDS);
            }
        }

        #region Command Handlers

        private void RobotRequestLocation(NXTMessage msg) {

            
            throw new NotImplementedException();
        }

        private void RobotHasArrivedAtDestination(NXTMessage msg) {

            throw new NotImplementedException();
        }

        private void SendSensorData(NXTMessage msg)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
