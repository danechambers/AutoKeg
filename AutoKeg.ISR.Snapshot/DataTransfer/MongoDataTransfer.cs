using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AutoKeg.ISR.Snapshot.DataTransfer
{
    public class MongoDataTransfer : IDataTransfer<BsonDocument>
    {
        private IMongoCollection<BsonDocument> Collection { get; }
        private static MongoClient Client { get; } = new MongoClient();

        public MongoDataTransfer(string database, string collection)
        {
            Collection = Client.GetDatabase(database)
                .GetCollection<BsonDocument>(collection);
        }

        public async Task SaveData(BsonDocument data) =>
            await Collection.InsertOneAsync(data);

        public async void SaveData(object data) =>
            await SaveData(data as BsonDocument);
    }
}