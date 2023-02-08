using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bookleus.Domain.Entities;
using System.Threading;

namespace Bookleus.Application.Common.Interfaces.Contexts
{
    public interface IDatabaseContext
    {
        DbSet<Book> Books { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
