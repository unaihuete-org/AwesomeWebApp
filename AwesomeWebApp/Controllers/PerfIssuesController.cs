using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace AwesomeWebApp.Controllers
{
    public class PerfIssuesController : Controller
    {
        // GET: PerfIssues
        public ActionResult Index()
        {
            SetViewBag();
            return View();
        }

        /// <summary>
        /// Generate memoryleak.
        /// </summary>
        /// <param name="MemSize">amount of memory to leak per call with default of 1M byte</param>
        /// <returns></returns>
        public ActionResult MemLeak(int MemSize = 1048576)
        {
            Byte[] arr = new byte[MemSize];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = (Byte)i;
            }
            MvcApplication._memListLeakGen.Add(arr);

            SetViewBag();
            return View("Index");
        }

        private void SetViewBag()
        {
            Process currentProcess = Process.GetCurrentProcess();
            ViewBag.Proc = currentProcess;
            ViewBag.Message = "Meory leak results";
            ViewBag.ListSize = MvcApplication._memListLeakGen.Count();
        }

        /// <summary>
        /// 
        /// </summary>
        public void SleepBtnHandler()
        {
            int i = 10000;
            int threadID = System.Threading.Thread.CurrentThread.ManagedThreadId;

            Trace.TraceInformation("Thread: {0}\tbefore sleeping for {1} msec",threadID, i);
            System.Threading.Thread.Sleep(10000);
            Trace.TraceInformation("Thread: {0}\tafter sleeping for {1} msec", threadID, i);
        }

        public void SpinCpuBtnHandler()
        {
            //System.Threading.Thread t = new System.Threading.Thread(foo);
            //t.Start();

            Trace.TraceInformation("Spinning CPU");
            RunAsynchronously(foo, null);
        }

        private void foo()
        {
            //Console.WriteLine("--- Primes between 3 and 10100 ---");
            int threadID = System.Threading.Thread.CurrentThread.ManagedThreadId;

            for (int i = 3; i < 10100; i++)
            {
                if (PrimeTool.IsPrime(i))
                {
                    string msg = String.Format("Thread: {0}\tPrime: {1}", threadID, i);
                    Trace.WriteLine(msg);
                }
            }
        }

        public static void RunAsynchronously(Action method, Action callback)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    method();
                }
                catch (ThreadAbortException) { /* dont report on this */ }
                catch (Exception ex)
                {
                    Trace.TraceError("failed torun thread..." + ex.Message);
                }
                // note: this will not be called if the thread is aborted
                if (callback != null) callback();
            });
        }

        private static class PrimeTool
        {
            public static bool IsPrime(int candidate)
            {
                // Test whether the parameter is a prime number.
                if ((candidate & 1) == 0)
                {
                    if (candidate == 2)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                // Note:
                // ... This version was changed to test the square.
                // ... Original version tested against the square root.
                // ... Also we exclude 1 at the end.
                for (int i = 3; (i * i) <= candidate; i += 2)
                {
                    if ((candidate % i) == 0)
                    {
                        return false;
                    }
                }
                return candidate != 1;
            }
        }
    }
}