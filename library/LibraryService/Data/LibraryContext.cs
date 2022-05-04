using Microsoft.EntityFrameworkCore;
using LibraryService.Data.Models;

namespace LibraryService.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options): base(options)
        {

        }        

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Dvd> Dvd { get; set; }
        public DbSet<BookHistory> BookHistory { get; set; }
    }
}
