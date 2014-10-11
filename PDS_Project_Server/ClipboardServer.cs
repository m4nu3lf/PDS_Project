using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PDS_Project_Common;
using System.Threading;

namespace PDS_Project_Server
{
    public class ClipboardServer : Server
    {
        public class AuthenticatedState : ReceivingState
        {
            public override void Update()
            {
                base.Update();

                if (_obj is TextMsgCBP)
                {
                    Clipboard.SetText(((TextMsgCBP)_obj).content);
                }
                else if (_obj is InitFileCBP)
                {
                    ClipboardFiles.RecvClipboardFiles(Server.CommSocket);
                }
                else if (_obj is GetMsgCBP)
                {
                    if (Clipboard.ContainsText())
                    {
                        MsgStream.Send(new TextMsgCBP(Clipboard.GetText()), Server.CommSocket);
                    }
                    if (Clipboard.ContainsFileDropList())
                    {
                        if (ClipboardFiles.GetCBFilesSize() > ClipboardFiles.MaxSize)
                        {
                            MsgStream.Send(new MaxSizeCBP(), Server.CommSocket);
                            Object response = MsgStream.Receive(Server.CommSocket);
                            if (!(response is ConfirmCBP))
                                return;
                        }
                        MsgStream.Send(new InitFileCBP(), Server.CommSocket);
                        ClipboardFiles.SendClipboardFiles(Server.CommSocket);
                        MsgStream.Send(new StopFileCBP(), Server.CommSocket);
                    }
                }
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
            _serverThread.SetApartmentState(System.Threading.ApartmentState.STA);
        }
    }
}
