
using Core.Entities;

namespace Core.Interface
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
