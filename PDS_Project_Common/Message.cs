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
        private KEYBDINPUT _ki;

        public KeyMsg(KEYBDINPUT ki)
        {
            _ki = ki;
        }


        public KEYBDINPUT ki
        {
            get { return _ki; }
            set { _ki = value; }
        }


    }


    [Serializable]
    public class MouseMsg : Message
    {
        
        private MOUSEINPUT _mi;

        public MouseMsg(MOUSEINPUT mi)
        {
            _mi = mi;
        }


        public MOUSEINPUT mi
        {
            get { return _mi; }
            set { _mi = value; }
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
