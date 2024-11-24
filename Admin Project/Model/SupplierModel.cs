using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SupplierModel
    {
        public int SupplierId { get; set; } = 0;
        public string SupplierName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address {  get; set; } = string.Empty;
        public bool Deleted { get; set; } = false;
    }
}
