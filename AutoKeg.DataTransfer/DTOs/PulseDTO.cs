using System;
using System.ComponentModel.DataAnnotations;

namespace AutoKeg.DataTransfer.DTOs
{
    public class PulseDTO
    {
        [Key]
        public int PulseCountId { get; set; }
        [ConcurrencyCheck]
        public DateTime DateCounted { get; set; } = DateTime.Now;
        public int Count { get; set; }
        public bool IsProcessed { get; set; }
    }
}