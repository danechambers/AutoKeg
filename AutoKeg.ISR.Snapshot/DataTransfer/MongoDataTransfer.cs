using System.Threading.Tasks;
using MongoDB.Driver;

namespace AutoKeg.ISR.Snapshot.DataTransfer
{
    public class MongoDataTransfer<T> : IDataTransfer<T> where T : new()
    {
        private IMongoCollection<T> Collection { get; }
        private static MongoClient Client { get; } = new MongoClient();

        public MongoDataTransfer(string database, string collection)
        {
            Collection = Client.GetDatabase(database)
                .GetCollection<T>(collection);
        }

        public async Task SaveData(T data) =>
            await Collection.InsertOneAsync(data);
    }
}