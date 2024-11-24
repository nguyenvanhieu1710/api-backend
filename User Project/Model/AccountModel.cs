using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AccountModel
    {
        public int AccountId { get; set; } = 0;
        public string AccountName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
        public DateTime DayCreated { get; set; } = DateTime.Now;
        public bool RememberPassword { get; set; } = false;
        public string Email { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public bool Deleted { get; set; } = false;
        public string Token { get; set; } = string.Empty;
    }
}
