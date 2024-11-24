using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CommentModel
    {
        public int CommentId { get; set; } = 0;
        public string Content { get; set; } = string.Empty;
        public DateTime Time { get; set; } = DateTime.Now;
        public int SenderId { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public bool Deleted { get; set; } = false;
    }
}
