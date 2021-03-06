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
                            catch (Exception e)
                            {
                                Console.WriteLine("Error during event comunication: " + e.Message);
                                System.Windows.Forms.MessageBox.Show("Ops...\nSomething goes wrong during Events Transfer.\nThe connection will be closed.\nTry again later.",
                                    "Event Transfer Error!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
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

        public void SendCB()
        {
            int i = _eas;
            if ((i != 0) && (i != 1) && (i != 2) && (i != 3)) return; 
            _sp[i].StartSendingCB();
        }

        public void EnqueueCBMsg(Message m)
        {
            lock (_dq) {
                _dq.Enqueue(m); 
            }    //enqueueing MSG
            _de.Set(); // setting the event to wake the thread
        }

        public void EnqueueCBMsg()
        {

            int i = _eas;
            if ((i != 0) && (i != 1) && (i != 2) && (i != 3)) return; 

            lock (_dq) {
                _dq.Enqueue(new GetMsgCBP(i)); //enqueueing MSG
                _sp[i].StartCBGetting();
            }    

            _de.Set(); // setting the event to wake the thread

        }

        public void ReceiveCBMsg()
        {

            Message m;
            int i = 0;

            while(true){ 
            
                while (_de.WaitOne())
                {

                    lock (_dq) { m = _dq.Dequeue(); }
                    i = ((GetMsgCBP)m).i;

                    //Console.WriteLine("Processing Getting RQ to: " + i);

                    if ( (i != 0) && (i != 1) && (i != 2) && (i != 3) ) continue;

                    if (_ds[i] != null)
                    {
                        _sp[i].CBGetting();

                        try
                        {
                            MsgStream.Send(m, _ds[i]);
                            //Console.WriteLine("richiesta di ricezione inviata");

                            do{

                                m = (Message)MsgStream.Receive(_ds[i]);

                                if (m is DataMsgCBP)
                                {
                                    DataMsgCBP dataMsgCBP = (DataMsgCBP)m;
                                    //Console.WriteLine("ricevuto pacchetto: " + dataMsgCBP.format);
                                    System.Windows.Forms.Clipboard.SetData(dataMsgCBP.format, dataMsgCBP.content);
                                }


                                if (m is InitFileCBP) //FILEMSG
                                {
                                    /*
                                    System.Windows.Forms.MessageBox.Show("Receiving file/s from server: " + i.ToString()
                                        + " .\nYou will be advised when the transfer is completed.", "Starting File/s Transfer",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                    */

                                    ClipboardFiles.RecvClipboardFiles(_ds[i]);
                                    Console.WriteLine("CBP : Received Files.");
                                    
                                    /*
                                    
                                    System.Windows.Forms.MessageBox.Show("File/s received from server: " + i.ToString()
                                        + " .", "Transfer completed",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                    */
                                    _sp[i].EnableCB();
                                    break;
                                }

                                if (m is MaxSizeCBP) 
                                {
                                    if (System.Windows.Forms.MessageBox.Show("The size of clipboard's content is greater than MaxSize: " + ClipboardFiles.MaxSize
                                            + " \nConfirm the transfer?", "File size exceeding", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                                    {


                                        System.Windows.Forms.MessageBox.Show("Receiving file/s from server: " + i.ToString()
                                            + " .\nYou will be advised when the transfer is completed.", "Starting File/s Transfer",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                                        MsgStream.Send(new ConfirmCBP(), _ds[i]);
                                        ClipboardFiles.RecvClipboardFiles(_ds[i]);

                                        System.Windows.Forms.MessageBox.Show("File/s received from server: " + i.ToString()
                                            + " .", "Transfer completed",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                                        _sp[i].EnableCB();
                                        break;
                                    }
                                    else
                                    {
                                        MsgStream.Send(new StopFileCBP(), _ds[i]);
                                        _sp[i].EnableCB();
                                        break;
                                    }
                                }

                            }while(!(m is StopFileCBP));

                            _sp[i].EnableCB();
                        }
                        catch (Exception e)
                        {
                            //Console.WriteLine("Errore nella richiesta CB: chiusura sockets e disconnessione.");
                            System.Windows.Forms.MessageBox.Show("Ops...\nSomething goes wrong during Clipboard Transfer[on Receiving].\nThe connection will be closed.\nTry again later.",
                                "Clipboard Transfer Error!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                            _sp[i].DisconnectionReq();
                            Console.WriteLine("Clipboard file transfer error: " + e.Message);
                        }

                    }//end checking socket nullity

                }//end wait condition

            }//end infinite loop

        }//end ReceiveCBMsg



    }

}
