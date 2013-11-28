using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.NXTPostMan
{
    public class NoMailException : ApplicationException
    {
        const string NO_MAIL_EXCEPTION = "No mail recieved";


        public NoMailException() : base(NO_MAIL_EXCEPTION) { }

    }
}
