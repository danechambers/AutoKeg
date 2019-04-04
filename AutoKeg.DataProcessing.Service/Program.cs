using System.IO;
using System.Threading.Tasks;
using AutoKeg.Configuration;
using AutoKeg.DataProcessing.Service.Configuration;
using AutoKeg.DataProcessing.Service.Interfaces;
using AutoKeg.DataProcessing.Service.Pulse;
using AutoKeg.DataTransfer.Configuration;
using AutoKeg.DataTransfer.DTOs;
using AutoKeg.DataTransfer.Interfaces;
using AutoKeg.DataTransfer.TransferContexts;
using AutoKeg.DataTransfer.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutoKeg.DataProcessing.Service
{
	public class Program
	{
		public static async Task Main( string[] args )
		{
			var host = new HostBuilder()
				.ConfigureAppConfiguration( ( hostContext, config ) =>
				{
					config.SetBasePath( Directory.GetCurrentDirectory() )
						.AddJsonFile( "appsettings.json", optional: false )
						.AddJsonFile( "datatransfersettings.json", optional: false )
						.AddCommandLine( args );
				} )
				.ConfigureLogging( ( context, logBuilder ) =>
				{
					logBuilder.AddConfiguration( context.Configuration.GetSection( "Logging" ) )
						.AddConsole();

					if( context.HostingEnvironment.IsDevelopment() )
						logBuilder.AddDebug();
				} )
				.ConfigureServices( ( context, services ) =>
				{
					var config = context.Configuration.Get<DataProcessingSettings>();
					var dbConfig = context.Configuration.Get<SqliteSettings>();

					services
						.Configure<ConsoleLifetimeOptions>( options => options.SuppressStatusMessages = true )
						.Configure<EverestApiSettings>( context.Configuration.GetSection( "EverestApi" ) )
						.AddSingleton<IPiSerialNumber, PiSerialNumber>()
						.AddDbContext<CountDataContext>( options =>
							options.UseSqlite( $"Data Source={dbConfig.Database}" ) )
						.AddScoped<ILocalRepository<PulseDTO>, SqliteDataTransfer>()
						.AddScoped<IDataPush, PulseProcessing>()
						.AddHostedService<Application>();
				} )
				.Build();

			using( host )
			{
				await host.StartAsync();
				await host.StopAsync();
			}
		}
	}
}
