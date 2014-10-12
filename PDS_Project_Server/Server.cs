using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using PDS_Project_Common;

namespace PDS_Project_Server
{
    public abstract class Server
    {
        public class StateBase
        {
            public Server Server
            {
                set;
                get;
            }

            virtual public String GetMsg()
            {
                return "None";
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
                // Ensure previous socket is closed correctly
                if (Server._commSocket != null && Server._commSocket.Connected)
                {
                    Server._commSocket.Shutdown(SocketShutdown.Both);
                    Server._commSocket.Close();
                }

                _startEvent.WaitOne();
                Server.State = new WaitingState();
            }

            public override void Signal()
            {
                _startEvent.Set();
            }

            public override string GetMsg()
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

            public override string GetMsg()
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
                    _obj = null;
                    _obj = MsgStream.Receive(Server._commSocket);
                    if (_obj == null)
                        Server.State = new WaitingState();
                }
                catch (SocketException)
                {

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
                        Server.SetAuthenticated();
                    }
                    else
                    {
                        MsgStream.Send(new AckMsg(false), Server._commSocket);
                        Server.State = new WaitingState();
                    }
                }
            }

            public override string GetMsg()
            {
                return "Waiting For Authentication";
            }
        }

        public void Run()
        {
            while (_running)
            {
                if (_state != null)
                    _state.Update();

                if (_newState != _state)
                {
                    if (_state != null)
                        _state.Exit();

                    StateBase tmpState;
                    lock (_newStateLock)
                    {
                        tmpState = _newState;
                    }

                    lock (_stateLock)
                    {
                        _state = tmpState;
                    }

                    if (_state != null)
                        _state.Enter();

                    if (_onStateChanged != null)
                        _onStateChanged(_state);
                }
            }

        }

        public void Start(int port, String password)
        {
            if (_serverThread.ThreadState == ThreadState.Unstarted)
                _serverThread.Start();
            lock (_stateLock)
            {
                if (!(_state is DisconnectedState))
                    return;

                _welcomeSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    _welcomeSocket.Bind(new IPEndPoint(IPAddress.Any, port));
                }
                catch (SocketException e)
                {
                    MessageBox.Show("The specified address and port couple is already in use "
                                    + "or it has not yet been freed by the system!",
                                    "Address Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return;
                }
                _welcomeSocket.Listen(1);
                _password = password;

                State.Signal();
            }
        }

        public void Stop()
        {
            StateBase prevState = State;
            State = new DisconnectedState();
            prevState.Signal();
        }

        public void Terminate()
        {
            _running = false;
            State.Signal();
        }

        public StateBase State
        {
            set
            {
                try
                {
                    lock (_newStateLock)
                    {
                        _newState = value;
                        _newState.Server = this;
                    }
                }
                catch (NullReferenceException)
                {

                }
            }

            get
            {
                StateBase tmpState;
                lock (_stateLock)
                {
                    tmpState = _state;
                }
                return tmpState;
            }
        }

        public Socket CommSocket
        {
            get
            {
                return _commSocket;
            }
        }

        abstract protected void SetAuthenticated();

        public Server(OnStateChanged onStateChanged)
        {
            _running = true;
            _onStateChanged = onStateChanged;
            _state = new StateBase();
            _newState = new DisconnectedState();
            _newState.Server = this;
            _serverThread = new Thread(this.Run);
            _serverThread.SetApartmentState(System.Threading.ApartmentState.STA);
            _serverThread.Start();
        }

        public delegate void OnStateChanged(StateBase newState = null); 

        protected Thread _serverThread;
        protected Socket _welcomeSocket;
        protected Socket _commSocket;
        protected String _password;
        protected StateBase _state;
        protected Object _stateLock = new Object();
        protected StateBase _newState;
        protected Object _newStateLock = new Object();
        protected OnStateChanged _onStateChanged;

        protected bool _running;
    }
}
