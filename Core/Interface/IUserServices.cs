using Core.Entities;
using Core.Entities.DTO;

namespace Core.Interface
{
    public interface IUserServices
    {
        Task<User> RegisterUser(RegisterDTO user);
        Task<bool> UserExist(string username);
    }
}
