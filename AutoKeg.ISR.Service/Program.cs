using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutoKeg.ISR.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var cancel = new CancellationTokenSource();

            var currentDomain = AppDomain.CurrentDomain;

            // Handle any exceptions prior to app run
            currentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(ErrorHandler);

            // handles external systems events like SIGINT
            currentDomain.ProcessExit += (s, e) => 
            {
                Console.WriteLine("Cancel received...");
                cancel.Cancel();
            };

            var app = new ApplicationBuilder(args).Build();
            try
            {
                app.Run(cancel.Token).Wait(Timeout.Infinite);
            }
            catch (AggregateException e)
            {
                if (e.InnerExceptions.All(ex => ex is TaskCanceledException))
                {
                    Console.WriteLine("AutoKeg has been cancelled. Goodbye...");
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Errors occurred during application run and must exit.");
                    foreach (var error in e.InnerExceptions.Where(ex => !(ex is TaskCanceledException)))
                    {
                        Console.WriteLine($"Error: {error.Message}");
                    }
                    Environment.Exit(10);
                }
            }
            finally
            {
                cancel.Dispose();
            }
        }

        private static void ErrorHandler(object sender, UnhandledExceptionEventArgs e)
        {
            var error = (Exception)e.ExceptionObject;
            Console.WriteLine($"Exception occurred during application startup: {error.Message}");
            Environment.Exit(20);
        }
    }
}
