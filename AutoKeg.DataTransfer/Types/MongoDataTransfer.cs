using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoKeg.DataTransfer.Interfaces;
// using MongoDB.Driver;

namespace AutoKeg.DataTransfer.Types
{
	public class MongoDataTransfer<T> : ILocalRepository<T> where T : new()
	{
		public IQueryable<T> DataSet => Enumerable.Empty<T>().AsQueryable();

		// private IMongoCollection<T> Collection { get; }

		// public MongoDataTransfer(string mongoUrl, string database, string collection)
		// {
		//     Collection = new MongoClient(mongoUrl)
		//         .GetDatabase(database)
		//         .GetCollection<T>(collection);
		// }

		// public async Task SaveDataAsync(T data, CancellationToken cancellationToken = default) =>
		//     await Collection.InsertOneAsync(data, options: null, cancellationToken);

		public void Dispose() { } // do nothing

		public Task SaveDataAsync( T data, CancellationToken cancellationToken = default )
		{
			return Task.CompletedTask;
		}

		public Task UpdateDataAsync( Func<T, bool> dataFilter, Action<T> updateDatAction, CancellationToken cancellationToken = default )
		{
			return Task.CompletedTask;
		}
	}
}