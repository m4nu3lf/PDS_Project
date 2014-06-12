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

namespace PDS_Project_Client
{
    public partial class ClientGUI : Form
    {

        private Host _Host;

        private ServerPanel     sp1, sp2, sp3, sp4;
        private int activePanel;

        private delegate IntPtr HookCB(int nCode, IntPtr wParam, IntPtr LParam);

        public ClientGUI()
        {

            _Host = new Host(4);
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

        }

        /* Capture Events */


        public static IntPtr KeyboardCB(int nCode, IntPtr wParam, IntPtr LParam)
        {
            return new IntPtr(1);
        }

        public static IntPtr MouseCB(int nCode, IntPtr wParam, IntPtr LParam)
        {
            
            return new IntPtr(1);
        }



    }
}
