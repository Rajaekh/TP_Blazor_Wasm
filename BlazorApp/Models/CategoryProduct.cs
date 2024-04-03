using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Models
{
    public class CategoryProduct
    {
        public int CategoryId { get; set; }
        public required Category Category { get; set; }
        public int ProductId { get; set; }
        public required Product Product { get; set; }
    }
}
