using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ProjectCV.Server.DB;
using ProjectCV.Server.IServices.IAccountservices;
using ProjectCV.Server.Models;

namespace ProjectCV.Server.Services.AccountServices
{
    public class RegisterServices : IRegisterServices
    {
        private readonly IUserServices _userServices;
        private readonly DBSetting _dBSetting;
        public RegisterServices(IUserServices userServices, DBSetting dBSetting)
        {
            _dBSetting = dBSetting;
            _userServices = userServices;
        }
        public virtual async Task<object> Register(User user)
        {

            User ketqua = await _userServices.GetOneUser(user.Username);
            try
            {
                if (ketqua == null)
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    await _dBSetting.User.AddAsync(user);
                    await _dBSetting.SaveChangesAsync();
                    return user;
                }
                return new { success = false, mess = "Account already exists " };
            }
            catch (Exception exception)
            {
                return new { success = false, mess = $"{exception.Message}" };

            }



        }
    }
}
