using BlogAppMVC.DTOs;
using BlogAppMVC.Models;

namespace BlogAppMVC.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAll();
        Task<int> CreateUser(UserDTO UserDto);

        Task<bool> IsUser(LoginViewModel model);

        Task <UserDTO> GetUser(LoginViewModel model);
        Task<UserDTO> GetUserProfile(string username);

    }
}
