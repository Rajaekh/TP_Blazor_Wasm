using Repositories.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Dtos
{
  public class CreateProductModel
    {
     
        public int Id { get; set; }
       
        public string? Name { get; set; }
      
        public string Description { get; set; }
    
        public int Price { get; set; }

        public int Qte { get; set; }
        public List<int> CategoriesId { get; set; } = new List<int>();
    }
}
