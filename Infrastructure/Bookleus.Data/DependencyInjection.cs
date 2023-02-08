using Microsoft.Extensions.Configuration;
using Bookleus.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Bookleus.Application.Common.Interfaces.Contexts;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IDatabaseContext>(provider =>
            {
                return provider.GetRequiredService<DatabaseContext>();
            });

            return services;
        }
    }
}
