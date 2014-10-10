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
    public class EventServer : Server
    {
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

            public override string GetMsg()
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
                    Server.State = new AuthenticatedState();
                }
                else if (_obj is KeyMsg)
                {
                    KeyMsg kMsg = (KeyMsg)_obj;
                    INPUT[] inputs = new INPUT[1];
                    inputs[0].type = (uint)InputType.INPUT_KEYBOARD;
                    inputs[0].U.ki.wVk = kMsg.VirtualKey;
                    Console.WriteLine(kMsg.VirtualKey);
                    Console.WriteLine(kMsg.Pressed);
                    if (!kMsg.Pressed)
                        inputs[0].U.ki.dwFlags = KEYEVENTF.KEYUP;
                    else
                        inputs[0].U.ki.dwFlags = 0;
                    inputs[0].U.ki.time = 0;
                    inputs[0].U.ki.dwExtraInfo = (UIntPtr)(UInt64)0;
                    WindowsAPI.SendInput(1, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));
                }
                else if (_obj is MouseMsg)
                {
                    MouseMsg mMsg = (MouseMsg)_obj; 
                    INPUT[] inputs = new INPUT[1];
                    inputs[0].type = (uint)InputType.INPUT_MOUSE;
                    inputs[0].U.mi.dx = mMsg.Dx;
                    inputs[0].U.mi.dy = mMsg.Dy;
                    MOUSEEVENTF dwFlags = 0;
                    if ((mMsg.Flags & (int)MouseEventFlags.LeftButtonDown) != 0)
                        dwFlags |= MOUSEEVENTF.LEFTDOWN;
                    if ((mMsg.Flags & (int)MouseEventFlags.LeftButtonUp) != 0)
                        dwFlags |= MOUSEEVENTF.LEFTUP;
                    if ((mMsg.Flags & (int)MouseEventFlags.RightButtonDown) != 0)
                        dwFlags |= MOUSEEVENTF.RIGHTDOWN;
                    if ((mMsg.Flags & (int)MouseEventFlags.RightButtonUp) != 0)
                        dwFlags |= MOUSEEVENTF.RIGHTUP;
                    if ((mMsg.Flags & (int)MouseEventFlags.MiddleButtonDown) != 0)
                        dwFlags |= MOUSEEVENTF.MIDDLEDOWN;
                    if ((mMsg.Flags & (int)MouseEventFlags.MiddleButtonUp) != 0)
                        dwFlags |= MOUSEEVENTF.MIDDLEUP;
                    if ((mMsg.Flags & (int)MouseEventFlags.Wheel) != 0)
                        dwFlags |= MOUSEEVENTF.WHEEL;
                    if ((mMsg.Flags & (int)MouseEventFlags.HWheel) != 0)
                        dwFlags |= MOUSEEVENTF.HWHEEL;
                    if ((mMsg.Flags & (int)MouseEventFlags.MouseMoved) != 0)
                        dwFlags |= MOUSEEVENTF.MOVE;
                    inputs[0].U.mi.dwFlags = dwFlags;
                    inputs[0].U.mi.mouseData = mMsg.MouseData;
                    inputs[0].U.mi.time = 0;
                    inputs[0].U.mi.dwExtraInfo = (UIntPtr)(UInt64)0;
                    WindowsAPI.SendInput(1, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));
                }
            }

            public override string GetMsg()
            {
                return "Active";
            }
        }

        override protected void SetAuthenticated()
        {
            State = new AuthenticatedState();
        }

        public EventServer(OnStateChanged onStateChanged = null) : base(onStateChanged)
        {
            
        }
    }
}
