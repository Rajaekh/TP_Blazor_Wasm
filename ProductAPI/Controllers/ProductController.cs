using Microsoft.AspNetCore.Mvc;
using Repositories.Dtos;
using Repositories.Models;
using Repositories.Persistence;


namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _CategoryRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository CategoryRepository)
        {

            _productRepository = productRepository;
            _CategoryRepository = CategoryRepository;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(int page=1, string sTerm = "")
        {
            sTerm = sTerm.ToLower(); // Convertir la chaîne de recherche en minuscules

            var products = await _productRepository.GetAllProductsAsync();

            // Filtrer les produits en fonction du terme de recherche
            if (!string.IsNullOrEmpty(sTerm))
            {
                products = products.Where(p => p.Name.ToLower().StartsWith(sTerm)).ToList();
            }
            // Pagination
            int skip = (page - 1) * 10;
            products = products.Skip(skip).Take(10).ToList();
            return Ok(products);
        }



        // GET api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody] CreateProductModel productModel)
        {

            var product = new Product
            {
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,
                Qte = productModel.Qte
            };
            foreach (var catid in productModel.CategoriesId)
            {
                product.Categories.Add(await _CategoryRepository.GetCategoryByIdAsync(catid));
            }
            try
            {
                await _productRepository.AddProductAsync(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, CreateProductModel product)
        {
            try
            {
                if (id != product.Id)
                {
                    return BadRequest("Product ID mismatch");
                }

                var updatedProduct = await _productRepository.UpdateProductAsync(id, product);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                var deletedProduct = await _productRepository.DeleteProductAsync(id);
                return Ok(deletedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
