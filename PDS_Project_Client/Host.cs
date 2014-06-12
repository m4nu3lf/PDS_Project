using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

using PDS_Project_Common;

namespace PDS_Project_Client
{
    public class Host
    {
        private Socket[] _es;        //event socket vector
        private Socket[] _cs;        //clipboard socket vector

        private Socket _aes;        //active event socket
        private Socket _acs;        //active clipboard socket

        private AutoResetEvent _e;     
        private Queue<Message> _q;


        public Host(int i) {
            _es = new Socket[i];
            _cs = new Socket[i];

            _q = new Queue<Message>();
            _e = new AutoResetEvent(false);

        }

        public Socket aes
        {
            set
            {
                lock (_e)
                {
                    _aes = value;
                }
            }
        }

        public Socket acs
        {
            set {
                lock (_e)
                {
                    _acs = value;
                }
            }
        }


        /* GET A SOCKET */

        public Socket es(int i)
        {
            return _es[i];
        }

        public Socket cs(int i)
        {
            return _cs[i];
        }


        /* SET A SOCKET */


        public void es(Socket es, int i)
        {
            _es[i] = es;
        }

        public void cs(Socket cs, int i)
        {
            _cs[i] = cs;
        }


        public void EnqueueMsg(Message m)
        {
            lock (_e)
            {

                //ENQUEUE ;
            }

            _e.Set();
        }


        public void SendMsg()
        {
            lock (_e)
            {
                // DEQUEUE
                // INVIO

                //if msg è 
            }
        }

        public void SendClipboard()
        {

        }

        public String ReceiveClipboard()
        {
            return "prova";
        }

    }

}
