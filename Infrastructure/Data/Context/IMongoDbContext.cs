using Core.Entities;
using MongoDB.Driver;

namespace Infrastructure.Data.Context
{
    public interface IMongoDbContext
    {
        public Task<User?> GetUserByName(string username);
        public Task RegisterUser(User user);
        Task SaveAsync<T>(T item) where T : class;
        Task<T> GetItemByIdAsync<T>(object id) where T : class;
        public Task<List<T>> GetItemsByConditionAsync<T>(FilterDefinition<T> filter,
                                                            SortDefinition<T>? sort = null) where T : class;
        Task<bool> UpdateItemByIdAsync<T>(object id, T item) where T : class;
        Task<bool> DeleteItemByIdAsync<T>(object id) where T : class;
        Task<IReadOnlyList<TResponse>> GetDistinctValuesAsync<TDocument, TResponse>(string fieldName,
                                                            FilterDefinition<TDocument>? filter = null) where TDocument : class;
    }
}
