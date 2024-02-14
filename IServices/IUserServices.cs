using ProjectCV.Server.Models;
using ProjectCV.Server.Services;

namespace ProjectCV.Server.IServices
{
    public interface IUserServices
    {
        Task<bool> CheckEmail(User user);
        Task<bool> Checkpassword(User user);
        Task<User> GetOneUser(string username);
    }
}
