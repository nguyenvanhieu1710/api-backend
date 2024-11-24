using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CartModel
    {
        public int ProductId {  get; set; } = 0;
        public int UserId { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public bool Selected { get; set; } = false;
        public decimal TotalAmount { get; set; } = 0;
    }
}
