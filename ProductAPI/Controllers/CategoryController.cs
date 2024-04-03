using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using Repositories.Persistence;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryRepository _CategoryRepository;
        public CategoryController(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }
        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCtegories()
        {
              var categories = await _CategoryRepository.GetAllCategoriesAsync();
                return Ok(categories);
           
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            try
            {
                var category = await _CategoryRepository.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _CategoryRepository.AddCategoryAsync(category);
          

            return Ok(category);
        }



        // PUT api/<CategoryController>/5

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCategory(int id, Category cat)
        {
            try
            {
                if (id != cat.Id)
                {
                    return BadRequest("Category  ID mismatch");
                }

                var updatedcategory = await _CategoryRepository.UpdateCategoryAsync(id, cat);
                return Ok(updatedcategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/CategoryController/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var category = await _CategoryRepository.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }

                var deletedCategory = await _CategoryRepository.DeleteCategoryAsync(id);
                return Ok(deletedCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
