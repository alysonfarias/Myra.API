using Myra.Domain.Core;

namespace Myra.Domain.Models.Enumerations
{
    public class UserRole : Enumeration
    {
        public static UserRole Admin = new(1, nameof(Admin));
        public static UserRole Common = new(2, nameof(Common));

        public UserRole(int id, string name) : base(id, name)
        {
        }
    }
}
