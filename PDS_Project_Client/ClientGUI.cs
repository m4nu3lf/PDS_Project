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

        private static ServerPanel[] sp = new ServerPanel[4];
        private static int activePanel;


        private static Dictionary<VirtualKeyShort, bool> _hostHK;

        private static Dictionary<VirtualKeyShort, bool> _sp1HK;
        private static Dictionary<VirtualKeyShort, bool> _sp2HK;
        private static Dictionary<VirtualKeyShort, bool> _sp3HK;
        private static Dictionary<VirtualKeyShort, bool> _sp4HK;
        

        private static IntPtr KEYhook;
        private static IntPtr MOUSEhook;

        private static HookCB k_delegate = new HookCB(KeyboardCB);
        private static HookCB m_delegate = new HookCB(MouseCB);



        public ClientGUI()
        {

            _Host = new Host();


            _hostHK = new Dictionary<VirtualKeyShort, bool>();
            _hostHK.Add(VirtualKeyShort.LCONTROL, false);
            _hostHK.Add(VirtualKeyShort.LMENU, false);
            _hostHK.Add(VirtualKeyShort.KEY_Q, false);


            _sp1HK = new Dictionary<VirtualKeyShort, bool>();
            _sp1HK.Add(VirtualKeyShort.LCONTROL, false);
            _sp1HK.Add(VirtualKeyShort.LMENU, false);
            _sp1HK.Add(VirtualKeyShort.KEY_0, false);


            _sp2HK = new Dictionary<VirtualKeyShort, bool>();
            _sp2HK.Add(VirtualKeyShort.LCONTROL, false);
            _sp2HK.Add(VirtualKeyShort.LMENU, false);
            _sp2HK.Add(VirtualKeyShort.KEY_1, false);


            _sp3HK = new Dictionary<VirtualKeyShort, bool>();
            _sp3HK.Add(VirtualKeyShort.LCONTROL, false);
            _sp3HK.Add(VirtualKeyShort.LMENU, false);
            _sp3HK.Add(VirtualKeyShort.KEY_2, false);


            _sp4HK = new Dictionary<VirtualKeyShort, bool>();
            _sp4HK.Add(VirtualKeyShort.LCONTROL, false);
            _sp4HK.Add(VirtualKeyShort.LMENU, false);
            _sp4HK.Add(VirtualKeyShort.KEY_3, false);


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

                //System.Console.WriteLine("tasto: " + vks.ToString() + "  " + dwf.ToString() + "  " + wsc.ToString());


                /* HOT KEY HOST */

                if (_hostHK.ContainsKey(vks))
                {

                    if ((wParam.ToInt32() == (int)ButtonEvent.WM_KEYDOWN))
                    {
                        _hostHK[vks] = true;
                        bool flag = true;

                        IDictionaryEnumerator ide = _hostHK.GetEnumerator();


                        while (ide.MoveNext())
                        {
                            if (!(bool)ide.Value)
                            {
                                flag = false;
                                break;
                            }
                        }

                        if (flag)
                        {
                            WindowsAPI.UnhookWindowsHookEx(KEYhook);
                            WindowsAPI.UnhookWindowsHookEx(MOUSEhook);
                            return new IntPtr(1);
                        }

                    }
                    else
                    {
                        _hostHK[vks] = false;
                    }

                }


                /* HOT KEY SERVER 0 */

                if (_sp1HK.ContainsKey(vks))
                {

                    if ((wParam.ToInt32() == (int)ButtonEvent.WM_KEYDOWN))
                    {
                        _sp1HK[vks] = true;
                        bool flag = true;

                        IDictionaryEnumerator ide = _sp1HK.GetEnumerator();


                        while (ide.MoveNext())
                        {
                            if (!(bool)ide.Value)
                            {
                                flag = false;
                                break;
                            }
                        }

                        if (flag)
                        {
                            if (activePanel != -1)
                            {
                                _Host.EnqueueMsg(new StopComm());
                                sp[activePanel].Activate(false);
                            }

                            sp[0].Activate(true);
                            activePanel = 0;
                            _Host.EnqueueMsg(new InitComm(activePanel));
                            
                            return new IntPtr(1);

                        }
                    }
                    else
                    {
                        _sp1HK[vks] = false;
                    }

                }

                /* HOT KEY SERVER 1 */

                if (_sp2HK.ContainsKey(vks))
                {

                    if ((wParam.ToInt32() == (int)ButtonEvent.WM_KEYDOWN))
                    {
                        _sp2HK[vks] = true;
                        bool flag = true;

                        IDictionaryEnumerator ide = _sp2HK.GetEnumerator();


                        while (ide.MoveNext())
                        {
                            if (!(bool)ide.Value)
                            {
                                flag = false;
                                break;
                            }
                        }

                        if (flag)
                        {
                            if (activePanel != -1)
                            {
                                _Host.EnqueueMsg(new StopComm());
                                sp[activePanel].Activate(false);
                            }

                            sp[1].Activate(true);
                            activePanel = 1;
                            _Host.EnqueueMsg(new InitComm(activePanel));

                            return new IntPtr(1);
                        }

                    }
                    else
                    {
                        _sp2HK[vks] = false;
                    }


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
