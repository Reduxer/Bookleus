using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookleus.Application.Dtos.Book;

namespace Bookleus.Application.Books.Queries.GetBooks
{
    public class BooksVM
    {
        public int Total { get; set; }

        public List<BookDto> Data { get; set; } = default!;
    }
}
