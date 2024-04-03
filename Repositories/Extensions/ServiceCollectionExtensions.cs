using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.DataAccess;
using Repositories.Persistence;

namespace Repositories.Extensions
{
    public static class ServiceCollectionExtensions

    {

        public static void AddRepositoryLayer(this IServiceCollection services, IConfiguration configuration)

        {

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddDbContext<ApplicationDbContext>(options =>

            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        }

    }
}
