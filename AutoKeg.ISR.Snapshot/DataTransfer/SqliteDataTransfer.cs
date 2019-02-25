using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AutoKeg.ISR.Snapshot.DataTransfer
{
    public class SqliteDataTransfer : IDataTransfer<PulseDTO>, IDisposable
    {
        private ILogger Logger { get; }
        private CountDataContext Db { get; }

        public SqliteDataTransfer(CountDataContext dbContext, ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<SqliteDataTransfer>();
            Db = dbContext;
        }

        public async Task SaveDataAsync(PulseDTO data, CancellationToken stoppingToken)
        {
            await Db.PulseCounts.AddAsync(data, stoppingToken);
            var count = await Db.SaveChangesAsync(stoppingToken);
            Logger.LogInformation($"{count} records saved to the database");
        }

        public void Dispose() => Db.Dispose();
    }
}