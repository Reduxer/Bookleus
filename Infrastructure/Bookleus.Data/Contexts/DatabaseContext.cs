using Microsoft.EntityFrameworkCore;
using Bookleus.Application.Common.Interfaces.Contexts;
using Bookleus.Domain.Entities;

namespace Bookleus.Data.Contexts
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Book> Books { get; set; }

        public DatabaseContext() {}

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        
    }
}
