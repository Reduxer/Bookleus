using Bookleus.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MediatR;
using Bookleus.Application.Books.Queries;
using Bookleus.Application.Books.Queries.GetBooks;

namespace Bookleus.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(string? searchString)
        {
            var booksQuery = new GetBooksQuery()
            {
                SearchString = searchString
            };

            var result = await _mediator.Send(booksQuery);
            ViewBag.SearchString = searchString;

            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}