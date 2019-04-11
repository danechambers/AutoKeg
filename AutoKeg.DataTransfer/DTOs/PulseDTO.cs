using System;
using System.ComponentModel.DataAnnotations;

namespace AutoKeg.DataTransfer.DTOs
{
	public class PulseDTO
	{
		[Key]
		public int ID { get; set; }
		[Timestamp]
		public byte[] Version { get; set; }
		public DateTime DateCounted { get; set; } = DateTime.Now;
		public int Count { get; set; }
	}
}