using Core.Entities;
using MongoDB.Driver;
using Core.Interfaces.Repository;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories
{
    // public class ProductRepository(MongoDbContext context) : IProductRepository
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoDbContext context;

        public ProductRepository(IMongoDbContext _context)
        {
            context = _context;
        }

        public string Root()
        {
            return "E-Commerse Root Page...";
        }

        public async Task<List<Product>?> GetProducts(string? brand, string? type, string? sort)
        {
            var filter = Builders<Product>.Filter.Empty; // Start with an empty filter (matches all)

            if (!string.IsNullOrEmpty(brand))
            {
                filter &= Builders<Product>.Filter.Eq(p => p.Brand, brand); // Add filter for brand
            }

            if (!string.IsNullOrEmpty(type))
            {
                filter &= Builders<Product>.Filter.Eq(p => p.Type, type); // Add filter for type
            }

            var sortDefinition = Builders<Product>.Sort.Ascending(p => p.Name); // Default sort by Name

            if (sort == "priceAsc")
            {
                sortDefinition = Builders<Product>.Sort.Ascending(p => p.Price); // Sort by price ascending
            }
            else if (sort == "priceDesc")
            {
                sortDefinition = Builders<Product>.Sort.Descending(p => p.Price); // Sort by price descending
            }

            // Execute the query with the constructed filter
            var result = await context.GetItemsByConditionAsync(filter, sortDefinition);
            return result;
        }

        public async Task<Product?> GetProductById(string id)
        {
            try
            {
                var products = await context.GetItemByIdAsync<Product>(id);
                return products;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IReadOnlyList<string>> GetBrands()
        {
            try
            {
                var brands = await context.GetDistinctValuesAsync<Product, string>("Brand");
                return brands;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve brands. Error: {ex.Message}");
            }
        }

        public async Task<IReadOnlyList<string>> GetTypes()
        {
            try
            {
                var types = await context.GetDistinctValuesAsync<Product, string>("Type");
                return types;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve types. Error: {ex.Message}");
            }
        }

        public async Task<bool> UpdateProduct(string id, Product prod)
        {
            try
            {
                return await context.UpdateItemByIdAsync<Product>(id, prod);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public async Task<string> AddProduct(Product prod)
        {
            try
            {
                await context.SaveAsync<Product>(prod);
                return "Added Product...";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<bool> DeleteProduct(string id)
        {
            try
            {
                return await context.DeleteItemByIdAsync<Product>(id);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> ExistProduct(string id)
        {

            var result = await context.GetItemByIdAsync<Product>(id);
            if (result == null)
            {
                return false;
            }
            return true;
        }

    }
}
