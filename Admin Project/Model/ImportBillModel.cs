using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ImportBillModel
    {
        public int ImportBillId { get; set; } = 0;
        public int SupplierId { get; set; } = 0;
        public int StaffId { get; set; } = 0;
        public DateTime InputDay { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;
        public List<ImportBillDetailModel> listjson_importBillDetail { get; set; }
    }
}
