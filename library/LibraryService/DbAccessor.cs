using LibraryService.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryService
{
    public class DbAccessor
    {
        public LibraryContext DataContext { get; set; }
        public string DbPath { get; private set; } = "Data Source=DESKTOP-QHQ0GDA\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=Library;Trust Server Certificate=true";

        public DbAccessor()
        {
            var dbBuilder = new DbContextOptionsBuilder<LibraryContext>();
            dbBuilder.UseSqlServer(DbPath);
            DataContext = new LibraryContext(dbBuilder.Options);
        }
    }
}
