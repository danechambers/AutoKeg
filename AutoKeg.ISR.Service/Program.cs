using System;
using System.IO;
using System.Threading.Tasks;
using AutoKeg.Configuration;
using AutoKeg.ISR.Service.Listeners;
using AutoKeg.ISR.Snapshot;
using AutoKeg.ISR.Snapshot.DataTransfer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutoKeg.ISR.Service
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Handle any exceptions prior to app run
            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(ErrorHandler);

            var host = new HostBuilder()
                .ConfigureHostConfiguration(config =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddCommandLine(args);
                })
                .ConfigureLogging((context, logBuilder) =>
                {
                    logBuilder.AddConfiguration(context.Configuration.GetSection("Logging"));
                    logBuilder.AddConsole();
                })
                .ConfigureServices((context, services) =>
                {
                    var config = context.Configuration.Get<AppSettings>();
                    var mongoConfig = config.Mongo;
                    services.AddSingleton<PulseCounter>(PulseCounter.Instance);
                    services.AddScoped<IDurationProvider>(provider =>
                        new DurationProvider(TimeSpan.FromSeconds(config.IdleTimer)));
                    services.AddScoped<IPinListener>(provider =>
                       new GpioPinListener(config.ListenToPin));
                    services.AddScoped<IDataTransfer<PulseDTO>>(provider =>
                        new MongoDataTransfer<PulseDTO>(mongoConfig.Host,
                           mongoConfig.Database, mongoConfig.Collection));
                    services.AddScoped<ISnapshotPulse, SnapshotCount>();
                    services.AddHostedService<Application>();
                });

            await host.RunConsoleAsync();
        }

        private static void ErrorHandler(object sender, UnhandledExceptionEventArgs e)
        {
            var error = (Exception)e.ExceptionObject;
            Console.WriteLine($"Exception occurred during application startup: {error.Message}");
            Environment.Exit(20);
        }
    }
}
