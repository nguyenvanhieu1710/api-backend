using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CategoryModel
    {
        public int CategoryId { get; set; } = 0;
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryImage {  get; set; } = string.Empty;
        public int DadCategoryId { get; set; } = 0;
        public bool Deleted { get; set; } = false;
    }
}
