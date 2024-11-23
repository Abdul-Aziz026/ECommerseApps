using Core.Entities;
using Core.Interface;
using MongoDB.Driver;
using System.Collections;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Product
    {
        private readonly MongoDbContext _context;

        public GenericRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var collection = _context.GetCollection<T>("product"); // Dynamically get the collection
            return await collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        // public async Task<IReadOnlyList<T>> ListAllAsync()
        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            var collection = _context.GetCollection<T>("product");
            try
            {
                return await collection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<T>();
            }
        }

        public async Task Add(T entity)
        {
            var collection = _context.GetCollection<T>("product");
            await collection.InsertOneAsync(entity);
        }

        public async Task Update(T entity)
        {
            var collection = _context.GetCollection<T>("product");
            await collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public async Task Remove(int id)
        {
            var collection = _context.GetCollection<T>("product");
            await collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<bool> SaveAllAsync()
        {
            // var collection = _context.GetCollection<T>("BaseEntity");

            try
            {
                // Bulk insert operation, assuming you want to insert multiple documents at once
                // await collection.InsertManyAsync(entities);
                return true;  // Return true if the operation was successful
            }
            catch (Exception)
            {
                return false;  // Return false if an error occurred
            }
        }

        public async Task<bool> Exist(int id)
        {
            var collection = _context.GetCollection<T>("product");
            return await collection.Find(x => x.Id == id).AnyAsync();
        }
    }

}
