using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Myra.Application.Constants;
using Myra.Domain.Models.Enumerations;
using System.Reflection.Metadata;
using System.Xml;
using Myra.Application.ViewModels.User;
using Myra.Application;

namespace Myra.API.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    [Route("api/users")]
    public class UserAdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserAdminController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserResponse>> Get(int id)
        {
            return await _userService.GetUserById(id);
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> Post([FromBody] UserRequest userView)
        {
            return await _userService.AddUser(userView);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserResponse>> Put([FromBody] UserRequest userRequest, [FromRoute] int id)
        {
            return await _userService.UpdateUser(userRequest, id);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UserResponse>> Delete(int id)
        {
            return await _userService.DeleteUser(id);
        }

        [HttpGet("user-roles")]
        public IEnumerable<UserRole> GetUserRoles()
        {
            return _userService.GetAllUserRoles();
        }

        

    }
}
