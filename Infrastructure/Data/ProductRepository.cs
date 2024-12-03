using Core.Interface;
using Infrastructure.Data;
using Core.Entities;
using MongoDB.Driver;

namespace Infrastructure.Data
{
    // public class ProductRepository(MongoDbContext context) : IProductRepository
    public class ProductRepository : IProductRepository
    {
        private readonly MongoDbContext context;

        public ProductRepository(MongoDbContext _context)
        {
            context = _context;
        }

        public string Root()
        {
            return "E-Commerse Root Page...";
        }

        public async Task<List<Product>?> GetProducts(string? brand, string? type, string? sort)
        {
            Console.WriteLine("brands: " + brand + " type: " + type);
            // Build the filter using Builder...
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
            var result = await context.product.Find(filter).Sort(sortDefinition).ToListAsync();

            for (int i = 0; i < result.Count; ++i)
            {
                Console.WriteLine("Result: " + result[i].Name);
            }
            return result;
        }


        public async Task<Product?> GetProductById(string id)
        {

            try
            {
                var prod = await context.product.Find(p => p.Id == id).FirstOrDefaultAsync();
                return prod;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n\nExcepion thrown...\n\n\n");
                return null;
            }
        }


        public async Task<IReadOnlyList<string>> GetBrands()
        {
            try
            {
                var brands = await context.product.Distinct<string>("Brand", Builders<Product>.Filter.Empty)
                            .ToListAsync();
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
                var types = await context.product.Distinct<string>("Type", Builders<Product>.Filter.Empty)
                            .ToListAsync();
                return types;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve types. Error: {ex.Message}");
            }
        }

        public async Task<int> UpdateProduct(string id, Product prod)
        {
            try
            {
                var result = await context.product.ReplaceOneAsync(p => p.Id == id, prod);

                if (result.MatchedCount == 0) return 0;

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public async Task<string> AddProduct(Product prod)
        {
            try
            {
                await context.product.InsertOneAsync(prod);
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
                var result = await context.product.DeleteOneAsync(p => p.Id == id);
                return (result.DeletedCount == 0) ? false : true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> ExistProduct(string id)
        {

            var result = await context.product.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (result == null)
            {
                return false;
            }
            return true;
        }

    }
}
