using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Bookleus.Application.Books.Queries.GetBooks;
using Bookleus.Web.Test.Utils;
using Bookleus.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Bookleus.Web.Test.ControllerTests
{
    public class HomeControllerTests
    {
        [Fact]
        public async Task Index_ReturnsViewResult_WithBooks()
        {
            var mockedMediator = new Mock<IMediator>();

            mockedMediator.Setup(m => m.Send(It.IsAny<GetBooksQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BooksVM()
                {
                    Data = BooksUtil.Books().ToList(),
                    Total = BooksUtil.Books().Count(),
                })
                .Verifiable();

            var mediator = mockedMediator.Object;
            var controller = new HomeController(mediator);
            
            var result = await controller.Index(null);

            mockedMediator.Verify();

            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<BooksVM>(viewResult.ViewData.Model);
            Assert.Equal(2, viewModel.Data.Count);
        }

        [Fact]
        public void Privacy_ReturnsViewResult()
        {
            var mockedMediator = new Mock<IMediator>();
            var mediator = mockedMediator.Object;
            var controller = new HomeController(mediator);

            var result = controller.Privacy();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
        }
    }
}
