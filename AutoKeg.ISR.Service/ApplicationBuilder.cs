using System.IO;
using AutoKeg.ISR.Service.Configuration;
using AutoKeg.ISR.Snapshot;
using AutoKeg.ISR.Snapshot.DataTransfer;
using Microsoft.Extensions.Configuration;

namespace AutoKeg.ISR.Service
{
    public class ApplicationBuilder
    {
        private PulseCounter Counter { get; } = PulseCounter.Instance;
        private IConfigurationBuilder Config { get; } = new ConfigurationBuilder();

        public ApplicationBuilder(string[] args)
        {
            Config
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddCommandLine(args);
        }

        public Application Build()
        {
            var settings = Config.Build().Get<AppSettings>();
            var mongoSettings = settings.Mongo;
            var pulseTransfer = new MongoDataTransfer<PulseDTO>(
                mongoSettings.Host, mongoSettings.Database, mongoSettings.Collection);

            return new Application(settings.ListenToPin, Counter, pulseTransfer);
        }
    }
}