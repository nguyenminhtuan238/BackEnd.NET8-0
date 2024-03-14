using ProjectCV.Server.Models;
using System.Threading.Tasks;

namespace ProjectCV.Server.IServices.ITypeServices
{
    public interface ITypeServices
    {
        Task<object> Get();
        Task<Types> GetById(int id);
        Task<object> Insert(Types type);
        Task<object> Update(Types type, int id);
        Task<object> Delete(int id);
    }
}
