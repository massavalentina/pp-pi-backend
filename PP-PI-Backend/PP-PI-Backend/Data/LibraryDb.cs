using Microsoft.EntityFrameworkCore;
using PP_PI_Backend.Models;

namespace PP_PI_Backend.Data
{
    public class LibraryDb : DbContext // creation of the Library database
    {
        // ctor for the representation of db in Entity Framework
        public LibraryDb(DbContextOptions<LibraryDb> options) : base(options) 
        { }
        public DbSet<Book> Books => Set<Book>(); // setting the Book entity as a property of the db Library
    }
}
