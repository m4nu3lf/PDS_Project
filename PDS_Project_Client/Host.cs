using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using PDS_Project_Common;

namespace PDS_Project_Client
{
    public class Host
    {
        private Socket eventSocket;
        private Socket clipboardSocket;

        public Host() { }

        public Host(Socket es, Socket cs )
        {
            eventSocket = es;
            clipboardSocket = cs;
        }

        public Socket es
        {
            get { return eventSocket; }
            set { eventSocket = value; }
        }

        public Socket cs
        {
            get { return clipboardSocket; }
            set { clipboardSocket = value; }
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
