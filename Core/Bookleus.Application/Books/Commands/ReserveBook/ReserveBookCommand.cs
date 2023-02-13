using Bookleus.Application.Books.Queries.GetBooks;
using Bookleus.Application.Common.Interfaces.Contexts;
using Bookleus.Application.Common.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookleus.Application.Books.Commands.ReserveBook
{
    public class ReserveBookCommand : IRequest<Unit>
    {
        public Guid SKU { get; set; }

        public string UserId { get; set; } = default!;
    }

    public class ReserveBookCommandHandler : IRequestHandler<ReserveBookCommand, Unit>
    {
        private readonly IUserService _userService;
        private readonly IMediator _mediator;
        private readonly IDatabaseContext _dbContext;

        public ReserveBookCommandHandler(IUserService userService, IMediator mediator, IDatabaseContext dbContext)
        {
            _userService = userService;
            _mediator = mediator;
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(ReserveBookCommand request, CancellationToken cancellationToken)
        {
            if (!_userService.ValidateUserExists(request.UserId)) throw new InvalidOperationException();

            var bookVm = await _mediator.Send(new GetBookBySKUQuery() { SKU = request.SKU }, cancellationToken);
            var book = bookVm.Data.First();

            if (!book.IsAvailable) throw new InvalidOperationException();

            _dbContext.CustomerBookReservations.Add(new()
            {
                BookSKU = book.SKU,
                CustomerId = request.UserId,
                IsActive = true
            });

            var totalInserted = await _dbContext.SaveChangesAsync(cancellationToken);

            if (totalInserted <= 0) throw new ApplicationException();

            return Unit.Value;
        }
    }
}
