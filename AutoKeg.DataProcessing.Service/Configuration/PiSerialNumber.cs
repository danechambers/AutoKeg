using System.IO;
using System.Linq;
using AutoKeg.DataProcessing.Service.Interfaces;

namespace AutoKeg.DataProcessing.Service.Configuration
{
	public class PiSerialNumber : IPiSerialNumber
	{
		public string Value { get; }

		public PiSerialNumber()
		{
			Value = GetSerialNumber();
		}

		private static string GetSerialNumber()
		{
			var serialNumber = File
				.ReadAllLines( "/proc/cpuinfo" )
				.First( line => line.StartsWith( "Serial" ) );

			var colonIdx = serialNumber.IndexOf( value: ':' ) + 1;

			return new string( serialNumber.Skip( colonIdx )
				.Take( serialNumber.Length - colonIdx )
				.Where( c => !char.IsWhiteSpace( c ) ).ToArray() );
		}
	}
}
