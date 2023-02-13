using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Bookleus.Domain.Entities;

namespace Bookleus.Application.Books.Queries.Specs
{
    public class GetAvailableBooksSpec : Specification<Book>
    {
        public GetAvailableBooksSpec()
        {
            Query.Include(b => b.BookReservations)
                .Where(b => !b.BookReservations.Any(br => br.IsActive == true));
        }
    }
}
