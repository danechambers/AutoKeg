using System;
using System.Threading;
using System.Threading.Tasks;
using AutoKeg.DataProcessing.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutoKeg.DataProcessing.Service
{
	public class Application : IHostedService
	{
		private IServiceProvider Services { get; }

		private ILogger Logger { get; }

		public Application( IServiceProvider services, ILoggerFactory logger )
		{
			Services = services;
			Logger = logger.CreateLogger<Application>();
		}

		public async Task StartAsync( CancellationToken cancellationToken )
		{
			Logger.LogInformation( "Executing data push" );
			using( var scope = Services.CreateScope() )
			using( var apiPush = scope.ServiceProvider.GetRequiredService<IDataPush>() )
			{
				await apiPush.PushData( cancellationToken );
			}
		}

		public Task StopAsync( CancellationToken cancellationToken )
		{
			return Task.CompletedTask;
		}
	}
}
