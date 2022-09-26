using Myra.Application.ViewModels.UserRole;
using Myra.Domain.Core;

namespace Myra.Application.ViewModels.User
{
    public class UserResponse : Register
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string VerificationToken { get; set; }
        public UserRoleResponse UserRole { get; set; }
    }
}
