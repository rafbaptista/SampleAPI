using UserAPI.Domain.Entities;
using UserAPI.Domain.Security;

namespace UserAPI.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        JwtTokenDto GenerateToken(User user);
    }
}
