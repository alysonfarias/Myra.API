using AutoMapper.Internal;
using AutoMapper;
using FluentValidation;
using Myra.Application.Exception;
using Myra.Application.Params;
using Myra.Application.ViewModels.User;
using Myra.Domain.Core;
using Myra.Domain.Interfaces.Repositories;
using Myra.Domain.Models.Enumerations;
using Myra.Domain.Models;
using Myra.Infra.UnitOfWork;
using Myra.Infra.Utils;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace Myra.Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IValidator<UserRequest> _validator;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public UserService(IUserRepository userRepository,
                           IValidator<UserRequest> validator,
                           IUnitOfWork unitOfWork,
                           IMapper mapper)
        {
            _userRepository = userRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserResponse> AddUser(UserRequest userRequest)
        {
            var validationResult = await _validator.ValidateAsync(userRequest);

            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult);

            userRequest.Password = PasswordHasher.Hash(userRequest.Password);
            var result = await _userRepository.Add(_mapper.Map<User>(userRequest));

            await _unitOfWork.Commit();
            return _mapper.Map<UserResponse>(result);

        }

        public async Task<UserResponse> UpdateUser(UserRequest userRequest, int id)
        {

            var userResult = await _userRepository.GetById(filter: u => u.Id == id);

            if (userResult == null)
                throw new BadRequestException(nameof(userResult.Name), "User not found");

            userRequest.Password = string.IsNullOrEmpty(userRequest.Password) ?
                userResult.Password : PasswordHasher.Hash(userRequest.Password);
            var validationResult = await _validator.ValidateAsync(userRequest);

            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult);

            _mapper.Map(userRequest, userResult);
            var result = await _userRepository.Update(userResult);
            await _unitOfWork.Commit();

            return _mapper.Map<UserResponse>(result);
        }

        public async Task<UserResponse> DeleteUser(int id)
        {
            var userResult = await _userRepository.GetById(filter: u => u.Id == id);

            if (userResult == null)
                throw new BadRequestException(nameof(userResult.Name), "User not found");

            await _userRepository.Remove(id);
            await _unitOfWork.Commit();
            return _mapper.Map<UserResponse>(userResult);
        }

        public async Task<UserResponse> GetUserById(int id)
        {
            return _mapper.Map<UserResponse>(await _userRepository
                .GetById(filter: u => u.Id == id, include: i => i.Include(r => r.UserRole)));
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsers(UserParams query)
        {
            return _mapper.Map<IEnumerable<UserResponse>>
                (await _userRepository.GetAll(predicate: query.Filter(),
                include: i => i.Include(r => r.UserRole), take: query.Take, skip: query.Skip));
        }

        public IEnumerable<UserRole> GetAllUserRoles()
        {
            return Enumeration.GetAll<UserRole>();
        }

        public async Task<int> CounUser()
        {
            return await _userRepository.CountAll();
        }

        
    }
}
