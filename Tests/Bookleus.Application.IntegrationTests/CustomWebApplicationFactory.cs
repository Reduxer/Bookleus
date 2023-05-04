using Bookleus.Application.Common.Interfaces.Contexts;
using Bookleus.Data.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace Bookleus.Application.IntegrationTests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                var dbContextOptionDescriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<DatabaseContext>));

                services.Remove(dbContextOptionDescriptor!);

                var dbContextDescriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DatabaseContext));

                services.Remove(dbContextDescriptor!);

                services.AddSingleton<DbConnection>((container) =>
                {
                    var connection = new SqliteConnection("DataSource=:memory:");
                    connection.Open();
                    return connection;
                });

                services.AddDbContext<DatabaseContext>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseSqlite(connection);
                });
            });

            builder.UseEnvironment("Development");
        }
    }
}
