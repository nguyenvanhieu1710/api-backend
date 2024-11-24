using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class VoucherModel
    {
        public int VoucherId { get; set; } = 0;
        public string VoucherName { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public decimal MinimumPrice { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public DateTime StartDay { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(30);
        public int Deleted { get; set; } = 0;
    }
}
