
using Core.Entities;
using Core.Entities.DTO;
using Core.Interface;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SharpCompress.Common;

namespace Infrastructure.Services
{
    public class UserServices : IUserServices
    {

        private readonly MongoDbContext _context;

        public UserServices(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<User> Login(LoginDTO loginDto)
        {
            var collection = _context.GetCollection<User>("Users");

            // Fetch user from the database
            var user = await collection.Find(u => u.Username == loginDto.Username).FirstOrDefaultAsync();

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                throw new Exception("Invalid username or password.");
            }

            var newUser = new User
            {
                Username = user.Username,
                Password = user.Password,
                Role = user.Role
            };
            return newUser;
        }
        
        public async Task<User> RegisterUser(RegisterDTO regiserDto)
        {
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(regiserDto.Password);

            var newUser = new User
            {
                Username = regiserDto.Username,
                Password = hashPassword,
                Role = regiserDto.Role
            };
            var collection = _context.GetCollection<User>("Users"); // Dynamically get the collection
            await collection.InsertOneAsync(newUser);
            return newUser;
        }

        public async Task<bool> UserExist(string username)
        {
            var collection = _context.GetCollection<User>("Users");
            var user = await collection.Find(u => u.Username == username).FirstOrDefaultAsync();
            return user != null;
        }
    }
}
