using Microsoft.EntityFrameworkCore;
using Services.Data.Models;

namespace Services.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options): base(options)
        {

        }

        public string DbPath { get; private set; } = "Data Source=sql03\\dev;Integrated Security=SSPI;Initial Catalog=AdamLibrary;";

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookHistory> dfsafdsa { get; set; }


    }
}
