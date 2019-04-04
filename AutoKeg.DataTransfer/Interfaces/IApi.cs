using System.Threading;
using System.Threading.Tasks;

namespace AutoKeg.DataTransfer.Interfaces
{
	public interface IApi
	{
		Task<T> PostDataAsync<T>( object data, CancellationToken cancellationToken = default )
			where T : new();

		Task PostDataAsync( object data, CancellationToken cancellationToken = default );
	}
}
