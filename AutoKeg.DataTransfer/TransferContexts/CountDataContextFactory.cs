using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AutoKeg.DataTransfer.TransferContexts
{
    public class CountDataContextFactory : IDesignTimeDbContextFactory<CountDataContext>
    {
        public CountDataContext CreateDbContext(string[] args) =>
            new CountDataContext(
                new DbContextOptionsBuilder<CountDataContext>()
                    .UseSqlite("Data Source=AuxiliarySensorSystem.db").Options);
    }
}