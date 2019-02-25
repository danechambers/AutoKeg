using Microsoft.EntityFrameworkCore;

namespace AutoKeg.ISR.Snapshot.DataTransfer
{
    public class CountDataContext : DbContext
    {
        public CountDataContext(DbContextOptions<CountDataContext> options) : base(options)
        { }

        public DbSet<PulseDTO> PulseCounts { get; set; }
    }
}