using Core.Entities;


namespace Core.Interface
{
    public interface IGenericRepository<T> where T: Product
    {
        Task<T?> GetByIdAsync(string id);
        Task<IReadOnlyList<T>> ListAllAsync();

        Task Add(T entity);
        Task Update(T entity);
        Task Remove(string id);


        Task<bool> SaveAllAsync();
        Task<bool> Exist(string id);
    }
}


