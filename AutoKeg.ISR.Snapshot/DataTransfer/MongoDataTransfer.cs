using System.Threading.Tasks;
using MongoDB.Driver;

namespace AutoKeg.ISR.Snapshot.DataTransfer
{
    public class MongoDataTransfer<T> : IDataTransfer<T> where T : new()
    {
        private IMongoCollection<T> Collection { get; }

        public MongoDataTransfer(string mongoUrl, string database, string collection)
        {
            Collection = new MongoClient(mongoUrl)
                .GetDatabase(database)
                .GetCollection<T>(collection);
        }

        public async Task SaveDataAsync(T data) =>
            await Collection.InsertOneAsync(data);
    }
}