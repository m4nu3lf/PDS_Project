using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDS_Project_Server
{
    public class ClipboardServer : Server
    {
        public class AuthenticatedState : ReceivingState
        {
            public override void Update()
            {
                base.Update();
            }

            public override string GetMsg()
            {
                return "Connected";
            }

        }

        override protected void SetAuthenticated()
        {
            State = new AuthenticatedState();
        }

        public ClipboardServer(OnStateChanged onStateChanged = null) : base(onStateChanged)
        {
            
        }
    }
}
