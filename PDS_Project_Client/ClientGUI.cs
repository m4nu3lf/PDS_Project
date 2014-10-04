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
        //private static int activePanel;


        private static VirtualKeyShort _hostHK;
        private static VirtualKeyShort _sp0HK, _sp1HK, _sp2HK, _sp3HK;




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

            _sp0HK = VirtualKeyShort.KEY_0;
            _sp1HK = VirtualKeyShort.KEY_1;
            _sp2HK = VirtualKeyShort.KEY_2;
            _sp3HK = VirtualKeyShort.KEY_3;

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
            
            KeyMsg km = new KeyMsg(); 

            unsafe
            {
                KBDLLHOOKSTRUCT* kp = (KBDLLHOOKSTRUCT*)LParam.ToPointer();
                
                km.VirtualKey = (VirtualKeyShort) kp->vkCode;
                km.Time =  kp->time;


                switch(wParam.ToInt32()){

                    case 260:
                        Console.WriteLine(km.VirtualKey.ToString() + ": Pressed - System");
                        km.Pressed = true;
                        break;

                    case (int)ButtonEvent.WM_KEYUP:
                        Console.WriteLine(km.VirtualKey.ToString() + ": Released");
                        km.Pressed = false;
                        break;

                    case (int)ButtonEvent.WM_KEYDOWN:
                        Console.WriteLine(km.VirtualKey.ToString() + ": Pressed");
                        km.Pressed = true;
                        break;

                    default: return new IntPtr(1);
                
                }
             

                    //  HOT KEY HOST 

                    if ( km.VirtualKey == _hostHK &&  !km.Pressed)
                    {

                        WindowsAPI.UnhookWindowsHookEx(KEYhook);
                        WindowsAPI.UnhookWindowsHookEx(MOUSEhook);


                        Console.WriteLine("Dectivating: SERVER");
                        _Host.EnqueueMsg(new StopComm(-1));

                        return new IntPtr(1);
                    }



                    // SERVER 0


                    if ( (km.VirtualKey == _sp0HK) && (!km.Pressed) )
                    {

                        System.Console.WriteLine("HOT KEY ACTIVATION: SERVER 0");
                        _Host.EnqueueMsg(new StopComm(0));
                        _Host.EnqueueMsg(new InitComm(0));

                        return new IntPtr(1);
                    }

                    // SERVER 1


                    if ((km.VirtualKey == _sp1HK) && (!km.Pressed))
                    {

                        System.Console.WriteLine("HOT KEY ACTIVATION: SERVER 0");
                        _Host.EnqueueMsg(new StopComm(1));
                        _Host.EnqueueMsg(new InitComm(1));

                        return new IntPtr(1);
                    }

                    // SERVER 2


                    if ((km.VirtualKey == _sp2HK) && (!km.Pressed))
                    {

                        System.Console.WriteLine("HOT KEY ACTIVATION: SERVER 0");
                        _Host.EnqueueMsg(new StopComm(2));
                        _Host.EnqueueMsg(new InitComm(2));

                        return new IntPtr(1);
                    }

                    // SERVER 3


                    if ((km.VirtualKey == _sp3HK) && (!km.Pressed))
                    {

                        System.Console.WriteLine("HOT KEY ACTIVATION: SERVER 0");
                        _Host.EnqueueMsg(new StopComm(3));
                        _Host.EnqueueMsg(new InitComm(3));

                        return new IntPtr(1);
                    }



                /* no hotkey identyfied , enqueing message */


                _Host.EnqueueMsg(km);

                return new IntPtr(1);
            }

        }


        public static IntPtr MouseCB(int nCode, IntPtr wParam, IntPtr LParam)
        {
            /*
            unsafe
            {
                MouseMsg mm = new MouseMsg();

                mm.Dx = ;
                mm.Dy = ;
                mm.Flags = ;
                mm.Time = ;
                mm.MouseData = ;

                _Host.EnqueueMsg(mm);

            }
                */
            return new IntPtr(1);
        }


    }
}
