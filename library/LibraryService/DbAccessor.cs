using LibraryService.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryService
{
    public class DbAccessor
    {
        public LibraryContext DataContext { get; set; }
        public string DbPath { get; private set; } = "Data Source=sql03\\dev;Integrated Security=SSPI;Initial Catalog=AdamLibrary;";

        public DbAccessor()
        {
            var dbBuilder = new DbContextOptionsBuilder<LibraryContext>();
            dbBuilder.UseSqlServer(DbPath);
            DataContext = new LibraryContext(dbBuilder.Options);
        }
    }
}
