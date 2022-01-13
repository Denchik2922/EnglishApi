using Microsoft.IdentityModel.Tokens;
using Models.Entities;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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