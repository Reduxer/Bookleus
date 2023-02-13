using MediatR;
using AutoMapper;
using Bookleus.Application.Dtos.Book;
using Bookleus.Application.Common.Interfaces.Contexts;
using Bookleus.Application.Books.Queries.Specs;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Bookleus.Application.Books.Queries.GetBooks
{
    public class GetBooksQuery : IRequest<BooksVM>
    {
        public string? SearchString { get; set; }
        public bool ExcludedReserved { get; set; }
    }

    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, BooksVM>
    {
        private readonly IDatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public GetBooksQueryHandler(IDatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<BooksVM> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var bookBySearchStringSpec = new BookBySearchStringSpec(request.SearchString);
            var bookWithReservationSpec = new BookWithReservationSpecs();

            var booksQuery = _dbContext.Books
                .WithSpecification(bookBySearchStringSpec)
                .WithSpecification(bookWithReservationSpec);

            if(request.ExcludedReserved)
            {
                var getAvailableBooksSpec = new GetAvailableBooksSpec();
                booksQuery = booksQuery.WithSpecification(getAvailableBooksSpec);
            }
            
            var books = await booksQuery.ProjectTo<BookDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new BooksVM()
            {
                Data = books,
                Total = books.Count
            };
        }
    }
}
