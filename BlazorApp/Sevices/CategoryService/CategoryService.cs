using BlazorApp.Models;
using System.Net.Http.Json;

namespace BlazorApp.Sevices.CategoryService
{
    public class CategoryService : ICategoryService
    {
        public HttpClient _http { get; }
        public List<Category> categories { get; set; } = new List<Category>();
        public CategoryService(HttpClient http)
        {
            
         _http = http;

        }
        public async Task GetCategories()
        {
            var result = await _http.GetFromJsonAsync<List<Category>>("https://localhost:7181/api/Category");
            if (result != null)
                categories = result;
        }
    }
}
