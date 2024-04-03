
using BlazorApp.Models;

namespace BlazorApp.Sevices.ProductService
{
    public interface IProductService
    {
        List<Product> Products { get; set; }
        List<Category> categories { get; set; }
        int CurPage { get; set; }
        Task GetProducts(string searchTerm = "",int CurPage=1);
        Task<Product> GetProductById(int id);
        Task UpdateProduct(int id, CreateProductModel prod);
        Task AddProduct(CreateProductModel prod);
        Task DeleteProduct(int id);
    }
}
