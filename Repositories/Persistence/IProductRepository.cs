using Repositories.Dtos;
using Repositories.Models;

namespace Repositories.Persistence
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> GetProductByNameAsync(string name);
        Task<Product> UpdateProductAsync(int id, CreateProductModel prod);
        Task<Product> DeleteProductAsync(int id);
        Task<Product> AddProductAsync(Product product);
     

    }
}
