using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AutoKeg.ISR.Snapshot.DataTransfer
{
    public class CountDataContextFactory : IDesignTimeDbContextFactory<CountDataContext>
    {
        public CountDataContext CreateDbContext(string[] args) =>
            new CountDataContext(
                new DbContextOptionsBuilder<CountDataContext>()
                    .UseSqlite("Data Source=PulseData.db").Options);
    }
}