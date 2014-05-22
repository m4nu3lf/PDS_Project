using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace PDS_Project
{
    public class Host
    {
        private Socket eventSocket;
        private Socket clipboardSocket;

        public Host(ref Socket es, ref Socket cs )
        {
            eventSocket = es;
            clipboardSocket = cs;
        }

        public void SendMsg(Message m)
        {

        }

        public void SendClipboard(Message m)
        {

        }

        public String ReceiveClipboard()
        {
            return "prova";
        }

    }

}
