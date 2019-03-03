using System.Threading.Tasks;
using NUnit.Framework;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using AutoKeg.DataTransfer.Types;
using AutoKeg.DataTransfer.TransferContexts;
using AutoKeg.DataTransfer.DTOs;

namespace AutoKeg.Tests.Sqlite
{
    public class SaveTests
    {
        [Test]
        public async Task TestDbSave()
        {
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();

            try
            {
                var options = new DbContextOptionsBuilder<CountDataContext>()
                    .UseSqlite(conn)
                    .Options;

                // Create the schema in the database
                using (var context = new CountDataContext(options))
                {
                    context.Database.EnsureCreated();
                }

                // Run the test against one instance of the context
                using (var context = new CountDataContext(options))
                {
                    var service = new SqliteDataTransfer(context, NullLoggerFactory.Instance);
                    await service.SaveDataAsync(new PulseDTO
                    {
                        Count = 123456
                    });
                }

                // Use a separate instance of the context to verify correct data was saved to database
                using (var context = new CountDataContext(options))
                {
                    var pulseCount = await context.PulseCounts.SingleAsync();

                    Assert.That(pulseCount.Count, Is.EqualTo(123456));
                }
            }
            finally
            {
                conn.Close();
            }
        }
    }
}