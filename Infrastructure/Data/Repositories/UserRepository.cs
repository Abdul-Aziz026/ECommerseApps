using Core.Entities;
using Core.Interfaces.Repository;
using Infrastructure.Data.Context;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly IMongoDbContext _context;

        public UserRepository(IMongoDbContext context)
        {
            _context = context;
        }
        public async Task<User> Login(string userName, string password)
        {
            var user = await _context.GetUserByName(userName);

            if ( !await UserExist(userName) || !IsEqual(password, user.Password))
            {
                throw new Exception("Invalid username or password.");
            }

            return user;
        }
        
        public async Task<User> RegisterUser(string userName, string password, string role)
        {
            if (await UserExist(userName))
            {
                throw new Exception("Username already exists.");
            }
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var newUser = new User
            {
                Username = userName,
                Password = hashPassword,
                Role = role
            };
            await _context.RegisterUser(newUser);
            return newUser;
        }
        public async Task<bool> UserExist(string username)
        {
            var user = await _context.GetUserByName(username);
            return (user != null) ? true : false;
        }
        private static bool IsEqual(string password, string userPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, userPassword);
        }
    }
}
