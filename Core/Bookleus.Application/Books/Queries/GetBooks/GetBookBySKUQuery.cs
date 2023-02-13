using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using Bookleus.Application.Books.Queries.Specs;
using Bookleus.Application.Common.Interfaces.Contexts;
using Bookleus.Application.Dtos.Book;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookleus.Application.Common.Exceptions;
using Bookleus.Domain.Entities;

namespace Bookleus.Application.Books.Queries.GetBooks
{
    public class GetBookBySKUQuery : IRequest<BooksVM>
    {
        public Guid SKU { get; set; }
    }

    public class GetBookBySKUQueryHandler : IRequestHandler<GetBookBySKUQuery, BooksVM>
    {
        private readonly IDatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public GetBookBySKUQueryHandler(IDatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<BooksVM> Handle(GetBookBySKUQuery request, CancellationToken cancellationToken)
        {
            var bookBySkuSpec = new BookBySKUSpec(request.SKU);
            var bookWithReservationSpec = new BookWithReservationSpecs();

            var book = await _dbContext.Books
                .WithSpecification(bookBySkuSpec)
                .WithSpecification(bookWithReservationSpec)
                .FirstOrDefaultAsync(cancellationToken);

            if(book is null)
            {
                throw new NotFoundException(nameof(Book), request.SKU);
            }

            return new BooksVM()
            {
                Data = new List<BookDto> { _mapper.Map<BookDto>(book) },
                Total = 1
            };
        }
    }
}
