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
        private static int activePanel;


        private static VirtualKeyShort _hostHK;

        private static VirtualKeyShort _sp0HK, _sp1HK, _sp2HK, _sp3HK;


        private static IntPtr KEYhook;
        private static IntPtr MOUSEhook;

        private static HookCB k_delegate = new HookCB(KeyboardCB);
        private static HookCB m_delegate = new HookCB(MouseCB);



        public ClientGUI()
        {

            _Host = new Host();

            activePanel = -1;


            _hostHK = VirtualKeyShort.KEY_Q;


            _sp0HK = VirtualKeyShort.KEY_0;
            _sp1HK = VirtualKeyShort.KEY_1;
            _sp2HK = VirtualKeyShort.KEY_2;
            _sp3HK = VirtualKeyShort.KEY_3;


            sp[0] = new ServerPanel(0, _Host);
            sp[1] = new ServerPanel(1, _Host);
            sp[2] = new ServerPanel(2, _Host);
            sp[3] = new ServerPanel(3, _Host);

            InitializeComponent();

            Thread eThread = new Thread((new EventThread()).run);

            eThread.Start(_Host);


        }


        /* EVENTS HANDLERS */

        private void hotkeyB_click(Object sender, EventArgs e) { 
        
        }

        private void continueB_click(Object sender, EventArgs e)
        {

            KEYhook = WindowsAPI.SetWindowsHookEx(WindowsAPI.WH_KEYBOARD_LL, k_delegate, IntPtr.Zero, 0);
            MOUSEhook = WindowsAPI.SetWindowsHookEx(WindowsAPI.WH_MOUSE_LL, m_delegate, IntPtr.Zero, 0);
        }



        /* Capture Events */


        public static IntPtr KeyboardCB(int nCode, IntPtr wParam, IntPtr LParam)
        {

            unsafe
            {
                KEYBDINPUT* kp = (KEYBDINPUT*)LParam.ToPointer();
                VirtualKeyShort vks = kp->wVk;
                ScanCodeShort wsc = kp->wScan;
                KEYEVENTF dwf = kp->dwFlags;

                System.Console.WriteLine("tasto: " + vks.ToString() + "  " + dwf.ToString() + "  " + wsc.ToString());


                /* HOT KEY HOST */

                /* HOST */
                WindowsAPI.UnhookWindowsHookEx(KEYhook);
                WindowsAPI.UnhookWindowsHookEx(MOUSEhook);


                Console.WriteLine("Dectivating: SERVER " + activePanel);
                _Host.EnqueueMsg(new StopComm());
                sp[activePanel].Deactivate();
                activePanel = -1;

                //return new IntPtr(1);

                /* SERVER */

                System.Console.WriteLine("HOT KEY ACTIVATION: SERVER 0");

                if ((activePanel != 0) && (activePanel != -1))
                {
                    Console.WriteLine("Dectivating: SERVER " + activePanel);
                    _Host.EnqueueMsg(new StopComm());
                    sp[activePanel].Deactivate();
                }

                if (activePanel != 0)
                {
                    Console.WriteLine("Activating: SERVER 0");
                    sp[0].Activate();
                    activePanel = 0;
                    _Host.EnqueueMsg(new InitComm(activePanel));
                }



                /* no hotkey identyfied , enqueing message */

                _Host.EnqueueMsg(new KeyMsg(*kp));

                return new IntPtr(1);
            }

        }


        public static IntPtr MouseCB(int nCode, IntPtr wParam, IntPtr LParam)
        {
            unsafe
            {
                MOUSEINPUT* kp = (MOUSEINPUT*)LParam.ToPointer();

                _Host.EnqueueMsg(new MouseMsg(*kp));

            }

            return new IntPtr(1);
        }


    }
}
