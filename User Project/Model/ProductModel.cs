using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ProductModel
    {
        public int ProductId { get; set; } = 0;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string ProductImage { get; set; } = string.Empty;
        public int Star { get; set; } = 5;
        public int CategoryId { get; set; } = 0;
        public string ProductDetail { get; set; } = string.Empty;
        public bool Deleted { get; set; } = false;
    }
}
