using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class OrderDetailModel
    {
        public int OrderDetailId { get; set; } = 0;
        public int OrderId { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public int DiscountAmount { get; set; } = 0;
        public int VoucherId { get; set; } = 0;
        public int OrderDetailStatus { get; set; } = 0;
    }
}
