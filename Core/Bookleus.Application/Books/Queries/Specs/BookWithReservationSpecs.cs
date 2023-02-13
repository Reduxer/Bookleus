using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Bookleus.Domain.Entities;

namespace Bookleus.Application.Books.Queries.Specs
{
    public class BookWithReservationSpecs : Specification<Book>
    {
        public BookWithReservationSpecs()
        {
            Query.Include(b => b.BookReservations);
        }
    }
}
