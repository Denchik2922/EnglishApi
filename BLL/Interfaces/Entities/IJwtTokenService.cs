using Models.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLL.Interfaces.Entities
{
    public interface IJwtTokenService
    {
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        string GenerateRefreshToken();
        Task<string> GetToken(User user);
    }
}