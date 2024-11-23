using Core.Entities;
using Core.Interface;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    public class G<T> : IG<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public G(MongoDbContext context, string collectionName)
        {
            _collection = context.GetCollection<T>(collectionName) ;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task Add(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task Update(T entity)
        {
            //await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
            return;
        }

        public async Task Remove(int id)
        {
            // int I = Int32.Parse(id);
            // await _collection.DeleteOneAsync(x => x.Id == id);
            return;
        }

        public Task<bool> SaveAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exist(int id)
        {
            return true;
            // return await _collection.Find(x => x.Id == id).AnyAsync();
        }
    }

}
