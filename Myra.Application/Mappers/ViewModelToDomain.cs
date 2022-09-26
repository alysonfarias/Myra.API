using AutoMapper;
using Myra.Application.ViewModels.User;
using Myra.Domain.Models;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Myra.Application.Mappers
{
    public class ViewModelToDomain : Profile
    {
        public ViewModelToDomain()
        {
            CreateMap<UserRequest, User>();
        }
    }
}
