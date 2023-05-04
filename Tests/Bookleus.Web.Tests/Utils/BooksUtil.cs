using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookleus.Application.Dtos.Book;

namespace Bookleus.Web.Test.Utils
{
    public static class BooksUtil
    {
        public static IEnumerable<BookDto> Books()
        {
            yield return new BookDto()
            {
                SKU = Guid.NewGuid(),
                Title = "Test Book",
                IsAvailable = true
            };

            yield return new BookDto()
            {
                SKU = Guid.NewGuid(),
                Title = "Test Book 2",
                IsAvailable = true
            };
        }
    }
}
