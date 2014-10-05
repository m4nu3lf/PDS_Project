﻿using System;
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

        private ServerPanel[] _sp;

        private AutoResetEvent _e;
        private Queue<Message> _q;
        private Queue<Message> _qToSend;

        private int _as;


        public Host() {
            _es = new Socket[4];
            _cs = new Socket[4];

            _sp = new ServerPanel[4];

            _q = new Queue<Message>();
            _e = new AutoResetEvent(false);

            _qToSend = new Queue<Message>();

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


        /* PANEL SETTING */

        public void setPanel(ServerPanel sp, int i) 
        { 
            _sp[i] = sp;
        }


        /* MSG MANAGING FUNCTION */


        public void EnqueueMsg(Message m)
        {

            /* putting in the message into the queue */
            lock (_q)
            {
                //System.Console.WriteLine("Inserito Messaggio");
               _q.Enqueue(m);
            }

            /* setting the event to wake the thread */

            _e.Set();
        }


        public void SendMsg()
        {

            Message m;
            int i = 0;

            while (_e.WaitOne())
            {
                lock (_q)
                {
                    while (true)
                    {
                        try
                        {
                            m = _q.Dequeue();
                            _qToSend.Enqueue(m);
                            i++;
                        }
                        catch (InvalidOperationException)
                        {
                            break;
                        }
                    }
                }

                while (i != 0)
                {
                    m = _qToSend.Dequeue();
                    i--;

                    /* checking type */


                    if ((m is StopComm) && (_as == ((StopComm)m).i) ) continue;

                    if (m is InitComm)
                    {
                        if (_as != -1)
                        {
                            if ( _es[_as]== null ) _as = -1;
                            else continue;
                        }
                        
                        _as = ((InitComm)m).i; // changing active socket 
                    }

                    if ((_as != -1) && (_es[_as] != null))
                    {
                        try
                        {
                            MsgStream.Send(m, _es[_as]); //sending data
                            //Console.WriteLine("Sent");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Errore nell'invio: chiusura socket.");
                            _sp[_as].DisconnectionReq();
                            _as = -1;
                            continue;
                        }

                        if (m is InitComm)
                        {
                            _sp[_as].Activate();
                            continue;
                        }

                        if (m is StopComm)
                        {
                            //Console.WriteLine("StopComm Msg Processed Well");
                            _sp[_as].Deactivate();
                            _as = -1;  // no active socket from now till a new InitComm Message
                        }

                    }

                }//end sending loop

            }//end wait on event

        }//end SendMsg



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
