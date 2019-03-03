using Microsoft.EntityFrameworkCore;
using AutoKeg.DataTransfer.DTOs;

namespace AutoKeg.DataTransfer.TransferContexts
{
    public class CountDataContext : DbContext
    {
        public CountDataContext(DbContextOptions<CountDataContext> options) : base(options)
        { }

        public DbSet<PulseDTO> PulseCounts { get; set; }
    }
}