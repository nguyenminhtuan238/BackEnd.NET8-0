using Microsoft.EntityFrameworkCore;
using ProjectCV.Server.Models;

namespace ProjectCV.Server.DB
{
    public class DBSetting: DbContext
    {
        public DbSet<User> User { get; set; }
       public DbSet<Types> Type { get; set; }
        public DbSet<Note> Note { get; set; }
        public DBSetting(DbContextOptions options) : base(options) { }

    }
}
