using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Myra.Application;
using Myra.Application.ViewModels.User;
using System.Security.Claims;

namespace Myra.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] UserRequest userRequest)
        {
            userRequest.IdUserRole = 2;
            await _userService.AddUser(userRequest);
            return Ok();
        }

        [HttpGet]
        public async Task<UserResponse> Get()
        {
            int id = GetUserId();
            return await _userService.GetUserById(id);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserRequest userRequest)
        {
            int id = GetUserId();
            userRequest.IdUserRole = 2;
            await _userService.UpdateUser(userRequest, id);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            int id = GetUserId();
            await _userService.DeleteUser(id);
            return Ok();
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.Sid)!.Value);
        }
    }
}
