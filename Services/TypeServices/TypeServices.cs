using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using ProjectCV.Server.DB;
using ProjectCV.Server.IServices.ITypeServices;
using ProjectCV.Server.Models;

namespace ProjectCV.Server.Services.TypeServices
{
    public class TypeServices: ITypeServices
    {
        private readonly DBSetting _dBSetting;
        public TypeServices(DBSetting bSetting)
        {
            _dBSetting = bSetting;
        }
        public async Task<object> Get()
        {
            try
            {

                var types = await _dBSetting.Type
                        .AsNoTracking()
                        .Select(x => new Types() { Id = x.Id, NameType = x.NameType })
                        .ToListAsync();
                return new { success = true, types };
            }
            catch (Exception ex)
            {
                return new { succes = false, ex };

            }
        }
        public async Task<Types> GetById(int id)
        {
          
                var types = await _dBSetting.Type
                        .AsNoTracking().SingleOrDefaultAsync(x=>x.Id==id);
            return types; 
           
         
        }
        public async Task<object> Insert(Types type)
            {
            try
            {
                var types = await _dBSetting.Type.AddAsync(type);
                _dBSetting.SaveChanges();
                return new { success = true, types };
            }
            catch (Exception ex)
            {
                return new { succes = false, ex };

            }
        }
        public async Task<object> Update(Types type,int id)
        {
            try
            {
                var types = await GetById(id);
               types.Id=type.Id;
                types.NameType= type.NameType;
                _dBSetting.SaveChanges();
                return new { success = true, types };
            }
            catch (Exception ex)
            {
                return new { succes = false, ex };

            }
        }
        public  async Task<object> Delete(int id)
        {
            try { 
                  var type = await GetById(id);
                  _dBSetting.Type.Remove(type);
                _dBSetting.SaveChanges();
                return new { success = true, type };
            }
            catch (Exception ex)
            {
                return new { succes = false, ex };

            }
        }
    }
}
