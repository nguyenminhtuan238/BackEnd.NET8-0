using Humanizer;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ProjectCV.Server.DB;
using ProjectCV.Server.Helpers;
using ProjectCV.Server.IServices.IAccountservices;
using ProjectCV.Server.Models;

namespace ProjectCV.Server.Services.AccountServices
{
    public class UserServices : IUserServices
    {
        private readonly DBSetting _dBSetting;
        public UserServices(DBSetting dBSetting)
        {
            _dBSetting = dBSetting;

        }
        public async Task<User> GetOneUser(string username)
        {

            return await _dBSetting.User.AsNoTracking().SingleOrDefaultAsync(x => x.Username == username);
        }
        public async Task<User> Getbyid(int id)
        {

            return await _dBSetting.User.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> CheckEmail(User user)
        {
            User ketqua = await GetOneUser(user?.Username);
            Exception exception = new ExeptionLogin();
            return ketqua == null ? throw exception : true;
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

                Exception exception = new ExeptionLogin();
                throw exception;
            }
            else
            {
                return true;
            }
        }
        public async Task<object> GetAll()
        {
            return new
            {
                success = true,
                users = await _dBSetting.User.AsNoTracking().Select(x => new User()
                {
                    Id = x.Id,
                    Username = x.Username,
                    Password = x.Password,
                }).ToListAsync()
            };

        }


    }
}
