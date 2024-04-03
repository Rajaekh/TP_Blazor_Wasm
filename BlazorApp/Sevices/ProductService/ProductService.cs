

using BlazorApp.Models;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BlazorApp.Sevices.ProductService
{
    public class ProductService : IProductService
    {
        
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Category> categories { get; set; } = new List<Category>();
        public HttpClient _http { get; }
        public int CurPage { get; set; }

        public ProductService(HttpClient http)
        {
            _http = http;
        }
        public async Task<Product> GetProductById(int id)
        {
            // Utiliser une requête HTTP pour récupérer le produit par son identifiant
            try
            {
                return await _http.GetFromJsonAsync<Product>($"https://localhost:7181/api/Product/{id}");
            }
            catch (Exception ex)
            {
                // Gérer les erreurs ou les exceptions ici
                Console.WriteLine($"Une erreur s'est produite lors de la récupération du produit avec l'ID {id}: {ex.Message}");
                return null;
            }
        }

        public async Task GetProducts(string searchTerm = "", int CurPage = 1)
        {
            var result = await _http.GetFromJsonAsync<List<Product>>($"https://localhost:7181/api/Product?sTerm={searchTerm}&page={CurPage}");
            if (result != null)
                Products = result;
                CurPage = CurPage;
        }
        public async Task UpdateProduct(int id, CreateProductModel prod)
        {
            try
            {
                // Utiliser une requête HTTP PUT pour mettre à jour le produit avec les nouvelles données
                HttpResponseMessage response = await _http.PutAsJsonAsync($"https://localhost:7181/api/Product/{id}",prod);

                // Vérifier si la requête a réussi
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("success");
                    return;
                }
                else
                {
                    // Si la mise à jour a échoué, afficher un message d'erreur ou gérer l'erreur selon vos besoins
                    Console.WriteLine($"Une erreur s'est produite lors de la mise à jour du produit avec l'ID {id}. Statut de la réponse : {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Gérer les erreurs ou les exceptions ici
                Console.WriteLine($"Une erreur s'est produite lors de la mise à jour du produit avec l'ID {id}: {ex.Message}");
            }
        }

        public async Task AddProduct(CreateProductModel prod)
        {
            try
            {
                HttpResponseMessage response = await _http.PostAsJsonAsync("https://localhost:7181/api/Product",prod);       
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Le produit a été ajouté avec succès.");
                }
                else
                {
                    Console.WriteLine($"Une erreur s'est produite lors de l'ajout du produit. Statut de la réponse : {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite lors de l'ajout du produit : {ex.Message}");
            }
        }

        public async Task DeleteProduct(int id)
        {
            try
            {
                HttpResponseMessage response = await _http.DeleteAsync($"https://localhost:7181/api/Product/{id}");
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Le produit a été supprimé avec succès.");
                }
                else
                {
                    Console.WriteLine($"Une erreur s'est produite lors de la suppression  du produit. Statut de la réponse : {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite lors de la suppression  du produit : {ex.Message}");
            }
        }   
    }
}
