using Bookleus.Application.Books.Queries.GetBooks;
using Bookleus.Application.Common.Interfaces.Services;
using Bookleus.Web.Test.Fixtures;
using MediatR;
using Bookleus.Web.Test.Utils;
using Bookleus.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;
using Bookleus.Application.Dtos.Book;
using Bookleus.Application.Common.Exceptions;
using Bookleus.Application.Books.Commands.ReserveBook;
using Moq;

namespace Bookleus.Web.Test.ControllerTests
{
    public class BooksControllerTests : IClassFixture<UserServiceFixture>
    {
        private readonly IUserService UserService;

        public BooksControllerTests(UserServiceFixture fixture)
        {
            UserService = fixture.UserService;
        }

        [Fact]
        public async Task ConfirmReserve_ReturnsNotFoundResult_WhenBookNotExists()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(mediator => mediator.Send(It.IsAny<GetBookBySKUQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException("Test", Guid.NewGuid()))
                .Verifiable();

            var mediator = mediatorMock.Object;

            var controller = new BooksController(mediator, UserService)
            {
                TempData = new TempDataDictionary(
                    new DefaultHttpContext(),
                    new Mock<ITempDataProvider>().Object)
            };

            var result = await controller.ConfirmReserve(Guid.NewGuid());

            mediatorMock.Verify();

            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.NotNull(notFoundResult);
        }

        [Fact]
        public async Task ConfirmReserve_ReturnsViewResult_WhenBookExists()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(mediator => mediator.Send(It.IsAny<GetBookBySKUQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new BooksVM()
                {
                    Total = 1,
                    Data = new()
                    {
                        BooksUtil.Books().First()
                    }
                })
                .Verifiable();

            var mediator = mediatorMock.Object;

            var controller = new BooksController(mediator, UserService)
            {
                TempData = new TempDataDictionary(
                    new DefaultHttpContext(),
                    new Mock<ITempDataProvider>().Object)
            };

            var result = await controller.ConfirmReserve(Guid.NewGuid());

            mediatorMock.Verify();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
            
            var viewModel = Assert.IsAssignableFrom<BookDto>(viewResult.ViewData.Model);
            Assert.NotNull(viewModel);
        }

        [Fact]
        public async Task Reserve_ReturnsRedirectToHomePage_WhenReservationSucceeded()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(mediator => mediator.Send(It.IsAny<ReserveBookCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value)
                .Verifiable();

            var mediator = mediatorMock.Object;

            var controller = new BooksController(mediator, UserService)
            {
                TempData = new TempDataDictionary(
                    new DefaultHttpContext(),
                    new Mock<ITempDataProvider>().Object)
            };

            var result = await controller.Reserve(Guid.NewGuid());

            mediatorMock.Verify();

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.NotNull(redirectResult);
            Assert.Equal("Home", redirectResult.ControllerName, ignoreCase: true);
            Assert.Equal("Index", redirectResult.ActionName, ignoreCase: true);
            Assert.True(controller.TempData.ContainsKey("book-reserve-success"));
        }

        [Fact]
        public async Task Reserve_ReturnsRedirectToConfirmReserve_WhenReservationFailed()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(mediator => mediator.Send(It.IsAny<ReserveBookCommand>(), It.IsAny<CancellationToken>()))
                .Throws(new Exception())
                .Verifiable();

            var mediator = mediatorMock.Object;

            var controller = new BooksController(mediator, UserService)
            {
                TempData = new TempDataDictionary(
                    new DefaultHttpContext(),
                    new Mock<ITempDataProvider>().Object)
            };

            var result = await controller.Reserve(Guid.NewGuid());

            mediatorMock.Verify();

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.NotNull(redirectResult);
            Assert.Null(redirectResult.ControllerName);
            Assert.Equal("ConfirmReserve", redirectResult.ActionName, ignoreCase: true);
            Assert.True(redirectResult!.RouteValues!.ContainsKey("sku"));
            Assert.True(controller.TempData.ContainsKey("unknow-error-thrown"));
        }
    }
}
