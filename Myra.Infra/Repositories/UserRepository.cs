using Myra.Domain.Interfaces.Repositories;
using Myra.Domain.Models;
using Myra.Infra.Context;
using Myra.Infra.Repositories.Base;

namespace Myra.Infra.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
