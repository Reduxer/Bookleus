using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Bookleus.Application.Common.Interfaces.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            IDatabaseContext context = serviceScope.ServiceProvider.GetRequiredService<IDatabaseContext>();

            SeedBookData(context);

            return app;
        }

        private static void SeedBookData(IDatabaseContext context)
        {

            if (context.Books.Any()) return;

            context.Books.Add(new()
            {
                SKU = Guid.Parse("9b0896fa-3880-4c2e-bfd6-925c87f22878"),
                Title = "CQRS for Dummies"
            });

            context.Books.Add(new()
            {
                SKU = Guid.Parse("0550818d-36ad-4a8d-9c3a-a715bf15de76"),
                Title = "Visual Studio Tips"
            });

            context.Books.Add(new()
            {
                SKU = Guid.Parse("8e0f11f1-be5c-4dbc-8012-c19ce8cbe8e1"),
                Title = "NHibernate Cookbook"
            });

            context.SaveChanges();
        }
    }
}
