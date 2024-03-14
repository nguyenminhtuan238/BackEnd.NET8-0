using ProjectCV.Server.Models;
using ProjectCV.Server.Services;

namespace ProjectCV.Server.IServices.IAccountservices
{
    public interface IUserServices
    {
        Task<bool> CheckEmail(User user);
        Task<bool> Checkpassword(User user);
        Task<User> GetOneUser(string username);
        Task<object> GetAll();
        Task<User> Getbyid(int id);
    }
}
