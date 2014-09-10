using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebJob1
{
    // To learn more about Microsoft Azure WebJobs, please see http://go.microsoft.com/fwlink/?LinkID=401557
    class Program
    {
        static void Main()
        {
            while (true)
            {
                //System.Diagnostics.Trace.TraceInformation
                System.Diagnostics.Trace.WriteLine("teched New Zeland");
                Console.WriteLine("teched New Zeland");; 
                System.Threading.Thread.Sleep(3750);
            }
        }
    }
}
