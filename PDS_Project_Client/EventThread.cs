using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace PDS_Project_Client
{

    public class ThreadParam
    {
        private AutoResetEvent are;
        private Queue<Int16> equ;

        public ThreadParam(Queue<Int16> eq, AutoResetEvent ae)
        {
            are = ae;
            equ = eq;
        }

        public AutoResetEvent ae
        {
            get { return are; }
        }

        public Queue<Int16> eq
        {
            get { return equ; }
        }

    }

    public class EventThread
    {
        public void run(object param)
        {

            AutoResetEvent ae = ((ThreadParam)param).ae;
            Queue<Int16> eq = ((ThreadParam)param).eq;
            Console.WriteLine("In attesa");
            Int16 i = 0;
            while (ae.WaitOne())
            {
                lock (eq)
                {
                    i = eq.Dequeue();
                    Console.WriteLine("i am in: " + i );
                    if (i == 2) break;
                }
            }
            Console.WriteLine("Bye");
            return;
        }
    }
}
