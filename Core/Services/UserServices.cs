
using Core.Entities;
using Core.Interface;
using Core.Interfaces.Repository;

namespace Core.Services
{
    public class UserServices : IUserServices
    {

        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Login(string userName, string password)
        {
            return await _userRepository.Login(userName, password);
        }
        
        public async Task<User> RegisterUser(string userName, string password, string role)
        {
            return await _userRepository.RegisterUser(userName, password, role);
        }
    }
}
