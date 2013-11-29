using System;
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
        private const int THREAD_SLEEP_INTERVAL_IN_MILLISECONDS = 10000;

        private INXTPostMan postman;
        private bool RUNNING = true;
        private LocationControl locCon;
        private NavigationControl navCon;

        #region cTor Chain.

        /// <summary>
        /// Default cTor.
        /// </summary>
        public RobotCommandInterpreter() : this(PostMan.getInstance(), new LocationControl(), new NavigationControl()) { }

        /// <summary>
        /// Master cTor.
        /// </summary>
        public RobotCommandInterpreter(PostMan p, LocationControl loc, NavigationControl nav)
        {
            postman = p;
            locCon = loc;
            navCon = nav;
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

            ICoordinate cord = locCon.FindRobotLocation();
            NXTMessage outMsg = new NXTMessage(NXTMessageType.RobotRequestsLocation, NXTEncoder.Encode(cord));
            postman.SendMessage(outMsg);
        }

        private void RobotHasArrivedAtDestination(NXTMessage msg) {

            navCon.SendRobotToNextLocation();
        }

        private void SendSensorData(NXTMessage msg)
        {

            throw new NotImplementedException();
        }
        #endregion
    }
}
