// compile with: /unsafe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Input;

using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;

using PDS_Project_Common;

namespace PDS_Project_Client
{
    public partial class ClientGUI : Form
    {

        private static Host _Host;
        private static ServerPanel[] sp = new ServerPanel[4];

        private Thread eThread;

        private static VirtualKeyShort _hostHK;


        /* Keyboard and Mouse events managing variables */


        private static IntPtr KEYhook;
        private static IntPtr MOUSEhook;

        private static HookCB k_delegate = new HookCB(KeyboardCB);
        private static HookCB m_delegate = new HookCB(MouseCB);


        public ClientGUI()
        {

            _Host = new Host();

            sp[0] = new ServerPanel(0, _Host);
            sp[1] = new ServerPanel(1, _Host);
            sp[2] = new ServerPanel(2, _Host);
            sp[3] = new ServerPanel(3, _Host);

            _Host.setPanel(sp[0], 0);
            _Host.setPanel(sp[1], 1);
            _Host.setPanel(sp[2], 2);
            _Host.setPanel(sp[3], 3);


            _hostHK = VirtualKeyShort.KEY_Q;

            InitializeComponent();
            
            eThread = new Thread((new EventThread()).run);
            eThread.Start(_Host);

        }


        private void Quit()
        {
            if (MessageBox.Show("The program is going to quit, all the connections will be shutdown. Confirm?", "Closing Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                for (int k = 0; k < 4; k++) sp[k].DisconnectionReq();
                eThread.Abort();
                Application.Exit();
            }
        }


        /* EVENTS HANDLERS */

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
            this.Quit();
        }


        private void hotkeyB_click(Object sender, EventArgs e) { 
        
        }

        private void exitB_click(Object sender, EventArgs e)
        {
            this.Quit();
        }

        private void continueB_click(Object sender, EventArgs e)
        {
            KEYhook = WindowsAPI.SetWindowsHookEx(WindowsAPI.WH_KEYBOARD_LL, k_delegate, IntPtr.Zero, 0);
            MOUSEhook = WindowsAPI.SetWindowsHookEx(WindowsAPI.WH_MOUSE_LL, m_delegate, IntPtr.Zero, 0);
          
        }



        /* Capture Events */


        public static IntPtr KeyboardCB(int nCode, IntPtr wParam, IntPtr LParam)
        {
            
            KeyMsg km = new KeyMsg(); 

            unsafe
            {
                KBDLLHOOKSTRUCT* kp = (KBDLLHOOKSTRUCT*)LParam.ToPointer();
                
                km.VirtualKey = (VirtualKeyShort) kp->vkCode;
                km.Time =  kp->time;


                switch(wParam.ToInt32()){

                    case 260:
                        //Console.WriteLine(km.VirtualKey.ToString() + ": Pressed - System");
                        km.Pressed = true;
                        break;

                    case (int)ButtonEvent.WM_KEYUP:
                        //Console.WriteLine(km.VirtualKey.ToString() + ": Released");
                        km.Pressed = false;
                        break;

                    case (int)ButtonEvent.WM_KEYDOWN:
                        //Console.WriteLine(km.VirtualKey.ToString() + ": Pressed");
                        km.Pressed = true;
                        break;

                    default: return new IntPtr(1);
                
                }
             

                //  HOT KEY HOST 

                if ( km.VirtualKey == _hostHK &&  km.Pressed)
                {

                    WindowsAPI.UnhookWindowsHookEx(KEYhook);
                    WindowsAPI.UnhookWindowsHookEx(MOUSEhook);

                    //Console.WriteLine("Dectivating: SERVER");
                    _Host.EnqueueMsg(new StopComm(-1));

                    return new IntPtr(1);
                }


                for (int k = 0; k < 4; k++)
                {
                    if ((km.VirtualKey == sp[k].hk) && (km.Pressed))
                    {
                        _Host.EnqueueMsg(new StopComm(k));
                        _Host.EnqueueMsg(new InitComm(k));

                        return new IntPtr(1);
                    }
                }

                /* no hotkey identyfied , enqueing message */

                _Host.EnqueueMsg(km);

                return new IntPtr(1);
            }

        }


        public static IntPtr MouseCB(int nCode, IntPtr wParam, IntPtr LParam)
        {
            
            unsafe
            {
                MouseMsg mm = new MouseMsg();
                
                MSLLHOOKSTRUCT* mp = (MSLLHOOKSTRUCT*)LParam.ToPointer();

                mm.Dx = mp->dx;
                mm.Dy = mp->dy;
                mm.Time = mp->time;
                mm.MouseData = mp->mouseData;

                switch(wParam.ToInt32()){
                    case (int)MouseEvent.WM_LBUTTONDOWN:
                        mm.Flags = (int)MouseEventFlags.LeftButtonDown;
                        break;

                    case (int)MouseEvent.WM_LBUTTONUP:
                        mm.Flags = (int)MouseEventFlags.LeftButtonUp;
                        break;

                    case (int)MouseEvent.WM_MOUSEMOVE:
                        mm.Flags = (int)MouseEventFlags.MouseMoved;
                        break;

                    case (int)MouseEvent.WM_MOUSEWHEEL:
                        mm.Flags = (int)MouseEventFlags.Wheel;
                        break;

                    case (int)MouseEvent.WM_MOUSEHWHEEL:
                        mm.Flags = (int)MouseEventFlags.HWheel;
                        break;

                    case (int)MouseEvent.WM_RBUTTONDOWN:
                        mm.Flags = (int)MouseEventFlags.RightButtonDown;
                        break;

                    case (int)MouseEvent.WM_RBUTTONUP:
                        mm.Flags = (int)MouseEventFlags.RightButtonUp;
                        break;
                
                }

                _Host.EnqueueMsg(mm);

            }
            
            return new IntPtr(1);
        }


    }
}
