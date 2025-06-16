using Core.Entities;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Data.Context
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;
        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionUri);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        private IMongoCollection<T> GetCollection<T>() where T : class
        {
            var collection = _database.GetCollection<T>(typeof(T).Name.ToLower());
            return collection;
        }

        public async Task SaveAsync<T>(T item) where T : class
        {
            var collection = GetCollection<T>();
            await collection.InsertOneAsync(item);
        }

        public async Task<User?> GetUserByName(string username)
        {
            var filter = Builders<User>.Filter.Eq("Username", username);
            var collection = GetCollection<User>();
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task RegisterUser(User user)
        {
            var collection = GetCollection<User>();
            await collection.InsertOneAsync(user);
        }
        public async Task<T> GetItemByIdAsync<T>(object id) where T : class
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id.ToString()));
            var collection = GetCollection<T>();
            var res = await collection.Find(filter).FirstOrDefaultAsync();
            return res;
        }

        public async Task<List<T>> GetItemsByConditionAsync<T>(FilterDefinition<T> filter, SortDefinition<T>? sort = null) where T : class
        {
            var collection = GetCollection<T>();
            var result = collection.Find(filter);
            if (sort != null)
            {
                result = result.Sort(sort);
            }
            return await result.ToListAsync();
        }
        public async Task<bool> UpdateItemByIdAsync<T>(object id, T item) where T : class
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var collection = GetCollection<T>();
            var result = await collection.ReplaceOneAsync(filter, item);
            return (result.MatchedCount > 0 && result.ModifiedCount > 0) ? true : false;
        }

        public async Task<bool> DeleteItemByIdAsync<T>(object id) where T : class
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var collection = GetCollection<T>();
            var result = await collection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

        public async Task<IReadOnlyList<TResponse>> GetDistinctValuesAsync<TDocument, TResponse>(string fieldName, 
                                                                                           FilterDefinition<TDocument>? filter = null) where TDocument : class
        {
            var collection = GetCollection<TDocument>();
            filter ??= Builders<TDocument>.Filter.Empty;

            var result = await collection.DistinctAsync<TResponse>(fieldName, filter);
            return await result.ToListAsync();
        }
    }
}
