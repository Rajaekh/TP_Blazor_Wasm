using System.ComponentModel.DataAnnotations;

namespace Repositories.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
