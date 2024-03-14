using ProjectCV.Server.Models;

namespace ProjectCV.Server.IServices.INoteServices
{
    public interface INoteServices
    {
        Task<object> Get();
        Task<Note> GetById(int id);
        Task<object> Insert(Note _note);
        Task<object> Update(Note _note, int id);
        Task<object> Delete(int id);
    }
}
