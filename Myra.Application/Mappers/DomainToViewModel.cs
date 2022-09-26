using AutoMapper;
using Microsoft.VisualBasic;
using Myra.Application.ViewModels.User;
using Myra.Application.ViewModels.UserRole;
using Myra.Domain.Models;
using Myra.Domain.Models.Enumerations;

namespace Myra.Application.Mappers
{
    public class DomainToViewModel : Profile
    {
        public DomainToViewModel()
        {
            CreateMap<User, UserResponse>();
            CreateMap<UserRole, UserRoleResponse>();
        }
    }
}
