using System.Text.Json;
using Core.Entities;
using Core.Interface;
using StackExchange.Redis;

namespace Infrastructure.Services
{
    public class CartService : ICartService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public CartService(IConnectionMultiplexer redis) // Use the interface here
        {
            _redis = redis; // Assign the resolved interface
            _database = _redis.GetDatabase(); // Access the database
        }
        public async Task<bool> DeleteCartAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }

        public async Task<ShoppingCart?> GetCartAsync(string key)
        {

            var data = await _database.StringGetAsync(key);



            if (data.IsNullOrEmpty)
            {
                return null;
            }

            return JsonSerializer.Deserialize<ShoppingCart>(data);
        }

        public async Task<ShoppingCart?> SetCartAsync(ShoppingCart cart)
        {
            var created = await _database.StringSetAsync(cart.Id, 
                JsonSerializer.Serialize(cart),
                TimeSpan.FromDays(30));
            if (!created) return null;

            return await GetCartAsync(cart.Id);
        }
    }
}
