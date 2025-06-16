using Core.Entities;

namespace Core.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<User> RegisterUser(string userName, string password, string role);
        Task<User> Login(string userName, string password);
    }
}
