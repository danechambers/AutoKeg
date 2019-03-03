using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.Extensions.Logging;
using AutoKeg.DataTransfer.TransferContexts;
using AutoKeg.DataTransfer.Interfaces;
using AutoKeg.DataTransfer.DTOs;

namespace AutoKeg.DataTransfer.Types
{
    public class SqliteDataTransfer : IDataTransfer<PulseDTO>
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
            await Db.PulseCounts.AddAsync(data, cancellationToken);
            var count = await Db.SaveChangesAsync(cancellationToken);
            Logger.LogInformation($"{count} records saved to the database");
        }

        public void Dispose() => Db.Dispose();
    }
}