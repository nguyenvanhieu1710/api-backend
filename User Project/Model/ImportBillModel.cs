﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class ImportBillModel
    {
        public int ImportBillId { get; set; } = 0;
        public int SupplierId { get; set; } = 0;
        public int StaffId { get; set; } = 0;
        public DateTime InputDay { get; set; } = DateTime.Now;
        public int Deleted { get; set; } = 0;
    }
}
