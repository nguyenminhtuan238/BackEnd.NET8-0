using ProjectCV.Server.Models;

namespace ProjectCV.Server.IServices.IAccountservices
{
    public interface IRegisterServices
    {
        Task<object> Register(User user);
    }
}
