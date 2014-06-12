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
    { }

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
        private bool _pressed;
        private short _wVk;
        private int _time;

        public KeyMsg(bool p, short wVk, int t)
        {
            _pressed = p;
            _wVk = wVk;
            _time = t;
        }


        public bool pressed
        {
            get { return _pressed; }
            set { _pressed = value; }
        }


        public short wVk
        {
            get { return _wVk; }
            set { _wVk = value; }
        }


        public int time
        {
            get { return _time; }
            set { _time = value; }
        }

    }


    [Serializable]
    public class MouseMsg : Message
    {
        private MOUSEEVENTF _me;
        private int _dx, _dy;
        private int _dw;

        public MouseMsg(MOUSEEVENTF me, int dx, int dy, int dw)
        {
            _me = me;
            _dx = dx;
            _dy = dy;
            _dw = dw;
        }

        public int dx
        {
            get { return _dx; }
            set { _dx = value; }
        }


        public int dy
        {
            get { return _dy; }
            set { _dy = value; }
        }


        public int dw
        {
            get { return _dw; }
            set { _dw = value; }
        }

        public MOUSEEVENTF me
        {
            get { return _me; }
            set { _me = value; }
        }

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
