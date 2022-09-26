using Microsoft.AspNetCore.Mvc;
using Myra.Application.Interfaces;
using Myra.Application.ViewModels.Login;
using Myra.Application;

namespace Myra.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private ITokenService _tokenService;
        private ILoginService _loginService;

        public AuthController(
            ITokenService tokenService,
            ILoginService loginService)
        {
            _tokenService = tokenService;
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<ActionResult> LoginRequest([FromBody] LoginRequest model)
        {
            var result = await _loginService.Login(model);
            var token = _tokenService.GenerateToken(result);

            return Ok(new
            {
                Token = token
            });
        }
    }
}
