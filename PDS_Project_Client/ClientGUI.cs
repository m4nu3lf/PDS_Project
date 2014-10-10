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
        private static ServerPanel[] _sp = new ServerPanel[4];

        private Thread eThread;
        private Thread dThread;

        private static VirtualKeyShort _hostHK;
        private static HookCB sk_delegate = new HookCB(KswitchCB);
        private static HookCB sm_delegate = new HookCB(MswitchCB);

        private static int _HKindex;
        private static VirtualKeyShort _nVKS;

        /* Keyboard and Mouse events managing variables */


        private static IntPtr KEYhook;
        private static IntPtr MOUSEhook;

        private static HookCB k_delegate = new HookCB(KeyboardCB);
        private static HookCB m_delegate = new HookCB(MouseCB);

        private static Point m_pos;
        private static bool CTRL, ALT;

        public ClientGUI()
        {

            _Host = new Host();

            _sp[0] = new ServerPanel(0, _Host);
            _sp[1] = new ServerPanel(1, _Host);
            _sp[2] = new ServerPanel(2, _Host);
            _sp[3] = new ServerPanel(3, _Host);

            _Host.setPanel(_sp[0], 0);
            _Host.setPanel(_sp[1], 1);
            _Host.setPanel(_sp[2], 2);
            _Host.setPanel(_sp[3], 3);


            _hostHK = VirtualKeyShort.KEY_Q;

            InitializeComponent();
            
            eThread = new Thread((new EventThread()).run);
            eThread.Start(_Host);

            dThread = new Thread((new DataThread()).run);
            dThread.SetApartmentState(ApartmentState.STA);
            dThread.Start(_Host);

            MessageBox.Show("Remember: to switch use L.ctrl + L.alt + 'HotKey'.", "How To Switch", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }


        private void Quit()
        {
            if (MessageBox.Show("The program is going to quit, all the connections will be shutdown.\nConfirm?", "Closing Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                for (int k = 0; k < 4; k++) _sp[k].DisconnectionReq();
                eThread.Abort();
                dThread.Abort();
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

        private void exitB_click(Object sender, EventArgs e)
        {
            this.Quit();
        }


        private void hotkeyB_click(Object sender, EventArgs e) 
        {
            String s = ((Button)sender).Name;

            switch (s)
            {
                case "Client HotKey":
                    _HKindex = -1;
                    break;
                case "Server1 HotKey":
                    _HKindex = 0;
                    break;
                case "Server2 HotKey":
                    _HKindex = 1;
                    break;
                case "Server3 HotKey":
                    _HKindex = 2;
                    break;
                case "Server4 HotKey":
                    _HKindex = 3;
                    break;
            }

            KEYhook = WindowsAPI.SetWindowsHookEx(WindowsAPI.WH_KEYBOARD_LL, sk_delegate, IntPtr.Zero, 0);
            MOUSEhook = WindowsAPI.SetWindowsHookEx(WindowsAPI.WH_MOUSE_LL, sm_delegate, IntPtr.Zero, 0);
            if (MessageBox.Show("Press a Key to change the: " + s + ".\nThen Press Ok to Confirm.", "Capturing new HotKey") == DialogResult.OK)
            {
                if (_HKindex == -1)
                {
                    _hostHK = _nVKS;
                    this.hotkey.Text = _nVKS.ToString();
                }
                else _sp[_HKindex].ChangeHK(_nVKS);
            }
            
        }


        private void continueB_click(Object sender, EventArgs e)
        {

            KEYhook = WindowsAPI.SetWindowsHookEx(WindowsAPI.WH_KEYBOARD_LL, k_delegate, IntPtr.Zero, 0);
            MOUSEhook = WindowsAPI.SetWindowsHookEx(WindowsAPI.WH_MOUSE_LL, m_delegate, IntPtr.Zero, 0);

            m_pos = MousePosition;
            
        }


        /* switch callback */



        public static IntPtr KswitchCB(int nCode, IntPtr wParam, IntPtr LParam)
        {

            unsafe
            {
                _nVKS = (VirtualKeyShort)((KBDLLHOOKSTRUCT*)LParam.ToPointer())->vkCode;
            }


            WindowsAPI.UnhookWindowsHookEx(KEYhook);
            WindowsAPI.UnhookWindowsHookEx(MOUSEhook);
            
            return new IntPtr(1);
        }


        public static IntPtr MswitchCB(int nCode, IntPtr wParam, IntPtr LParam)
        {
            return new IntPtr(1);
        }

        /* Capture Events */


        public static IntPtr KeyboardCB(int nCode, IntPtr wParam, IntPtr LParam)
        {
            
            KeyMsg km = new KeyMsg();

            unsafe
            {
                KBDLLHOOKSTRUCT* kp = (KBDLLHOOKSTRUCT*)LParam.ToPointer();
                
                km.VirtualKey = (VirtualKeyShort) kp->vkCode;


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

                if (km.VirtualKey == VirtualKeyShort.LMENU) ALT = km.Pressed;
                if (km.VirtualKey == VirtualKeyShort.LCONTROL) CTRL = km.Pressed;

                if (CTRL && ALT)
                {

                    //  HOT KEY HOST 

                    if (km.VirtualKey == _hostHK && km.Pressed)
                    {

                        WindowsAPI.UnhookWindowsHookEx(KEYhook);
                        WindowsAPI.UnhookWindowsHookEx(MOUSEhook);

                        _Host.EnqueueEventMsg(new KeyMsg(VirtualKeyShort.LMENU, false));
                        _Host.EnqueueEventMsg(new KeyMsg(VirtualKeyShort.LCONTROL, false));

                        _Host.EnqueueEventMsg(new StopComm(-1));

                        return new IntPtr(1);
                    }


                    for (int k = 0; k < 4; k++)
                    {
                        if ((km.VirtualKey == _sp[k].hk) && (km.Pressed))
                        {

                            _Host.EnqueueEventMsg(new KeyMsg(VirtualKeyShort.LMENU, false));
                            _Host.EnqueueEventMsg(new KeyMsg(VirtualKeyShort.LCONTROL, false));

                            _Host.EnqueueEventMsg(new StopComm(k));
                            _Host.EnqueueEventMsg(new InitComm(k));

                            return new IntPtr(1);
                        }
                    }
                }

                /* no hotkey identyfied , enqueing message */

                _Host.EnqueueEventMsg(km);

                return new IntPtr(1);
            }

        }


        public static IntPtr MouseCB(int nCode, IntPtr wParam, IntPtr LParam)
        {
            
            unsafe
            {
                MouseMsg mm = new MouseMsg();
                
                MSLLHOOKSTRUCT* mp = (MSLLHOOKSTRUCT*)LParam.ToPointer();

                mm.Dx = (mp->dx) - m_pos.X;
                mm.Dy = (mp->dy) - m_pos.Y;
                mm.MouseData = (mp->mouseData) >> 16;

                switch(wParam.ToInt32()){
                    case (int)MouseEvent.WM_LBUTTONDOWN:
                        mm.Flags = (uint)MouseEventFlags.LeftButtonDown;
                        break;

                    case (int)MouseEvent.WM_LBUTTONUP:
                        mm.Flags = (uint)MouseEventFlags.LeftButtonUp;
                        break;

                    case (int)MouseEvent.WM_MOUSEMOVE:
                        mm.Flags = (uint)MouseEventFlags.MouseMoved;
                        break;

                    case (int)MouseEvent.WM_MOUSEWHEEL:
                        mm.Flags = (uint)MouseEventFlags.Wheel;
                        break;

                    case (int)MouseEvent.WM_MOUSEHWHEEL:
                        mm.Flags = (uint)MouseEventFlags.HWheel;
                        break;

                    case (int)MouseEvent.WM_RBUTTONDOWN:
                        mm.Flags = (uint)MouseEventFlags.RightButtonDown;
                        break;

                    case (int)MouseEvent.WM_RBUTTONUP:
                        mm.Flags = (uint)MouseEventFlags.RightButtonUp;
                        break;
                
                }

                _Host.EnqueueEventMsg(mm);

            }
            
            return new IntPtr(1);
        }


    }
}
