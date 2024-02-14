using ProjectCV.Server.DB;
using ProjectCV.Server.IServices;
using ProjectCV.Server.Models;

namespace ProjectCV.Server.Services
{
    public class LoginServicescs:ILoginServicescs
    {
        private readonly IUserServices _userServices;
        private readonly IJwtServices _jwtServices;
        public LoginServicescs(IUserServices userServices,IJwtServices jwtServices) {
            _userServices = userServices;
            _jwtServices = jwtServices;
        
        }
        public virtual async Task<object> Login(User user)
        {
            try
            {
                if (await _userServices.CheckEmail(user) && await _userServices.Checkpassword(user))
                {
                    var tokenString = await _jwtServices.GenerateJSONWebToken(user.Username);
                    return new { success = true, tokenString };
                }
                return new { success = false };
            }
            catch (Exception exception)
            {
                return new { success = false, mess = $"{exception.Message}" };
            }
        }

    }
}
