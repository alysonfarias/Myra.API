using Myra.Application.Exception;
using Myra.Application.Interfaces;
using Myra.Application.ViewModels.Login;
using Myra.Domain.Interfaces.Repositories;
using Myra.Infra.Utils;
using System.Security.Claims;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Myra.Application.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _userRepository;

        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Claim>> Login(LoginRequest login)
        {
            var result = await _userRepository.GetById(filter: u => u.UserName == login.UserName, include: i => i.Include(r => r.UserRole));

            if (!PasswordHasher.Verify(login.Password, result.Password))
                throw new BadRequestException(nameof(login.Password), "Password or username are incorret. Please try again.");

            return new List<Claim>
            {
                new Claim(ClaimTypes.Sid, result.Id.ToString()),
                new Claim(ClaimTypes.Email, result.Email),
                new Claim(ClaimTypes.Name, result.Name),
                new Claim(ClaimTypes.Role, result.UserRole.Name),
            };
        }
    }
}
