using Microsoft.EntityFrameworkCore;
using PP_PI_Backend.Entities;
using PP_PI_Backend.Models;

namespace PP_PI_Backend.Data
{
    public class LibraryDb : DbContext // creation of the Library database
    {
        // ctor for the representation of db in Entity Framework
        public LibraryDb(DbContextOptions<LibraryDb> options) : base(options) 
        { }
        public DbSet<Book> Books => Set<Book>(); // setting the Book entity as a property of the db Library
        public DbSet<Author> Authors => Set<Author>();
        // setting the Author entity as a property of the db Library (a table in the DB)
        public DbSet<Review> Reviews => Set<Review>();
        // Setting the Review entity as a property of the db Library (a table in the DB )

        public DbSet<Publisher> Publishers => Set<Publisher>();
    }
}
