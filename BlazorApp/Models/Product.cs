using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public  string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Qte { get; set; }
        [Required]
        public List<Category> Categories{ get; set; } = new List<Category>();
    }
}
