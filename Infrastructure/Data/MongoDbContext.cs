using Core.Entities;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionUri, string databaseName)
        {
            var client = new MongoClient(connectionUri);
            _database = client.GetDatabase(databaseName);
        }
        public IMongoCollection<Product> product => _database.GetCollection<Product>("product");
        public IMongoCollection<User> user => _database.GetCollection<User>("Users");
        // Generic method to get a collection based on type
        public IMongoCollection<T> GetCollection<T>(string collectionName) where T : class
        {
            return _database.GetCollection<T>(collectionName);
        }

    }
}
