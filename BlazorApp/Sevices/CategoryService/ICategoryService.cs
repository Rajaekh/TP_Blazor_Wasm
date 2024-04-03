using BlazorApp.Models;

namespace BlazorApp.Sevices.CategoryService
{
    public interface ICategoryService
    {
        List<Category> categories { get; set; }
        Task GetCategories();
    }
}
