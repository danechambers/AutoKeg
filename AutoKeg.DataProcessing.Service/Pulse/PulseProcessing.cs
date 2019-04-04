using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoKeg.DataProcessing.Service.Interfaces;
using AutoKeg.DataTransfer.DTOs;
using AutoKeg.DataTransfer.Interfaces;
using Microsoft.Extensions.Logging;

namespace AutoKeg.DataProcessing.Service.Pulse
{
	public class PulseProcessing : IDataPush
	{
		private ILocalRepository<PulseDTO> PulseRepo { get; }
		private IApi Api { get; }
		private string PiSerialNumber { get; }

		private ILogger Logger { get; }

		public PulseProcessing( ILocalRepository<PulseDTO> pulseDataRepo,
			IPiSerialNumber serialNumber, IApi pushApi,
			ILoggerFactory logger )
		{
			PulseRepo = pulseDataRepo;
			Api = pushApi;
			PiSerialNumber = serialNumber.Value;
			Logger = logger.CreateLogger<PulseProcessing>();
		}

		public async Task PushData( CancellationToken cancellationToken = default )
		{
			var unProcessedData = PulseRepo.DataSet.ToAsyncEnumerable().Where( data => !data.IsProcessed );
			var dataSet = await unProcessedData
				.Select( data => new { PiSerialNumber, data.Count, data.DateCounted, data.ID } )
				.ToList( cancellationToken );

			Logger.LogInformation( $"Sending {dataSet.Count} records to the api" );

			try
			{
				await Api.PostDataAsync( dataSet, cancellationToken );
			}
			catch( Exception e )
			{
				Logger.LogError( $"There were errors when sending the data to the api: {e.Message}" );
			}
		}

		public void Dispose() => PulseRepo.Dispose();
	}
}
