using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace PDS_Project_Client
{


    public class EventThread
    {
        public void run(object param)
        {
            Host _host = (Host)param;
            _host.SendMsg();
        }


    }

    public class DataThread
    {
        public void run(object param)
        {
            Host _host = (Host)param;
            _host.SendCBMsg();
        }

    }

}
