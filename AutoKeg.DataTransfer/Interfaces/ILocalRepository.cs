using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AutoKeg.DataTransfer.Interfaces
{
	public interface ILocalRepository<T> : IDisposable where T : new()
	{
		/// <summary>
		/// Reference to data set in the repo
		/// </summary>
		IQueryable<T> DataSet { get; }

		/// <summary>
		/// Save a single data point to the repo
		/// </summary>
		Task SaveDataAsync( T data, CancellationToken cancellationToken = default );

		Task UpdateDataAsync( Func<T, bool> dataFilter, Action<T> updateDatAction, CancellationToken cancellationToken = default );
	}
}