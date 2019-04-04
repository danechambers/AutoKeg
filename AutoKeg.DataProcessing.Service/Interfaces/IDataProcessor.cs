using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoKeg.DataProcessing.Service.Interfaces
{
	public interface IDataPush : IDisposable
	{
		Task PushData( CancellationToken cancellationToken = default );
	}
}
