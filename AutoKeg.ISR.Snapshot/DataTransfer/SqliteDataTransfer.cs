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

        public async Task SaveDataAsync(PulseDTO data, CancellationToken cancellationToken = default)
        {
            await Db.Database.EnsureCreatedAsync(cancellationToken);
            await Db.PulseCounts.AddAsync(data, cancellationToken);
            var count = await Db.SaveChangesAsync(cancellationToken);
            Logger.LogInformation($"{count} records saved to the database");
        }

        public void Dispose() => Db.Dispose();
    }
}