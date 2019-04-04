using System.Threading;
using System.Threading.Tasks;

namespace AutoKeg.DataTransfer.Interfaces
{
	public interface IApi
	{
		Task<IApiResult> PostDataAsync( object data,
			CancellationToken cancellationToken = default );
	}
}
