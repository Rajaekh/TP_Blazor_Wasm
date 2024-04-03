using Microsoft.Extensions.DependencyInjection;
using Services.Persistence;

namespace Repositories.Extensions
{
    public static class ServiceCollectionExtension

    {
        public static void AddServiceLayer(this IServiceCollection services)

        {

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<ICategoryService, CategoryService>();

            //services.AddAutoMapper(Assembly.GetExecutingAssembly());

        }


    }
}
