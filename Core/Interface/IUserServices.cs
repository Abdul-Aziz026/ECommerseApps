using Core.Entities;
using Core.Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Core.Interface
{
    public interface IUserServices
    {
        Task<User> RegisterUser(RegisterDTO regiserDto);
        Task<bool> UserExist(string username);
        Task<User> Login(LoginDTO loginDto);
    }
}
