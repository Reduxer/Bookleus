using Microsoft.EntityFrameworkCore;
using Bookleus.Application.Common.Interfaces.Contexts;
using Bookleus.Domain.Entities;

namespace Bookleus.Data.Contexts
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Book> Books => Set<Book>();

        public DbSet<CustomerBookReservation> CustomerBookReservations => Set<CustomerBookReservation>();

        public DatabaseContext() {}

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        
    }
}
