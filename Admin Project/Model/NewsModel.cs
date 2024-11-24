using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class NewsModel
    {
        public int NewsId { get; set; } = 0;
        public string NewsName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string NewsImage {  get; set; } = string.Empty;
        public DateTime PostingDate { get; set; } = DateTime.Now;
        public int PersonPostingId { get; set; } = 0;
        public bool Deleted { get; set; } = false;
    }
}
