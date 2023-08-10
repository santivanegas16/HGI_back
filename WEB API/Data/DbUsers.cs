using System.Data.Entity;
using WEB_API.Models;

namespace WEB_API.Data
{
    public class UserDB : DbContext
    {
        public UserDB() { }
        public DbSet<User> Users { get; set; }
    }
}
