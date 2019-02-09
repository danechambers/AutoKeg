using MongoDB.Driver;
using MongoDB.Bson;
using NUnit.Framework;

namespace AutoKeg.Tests.Mongo
{
    public class Alive
    {
        [Test]
        public void MongoIsLive()
        {
            var client = new MongoClient("mongodb://192.168.1.19");
            var database = client.GetDatabase("pingdb");
            bool isMongoLive = database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
            Assert.That(isMongoLive, Is.True);
        }
    }
}