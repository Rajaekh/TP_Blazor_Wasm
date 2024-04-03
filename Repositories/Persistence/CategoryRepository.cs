using Microsoft.EntityFrameworkCore;
using Repositories.DataAccess;
using Repositories.Models;

namespace Repositories.Persistence
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            try
            {
                return await _context.Categories.Include(p=>p.Products).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving categories.", ex);
            }
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            try
            {
                return await _context.Categories.Include(p=>p.Products).FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving category with ID {id}.", ex);
            }
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            try
            {
                return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving category with name {name}.", ex);
            }
        }
        public async Task<Category> AddCategoryAsync(Category cat)
        {
            
                _context.Categories.Add(cat);
                await _context.SaveChangesAsync();
                return cat;
           
        }
        public async Task<Category> UpdateCategoryAsync(int id, Category category)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                _context.Categories.Update(existingCategory);
                await _context.SaveChangesAsync();
                return existingCategory;
            }
            else
            {
                throw new InvalidOperationException($"Category with ID {id} not found.");
            }
        }

        public async Task<Category> DeleteCategoryAsync(int id)
        {
            var categoryToDelete = await _context.Categories.FindAsync(id);

            if (categoryToDelete != null)
            {
                _context.Categories.Remove(categoryToDelete);
                await _context.SaveChangesAsync();
                return categoryToDelete;
            }
            else
            {
                throw new InvalidOperationException($"Category with ID {id} not found.");
            }
        }


    }
}
