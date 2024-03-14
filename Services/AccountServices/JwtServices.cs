using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectCV.Server.DB;
using ProjectCV.Server.Helpers;
using ProjectCV.Server.IServices.IAccountservices;
using ProjectCV.Server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProjectCV.Server.Services.AccountServices
{
    public class JwtServices : IJwtServices
    {
        private readonly DBSetting _dBSetting;
        private readonly IConfiguration _config;
        public JwtServices(DBSetting bSetting, IConfiguration config)
        {
            _dBSetting = bSetting;
            _config = config;
        }
        public async Task<TokenHelpers> GenerateRefreshToken(string username)
        {
            return await GenerateJSONWebToken(username);
        }
        public virtual async Task<TokenHelpers> GenerateJSONWebToken(string userName)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var ketqua = await _dBSetting.User.SingleOrDefaultAsync(x => x.Username == userName);
                var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, ketqua.Id.ToString()),
                 new Claim(ClaimTypes.Role, ketqua.IsAdmin?"Admin":"User"),

                };
                var tokenDescriptor = new JwtSecurityToken(_config["Jwt:Issuer"],
                  _config["Jwt:Issuer"],
                 claims,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
                var refreshToken = GenerateRefreshToken();


                return new TokenHelpers { Access_Token = token, Refresh_Token = refreshToken }; ;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);

                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var Key = Encoding.UTF8.GetBytes(_config["JWT:Key"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }


            return principal;
        }
    }
}
