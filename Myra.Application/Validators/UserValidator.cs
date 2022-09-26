using System.Data.Entity;
using Myra.Application.ViewModels.User;
using Myra.Domain.Core;
using Myra.Domain.Interfaces.Repositories;
using Myra.Domain.Models.Enumerations;
using FluentValidation;

namespace Myra.Application.Validators
{
    public class UserValidator : AbstractValidator<UserRequest>
    {
        public UserValidator(IUserRepository userRepository)
        {

            RuleFor(u => u.UserName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(u => u.UserName)
                .Must(userName => userRepository.FirstQuery().AsNoTracking().All(a => a.UserName != userName))
                .WithMessage("User name already exists.");

            RuleFor(u => u.Email)
              .EmailAddress()
              .NotEmpty();

            RuleFor(u => u.Email)
                .Must(email => userRepository.FirstQuery().AsNoTracking().All(x => x.Email != email))
                .WithMessage("Email already exists.");

            RuleFor(u => u.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(u => u.IdUserRole)
                .Must(userRole => Enumeration.GetAll<UserRole>().Any(userType => userType.Id == userRole))
                .WithMessage("User Role not exists.");
        }
    }
}

