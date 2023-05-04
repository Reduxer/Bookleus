using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bookleus.Application.Books.Queries.GetBooks;
using MediatR;
using Bookleus.Application.Common.Exceptions;
using Bookleus.Application.Common.Interfaces.Services;
using Bookleus.Application.Books.Commands.ReserveBook;
using Bookleus.Application.Books.Queries.Specs;

namespace Bookleus.Web.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class BooksController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUserService _userService;

        public BooksController(IMediator mediator, IUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmReserve(Guid sku)
        {
            var bookBySkuQuery = new GetBookBySKUQuery()
            {
                SKU = sku
            };

            try
            {
                var result = await _mediator.Send(bookBySkuQuery);

                if (TempData["unknow-error-thrown"] is not null)
                {
                    ModelState.AddModelError("", "An error occured while processing your request");
                }

                var book = result.Data.First();

                var test = "";

                return NotFound();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Reserve(Guid sku)
        {
            try
            {
                var userId = _userService.GetUserId(User);
                _ = await _mediator.Send(new ReserveBookCommand() { SKU = sku, UserId = userId });
            }
            catch(Exception)
            {
                TempData["unknow-error-thrown"] = true;
                return RedirectToAction(nameof(ConfirmReserve), new { sku });
            }

            TempData["book-reserve-success"] = "Book reservation successful.";
            return RedirectToAction("Index", "Home"); 
        }
    }
}
