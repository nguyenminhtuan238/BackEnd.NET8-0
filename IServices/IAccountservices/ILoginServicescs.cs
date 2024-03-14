using ProjectCV.Server.Models;

namespace ProjectCV.Server.IServices.IAccountservices
{
    public interface ILoginServicescs
    {
        Task<object> Login(User user);
    }
}
