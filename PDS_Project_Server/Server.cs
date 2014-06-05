using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using PDS_Project_Common;

namespace PDS_Project_Server
{
    class Server
    {
        public enum State
        {
            Disconnected,
            Waiting,
            Connected,
            Active
        }

        public delegate void OnStateChanged(State newState); 

        public Server(OnStateChanged onStateChanged)
        {
            _startEvent = new AutoResetEvent(false);
            _serverThread = new Thread(this.run);
            _serverThread.Start();
            _onStateChanged = onStateChanged;
        }

        public void run()
        {
            while (!_termRequest)
            {
                _onStateChanged(State.Disconnected);
                _startEvent.WaitOne();
                while (!_stopRequest && !_termRequest)
                {
                    if (_commSocket == null)
                    {
                        try
                        {

                            Console.WriteLine("Listening");
                            _onStateChanged(State.Waiting);
                            _commSocket = _welcomeSocket.Accept();
                            _onStateChanged(State.Connected);
                            Console.WriteLine("Connected");
                        }
                        catch (SocketException)
                        {
                        }
                    }
                    else
                    {
                        object obj = MsgStream.Receive(_commSocket);
                        if (obj is AuthMsg)
                        {
                            // authentication
                            AuthMsg authMsg = (AuthMsg)obj;
                            if (authMsg.psw == _password)
                            {
                                MsgStream.Send(new AckMsg(true), _commSocket);
                            }
                            else
                            {
                                MsgStream.Send(new AckMsg(false), _commSocket);
                                _stopRequest = true;
                            }
                        } else if (obj is )
                    }
                }
                _stopRequest = false;
            }
        }

        public void Start(IPAddress address, int port, String password)
        {
            lock (_startNStopLock)
            {
                if (_welcomeSocket != null)
                    return;
                _welcomeSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                _welcomeSocket.Bind(new IPEndPoint(address, port));
                _welcomeSocket.Listen(1);
                _stopRequest = false;
                _termRequest = false;
                _startEvent.Set();
                _password = password;
            }
        }

        public void Stop()
        {
            lock (_startNStopLock)
            {
                if (_welcomeSocket == null)
                    return;
                _stopRequest = true;
                _welcomeSocket.Close();
                if (_commSocket != null)
                {
                    try
                    {
                        _commSocket.Close();
                    }
                    catch (SocketException e)
                    {

                    }
                }
                _welcomeSocket = null;
                _commSocket = null;
            }
        }

        public void Terminate()
        {
            _termRequest = true;
            _startEvent.Set();
        }

        private Thread _serverThread;
        private Socket _welcomeSocket;
        private Socket _commSocket;
        private bool _stopRequest = false;
        private bool _termRequest = false;
        private OnStateChanged _onStateChanged;
        private String _password;

        private object _startNStopLock =  new object();
        private AutoResetEvent _startEvent;
    }
}
