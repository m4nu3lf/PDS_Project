﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using PDS_Project_Common;

namespace PDS_Project_Server
{
    public class Server
    {
        public class StateBase
        {
            public Server Server
            {
                set; get;
            }

            /// <summary>
            ///  To be called whenever the state enters
            /// </summary>
            virtual public void Enter()
            {
            }

            /// <summary>
            /// Unlocks the state from a waiting condition
            /// It is safe to call this function from another thread.
            /// </summary>
            virtual public void Signal()
            {
            }

            /// <summary>
            /// To be called in a loop as many time as possible
            /// </summary>
            virtual public void Update()
            {

            }

            /// <summary>
            /// To be called whenever the state exits
            /// </summary>
            virtual public void Exit()
            {

            }
        }

        public class DisconnectedState : StateBase
        {
            public override void Update()
            {
                _startEvent.WaitOne();
                Server.State = new WaitingState();
            }

            public override void Signal()
            {
                _startEvent.Set();
            }

            public override string ToString()
            {
                return "Disconnected";
            }

            private AutoResetEvent _startEvent = new AutoResetEvent(false);
        }

        public class WaitingState : StateBase
        {

            public override void Update()
            {
                try
                {
                    Console.WriteLine("Listening");
                    Server._commSocket = Server._welcomeSocket.Accept();
                    Server.State = new ConnectedState();
                    Console.WriteLine("Connected");
                }
                catch (SocketException)
                {
                }
                catch (ObjectDisposedException)
                {
                }
            }

            public override void Signal()
            {
                try
                {
                    if (Server._welcomeSocket != null)
                        Server._welcomeSocket.Close();
                }
                catch (SocketException)
                {

                }
            }

            public override string ToString()
            {
                return "Waiting For Connection";
            }
        }

        public class ReceivingState : StateBase
        {
            protected object _obj;
            public override void Update()
            {
                try
                {
                    _obj = MsgStream.Receive(Server._commSocket);
                }
                catch (SocketException)
                {
                    Server.State = new WaitingState();
                    _obj = null;
                }
            }

            public override void Signal()
            {
                try
                {
                    Server._commSocket.Close();
                }
                catch (SocketException)
                {

                }
            }
        }

        public class ConnectedState : ReceivingState
        {
            public override void Update()
            {
                base.Update();

                if (_obj is AuthMsg)
                {
                    // authentication
                    AuthMsg authMsg = (AuthMsg)_obj;
                    if (authMsg.psw == Server._password)
                    {
                        MsgStream.Send(new AckMsg(true), Server._commSocket);
                        Server.State = new AuthenticatedState();
                    }
                    else
                    {
                        MsgStream.Send(new AckMsg(false), Server._commSocket);
                        Server.State = new WaitingState();
                    }
                }
            }

            public override string ToString()
            {
                return "Waiting For Authentication";
            }
        }

        public class AuthenticatedState : ReceivingState
        {
            public override void Update()
            {
                base.Update();
                
                if (_obj is InitComm)
                {
                    Server.State = new ActiveState();
                }
            }

            public override string ToString()
            {
                return "Connected";
            }

        }

        public class ActiveState : ReceivingState
        {
            public override void Update()
            {
                base.Update();

                if (_obj is StopComm)
                {
                    Server.State = new WaitingState();
                }
                else if (_obj is KeyMsg)
                {
                    INPUT[] inputs = new INPUT[1];
                    inputs[0].type = (uint)InputType.INPUT_KEYBOARD;
                    inputs[0].U.ki = ((KeyMsg)_obj).ki;
                    WindowsAPI.SendInput(1, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));
                }
                else if (_obj is MouseMsg)
                {
                    INPUT[] inputs = new INPUT[1];
                    inputs[0].type = (uint)InputType.INPUT_MOUSE;
                    inputs[0].U.mi = ((MouseMsg)_obj).mi;
                    WindowsAPI.SendInput(1, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));
                }
            }

            public override string ToString()
            {
                return "Active";
            }
        }

        public StateBase State
        {
            set
            {
                try
                {
                    _newState = value;
                    _newState.Server = this;
                    _state.Signal();
                }
                catch (NullReferenceException)
                {

                }
            }

            get
            {
                return _state;
            }
        }

        public delegate void OnStateChanged(StateBase newState); 

        public Server(OnStateChanged onStateChanged)
        {
            _onStateChanged = onStateChanged;
            State = new DisconnectedState();
            _state = new StateBase();
            _state.Server = this;
            _serverThread = new Thread(this.Run);
            _serverThread.Start();
        }

        public void Run()
        {
            while (_state != null)
            {
                _state.Update();

                if (_newState != _state)
                {
                    _state.Exit();

                    lock (_state)
                    {
                        _state = _newState;
                    }

                    if (_state != null)
                        _state.Enter();

                    _onStateChanged(_state);
                }
            }

        }

        public void Start(IPAddress address, int port, String password)
        {
            lock (State)
            {
                if (! (State is DisconnectedState))
                    return;

                _welcomeSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                _welcomeSocket.Bind(new IPEndPoint(address, port));
                _welcomeSocket.Listen(1);
                _password = password;

                State.Signal();
            }
        }

        public void Stop()
        {
            State = new DisconnectedState();
        }

        public void Terminate()
        {
            State = null;
        }

        private Thread _serverThread;
        private Socket _welcomeSocket;
        private Socket _commSocket;
        private String _password;
        private StateBase _state;
        private StateBase _newState;
        private OnStateChanged _onStateChanged;
    }
}
