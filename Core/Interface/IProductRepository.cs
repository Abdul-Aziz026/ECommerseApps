using Core.Entities;

namespace Core.Interface
{
    public interface IProductRepository
    {
        
        string Root();
        Task<List<Product>?> GetProducts(string? brand, string? type, string? sort);
        Task<Product?> GetProductById(int id);
        Task<IReadOnlyList<string>> GetBrands();
        Task<IReadOnlyList<string>> GetTypes();
        Task<string> AddProduct(Product prod);
        Task<int> UpdateProduct(int id, Product prod);
        Task<bool> DeleteProduct(int id);
        Task<bool> ExistProduct(int id);
    }
}
