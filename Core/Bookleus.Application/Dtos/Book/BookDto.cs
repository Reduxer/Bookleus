using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookleus.Application.Dtos.Book
{
    public class BookDto
    {
        public Guid SKU { get; set; }

        public string Title { get; set; } = default!;
    }
}
