using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.Domain.Models
{
    public class User : DomainObject
    {
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime DateJoined { get; set; } = DateTime.Now;

    }
}
