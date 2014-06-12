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

using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;

using PDS_Project_Common;

namespace PDS_Project_Client
{
    public partial class ClientGUI : Form
    {

        private static Host _Host;

        private ServerPanel     sp1, sp2, sp3, sp4;
        private int activePanel;

        private static Dictionary<VirtualKeyShort, bool> _hostHK;

        private static Dictionary<VirtualKeyShort, bool> _sp1HK;
        private static Dictionary<VirtualKeyShort, bool> _sp2HK;
        private static Dictionary<VirtualKeyShort, bool> _sp3HK;
        private static Dictionary<VirtualKeyShort, bool> _sp4HK;

        private static IntPtr KEYhook;
        private static IntPtr MOUSEhook;


        public ClientGUI()
        {

            _Host = new Host(4);


            _hostHK = new Dictionary<VirtualKeyShort, bool>();
            _hostHK.Add(VirtualKeyShort.CONTROL, false);
            _hostHK.Add(VirtualKeyShort.ALT, false);
            _hostHK.Add(VirtualKeyShort.KEY_0, false);


            _sp1HK = new Dictionary<VirtualKeyShort, bool>();
            _sp1HK.Add(VirtualKeyShort.CONTROL, false);
            _sp1HK.Add(VirtualKeyShort.ALT, false);
            _sp1HK.Add(VirtualKeyShort.KEY_1, false);


            _sp2HK = new Dictionary<VirtualKeyShort, bool>();
            _sp2HK.Add(VirtualKeyShort.CONTROL, false);
            _sp2HK.Add(VirtualKeyShort.ALT, false);
            _sp2HK.Add(VirtualKeyShort.KEY_2, false);


            _sp3HK = new Dictionary<VirtualKeyShort, bool>();
            _sp3HK.Add(VirtualKeyShort.CONTROL, false);
            _sp3HK.Add(VirtualKeyShort.ALT, false);
            _sp3HK.Add(VirtualKeyShort.KEY_3, false);


            _sp4HK = new Dictionary<VirtualKeyShort, bool>();
            _sp4HK.Add(VirtualKeyShort.CONTROL, false);
            _sp4HK.Add(VirtualKeyShort.ALT, false);
            _sp4HK.Add(VirtualKeyShort.KEY_4, false);


            activePanel = -1;

            sp1 = new ServerPanel(1, _Host);
            sp2 = new ServerPanel(2, _Host);
            sp3 = new ServerPanel(3, _Host);
            sp4 = new ServerPanel(4, _Host);

            InitializeComponent();

            Thread eThread = new Thread((new EventThread()).run);

            eThread.Start(_Host);


        }



        /* EVENTS HANDLERS */

        private void hotkeyB_click(Object sender, EventArgs e) { 
        
        }

        private void continueB_click(Object sender, EventArgs e)
        {
            KEYhook = WindowsAPI.SetWindowsHookEx(WindowsAPI.WH_KEYBOARD_LL, KeyboardCB, IntPtr.Zero, 0);
            MOUSEhook = WindowsAPI.SetWindowsHookEx(WindowsAPI.WH_MOUSE_LL, KeyboardCB, IntPtr.Zero, 0);
        }

        /* Capture Events */


        public static IntPtr KeyboardCB(int nCode, IntPtr wParam, IntPtr LParam)
        {
            unsafe
            {
                KEYBDINPUT* kp = (KEYBDINPUT*)LParam.ToPointer();
                VirtualKeyShort vks = kp->wVk;
                KEYEVENTF dwf = kp->dwFlags;

                if (_hostHK.ContainsKey(vks))
                {
                    if ((dwf & KEYEVENTF.KEYUP) != 0)
                    {
                        _hostHK[vks] = true;
                        bool flag = true;

                        IDictionaryEnumerator ide = _hostHK.GetEnumerator();

                        do
                        {
                            if ( !(bool)ide.Value )
                            { 
                                flag = false;
                                break;
                            }
                        }while(ide.MoveNext());


                        if (flag)
                        {
                            WindowsAPI.UnhookWindowsHookEx(KEYhook);
                            WindowsAPI.UnhookWindowsHookEx(MOUSEhook);
                            return new IntPtr(1);
                        }

                    }
                    else _hostHK[vks] = false;
                }

                _Host.EnqueueMsg(new KeyMsg(*kp));

            }


            return new IntPtr(1);
        }

        public static IntPtr MouseCB(int nCode, IntPtr wParam, IntPtr LParam)
        {
            
            return new IntPtr(1);
        }



    }
}
