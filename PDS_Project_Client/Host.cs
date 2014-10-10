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
        private ServerPanel[] _sp;

        /* variables for managing events */

        private Socket[] _es;        //event socket vector
        private AutoResetEvent _ee;
        private Queue<Message> _eq;
        private Queue<Message> _eqToSend;

        private int _eas;       //event socket active

        /* variables for managing clipboard protocol */

        private Socket[] _ds;        //clipboard socket vector
        private AutoResetEvent _de;
        private Queue<Message> _dq; 


        public Host() {
            _es = new Socket[4];
            _ds = new Socket[4];
            _sp = new ServerPanel[4];


            _eq = new Queue<Message>();
            _ee = new AutoResetEvent(false);
            _eqToSend = new Queue<Message>();


            _dq = new Queue<Message>();
            _de = new AutoResetEvent(false);

            _eas = -1;
        }
        

        /* GET A SOCKET */

        public Socket es(int i){ return _es[i]; }
        public Socket ds(int i){ return _ds[i]; }


        /* SET A SOCKET */

        public void es(Socket es, int i){ _es[i] = es; }
        public void ds(Socket ds, int i){ _ds[i] = ds; }


        /* PANEL SETTING */

        public void setPanel(ServerPanel sp, int i) { _sp[i] = sp; }





        /* MSG MANAGING FUNCTION */


        public void EnqueueEventMsg(Message m)
        {
            lock (_eq) { _eq.Enqueue(m); }    //enqueueing MSG
            _ee.Set(); // setting the event to wake the thread
        }


        public void SendMsg()
        {

            Message m;
            int i = 0;

            while (true)
            {
                while (_ee.WaitOne())
                {
                    lock (_eq)
                    {
                        while (true)
                        {
                            try
                            {
                                m = _eq.Dequeue();
                                _eqToSend.Enqueue(m);
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
                        m = _eqToSend.Dequeue();
                        i--;

                        /* checking type */

                        if ((m is StopComm) && (_eas == ((StopComm)m).i)) continue;

                        if (m is InitComm)
                        {
                            if (_eas != -1)
                            {
                                if (_es[_eas] == null) _eas = -1;
                                else continue;
                            }

                            _eas = ((InitComm)m).i; // changing active socket 
                        }

                        if ((_eas != -1) && (_es[_eas] != null))
                        {
                            try
                            {
                                MsgStream.Send(m, _es[_eas]); //sending data
                                //Console.WriteLine("Sent");
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Errore nell'invio: chiusura sockets.");
                                _sp[_eas].DisconnectionReq();
                                _eas = -1;
                                continue;
                            }

                            if (m is InitComm)
                            {
                                _sp[_eas].Activation();
                                continue;
                            }

                            if (m is StopComm)
                            {
                                //Console.WriteLine("StopComm Msg Processed Well");
                                _sp[_eas].Deactivation();
                                _eas = -1;  // no active socket from now till a new InitComm Message
                            }

                        }

                    }//end sending loop

                }//end wait on event

            }//end infinite loop

        }//end SendMsg



        /* clipboard managing */

        public void EnqueueCBMsg(Message m)
        {
            lock (_dq) { _dq.Enqueue(m); }    //enqueueing MSG
            _de.Set(); // setting the event to wake the thread
        }


        public void ReceiveCBMsg()
        {

            Message m;
            int i = 0;

            while (true)
            {

                while (_de.WaitOne())
                {

                    lock (_dq) { m = _dq.Dequeue(); }
                    i = ((GetMsgCBP)m).i;

                    if (_ds[i] != null)
                    {
                        
                        try
                        {
                            MsgStream.Send(m, _ds[i]);
                            m = (Message)MsgStream.Receive(_ds[i]);


                            if (m is TextMsgCBP) //TEXTMSG
                            {
                                string content = ((TextMsgCBP)m).content;
                                System.Windows.Forms.Clipboard.SetText(content);
                            }

                            if (m is InitFileMsgCBP) //FILEMSG
                            {

                                System.Windows.Forms.MessageBox.Show("Receiving file/s from server: " + i.ToString()
                                    + " .\nYou will be advised when the transfer is completed.", "Starting Transfer",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                                //chiamo il metodo

                                System.Windows.Forms.MessageBox.Show("File/s received from server: " + i.ToString()
                                    + " .", "Transfer completed",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                            
                            }

                            if (m is MaxSizeMsgCBP) 
                            { 
                                //se confermo
                                //chiamo metodo per ricevere file
                                //se non confermo esco
                            }

                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Errore nella richiesta CB: chiusura sockets e disconnessione.");
                            _sp[i].DisconnectionReq();
                        }

                    }//end checking socket nullity

                }//end wait condition

            }//end infinite loop

        }//end ReceiveCBMsg



    }

}
