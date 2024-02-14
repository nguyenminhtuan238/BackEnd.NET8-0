using ProjectCV.Server.Models;

namespace ProjectCV.Server.IServices
{
    public interface ILoginServicescs
    {
        Task<object> Login(User user);
    }
}
