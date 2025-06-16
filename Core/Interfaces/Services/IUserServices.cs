using Core.Entities;

namespace Core.Interface
{
    public interface IUserServices
    {
        Task<User> RegisterUser(string userName, string password, string role);
        Task<User> Login(string userName, string password);
    }
}
