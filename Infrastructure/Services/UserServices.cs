
using Core.Entities;
using Core.Entities.DTO;
using Core.Interface;
using Infrastructure.Data;

namespace Infrastructure.Services
{
    public class UserServices : IUserServices
    {

        private readonly MongoDbContext _context;

        public UserServices(MongoDbContext context)
        {
            _context = context;
        }



        public Task<User> RegisterUser(RegisterDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserExist(string username)
        {
            throw new NotImplementedException();
        }
    }
}
