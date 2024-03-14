using ProjectCV.Server.DB;
using ProjectCV.Server.Helpers;
using ProjectCV.Server.IServices.IAccountservices;
using ProjectCV.Server.Models;

namespace ProjectCV.Server.Services.AccountServices
{
    public class ResetPasswordServices : IResetPasswordServices
    {
        private readonly IUserServices _userServices;
        private readonly DBSetting _dBSetting;
        public ResetPasswordServices(IUserServices userServices, DBSetting dBSetting)
        {
            _userServices = userServices;
            _dBSetting = dBSetting;
        }
        private async Task<bool> Reset(string newpassword, string password, int id)

        {
            User result = await _userServices.Getbyid(id);
            if (result != null ? !BCrypt.Net.BCrypt.Verify(password, result.Password) : false)
            {

                Exception exception = new ExeptionLogin();
                throw exception;
            }
            else
            {
                result.Password = BCrypt.Net.BCrypt.HashPassword(newpassword);
                _dBSetting.SaveChanges();
                return true;
            }

        }
        public async Task<object> ResetPassword(string newpassword, string password, int id)
        {
            try
            {
                var result = await Reset(newpassword, password, id);
                return new { success = true, result };

            }
            catch (Exception exception)
            {
                return new { success = false, mess = $"{exception.Message}" };
            }
        }

    }
}
