using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;


namespace TaskThread
{
    class Program
    {
        static void MethodA()
        {
            Console.WriteLine("Starting Method A...");
            Thread.Sleep(3000); // 3초간 대기한다.
            Console.WriteLine("Finished Method A.");
        }

        static void MethodB()
        {
            Console.WriteLine("Starting Method B...");
            Thread.Sleep(2000); // 2초간 대기한다. 
            Console.WriteLine("Finished Method B.");
        }

        static void MethodC()
        {
            Console.WriteLine("Starting Method C...");
            Thread.Sleep(1000); // 1초간 대기한다. 
            Console.WriteLine("Finished Method C.");
        }

        static decimal CallWebService()
        {
            Console.WriteLine("Starting call to web service...");
            Thread.Sleep((new Random()).Next(2000, 4000));
            Console.WriteLine("Finished call to web service.");
            return 89.99M;
        }

        static string CallStoredProcedure(decimal amount)
        {
            Console.WriteLine("Starting call to stored procedure...");
            Thread.Sleep((new Random()).Next(2000, 4000));
            Console.WriteLine("Finished call to stored procedure.");
            return $"12 products cost more than {amount:C}.";
        }

        static void NestedAndChildTasks()
        {
            var outer = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Outer task starting...");
                var inner = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Inner task starting...");
                    Thread.Sleep(2000);
                    Console.WriteLine("Inner task finished.");
                }, TaskCreationOptions.AttachedToParent);
            });
            outer.Wait();
            Console.WriteLine("Outer task finished.");
            Console.WriteLine("Press ENTER to end.");
            Console.ReadLine();

        }

        static void LockAndMonitor()
        {

        }

        static void Main(string[] args)
        {
            var timer = Stopwatch.StartNew();

            //Console.WriteLine("Running methods synchronously on one thread.");
            //MethodA();
            //MethodB();
            //MethodC();

            Console.WriteLine("Running methods asynchronously on multiple threads.");
            Task taskA = new Task(MethodA);
            taskA.Start();
            Task taskB = Task.Factory.StartNew(MethodB);
            Task taskC = Task.Run(new Action(MethodC));

            Task[] tasks = { taskA, taskB, taskC };
            Task.WaitAll(tasks);

            //Console.WriteLine("Passing the result of one task as an input into another.");

            //var taskCallWebServiceAndThenStoredProcedure =
            //  Task.Factory.StartNew(CallWebService)
            //  .ContinueWith(previousTask =>
            //    CallStoredProcedure(previousTask.Result));

            //Console.WriteLine($"{taskCallWebServiceAndThenStoredProcedure.Result}");

            //Console.WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");
            //Console.WriteLine("Press ENTER to end.");
            //Console.ReadLine();
        }
    }
}
