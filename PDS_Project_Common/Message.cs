using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace PDS_Project_Common
{

    [Serializable]
    public class Message
    {
        public Message() { }
    }

    /* AUTHENTICATION MSG PROTOCOL */

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
            _ack = b;
        }

        public bool ack
        {
            get { return _ack; }
        }
    }



    /* EVENT MSG PROTOCOL */

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
    public class KeyMsg : Message
    {
        public VirtualKeyShort VirtualKey { get; set; }
        public bool Pressed { get; set; }

        public KeyMsg() { }

        public KeyMsg(VirtualKeyShort vk, bool p)
        {
            VirtualKey = vk;
            Pressed = p;
        }
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
        public uint Flags { get; set; }
    }




    /* CLIPBOARD MSG PROTOCOL */


    [Serializable]
    public class GetMsgCBP : Message
    {
        private int _i;

        public GetMsgCBP(int index) { _i = index; }

        public int i { get { return _i; } }

    }



    /* TEXT PROTOCOL */

    [Serializable]
    public class TextMsgCBP : Message
    {
        public string content { set; get; }
        public TextMsgCBP(string c) 
        {
            content = c;
        }

    }



    /* FILE PROTOCOL */
    
    [Serializable]
    public class InitFileCBP : Message
    {
        public InitFileCBP(){ }
    }


    [Serializable]
    public class DirMsgCBP : Message
    {
        public string name { get; set; }
        public bool root { get; set; }

        public DirMsgCBP(string n, bool r)
        {
            name = n;
            root = r;
        }

    }

    [Serializable]
    public class FileMsgCBP : Message
    {
        public string name { get; set; }
        public Byte[] content { get; set; }
        public bool root { get; set; }

        public FileMsgCBP(string n, Byte[] c, bool r)
        {
            content = c;
            name = n;
            root = r;
        }

    }


    [Serializable]
    public class StopFileCBP : Message
    {
        public StopFileCBP() { }
    }

    [Serializable]
    public class ConfirmCBP : Message
    {
        public ConfirmCBP() { }
    }

    [Serializable]
    public class MaxSizeCBP : Message
    {
        public MaxSizeCBP() { }
    }


}
