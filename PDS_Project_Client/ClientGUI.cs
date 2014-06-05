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

namespace PDS_Project_Client
{
    public partial class ClientGUI : Form
    {

        private Host aHost;

        private ServerPanel sp1, sp2, sp3, sp4;
        private AutoResetEvent ae;
        private Queue<Int16> eq;

        public ClientGUI()
        {
            eq = new Queue<Int16>();
            ae = new AutoResetEvent(false);

            sp1 = new ServerPanel(1, aHost);
            sp2 = new ServerPanel(2, aHost);
            sp3 = new ServerPanel(3, aHost);
            sp4 = new ServerPanel(4, aHost);

            InitializeComponent();

            Thread eThread = new Thread((new EventThread()).run);

            eThread.Start(new ThreadParam(eq, ae, aHost));

            lock (eq)
            {
                Console.WriteLine("sleeping");
                Thread.Sleep(1);
                eq.Enqueue(1);
            }

            ae.Set();

            lock (eq)
            {
                Console.WriteLine("sleeping again");
                Thread.Sleep(1);
                eq.Enqueue(2);
            }

            ae.Set();

            lock (eq)
            {
                Console.WriteLine("byebye");
            }

        }



        /* EVENTS HANDLERS */

        private void hotkeyB_click(Object sender, EventArgs e) { 
        
        }

    }
}
