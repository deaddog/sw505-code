using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RobotServices
{
    public enum IncomingCommand
    {
        RobotRequestsLocation = 'a',
        RobotHasArrivedAtDestination = 'b'
    }
}
