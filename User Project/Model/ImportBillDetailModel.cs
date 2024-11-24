using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class ImportBillDetailModel
    {
        public int ImportBillDetailId { get; set; } = 0;
        public int ImportBillId { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public decimal ImportPrice { get; set; } = 0;
        public int ImportQuantity { get; set; } = 0;
    }
}
