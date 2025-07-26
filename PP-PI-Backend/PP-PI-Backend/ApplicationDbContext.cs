using Microsoft.EntityFrameworkCore;
using PP_PI_Backend.Entities;

namespace PP_PI_Backend
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) :  base(options) 
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Publisher> Publishers { get; set; }
    }
}
