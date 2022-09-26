using Myra.Application.Params;
using Myra.Application.ViewModels.User;
using Myra.Domain.Models.Enumerations;

namespace Myra.Application
{
    public interface IUserService
    {
        Task<UserResponse> AddUser(UserRequest userViewModel);
        Task<UserResponse> UpdateUser(UserRequest userResponse, int id);
        Task<UserResponse> DeleteUser(int id);
        Task<IEnumerable<UserResponse>> GetAllUsers(UserParams query);
        Task<int> CounUser();
        Task<UserResponse> GetUserById(int id);
        IEnumerable<UserRole> GetAllUserRoles();
        
    }
}
