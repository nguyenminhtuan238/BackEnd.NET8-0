using Microsoft.EntityFrameworkCore;

using ProjectCV.Server.DB;
using ProjectCV.Server.IServices;
using ProjectCV.Server.Models;

namespace ProjectCV.Server.Services
{
    public class UserServices:IUserServices
    {
        private readonly DBSetting _dBSetting;
        public  UserServices(DBSetting dBSetting)
        {
            _dBSetting = dBSetting;
           
        }
        public async Task<User> GetOneUser(string username)
        {
           
            return await _dBSetting.User.SingleOrDefaultAsync(x => x.Username == username);
        }
        public async Task<bool> CheckEmail(User user)
        {
            User ketqua = await GetOneUser(user?.Username);
            Exception exception = new("Username and password is wrong");
            return ketqua==null?throw exception:true;
            //if (ketqua == null)
            //{
            //    Exception exception = new("Username and password is wrong");
            //    throw exception;
            //}
            //else
            //{
            //    return true;
            //}


        }
        public async Task<bool> Checkpassword(User user)
        {
            User ketqua = await GetOneUser(user.Username);
            if (ketqua != null ? !BCrypt.Net.BCrypt.Verify(user.Password, ketqua.Password) : false)
            {

                Exception exception = new ("Username and password is wrong");
                throw exception;
            }
            else
            {
                return true;
            }
        }
       
      

    }
}
