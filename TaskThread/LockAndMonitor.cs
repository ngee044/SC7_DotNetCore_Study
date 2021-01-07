using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TaskThread
{
    class LockAndMonitor
    {
        static Random r = new Random();
        static string Message; // 공유 리소스
        static int Counter; // 또 다른 공유 리소스

        static object conch = new object();

        static void MethodA()
        {
            try
            {
                Monitor.TryEnter(conch, TimeSpan.FromSeconds(15));
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(r.Next(2000));
                    Message += "A";
                    Interlocked.Increment(ref Counter);
                    Console.Write(".");
                }
            }
            finally
            {
                Monitor.Exit(conch);
            }
        }

        static void MethodB()
        {
            try
            {
                Monitor.TryEnter(conch, TimeSpan.FromSeconds(15));
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(r.Next(2000));
                    Message += "B";
                    Interlocked.Increment(ref Counter);
                    Console.Write(".");
                }
            }
            finally
            {
                Monitor.Exit(conch);
            }
        }

        static void LockAndMonitorExample()
        {
            Console.WriteLine("Please wait for the tasks to complete.");
            Stopwatch watch = Stopwatch.StartNew();

            Task a = Task.Factory.StartNew(MethodA);
            Task b = Task.Factory.StartNew(MethodB);

            Task.WaitAll(new Task[] { a, b });
            Console.WriteLine();
            Console.WriteLine($"Results: {Message}.");
            Console.WriteLine($"{watch.ElapsedMilliseconds:#,##0} elapsed milliseconds.");
            Console.WriteLine($"{Counter} string modifications.");

        }
    }
}
