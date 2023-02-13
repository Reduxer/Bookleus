using Ardalis.Specification;
using Bookleus.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookleus.Application.Books.Queries.Specs
{
    public class BookBySearchStringSpec : Specification<Book>
    {
        public BookBySearchStringSpec(string? searchString)
        {
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                Query.Where((b) =>
                    b.SKU.ToString() == searchString
                        || EF.Functions.Like(b.Title, $"%{searchString}%")
                );
            }
        }
    }
}
