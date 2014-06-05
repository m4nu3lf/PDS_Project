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
        public KeyMsg()
        {

        }
    }


    [Serializable]
    public class MouseMsg : Message
    {
        public MouseMsg()
        {

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
