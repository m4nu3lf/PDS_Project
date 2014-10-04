using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDS_Project_Common
{

    [Serializable]
    public class Message
    {
        public Message() { }

    }


    [Serializable]
    public class StopComm : Message
    {
        private int _i;

        public StopComm(int index)
        {
            _i = index;
        }

        public int i
        {
            get { return _i; }
        }

    }

    [Serializable]
    public class InitComm : Message
    {
        private int _i;

        public InitComm(int index)
        {
            _i = index;
        }

        public int i
        {
            get { return _i; }
        }
    }

    [Serializable]
    public class AuthMsg : Message
    {
        String _psw;

        public AuthMsg(String p)
        {
            _psw = p;
        }

        public String psw
        {
            get { return _psw; }
        }

    }


    [Serializable]
    public class AckMsg : Message
    {
        bool _ack;

        public AckMsg(bool b)
        {
             _ack = b ;
        }

        public bool ack
        {
            get { return _ack; }
        }
    }


    [Serializable]
    public class KeyMsg : Message
    {
        public VirtualKeyShort VirtualKey { get; set; }
        public bool Pressed { get; set; }
        public uint Time { get; set; }
    }

    public enum MouseEventFlags : uint
    {
        LeftButtonDown = 1,
        LeftButtonUp = 2,
        RightButtonDown = 4,
        RightButtonUp = 8,
        MiddleButtonDown = 16,
        MiddleButtonUp = 32,
        Wheel = 64,
        HWheel = 128,
        MouseMoved = 256
    }


    [Serializable]
    public class MouseMsg : Message
    {
        public int Dx { get; set; }
        public int Dy { get; set; }
        public int MouseData { get; set; }
        public int Flags { get; set; }
        public uint Time { get; set; }
    }


    [Serializable]
    public class DataMsg : Message
    {
        private String clipboard;

        public DataMsg(String clb)
        {
            clipboard = clb;
        }

        public String clb
        {
            get { return clipboard; }
        }

    }


}
