using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UsersModel
    {
        public int UserId { get; set; } = 0;
        public string UserName { get; set; } = string.Empty;
        public DateTime Birthday { get; set; } = DateTime.Now;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Ranking { get; set; } = string.Empty;
        public bool Deleted { get; set; } = false;
    }
}
