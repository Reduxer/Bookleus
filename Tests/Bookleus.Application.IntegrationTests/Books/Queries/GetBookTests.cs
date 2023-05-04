using Bookleus.Application.Books.Queries.GetBooks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookleus.Application.IntegrationTests.Books.Queries
{
    public class GetBookTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public GetBookTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetBooksQuery_ShouldReturnBooks()
        {
            using var serviceScope = _factory.Services.CreateScope();
            var mediator = serviceScope.ServiceProvider.GetRequiredService<IMediator>();

            var result = await mediator.Send(new GetBooksQuery());

            Assert.NotNull(result);
            Assert.NotEmpty(result.Data);
            Assert.Equal(result.Total, result.Data.Count);   
        }
    }
}
