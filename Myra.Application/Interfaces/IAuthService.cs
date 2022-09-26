using System.Security.Claims;

namespace Myra.Application.Interfaces
{
    public interface IAuthService
    {
        int GetUserId(ClaimsPrincipal claims);
    }
}

