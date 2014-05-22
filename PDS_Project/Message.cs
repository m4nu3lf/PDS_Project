using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDS_Project
{
    public enum CmType : byte
    {
        Authentication,
        EnterComm,
        ExitComm,
        Mouse,
        Key,
        Data
    }

    public class Message
    {
        private CmType cmType; 
    

        protected Message(CmType cmt){
            cmType = cmt;
        }

        public CmType type
        {
            get { return cmType; }
        }

    }

    public class AuthMsg : Message
    {
        String psw;

        public AuthMsg(CmType cmt, String p) 
            : base(cmt) 
        {
            psw = p;
        }
    
    }


    public class KeyMsg : Message
    {
        public KeyMsg(CmType cmt)
            : base(cmt)
        {

        }
    }


    public class MouseMsg : Message
    {
        public MouseMsg(CmType cmt)
            : base(cmt)
        {

        }
    }


    public class DataMsg : Message
    {
        private String clipboard;

        public DataMsg(CmType cmt, String clb)
            : base(cmt)
        {
            clipboard = clb;
        }

        public String clb
        {
            get { return clipboard; }
        }
    }

}
