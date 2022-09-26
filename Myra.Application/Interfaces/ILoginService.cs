using Myra.Application.ViewModels.Login;
using System.Security.Claims;

namespace Myra.Application.Interfaces
{
    public interface ILoginService
    {
        Task<IEnumerable<Claim>> Login(LoginRequest loginUserView);
    }
}
