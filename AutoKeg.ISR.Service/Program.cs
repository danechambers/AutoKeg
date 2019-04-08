using System;
using System.IO;
using System.Threading.Tasks;
using AutoKeg.DataTransfer.DTOs;
using AutoKeg.DataTransfer.Interfaces;
using AutoKeg.DataTransfer.TransferContexts;
using AutoKeg.DataTransfer.Types;
using AutoKeg.ISR.Service.Configuration;
using AutoKeg.ISR.Service.Listeners;
using AutoKeg.ISR.Snapshot;
using AutoKeg.ISR.Snapshot.Configuration;
using Microsoft.EntityFrameworkCore;
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
                       .AddJsonFile("datatransfersettings.json", optional: false, reloadOnChange: true)
                       .AddCommandLine(args);
               })
                .ConfigureLogging((context, logBuilder) =>
               {
                   logBuilder
                       .AddConfiguration(context.Configuration.GetSection("Logging"))
                       .AddConsole();
               })
                .ConfigureServices((context, services) =>
               {
                   var dbConfig = context.Configuration.GetSection("Sqlite")["Database"];

                   services
                       .Configure<AppSettings>(context.Configuration.GetSection("Gpio"))
                       .Configure<SnapshotSettings>(context.Configuration.GetSection("Snapshot"))
                       .AddDbContext<CountDataContext>(options =>
                           options.UseSqlite($"Data Source={dbConfig}"))
                       .AddSingleton(PulseCounter.Instance)
                       .AddTransient<IDurationProvider, DurationProvider>()
                       .AddSingleton<IPinListener, GpioPinListener>()
                       .AddScoped<ILocalRepository<PulseDTO>, SqliteDataTransfer>()
                       .AddScoped<ISnapshotPulse, SnapshotCount>()
                       .AddHostedService<Application>();
               });

            await host.RunConsoleAsync();
        }

        private static void ErrorHandler(object sender, UnhandledExceptionEventArgs e)
        {
            var error = (Exception)e.ExceptionObject;
            Console.WriteLine($"Exception occurred during application runtime: {error.Message}");
            Environment.Exit(20);
        }
    }
}
