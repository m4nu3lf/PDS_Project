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

        private AutoResetEvent _e;     
        private Queue<Message> _q;

        private int _as;

        private const int _ns = 4;

        public Host() {
            _es = new Socket[4];
            _cs = new Socket[4];

            _q = new Queue<Message>();
            _e = new AutoResetEvent(false);

            _as = -1;

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

            /* putting in the message into the queue */

            lock (_q)
            {
                System.Console.WriteLine("Inserito Messaggio");
               _q.Enqueue(m);
            }

            /* setting the event to wake the thread */

            _e.Set();
        }


        public void SendMsg()
        {
            Message m;

            while (_e.WaitOne()) {

                /* new message */

                lock (_q)
                {
                    m = _q.Dequeue();
                }

                /* checking type */

                if (m is InitComm)
                {
                    _as = ((InitComm)m).i; // changing active socket 
                    return;
                }

                if( (_as != -1) && (_es[_as] != null) ) MsgStream.Send(m, _es[_as]); //sending data

                if (m is StopComm)   _as = -1;  // no active socket from now till a new InitComm Message
            }
        }




        /* clipboard managing */



        public void SendClipboard()
        {

        }

        public String ReceiveClipboard()
        {
            return "prova";
        }




    }

}
