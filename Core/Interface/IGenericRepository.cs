using Core.Entities;


namespace Core.Interface
{
    public interface IGenericRepository<T> where T: Product
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();

        Task Add(T entity);
        Task Update(T entity);
        Task Remove(int id);


        Task<bool> SaveAllAsync();
        Task<bool> Exist(int id);
    }
}


