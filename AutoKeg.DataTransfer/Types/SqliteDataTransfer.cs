using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoKeg.DataTransfer.DTOs;
using AutoKeg.DataTransfer.Interfaces;
using AutoKeg.DataTransfer.TransferContexts;
using Microsoft.Extensions.Logging;

namespace AutoKeg.DataTransfer.Types
{
	public class SqliteDataTransfer : ILocalRepository<PulseDTO>
	{
		private ILogger Logger { get; }
		private CountDataContext Db { get; }

		public IQueryable<PulseDTO> DataSet => Db.PulseCounts;

		public SqliteDataTransfer( CountDataContext dbContext, ILoggerFactory loggerFactory )
		{
			Logger = loggerFactory.CreateLogger<SqliteDataTransfer>();
			Db = dbContext;
		}

		public async Task SaveDataAsync( PulseDTO data, CancellationToken cancellationToken = default )
		{
			Db.PulseCounts.Add( data );
			var count = await Db.SaveChangesAsync( cancellationToken );
			Logger.LogInformation( $"{count} records saved to the database" );
		}

		public async Task UpdateDataAsync( Func<PulseDTO, bool> dataFilter,
			Action<PulseDTO> updateDataAction,
			CancellationToken cancellationToken = default )
		{
			var hasUpdates = false; // avoid multiple enumeration
			foreach( var data in Db.PulseCounts.Where( dataFilter ) )
			{
				updateDataAction( data );
				hasUpdates = true;
			}

			if( !hasUpdates )
				return;

			var count = await Db.SaveChangesAsync( cancellationToken );
			Logger.LogInformation( $"{count} records updated in the database" );
		}

		public void Dispose() => Db.Dispose();
	}
}