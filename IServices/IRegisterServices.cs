using ProjectCV.Server.Models;

namespace ProjectCV.Server.IServices
{
    public interface IRegisterServices
    {
        Task<object> Register(User user);
    }
}
