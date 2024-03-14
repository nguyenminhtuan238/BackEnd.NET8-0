using ProjectCV.Server.DB;
using ProjectCV.Server.Models;
using Microsoft.EntityFrameworkCore;
using ProjectCV.Server.IServices.INoteServices;

namespace ProjectCV.Server.Services.NoteServices
{
    public class NoteServices:INoteServices
    {
        private readonly DBSetting _dBSetting;
        public NoteServices(DBSetting bSetting)
        {
            _dBSetting = bSetting;
        }
        public async Task<object> Get()
        {
            try
            {

                var Notes = await _dBSetting.Note
                        .AsNoTracking()
                        .Select(x => new Note() { Id = x.Id, Content = x.Content,DateTime=x.DateTime,TypeId=x.TypeId })
                        .ToListAsync();
                return new { success = true, Notes };
            }
            catch (Exception ex)
            {
                return new { succes = false, ex };

            }
        }
        public async Task<Note> GetById(int id)
        {

            var Note = await _dBSetting.Note
                    .AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            return Note;


        }
        public async Task<object> Insert(Note _note)
        {
            try
            {
                var note = await _dBSetting.Note.AddAsync(_note);
                _dBSetting.SaveChanges();
                return new { success = true, note };
            }
            catch (Exception ex)
            {
                return new { succes = false, ex };

            }
        }
        public async Task<object> Update(Note _note, int id)
        {
            try
            {
                var Note = await GetById(id);
                Note.Id = _note.Id;
               Note.Content = _note.Content;
                Note.DateTime = _note.DateTime;
                Note.TypeId = _note.TypeId;
                _dBSetting.SaveChanges();
                return new { success = true, Note };
            }
            catch (Exception ex)
            {
                return new { succes = false, ex };

            }
        }
        public async Task<object> Delete(int id)
        {
            try
            {
                var Note = await GetById(id);
                _dBSetting.Note.Remove(Note);
                _dBSetting.SaveChanges();
                return new { success = true, Note };
            }
            catch (Exception ex)
            {
                return new { succes = false, ex };

            }
        }
    }
}
