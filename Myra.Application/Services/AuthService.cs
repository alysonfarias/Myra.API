using Microsoft.AspNetCore.Http;
using Myra.Application.Interfaces;
using System.Security.Claims;

namespace Myra.Application.Services
{
    public class AuthService : IAuthService
    {

        public AuthService(IHttpContextAccessor httpContextAcessor = null)
        {
            if (httpContextAcessor != null)
                GetUserId(httpContextAcessor.HttpContext.User);
        }

        public int GetUserId(ClaimsPrincipal claims)
        {
            return int.Parse(claims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid).Value);
        }
    }
}