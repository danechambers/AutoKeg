using MongoDB.Driver;
using MongoDB.Bson;
using NUnit.Framework;
using System.Threading.Tasks;

namespace AutoKeg.Tests.Mongo
{
    public class Alive
    {
        [Ignore("No longer using mongo")]
        [Test]
        public async Task MongoIsLive()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("pingdb");
            var returnDoc = await database.RunCommandAsync((Command<BsonDocument>)"{ping:1}");
            var expectedDoc = new BsonDocument { { "ok", 1 } };
            Assert.That(returnDoc, Is.EqualTo(expectedDoc));
        }
    }
}