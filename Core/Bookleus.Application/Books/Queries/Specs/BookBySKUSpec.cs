using Ardalis.Specification;
using Bookleus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookleus.Application.Books.Queries.Specs
{
    public class BookBySKUSpec : Specification<Book>, ISingleResultSpecification<Book>
    {
        public BookBySKUSpec(Guid sku)
        {
            Query.Where(book => book.SKU == sku);
        }
    }
}
