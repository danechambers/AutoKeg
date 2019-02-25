using System;
using System.ComponentModel.DataAnnotations;

namespace AutoKeg.ISR.Snapshot.DataTransfer
{
    public class PulseDTO
    {
        [Key]
        public int PulseCountId { get; set; }
        public DateTime DateCounted { get; set; }
        public int Count { get; set; }
    }
}