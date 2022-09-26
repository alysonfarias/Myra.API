using Myra.Domain.Core;
using Myra.Domain.Models.Enumerations;

namespace Myra.Domain.Models
{
    public class User : Register
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
        public int IdUserRole { get; set; }
    }
}
