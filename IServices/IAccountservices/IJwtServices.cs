using ProjectCV.Server.Helpers;
using ProjectCV.Server.Models;
using System.Security.Claims;

namespace ProjectCV.Server.IServices.IAccountservices
{
    public interface IJwtServices
    {
        Task<TokenHelpers> GenerateJSONWebToken(string userName);
        Task<TokenHelpers> GenerateRefreshToken(string userName);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
