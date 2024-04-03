using Microsoft.EntityFrameworkCore;
using Repositories.DataAccess;
using Repositories.Dtos;
using Repositories.Models;

namespace Repositories.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _context.Products.Include(p=>p.Categories).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving products.", ex);
            }
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while adding the product.", ex);
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                return await _context.Products.Include(p=>p.Categories).FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving product with ID {id}.", ex);
            }
        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            try
            {
                return await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving product with name {name}.", ex);
            }
        }

        public async Task<Product> UpdateProductAsync(int id, CreateProductModel prod)
        {
            var existingProduct = await _context.Products
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existingProduct != null)
            {
                existingProduct.Name = prod.Name;
                existingProduct.Description = prod.Description;
                existingProduct.Price = prod.Price;
                existingProduct.Qte = prod.Qte;

                // Effacer les catégories existantes
                existingProduct.Categories.Clear();

                // Ajouter les nouvelles catégories
                if (prod.CategoriesId != null)
                {
                    foreach (var categoryId in prod.CategoriesId)
                    {
                        var category = await _context.Categories.FindAsync(categoryId);
                        if (category != null)
                        {
                            existingProduct.Categories.Add(category);
                        }
                    }
                }
                _context.Products.Update(existingProduct);
                await _context.SaveChangesAsync();
                return existingProduct;
            }
            else
            {
                throw new InvalidOperationException($"Product with ID {id} not found.");
            }
        }
        public async Task<Product> DeleteProductAsync(int id)
        {
            var productToDelete = await _context.Products.FindAsync(id);

            if (productToDelete != null)
            {
                _context.Products.Remove(productToDelete);
                await _context.SaveChangesAsync();

                return productToDelete;
            }
            else
            {

                throw new InvalidOperationException($"Product with ID {id} not found.");
            }
        }

    }
}
